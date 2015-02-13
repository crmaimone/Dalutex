using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Dalutex.Models;
using System.Configuration;
using Dalutex.Models.DataModels;
using System.Data.Entity;
using System.Web.Helpers;
using System.IO;

namespace Dalutex.Controllers
{
    public class PedidoController : BaseController
    {

        public ActionResult MenuColecoes()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DesenhosPorColecao()
        {
            PedidoViewModel model = new PedidoViewModel();

            using (var ctx = new TIDalutexContext())
            {
                var query =
                from vw in ctx.VW_COLECAO_ATUAL
                select vw;

                model.Galeria = query.ToList();
            }

            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];
            return View(model);
        }

        public ActionResult ArtigosDisponiveis(string desenho, string variante)
        {
            ArtigosDisponiveisViewModel model = new ArtigosDisponiveisViewModel();
            model.Desenho = desenho;
            model.Variante = variante;
            model.Imagem = ConfigurationManager.AppSettings["PASTA_DESENHOS"] + desenho + "_" + variante + ".jpg";

            List<VW_CARACT_DESENHOS> lstQuery = null;

            using (var ctx = new TIDalutexContext())
            {
                var query =
                    from vw in ctx.VW_CARACT_DESENHOS
                    where vw.DESENHO == desenho
                    select vw;

                lstQuery = query.ToList();
            }

            VW_CARACT_DESENHOS objPrimeiroCarac = lstQuery.FirstOrDefault();
            int? iIDTecnologia;

            if (objPrimeiroCarac != null)
            {
                iIDTecnologia = objPrimeiroCarac.ID_TECNOLOGIA;

                List<int?> lstCaracteristicas = new List<int?>();

                foreach (VW_CARACT_DESENHOS item in lstQuery)
                {
                    lstCaracteristicas.Add(item.ID_CARAC_TEC);
                }

                using (var ctx = new TIDalutexContext())
                {
                    //TODO:Verificar casos em que a key está nula e está omitindo do resultado.
                    var query =
                        from ar in ctx.VW_ARTIGOS_DISPONIVEIS
                        where
                            (ar.ID_TECNOLOGIA == null || ar.ID_TECNOLOGIA != iIDTecnologia)
                            &&
                            (ar.ID_CARAC_TEC == null || !lstCaracteristicas.Contains(ar.ID_CARAC_TEC))
                        select ar;

                    model.Artigos = query.ToList();
                }
            }

            return View(model);
        }

        public ActionResult InserirNoCarrinho(string desenho, string variante, string artigo, string tecnologia)
        {
            InserirNoCarrinhoViewModel model = new InserirNoCarrinhoViewModel();
            model.Desenho = desenho;
            model.Variante = variante;
            model.Artigo = artigo;
            model.Tecnologia = tecnologia;

            using (var ctxTI = new TIDalutexContext())
            {
                var query =
                    from app in ctxTI.ARTIGO_PESO_PADRAO
                    where
                        (
                            app.ATIVO == true
                            && app.ARTIGO == artigo
                            && app.TECNOLOGIA == tecnologia.Substring(0, 1)
                        )
                    select app;

                ARTIGO_PESO_PADRAO objValorPadrao = query.FirstOrDefault();
                if (objValorPadrao != null)
                {
                    model.UnidadeMedida = objValorPadrao.UM;
                    model.ValorPadrao = objValorPadrao.VALOR;
                }
                else
                {
                    ModelState.AddModelError("", "Valor padrão não encontrado.");
                }
            }

            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                int[] tiposPedidos = new int[] { 0, 6, 7, 9, 15, 16, 2, 21, 3 };

                model.TiposPedido = ctxDalutex.COML_TIPOSPEDIDOS.Where(x => tiposPedidos.Any(tipo => x.TIPOPEDIDO.Equals(tipo))).ToList();
            }

            model.ObterTipoPedido = base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult InserirNoCarrinho(InserirNoCarrinhoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (base.Session_Carrinho == null)
                    {
                        base.Session_Carrinho = new ConclusaoPedidoViewModel();
                        base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();
                    }
                    else
                    {
                        if (base.Session_Carrinho.Itens == null)
                        {
                            base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();
                        }
                    }

                    if (model.IDTipoPedido >= 0)
                        base.Session_Carrinho.IDTipoPedido = model.IDTipoPedido;

                    model.Quantidade = model.Pecas * model.ValorPadrao;

                    using(var ctx = new TIDalutexContext())
                    {
                        int iID_DISP = ctx.DISPONIBILIDADE_MALHA
                                                .OrderByDescending(x => x.ID_DISP)
                                                .First()
                                                .ID_DISP;
                                             
                        model.DataEntregaItem = ctx.DISPONIBILIDADE_MALHA
                                                .Where(x => x.ARTIGO == model.Artigo && x.MAQUINA == model.Tecnologia && x.ID_DISP == iID_DISP)
                                                .First()
                                                .DISPONIBILIDADE_PCP
                                                .GetValueOrDefault();


                        if (model.DataEntregaItem == default(DateTime))
                            model.DataEntregaItem = DateTime.Today.AddYears(1);

                        if (base.Session_Carrinho.DataEntrega < model.DataEntregaItem)
                            base.Session_Carrinho.DataEntrega = model.DataEntregaItem;
                    }

                    base.Session_Carrinho.Itens.Add(model);

                    return RedirectToAction("ArtigosDisponiveis", "Pedido", new { desenho = model.Desenho, variante = model.Variante, });
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Carrinho()
        {
            ViewBag.Carrinho = base.Session_Carrinho;

            return View();
        }

        public ActionResult ConclusaoPedido()
        {
            ConclusaoPedidoViewModel model = new ConclusaoPedidoViewModel();
            model.QualidadeComercial = new List<KeyValuePair<string, string>>();
            model.QualidadeComercial.Add(new KeyValuePair<string, string>(Enums.QualidadeComercial.A.ToString(), Enums.QualidadeComercial.A.ToString()));
            model.QualidadeComercial.Add(new KeyValuePair<string, string>(Enums.QualidadeComercial.B.ToString(), Enums.QualidadeComercial.B.ToString()));
            model.QualidadeComercial.Add(new KeyValuePair<string, string>(Enums.QualidadeComercial.C.ToString(), Enums.QualidadeComercial.C.ToString()));
            
            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                model.Moedas = ctxDalutex.CADASTRO_MOEDAS.ToList();
                model.ViasTransporte = ctxDalutex.COML_VIASTRANSPORTE.ToList();
                model.Fretes = ctxDalutex.COML_TIPOSFRETE.ToList();
                model.CanaisVenda = ctxDalutex.CANAIS_VENDA.ToList();
                model.GerentesVenda = ctxDalutex.COML_GERENCIAS.Where(x => x.CANALVENDA == (int)Enums.CanaisVenda.TELEVENDAS).ToList();
            }

            using (TIDalutexContext ctxTI = new TIDalutexContext())
            {
                model.LocaisVenda = ctxTI.LOCALVENDA.ToList();
                model.TiposAtendimento = ctxTI.PRE_PEDIDO_ATEND.ToList();
                model.CondicoesPagto = ctxTI.VW_CONDICAO_PGTO.ToList();
            }

            if (base.Session_Carrinho != null)
                model.Itens = base.Session_Carrinho.Itens;

            return View(model);
        }

        [HttpPost]
        public ActionResult ConclusaoPedido(ConclusaoPedidoViewModel model)
        {            
            try
            {
                int iNUMERO_PEDIDO_BLOCO = default(int);

                while(iNUMERO_PEDIDO_BLOCO == default(int))
                {
                    using(var ctx = new TIDalutexContext())
                    {
                        PROXIMO_NUMERO_PEDIDO reservar = ctx.PROXIMO_NUMERO_PEDIDO.Where(x => x.DISPONIVEL == 0).OrderBy(x => x.NUMERO_PEDIDO).First();
                        iNUMERO_PEDIDO_BLOCO = reservar.NUMERO_PEDIDO;
                        reservar.DISPONIVEL = 1;
                        ctx.SaveChanges();
                    }
                }
               

                if (ModelState.IsValid)
                {
                    PRE_PEDIDO objPrePedido = new PRE_PEDIDO()
                    {
                        NUMERO_PEDIDO_BLOCO = iNUMERO_PEDIDO_BLOCO,
                        TIPO_PEDIDO = base.Session_Carrinho.IDTipoPedido,
                        ID_REPRESENTANTE = base.Session_Carrinho.IDRepresentante,
                        ID_CLIENTE = base.Session_Carrinho.IDClienteFatura,
                        QUALIDADE_COM = model.IDQualidadeComercial,
                        COD_COND_PGTO = model.IDCondicoesPagto,
                        OBSERVACOES = model.Observacoes,
                        DATA_ENTREGA = base.Session_Carrinho.DataEntrega,
                        ID_CLIENTE_ENTREGA = base.Session_Carrinho.IDClienteEntrega,
                        ID_TRANSPORTADORA = base.Session_Carrinho.IDTransportadora,
                        USUARIO_INICIO = base.Session_Usuario.NOME_USU,
                        DATA_INICIO = DateTime.Now,
                        DATA_FINAL = DateTime.Now,
                        ID_LOCAL = model.IDLocaisVenda,
                        COD_MOEDA = model.IDMoedas,
                        CANAL_VENDAS = model.IDCanaisVenda,
                        ATENDIMENTO = model.IDTiposAtendimento,
                        TIPOFRETE = model.IDFretes,
                        //GERENTE = model.IDGerentesVenda,// não é necessario gravar neste campo para pedidos <> de PE
                        VIATRANSPORTE = model.IDViasTransporte,
                        COMISSAO = Session_Carrinho.PorcentagemComissao,// ver com cassiano
                        ORIGEM = "PW" // APENAS PRA INFORMAR QUE ESTE PEDIDO VEIO DO PEDIDO WEB NOVO.                                                                                                                                                                                                                                                                                                                                              
                    };

                    List<PRE_PEDIDO_ITENS> lstItens = new List<PRE_PEDIDO_ITENS>();

                    foreach(InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                    {
                        PRE_PEDIDO_ITENS objItem = new PRE_PEDIDO_ITENS(){

                        };

                        //lstItens.Add
                    }
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
          
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public JsonResult ObterDesenho(string desenho, string variante)
        //{
        //    string path = ConfigurationManager.AppSettings["PASTA_DESENHOS"] + desenho + "_" + variante + ".jpg";
        //    if(System.IO.File.Exists(path))
        //    {
        //        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        //        byte[] buffer = new byte[fileStream.Length];
        //        fileStream.Read(buffer, 0, (int)fileStream.Length);
        //        fileStream.Close();
        //        string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
        //        return Json(new { Image = str, JsonRequestBehavior.AllowGet });
        //    }
        //    else
        //    {
        //        return Json(new { Image = "", JsonRequestBehavior.AllowGet });
        //    }
        //}
    }
}