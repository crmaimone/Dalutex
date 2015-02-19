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
        public ActionResult MenuColecoes(string IDColecao)
        {
            MenuColecoesViewModel model = new MenuColecoesViewModel();

            int iIDColecao;

            if (int.TryParse(IDColecao, out iIDColecao))
            {
                using (var ctx = new TIDalutexContext())
                {
                    VW_COLECAO objColecao = ctx.VW_COLECAO.Where(x => x.ID_COLECAO == iIDColecao).First();
                    model.Colecoes = new List<VW_COLECAO>();
                    model.Colecoes.Add(objColecao);
                    model.Filtro = objColecao.NOME_COLECAO;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult MenuColecoes(MenuColecoesViewModel model)
        {
            using (var ctx = new TIDalutexContext())
            {
                model.Colecoes = ctx.VW_COLECAO.Where(x => x.NOME_COLECAO.ToUpper().Contains(model.Filtro.ToUpper())).OrderBy(x => x.NOME_COLECAO).ToList();
            }

            return View(model);
        }

        public ActionResult DesenhosPorColecao(string IDColecao, string pagina)
        {
            DesenhosPorColecaoViewModel model = new DesenhosPorColecaoViewModel();
            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            using (var ctx = new TIDalutexContext())
            {
                if( IDColecao == "ATUAL")
                {
                    model.IDColecao = int.Parse(ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Atual).PARAMETRO1);
                }
                else if( IDColecao == "POCKET")
                {
                    model.IDColecao = int.Parse(ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Pocket).INT1.ToString());
                }
                else if(IDColecao == null)
                {
                    ModelState.AddModelError("", "Coleção não informada.");
                    return View(model);
                }
                else
                {
                    model.IDColecao = int.Parse(IDColecao);
                }

                var query =
                    from dc in ctx.VW_DESENHOS_POR_COLECAO
                    where
                        dc.COLECAO == model.IDColecao
                    group dc by
                        new
                        {
                            dc.DESENHO,
                            dc.VARIANTE
                        }
                        into dv
                        select new DesenhoVariante
                        {
                            Desenho = dv.Key.DESENHO,
                            Variante = dv.Key.VARIANTE
                        };

                model.Galeria = query.OrderBy(x => x.Desenho).ThenBy(x => x.Variante).Skip((model.Pagina - 1) * 24).Take(24).ToList();
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

        public ActionResult Ampliacao(string desenho, string variante)
        {
            AmpliacaoViewModel model = new AmpliacaoViewModel()
            {
                Desenho = desenho,
                Variante = variante,
                Imagem = ConfigurationManager.AppSettings["PASTA_DESENHOS"] + desenho + "_" + variante + ".jpg",
            };

            return View(model);
        }

        public ActionResult InserirNoCarrinho(string desenho, string variante, string artigo, string tecnologia)
        {
            InserirNoCarrinhoViewModel model = new InserirNoCarrinhoViewModel();
            model.Desenho = desenho;
            model.Variante = variante;
            model.Artigo = artigo;
            model.TecnologiaPorExtenso = tecnologia;

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
                    VMASCARAPRODUTOACABADO objValorPadraoView = null;

                    using (var ctx = new DalutexContext())
                    {
                        objValorPadraoView = ctx.VMASCARAPRODUTOACABADO.Where(x => x.ARTIGO == model.Artigo).FirstOrDefault();
                        if(objValorPadraoView != null)
                        {
                            model.UnidadeMedida = objValorPadraoView.UM;
                            if (model.UnidadeMedida.ToUpper() == "KG")
                            {
                                model.ValorPadrao = (decimal) Enums.ValorPadraoUnidade.Quilo;
                            }
                            else if (model.UnidadeMedida.ToUpper() == "MT")
                            {
                                model.ValorPadrao = (decimal) Enums.ValorPadraoUnidade.Metro;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Unidade de medida inválida.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unidade de medida não encontrada.");
                        }
                    }
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
            bool hasErrors = false;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ValorPadrao <= 0)
                    {
                        ModelState.AddModelError("", "Não é permitido salvar sem valor padrão definido.");
                        hasErrors = true;
                    }
                    if (model.Pecas <= 0)
                    {
                        ModelState.AddModelError("", "Campo \"Peças\" não pode ser menor ou igual a zero.");
                        hasErrors = true;
                    }
                    if(model.Preco <= 0)
                    {
                        ModelState.AddModelError("", "Campo \"Preço\" não pode ser menor ou igual a zero.");
                        hasErrors = true;
                    }

                    if(hasErrors)
                    {
                        return View(model);
                    }

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
                    model.ValorTotalItem = model.Quantidade * model.Preco;

                    using(var ctx = new DalutexContext())
                    {
                        VMASCARAPRODUTOACABADO objReduzido = ctx.VMASCARAPRODUTOACABADO.Where(
                                x => 
                                    x.ARTIGO == model.Artigo 
                                    && x.DESENHO == model.Desenho 
                                    && x.VARIANTE == model.Variante 
                                    && x.MAQUINA == model.Tecnologia
                                ).FirstOrDefault();

                        if(objReduzido != null && objReduzido.CODIGO_REDUZIDO > default(int))
                        {
                            model.Reduzido = objReduzido.CODIGO_REDUZIDO;
                        }
                    }

                    using (var ctx = new TIDalutexContext())
                    {
                        int iID_DISP = ctx.DISPONIBILIDADE_MALHA
                                                .OrderByDescending(x => x.ID_DISP)
                                                .First()
                                                .ID_DISP;

                        DISPONIBILIDADE_MALHA objDisponibilidade = ctx.DISPONIBILIDADE_MALHA
                                                .Where(x => x.ARTIGO == model.Artigo && x.MAQUINA == model.Tecnologia && x.ID_DISP == iID_DISP)
                                                .FirstOrDefault();

                        if (objDisponibilidade != null && objDisponibilidade.DISPONIBILIDADE_PCP != null)
                            model.DataEntregaItem = (DateTime)objDisponibilidade.DISPONIBILIDADE_PCP;
                        else
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

            foreach(InserirNoCarrinhoViewModel item in model.Itens)
            {
                model.TotalPedido += item.ValorTotalItem;
            }

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
                    if(model.IDTiposAtendimento.Equals((int)Enums.TiposAtendimento.PedidoCompleto))
                    {
                        int numParcelas = model.CondicoesPagto.Where(x => x.ID_COND == model.IDCondicoesPagto).First().PARCELAS;
                        int fatorMultiplicacao = 0;
                        decimal valorMinimoParcelas = decimal.Parse(ConfigurationManager.AppSettings["VALOR_PARCELA_MINIMA"]);
                        
                        if(model.IDQualidadeComercial == Enums.QualidadeComercial.A.ToString())
                        {
                            fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.A;
                        }
                        else if(model.IDQualidadeComercial == Enums.QualidadeComercial.B.ToString())
                        {
                            fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.B;
                        }
                        else if(model.IDQualidadeComercial == Enums.QualidadeComercial.C.ToString())
                        {
                            fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.C;
                        }

                        if (((model.TotalPedido * fatorMultiplicacao ) / numParcelas) < valorMinimoParcelas)
                        {
                            ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C"));
                            return View(model);
                        }
                    }

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
                        COMISSAO = Session_Carrinho.PorcentagemComissao,
                        ORIGEM = "PW" // APENAS PRA INFORMAR QUE ESTE PEDIDO VEIO DO PEDIDO WEB NOVO.                                                                                                                                                                                                                                                                                                                                              
                    };

                    List<PRE_PEDIDO_ITENS> lstItens = new List<PRE_PEDIDO_ITENS>();

                    int i = 0;
                    foreach(InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                    {
                        i++;

                        PRE_PEDIDO_ITENS objItem = new PRE_PEDIDO_ITENS(){
                            ARTIGO = item.Artigo,
                            //COR = TODO,
                            DATA_ENTREGA = item.DataEntregaItem,
                            DESENHO = item.Desenho,
                            ITEM = i,
                            LISO_ESTAMP = item.Tecnologia,
                            NUMERO_PEDIDO_BLOCO = iNUMERO_PEDIDO_BLOCO,
                            //COLECAO = Discutir com a Etiane
                            PE = "N",
                            PRECO_UNIT = item.Preco,
                            //PRECOLISTA = Buscar qdo tivermos o preço da tabela
                            QTDEPC = item.Pecas,
                            QUANTIDADE = item.Quantidade,
                            REDUZIDO_ITEM = item.Reduzido > default(int)? item.Reduzido : -2,
                            UM = item.UnidadeMedida,
                            VALOR_TOTAL = item.Preco * item.Quantidade,
                            VARIANTE = item.Variante
                        };

                        lstItens.Add(objItem);
                    }

                    using(var ctx = new TIDalutexContext())
                    {
                        ctx.PRE_PEDIDO.Add(objPrePedido);

                        ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                        {
                            NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                            COD_CRITICA = (decimal)Enums.TiposCritica.LiberacaoFinanceira,
                            FLG_STATUS = "C"
                        });

                        bool hasItemSemReduzido = false;

                        foreach (PRE_PEDIDO_ITENS item in lstItens)
                        {
                            if(!hasItemSemReduzido && item.REDUZIDO_ITEM.GetValueOrDefault() == -2)
                            {
                                hasItemSemReduzido = true;
                            }

                            ctx.PRE_PEDIDO_ITENS.Add(item);
                        }

                        if (hasItemSemReduzido)
                        {
                            ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                            {
                                NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                                COD_CRITICA = (decimal)Enums.TiposCritica.SemReduzido,
                                FLG_STATUS = "C"
                            });
                        }


                        ctx.SaveChanges();
                    }

                    return RedirectToAction("ConfirmacaoPedido", "Pedido", new { NumeroPedido = iNUMERO_PEDIDO_BLOCO.ToString() });
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
          
        }

        public ActionResult ConfirmacaoPedido(string NumeroPedido)
        {
            ViewBag.NumeroPedido = NumeroPedido;
            return View();
        }

        public ActionResult EspelhoPedido()
        {
            return View();
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