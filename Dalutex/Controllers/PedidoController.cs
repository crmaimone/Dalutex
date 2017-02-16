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
using Dalutex.Models.Utils;
using System.Net.Mail;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Text;

namespace Dalutex.Controllers
{
    public class PedidoController : BaseController
    {
        #region Shared

        private ActionResult ValidarTipoPedido(object model)
        {
            if (base.Session_Carrinho != null && base.Session_Carrinho.IDTipoPedido != -1) //Pedido de algum tipo já iniciado
            {
                if (base.Session_Carrinho.Itens == null || base.Session_Carrinho.Itens.Count == 0)//Se já foi iniciado mas ainda não tem itens, limpar
                {
                    base.Session_Carrinho.IDTipoPedido = -1;
                    return null;
                }
                else
                {
                    if ((model is ItensParaReservaViewModel))//Se for pedido de reserva
                    {
                        if (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA)
                        {
                            //Não permitir
                            return RedirectToAction("Message", new { message = "Iniciado pedido de venda. Esvazie o carrinho antes de incluir pedidos de reserva.", title = "Pedido já iniciado." });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else//Outros tipos de pedidos
                    {
                        if (base.Session_Carrinho.IDTipoPedido == (int)Enums.TiposPedido.RESERVA)//Se uma reserva já tiver iniciada
                        {
                            //Não permitir
                            return RedirectToAction("Message", new { message = "Iniciado pedido de reserva. Esvazie o carrinho antes de incluir outros tipos de pedidos.", title = "Pedido já iniciado." });
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public ActionResult EmConstrucao()
        {
            return View();
        }

        [HttpGet]
        public JsonResult VerificaDesenhoBloqueado(string desenho)
        {
            ////ver com cassiano --- 25/02/2016 -----------------------------------------------------------------------------------------
            using (var ctx = new TIDalutexContext())
            {
                VW_ORDEM_ANTERIOR_BLOQ obj = ctx.VW_ORDEM_ANTERIOR_BLOQ.Where(x => x.DESENHO == desenho).FirstOrDefault();
                //se encontrou, está bloqueado.... mostrar mensagem na tela para o usuario e não deixar selecionar o desenho ..........
                if (obj != null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VerificaTecnologiaPilotagem(int IDTipoPedido, string TecOriginal, string TecSelecionada, string Artigo)
        {
            if (IDTipoPedido == (int)Enums.TiposPedido.AMOSTRA || IDTipoPedido == (int)Enums.TiposPedido.PILOTAGEM)
            {
                if ((TecOriginal != TecSelecionada) && (TecOriginal == "CILINDRO"))
                {
                    //oda -- 20/09/2016 ---- validar se tem estrição do artigo na tecnologia original --------
                    using (var ctx = new TIDalutexContext())
                    {
                        //List<VW_ARTIGOS_DISPONIVEIS_NEW> ListaArtigos = ctx.VW_ARTIGOS_DISPONIVEIS_NEW.Where(x => x.TECNOLOGIA == TecOriginal && 
                        //    x.ARTIGO == Artigo && x.CARACT_TECNICA != "X").ToList();

                        VW_ARTIGOS_DISPONIVEIS_NEW restricao = ctx.VW_ARTIGOS_DISPONIVEIS_NEW.Where(x => x.TECNOLOGIA == TecOriginal &&
                            x.ARTIGO == Artigo && x.CARACT_TECNICA != "X").FirstOrDefault();

                        if (restricao != null)
                        {
                            return Json(restricao.CARACT_TECNICA, JsonRequestBehavior.AllowGet);   

                            //foreach (var item in ListaArtigos)
                            //{
                            //    return Json(item.CARACT_TECNICA, JsonRequestBehavior.AllowGet);                         
                            //}
                                                       
                           // return Json(true, JsonRequestBehavior.AllowGet);
                        }
                    }                    
                }
            }
            
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void AtualizaComplentoPedido(string numPedidoCliente, string observacoes)
        {
            if (!string.IsNullOrWhiteSpace(numPedidoCliente))
            {
                base.Session_Carrinho.PedidoCliente = int.Parse(numPedidoCliente);
            }
            else
            {
                base.Session_Carrinho.PedidoCliente = 0;
            }

            base.Session_Carrinho.Observacoes = observacoes;
        }
        
        public ActionResult ArtigosDisponiveis(
           string desenho,
           string variante,
           int idcolecao,
           string nmcolecao,
           int pagina,
           int idvariante,
           int pedidoreserva,
           int itempedidoreserva,
           int tipo,
           string errMessage)
        {                        
            ArtigosDisponiveisViewModel model = new ArtigosDisponiveisViewModel();
            model.Desenho = desenho;
            model.Variante = variante;
            model.IDColecao = idcolecao;
            model.NMColecao = nmcolecao;
            model.Pagina = pagina;
            model.Imagem = ConfigurationManager.AppSettings["PASTA_DESENHOS"] + desenho + "_" + variante + ".jpg";
            model.PedidoReserva = pedidoreserva;
            model.IDVariante = idvariante;
            model.ItemPedidoReserva = itempedidoreserva;
            model.Tipo = (Enums.ItemType)tipo;

                using (var ctx = new TIDalutexContext())
                {
                    VW_CARACT_DESENHOS objPrimeiroCarac = ctx.VW_CARACT_DESENHOS.Where(x => x.DESENHO == desenho).FirstOrDefault();
                    if (objPrimeiroCarac != null && objPrimeiroCarac.TECNOLOGIA != null)            
                    {
                        model.TecnologiaAtual = objPrimeiroCarac.TECNOLOGIA.Replace(" ", "_");
                        List<VW_TROCA_TEC> lstTrocas = ctx.VW_TROCA_TEC.Where(x => x.ID_TEC_ORIGINAL == objPrimeiroCarac.ID_TECNOLOGIA).ToList();

                        List<int?> lstTecnologias = new List<int?>();
                        lstTecnologias.Add(objPrimeiroCarac.ID_TECNOLOGIA);

                        foreach (VW_TROCA_TEC item in lstTrocas)
                        {
                            lstTecnologias.Add(item.ID_TEC_NOVA);
                        }

                    
                    //oda -- 27/05/2016 --- alteração para carregar toda lista de arigos disponiveis porem marcando como restrição
                    List<string> strCaracterisNew = ctx.VW_CARACT_DESENHOS_NEW.Where(x => x.DESENHO == desenho).Select(x => x.CARACT_TECNICA).ToList();
                    List<VW_ARTIGOS_DISPONIVEIS_NEW> ListaArtigos = ctx.VW_ARTIGOS_DISPONIVEIS_NEW.Where(x => lstTecnologias.Contains(x.ID_TECNOLOGIA)).OrderBy(o => o.ID_DA_VIEW).ToList();

                    model.Artigos = 
                        (
                        from ar in ListaArtigos
                            group ar by
                                new
                                {
                                    ar.ARTIGO,
                                    ar.TECNOLOGIA,
                                }
                                into grp
                                //orderby vw.ID_DA_VIEW
                                select new ArtigoTecnologia
                                {
                                    Artigo = grp.Key.ARTIGO,
                                    Tecnologia = grp.Key.TECNOLOGIA
                                }).ToList();

                    foreach (var item in model.Artigos)
                    {
                        var elemento = ListaArtigos.Where(x => x.ARTIGO == item.Artigo && x.TECNOLOGIA == item.Tecnologia && strCaracterisNew.Contains(x.CARACT_TECNICA)).FirstOrDefault();
                        if( elemento != null)
                            item.Restricao += " (Restrição: " + elemento.CARACT_TECNICA + ")";

                        if(ListaArtigos.Where(x => x.ARTIGO == item.Artigo && x.TECNOLOGIA == item.Tecnologia).First().ART_DISP_PCP == "XX")
                            item.Restricao += " (Sem Disp. PCP)";
                    }

                    if (base.Session_Carrinho != null)
                    {
                        foreach (var item in model.Artigos)
                        {
                            if (base.Session_Carrinho.Itens.Exists(
                                delegate(InserirNoCarrinhoViewModel incluido)
                                {
                                    return incluido.Artigo == item.Artigo
                                        && incluido.TecnologiaPorExtenso == item.Tecnologia
                                        && incluido.Desenho == desenho
                                        && incluido.Variante == variante;
                                }))
                            {
                                item.TemNoCarrinho = true;
                            }
                        }
                    }
                }
            }

            if(!string.IsNullOrWhiteSpace(errMessage ))
            {
                ModelState.AddModelError("", errMessage);
            }

            return View(model);
        }

        public ActionResult Ampliacao(
            string desenho,
            string variante,
            string idcolecao,
            string nmcolecao,
            int pagina,
            string retornarpara,
            string codstudio,
            int tipo)
        {
            AmpliacaoViewModel model = new AmpliacaoViewModel()
            {
                Desenho = desenho,
                Variante = variante,
                IDColecao = idcolecao,
                NMColecao = nmcolecao,
                Pagina = pagina,
                RetornarPara = retornarpara,
                CodStudio = codstudio,
                //Tipo = (Enums.ItemType)tipo
                Tipo = tipo
            };

            if (tipo == (int)Enums.ItemType.Estampado || tipo == (int)Enums.ItemType.ValidacaoReserva)
            {
                model.Imagem = ConfigurationManager.AppSettings["PASTA_DESENHOS"].Replace("~", "") + desenho + "_" + variante + ".jpg";
            }
            else if (tipo == (int)Enums.ItemType.Reserva)
            {
                model.Imagem = ConfigurationManager.AppSettings["PASTA_RESERVAS"].Replace("~", "") + codstudio + ".jpg";
            }

            return View(model);
        }


        public ActionResult InserirNoCarrinho(
            string idcolecao
            , string nmcolecao
            , string pagina
            , string desenho
            , string variante
            , string artigo
            , string tecnologia
            , string tecnologiaoriginal
            , string cor
            , string modo
            , string rgb
            , int reduzido
            , string codstudio
            , string coddal
            , int tipo
            , int idstudio
            , int iditemstudio
            , int idvariante
            , int pedidoreserva
            , int itempedidoreserva
            , int iditem
            , bool preexistente
            , bool TemRestricao = false
            , string Restricao = ""
            , bool ehreacabamento = false
            , string um = ""
            , int codcompose = -1           
            )
        {
            InserirNoCarrinhoViewModel model = new InserirNoCarrinhoViewModel();

            model.Desenho = desenho;
            model.Variante = variante;
            model.Artigo = artigo;
            model.TecnologiaPorExtenso = tecnologia;
            model.TecnologiaOriginal = tecnologiaoriginal;           
            model.IDColecao = idcolecao;
            model.NMColecao = nmcolecao;
            model.Cor = cor;
            model.RGB = rgb;
            model.Tipo = (Enums.ItemType)tipo;
            model.CodDal = coddal;
            model.CodStudio = codstudio;
            model.IDStudio = idstudio;
            model.IDItemStudio = iditemstudio;
            model.IDVariante = idvariante;
            model.PedidoReserva = pedidoreserva;
            model.ItemPedidoReserva = itempedidoreserva;
            model.Reduzido = reduzido;
            model.ID = iditem;
            model.PreExistente = preexistente;
            model.EhReacabamento = ehreacabamento;
            model.Compose = codcompose;
            model.NumeroSequencial = iditem;

            #region Restrições
            //if ((model.TecnologiaPorExtenso != "L") && (!ehreacabamento))
            if ((!ehreacabamento) && (model.IDTipoPedido == (int)Enums.TiposPedido.RESERVA))
            {
                using (var ctxTI = new TIDalutexContext())
                {
                    List<VW_ARTIGOS_DISPONIVEIS_NEW> ListaArtigosInativo = ctxTI.VW_ARTIGOS_DISPONIVEIS_NEW
                        .Where(x => x.TECNOLOGIA == tecnologia && x.ARTIGO == artigo)
                        .OrderBy(o => o.ID_DA_VIEW).ToList();

                    if (ListaArtigosInativo.Count == 0)
                    {
                        //oda-- 15/02/2017 -- bloquear entrada de artigo inativo - solicitação Ludovit ----------------------
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["BLOQUEIA_ARTIGO_INATIVO"]) == true)                                                            
                        {
                            if (model.TecnologiaPorExtenso != "L") //se for LISO, o link não funciona pra selecionar o item, ou seja, não chega aki.... ou chega aki apenas o que não for LISO
                            {
                                return RedirectToAction("ArtigosDisponiveis", "Pedido",
                                    new
                                    {
                                        desenho = model.Desenho,
                                        variante = model.Variante,
                                        idcolecao = model.IDColecao,
                                        nmcolecao = model.NMColecao,
                                        pagina = model.Pagina,
                                        pedidoreserva = model.PedidoReserva,
                                        idvariante = model.IDVariante,
                                        itempedidoreserva = model.ItemPedidoReserva,
                                        tipo = (int)model.Tipo,
                                        errMessage = "Artigo com Restrição: (ARTIGO INATIVO)"
                                    });
                            }                           
                        }
                        else
                        {                        
                            model.TemRestricao = true;
                            model.Restricao = "Artigo Inativo";
                        }
                    }

                    List<string> strCaracterisNew = ctxTI.VW_CARACT_DESENHOS_NEW.Where(x => x.DESENHO == desenho).Select(x => x.CARACT_TECNICA).ToList();

                    List<VW_ARTIGOS_DISPONIVEIS_NEW> ListaArtigos = ctxTI.VW_ARTIGOS_DISPONIVEIS_NEW
                        .Where(x => x.TECNOLOGIA == tecnologia && x.ARTIGO == artigo)//&& strCaracterisNew.Contains(x.CARACT_TECNICA))
                        .OrderBy(o => o.ID_DA_VIEW).ToList();

                    if (ListaArtigos.Count > 0)
                    {
                        if (ListaArtigos[0].ART_DISP_PCP == "XX")
                        {
                            //oda-- 29/07/2016 -- bloquear entrada com restrição ----------------------
                            if (Convert.ToBoolean(ConfigurationManager.AppSettings["BLOQUEIA_RESTRICAO"]) == true)
                            {
                                ModelState.AddModelError("", "Artigo Sem Disp. PCP.");
                                
                                return RedirectToAction("ArtigosDisponiveis", "Pedido",
                                    new
                                    {
                                        desenho = model.Desenho,
                                        variante = model.Variante,
                                        idcolecao = model.IDColecao,
                                        nmcolecao = model.NMColecao,
                                        pagina = model.Pagina,
                                        pedidoreserva = model.PedidoReserva,
                                        idvariante = model.IDVariante,
                                        itempedidoreserva = model.ItemPedidoReserva,
                                        tipo = (int)model.Tipo
                                    });
                            }
                            else
                            {
                                //deixa entrar com restrição -----------------------------------------
                                model.TemRestricao = true;
                                model.Restricao += " (Sem Disp. PCP)";
                            }                                                                                    
                        }

                        foreach (var item in ListaArtigos)
                        {
                            if (strCaracterisNew.Contains(item.CARACT_TECNICA))
                            {
                                //oda-- 29/07/2016 -- bloquear entrada com restrição ----------------------
                                if (Convert.ToBoolean(ConfigurationManager.AppSettings["BLOQUEIA_RESTRICAO"]) == true)
                                {                                    
                                    return RedirectToAction("ArtigosDisponiveis", "Pedido",
                                    new
                                    {
                                        desenho = model.Desenho,
                                        variante = model.Variante,
                                        idcolecao = model.IDColecao,
                                        nmcolecao = model.NMColecao,
                                        pagina = model.Pagina,
                                        pedidoreserva = model.PedidoReserva,
                                        idvariante = model.IDVariante,
                                        itempedidoreserva = model.ItemPedidoReserva,
                                        tipo = (int)model.Tipo,
                                        errMessage = "Artigo com Restrição: (" + strCaracterisNew[0] + ")"
                                    });
                                }
                                else
                                {
                                    model.TemRestricao = true;
                                    model.Restricao += " (Restrição: " + strCaracterisNew[0] + ")";
                                    break;
                                }                                                                
                            }
                        }
                    }
                }
            }
            #endregion

            if (base.Session_Carrinho != null)
            {
                model.IDTipoPedido = base.Session_Carrinho.IDTipoPedido;

                if (model.IDTipoPedido >= 0)
                {                
                    using (var ctx = new DalutexContext())
                    {
                        model.DescTipoPedido = ctx.COML_TIPOSPEDIDOS.Find(model.IDTipoPedido).DESCRICAO.Trim();
                    }
                }

                ViewBag.ItensCarrinho = base.Session_Carrinho.Itens;
               
                if (modo == "I")//Inclusão
                {
                    if (base.Session_Carrinho.Itens != null)
                    {
                        foreach (var item in base.Session_Carrinho.Itens)
                        {
                            if (model.IDTipoPedido == (int)Enums.TiposPedido.BNFPROPRIO)
                            {
                                if (item.Reduzido == model.Reduzido)
                                {
                                    ModelState.AddModelError("", "Este item já foi incluído no carrinho.");

                                    return RedirectToAction("Reacabamento", "Pedido");//, new { pagina = model.Pagina });//, pagina = 1 
                                }
                            }
                            else
                            {
                                if (model.TecnologiaPorExtenso != "L")
                                {
                                    if (item.Artigo == model.Artigo && item.Tecnologia == model.Tecnologia && item.Desenho == model.Desenho && item.Variante == model.Variante)
                                    {
                                        ModelState.AddModelError("", "Este item já foi incluído no carrinho.");

                                        return RedirectToAction("Desenhos", "Pedido", new { idcolecao = model.NMColecao, nmcolecao = model.NMColecao, filtro = model.Desenho, pagina = "1", totalpaginas = "1" });
                                    }
                                }
                                else
                                {
                                    if (item.Reduzido == model.Reduzido)
                                    {
                                        ModelState.AddModelError("", "Este item já foi incluído no carrinho.");

                                        return RedirectToAction("Lisos", "Pedido", new { idcolecao = "LISOS", pagina = 1 });
                                    }
                                }
                            }
                        }
                    }
                }                                
            }

            if (pagina != null)
                model.Pagina = int.Parse(pagina);

            if (modo == "A")//Alterando item
            {
                model = base.Session_Carrinho.Itens.Where(x => x.Equals(model)).First();
            }

            model.Modo = modo;

            if ( (model.Tipo != Enums.ItemType.Reserva) && (!ehreacabamento) )//se for pedido diferente de reserva e não for reacabamento
            {
                using (var ctxTI = new TIDalutexContext())
                {
                    if (model.Tipo != Enums.ItemType.Liso) //se tipo for estampado
                    {
                        using (var ctx = new DalutexContext())
                        {
                            string _cor = "0000000";
                            if (model.Tipo == Enums.ItemType.ValidacaoReserva)//se for pedido de validação de reserva, procura pelo item exclusivo "E"
                            {
                                _cor = "E000000";
                            }

                            //procura o reduzido pela combinação de itens [Artigo, Tec, Desenho, Var]
                            VMASCARAPRODUTOACABADO objReduzido = ctx.VMASCARAPRODUTOACABADO.Where(
                                    x =>
                                        x.ARTIGO == model.Artigo
                                        && x.DESENHO == model.Desenho
                                        && x.VARIANTE == model.Variante
                                        && x.MAQUINA == model.Tecnologia
                                        && x.COR == _cor
                                    ).FirstOrDefault();

                            if (objReduzido != null && objReduzido.CODIGO_REDUZIDO > default(int))
                            {
                                model.Reduzido = objReduzido.CODIGO_REDUZIDO;
                                try
                                {
                                    model.Colecao = ctxTI.VW_GRUPO_COL_RED.Where(x => x.REDUZIDO == objReduzido.CODIGO_REDUZIDO).FirstOrDefault().COLECAO;
                                }
                                catch 
                                { 
                                    return RedirectToAction("ArtigosDisponiveis", "Pedido",
                                    new
                                    {
                                        desenho = model.Desenho,
                                        variante = model.Variante,
                                        idcolecao = model.IDColecao,
                                        nmcolecao = model.NMColecao,
                                        pagina = model.Pagina,
                                        pedidoreserva = model.PedidoReserva,
                                        idvariante = model.IDVariante,
                                        itempedidoreserva = model.ItemPedidoReserva,
                                        tipo = (int)model.Tipo,
                                        errMessage = "GRUPO COLEÇÃO NÃO INFORMADO PARA ESTA COLEÇÃO. VERIFICAR COM COMERCIAL."
                                    });
                                }                                                                    
                            }
                            else
                                model.Reduzido = -2; //reduzido criado por JOB posteriormente
                        }
                    }                 
                    CarregarTamanhosPadrao(model);

                    if (model.TamanhoPadrao.Count > 0)
                        model.IDTamanhoPadrao = model.TamanhoPadrao[0].VALOR_PADRAO;
                    else
                        ModelState.AddModelError("", "TAMANHO PADRÃO NÃO LOCALIZADO PARA O ARTIGO INFORMADO.");

                }

                this.CarregarTiposPedidos(model);
                this.ObterPrevisaoEntrega(model);

                model.DtItemSolicitada = model.DataEntregaItem.AddDays(int.Parse(ConfigurationManager.AppSettings["DIAS_DATA_SOLICITACAO"]));

                model.ObterTipoPedido = (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)? "S" : "N";
            }
            else
            {
                model.ObterTipoPedido = "N";                
            }

            if (ehreacabamento)
            {
                this.CarregarTiposPedidos(model);
                model.IDTipoPedido = (int)Enums.TiposPedido.BNFPROPRIO;
                this.ObterPrevisaoEntrega(model);
                model.DtItemSolicitada = model.DataEntregaItem.AddDays(int.Parse(ConfigurationManager.AppSettings["DIAS_DATA_SOLICITACAO"]));
                model.ObterTipoPedido = "N";
                model.UnidadeMedida = um;
            }

            ViewBag.POGReduzido = model.Reduzido;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserirNoCarrinho(InserirNoCarrinhoViewModel model)
        {
            bool hasErrors = false;
            ViewBag.POGReduzido = model.Reduzido;
            
            try
            {
                if (ModelState.IsValid)
                {
                    #region Validação de Qtdes Maximas e Minimas
                    if ((model.Tipo != Enums.ItemType.Reserva) && (model.IDTipoPedido != (int)Enums.TiposPedido.ESPECIAL))
                    {
                        decimal QtdeMaxima = 0;
                        decimal QtdeMinima = 0;
                        decimal QtdeConvertida = 0;
                        decimal Rendimento = 0;

                        if ((model.IDTipoPedido != (int)Enums.TiposPedido.AMOSTRA) && (model.IDTipoPedido != (int)Enums.TiposPedido.PILOTAGEM) && (model.IDTipoPedido != (int)Enums.TiposPedido.BNFPROPRIO))
                        {
                            model.Quantidade = model.IDTamanhoPadrao.GetValueOrDefault() * model.Pecas;
                            model.QuantidadeConvertida = model.Quantidade;

                            if(model.UnidadeMedida == "KG")
                            {
                                using (var ctx = new TIDalutexContext())
                                {
                                    QtdeConvertida = model.Quantidade;
                                    Rendimento = ctx.Database.SqlQuery<decimal>("select dalutex.Pega_Rendimento_Real(:p0, :p1) from dual", model.Artigo, model.Tecnologia).FirstOrDefault();
                                    QtdeConvertida = model.Quantidade * Rendimento;
                                    model.QuantidadeConvertida = QtdeConvertida;
                                }
                            }
                        }
                        
                        #region Validação dos campos qtde

                        if ((model.IDTipoPedido != (int)Enums.TiposPedido.AMOSTRA) && (model.IDTipoPedido != (int)Enums.TiposPedido.PILOTAGEM) && (model.IDTipoPedido != (int)Enums.TiposPedido.BNFPROPRIO))
                        {
                            if (model.IDTamanhoPadrao <= 0)
                            {
                                ModelState.AddModelError("", "TAMANHO PADRÃO NÃO SELECIONADO.");
                                hasErrors = true;
                            }
                        }
                        if (model.IDTipoPedido == 0 && model.Pecas <= 0)
                        {
                            ModelState.AddModelError("", "CAMPO \"PEÇAS\" NÃO PODE SER MENOR OU IGUAL A ZERO.");
                            hasErrors = true;
                        }
                        if (model.IDTipoPedido != 0 && model.Quantidade <= 0)
                        {
                            ModelState.AddModelError("", "CAMPO \"" + model.UnidadeMedida + "\" NÃO PODE SER MENOR OU IGUAL A ZERO.");
                            hasErrors = true;
                        }
                        if (model.Preco <= 0)
                        {
                            ModelState.AddModelError("", "CAMPO \"PREÇO\" NÃO PODE SER MENOR OU IGUAL A ZERO.");
                            hasErrors = true;
                        }
                        #endregion

                        if (model.IDTipoPedido != (int)Enums.TiposPedido.BNFPROPRIO)
                        {                                                         
                            using (var ctx = new TIDalutexContext())
                            {                               
                                REGRAS_QTD_PEDIDOX objMinMaxX = null;
                                //int IDColecao = int.Parse(model.IDColecao);
                                int IDColecao = 0;
                                try { IDColecao = int.Parse(model.IDColecao); } catch { IDColecao = 0; }
                                
                                VW_GRUPO_COL_RED objGrupoCol = null;

                                if (IDColecao > 0)
                                {
                                    objGrupoCol = ctx.VW_GRUPO_COL_RED.Where(m => m.ID_COL == IDColecao).FirstOrDefault();
                                }
                                else if (model.Reduzido > 0)
                                {
                                    objGrupoCol = ctx.VW_GRUPO_COL_RED.Where(m => m.REDUZIDO == model.Reduzido).FirstOrDefault();
                                }
                                else
                                {
                                    model.IDGrupoColecao = (int)Enums.GrupoColecoes.Colecao;
                                }


                                if (model.IDGrupoColecao == 0)
                                {
                                    if (model.IDColecao == ((int)Enums.TipoColecaoEspecial.Exclusivos).ToString())
                                        model.IDGrupoColecao = (int)Enums.GrupoColecoes.Exclusivos;
                                    else
                                        model.IDGrupoColecao = objGrupoCol.ID_GRUPO_COL;
                                }

                                #region Nova Regra de Validação Qtde
                                decimal? objDesenhoPronto = 0;
                                bool desenhoPronto = false;

                                if (model.Tecnologia != "L")
                                {
                                    try { objDesenhoPronto = ctx.VW_DESENHOS_POR_COLECAO.Where(x => x.DESENHO == model.Desenho).FirstOrDefault().DESENHO_PRONTO; }
                                    catch { objDesenhoPronto = null; }

                                    if (objDesenhoPronto != null)
                                    { desenhoPronto = (objDesenhoPronto == 1); }
                                }

                                //oda -- 17/05/2016 -- nova regra validação ------------
                                var qryQtdeMinMaxX =
                                            from Qtde in ctx.REGRAS_QTD_PEDIDOX
                                            where
                                                    Qtde.TECNOLOGIA_DESTINO == model.Tecnologia
                                                && Qtde.GRUPO_COLECAO == model.IDGrupoColecao
                                                && Qtde.DESENHO_PRONTO == desenhoPronto
                                                && Qtde.TIPO_PEDIDO == model.IDTipoPedido
                                                && Qtde.UM == model.UnidadeMedida
                                                && Qtde.EXCLUIDO == false
                                            select
                                                Qtde;

                                objMinMaxX = qryQtdeMinMaxX.FirstOrDefault();

                                if (objMinMaxX != null)
                                {
                                    QtdeMaxima = objMinMaxX.QTD_MAX_VAR;
                                    QtdeMinima = objMinMaxX.QTD_MIN_VAR;

                                    if (model.Quantidade < QtdeMinima)
                                    {
                                        ModelState.AddModelError("", "A QUANTIDADE MÍNIMA POR VARIANTE NÃO PODE SER MENOR QUE: " + QtdeMinima.ToString());
                                        hasErrors = true;
                                    }
                                    else if (model.Quantidade > QtdeMaxima)
                                    {
                                        ModelState.AddModelError("", "A QUANTIDADE MÁXIMA POR VARIANTE NÃO PODE SER MAIOR QUE: " + QtdeMaxima.ToString());
                                        hasErrors = true;
                                    }
                                }
                                else
                                {   //se não achou a qtde na regra, verifica se a UM é KG pra tentar achar em MT, se não..... num deu!
                                    if (model.UnidadeMedida == "KG")
                                    {
                                        var qryQtdeMinMaxMT =
                                            from Qtde in ctx.REGRAS_QTD_PEDIDOX
                                            where
                                                    Qtde.TECNOLOGIA_DESTINO == model.Tecnologia
                                                && Qtde.GRUPO_COLECAO == model.IDGrupoColecao
                                                && Qtde.DESENHO_PRONTO == desenhoPronto
                                                && Qtde.TIPO_PEDIDO == model.IDTipoPedido
                                                && Qtde.UM == "MT"
                                                && Qtde.EXCLUIDO == false
                                            select
                                                Qtde;

                                        objMinMaxX = qryQtdeMinMaxMT.FirstOrDefault();

                                        if (objMinMaxX != null)
                                        {
                                            QtdeMaxima = objMinMaxX.QTD_MAX_VAR;
                                            QtdeMinima = objMinMaxX.QTD_MIN_VAR;

                                            if (model.QuantidadeConvertida < QtdeMinima)
                                            {
                                                ModelState.AddModelError("", "A QUANTIDADE MÍNIMA POR VARIANTE NÃO PODE SER MENOR QUE: " + QtdeMinima.ToString()
                                                    + " | MTs. ARTIGO: " + model.Artigo +
                                                        " | RENDIMENTO: " + Rendimento.ToString() +
                                                        " | TOTAL CONVERTIDO: " + QtdeConvertida.ToString() +
                                                        " MTs");
                                                hasErrors = true;
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "QUANTIDADES MÍNIMAS E MÁXIMAS POR VARIANTE NÃO CADASTRADO PARA ESTE ITEM. ENTRAR EM CONTATO COM DEPTO. COMERCIAL: " +
                                                " | Tecnologia: " + model.Tecnologia.ToString() +
                                                " | IDGrupoColecao: " + model.IDGrupoColecao +
                                                " | desenhoPronto: " + desenhoPronto.ToString() +
                                                " | IDTipoPedido: " + model.IDTipoPedido +
                                                " | UnidadeMedida: " + model.UnidadeMedida +
                                                " | Excluido: false" +
                                                " | [Regra para item em convertido em MT]"
                                                );
                                            hasErrors = true;
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "QUANTIDADES MÍNIMAS E MÁXIMAS POR VARIANTE NÃO CADASTRADO PARA ESTE ITEM. ENTRAR EM CONTATO COM DEPTO. COMERCIAL: " +
                                            " Tecnologia: " + model.Tecnologia.ToString() +
                                                " | IDGrupoColecao: " + model.IDGrupoColecao +
                                                " | desenhoPronto: " + desenhoPronto.ToString() +
                                                " | IDTipoPedido: " + model.IDTipoPedido +
                                                " | UnidadeMedida: " + model.UnidadeMedida +
                                                " | Excluido: false" +
                                                " |  [Não encontrou na regra]"
                                                );
                                        hasErrors = true;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion

                    if (hasErrors)
                    {
                        if (model.Tipo != Enums.ItemType.Reserva)
                        {
                            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                            {
                                model.ObterTipoPedido = "S";
                                this.CarregarTiposPedidos(model);
                            }
                        } else{model.ObterTipoPedido = "N";}

                        CarregarTamanhosPadrao(model);
                        ObterPrevisaoEntrega(model);
                        model.NumeroMaximoDiasDataSolicitacao = int.Parse(ConfigurationManager.AppSettings["DIAS_DATA_SOLICITACAO"]);

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

                    if (model.IDTipoPedido >= default(int))
                        base.Session_Carrinho.IDTipoPedido = model.IDTipoPedido;

                    if (model.IDTipoPedido != (int)Enums.TiposPedido.VENDA)                        
                        model.Pecas = 1;

                    model.ValorTotalItem = model.Quantidade * model.Preco;


                    if ((model.Tipo != Enums.ItemType.Reserva) && (model.Tipo != Enums.ItemType.Reacabamento))
                    {
                        //adicionar valor para "Farol" ------------ oda -- 05/08/2015 -----------
                        using (var ctx = new TIDalutexContext())
                        {
                            VW_FAROL objFarol = ctx.VW_FAROL.Where(x => x.ARTIGO == model.Artigo && x.DESENHO == model.Desenho && x.VARIANTE == model.Variante).FirstOrDefault();

                            if (objFarol != null)
                                model.Farol = objFarol.FAROL;
                            else
                                model.Farol = 0;
                        }
                    }

                    ObterPrevisaoEntrega(model);

                    if (model.IDTipoPedido != (int)Enums.TiposPedido.PILOTAGEM && model.IDTipoPedido != (int)Enums.TiposPedido.AMOSTRA)
                    {
                        if (model.DtItemSolicitada < model.DataEntregaItem)
                        {
                            model.DtItemSolicitada = model.DataEntregaItem;
                        }
                        if (model.DtItemSolicitada < model.DataEntregaItem || model.DataEntregaItem.AddDays(int.Parse(ConfigurationManager.AppSettings["DIAS_DATA_SOLICITACAO"])) < model.DtItemSolicitada)
                        {
                            ModelState.AddModelError("", "Data de entrega solicitada inválida.");
                            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                            {
                                model.ObterTipoPedido = "S";
                                this.CarregarTiposPedidos(model);
                            }
                            this.CarregarTamanhosPadrao(model);

                            return View(model);
                        }
                    }

                    if (model.Modo == "I")//Inclusão
                    {
                        if (base.Session_Carrinho.Itens.Count == 0)
                            model.NumeroSequencial = 0;
                        else
                            model.NumeroSequencial = base.Session_Carrinho.Itens.Max(x => x.NumeroSequencial) + 1;

                        base.Session_Carrinho.Itens.Add(model);

                        if (model.IDTipoPedido == (int)Enums.TiposPedido.BNFPROPRIO)                  //totalpaginas 
                            return RedirectToAction("Reacabamento", "Pedido");//, new {pagina = model.Pagina});
                        else
                        if (model.Tipo == Enums.ItemType.Estampado)// || model.Tipo != Enums.ItemType.ValidacaoReserva)
                        {
                            return RedirectToAction("Desenhos", "Pedido",
                                new
                                {
                                    idcolecao = model.NMColecao, 
                                    nmcolecao = model.NMColecao, 
                                    filtro = model.Desenho,
                                    pagina = "1", 
                                    totalpaginas = "1"
                                });
                        }
                        else if (model.Tipo == Enums.ItemType.ValidacaoReserva)
                        {
                            return RedirectToAction("DesenhosValidaReserva", "Pedido", new{pedidoreserva = model.PedidoReserva});                           
                        }
                        else if (model.Tipo == Enums.ItemType.Liso)
                            return RedirectToAction("Lisos", "Pedido", new { idcolecao = model.IDColecao, nmcolecao = model.NMColecao, pagina = model.Pagina });
                        else if (model.Tipo == Enums.ItemType.Reserva)
                            return RedirectToAction("ItensParaReserva", "Pedido", new { pagina = model.Pagina });                                                
                        else
                            return RedirectToAction("Index", "Home");                      
                    }
                    else
                    {
                        int index = base.Session_Carrinho.Itens.FindIndex(x => x.Equals(model));
                        if (index < 0)
                        {
                            ModelState.AddModelError("", "Este item não foi encontrado no carrinho para alteração.");
                            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                            {
                                model.ObterTipoPedido = "S";
                                this.CarregarTiposPedidos(model);
                            }
                            this.CarregarTamanhosPadrao(model);
                            return View(model);
                        }

                        base.Session_Carrinho.Itens[index] = model;
                        return RedirectToAction("Carrinho", "Pedido");
                    }
                }
                else
                {
                    if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                    {
                        model.ObterTipoPedido = "S";
                        this.CarregarTiposPedidos(model);
                    }
                    CarregarTamanhosPadrao(model);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            
            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
            {
                model.ObterTipoPedido = "S";
                this.CarregarTiposPedidos(model);
            }
            return View(model);
        }

        private void CarregarTamanhosPadrao(InserirNoCarrinhoViewModel model)
        {
            using (var ctxTI = new TIDalutexContext())
            {
                if (model.Reduzido > -2) //se tem reduzido
                {
                    int _pi = 0;

                    VW_PI_RED objvw_pi_red = null;

                    objvw_pi_red = ctxTI.VW_PI_RED.Where(x => x.REDUZIDO == model.Reduzido).FirstOrDefault();

                    if (objvw_pi_red == null)
                    {
                        //ModelState.AddModelError("", "PI NÃO CADSATRADO PARA O REDUZIDO. FAVOR VERIFICAR COM A ENGENHARIA DE PRODUTOS.");
                    }
                    else
                    {
                        //model.UnidadeMedida = ctxTI.VW_PI_RED.Where(x => x.REDUZIDO == model.Reduzido).FirstOrDefault().UM; 
                        model.UnidadeMedida = objvw_pi_red.UM;

                        // pega o PI do cadastro do SGT para o reduzido.
                        _pi = ctxTI.VW_PI_RED.Where(x => x.REDUZIDO == model.Reduzido).FirstOrDefault().PI;

                        //Tenta pegar agora a combinação do reduzido + PI na tabela de padrões
                        model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.REDUZIDO == model.Reduzido && x.ID_PROCESSO == _pi).OrderByDescending(x => x.PADRAO).ToList();

                        //Se não encontrou, tenta encontar apenas pelo reduzido
                        if (model.TamanhoPadrao == null || model.TamanhoPadrao.Count() == 0)
                        {
                            model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.REDUZIDO == model.Reduzido &&
                                                                                x.ID_PROCESSO == null).OrderByDescending(x => x.PADRAO).ToList();
                        }

                        //Não encontrou na tabela, tentar encontar sem o reduzido - [Tipo prod, Tec, Artigo e PI]
                        if (model.TamanhoPadrao == null || model.TamanhoPadrao.Count() == 0)
                        {
                            model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.TIPO_PRODUTO == "A" &&
                                                                                x.ARTIGO == model.Artigo &&
                                                                                x.TECNOLOGIA == model.Tecnologia &&
                                                                                x.ID_PROCESSO == _pi
                                                                        ).OrderByDescending(x => x.PADRAO).ToList();
                        }
                    }
                }
                if ((model.Reduzido == -2) || (model.TamanhoPadrao == null) || (model.TamanhoPadrao.Count() == 0))
                {
                    //Verifica se pegou a UM, se não procura a Unoida de Medida do itens estoque-----
                    if (model.UnidadeMedida == null)
                    {
                        VMASCARAPRODUTOACABADO objValorPadraoView = null;

                        using (var ctx = new DalutexContext())
                        {
                            objValorPadraoView = ctx.VMASCARAPRODUTOACABADO.Where(x => x.ARTIGO == model.Artigo).FirstOrDefault();
                            if (objValorPadraoView != null)
                            {
                                model.UnidadeMedida = objValorPadraoView.UM;
                                if (model.UnidadeMedida.ToUpper() == "KG")
                                {
                                    model.ValorPadrao = (decimal)Enums.ValorPadraoUnidade.Quilo;
                                }
                                else if (model.UnidadeMedida.ToUpper() == "MT")
                                {
                                    model.ValorPadrao = (decimal)Enums.ValorPadraoUnidade.Metro;
                                }
                                else
                                {
                                    ModelState.AddModelError("", "UNIDADE DE MEDIDA INVÁLIDA.");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "UNIDADE DE MEDIDA NÃO ENCONTRADA.");
                            }
                        }
                    }

                    //Se Não encontrou na tabela ainda, tentar encontar sem o reduzido na combinação - [Tipo prod, Tecnologia, Artigo e UM]
                    if (model.TamanhoPadrao == null || model.TamanhoPadrao.Count() == 0)
                    {
                        model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.TIPO_PRODUTO == "A" &&
                                                                            x.ARTIGO == model.Artigo &&
                                                                            x.TECNOLOGIA == model.Tecnologia &&
                                                                            x.UM == model.UnidadeMedida
                                                                    ).OrderByDescending(x => x.PADRAO).ToList();
                    }

                    //Se Não encontrou na tabela ainda, tentar encontar sem o reduzido na combinação - [Tipo prod, Tecnologia e UM]
                    if (model.TamanhoPadrao == null || model.TamanhoPadrao.Count() == 0)
                    {
                        model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.TIPO_PRODUTO == "A" &&
                                                                            x.TECNOLOGIA == model.Tecnologia &&
                                                                            x.UM == model.UnidadeMedida &&
                                                                            x.ARTIGO == null
                                                                    ).OrderByDescending(x => x.PADRAO).ToList();
                    }

                    //Se Não encontrou na tabela ainda, tentar encontar sem o reduzido na combinação - [Tipo prod, UM]
                    if (model.TamanhoPadrao == null || model.TamanhoPadrao.Count() == 0)
                    {
                        model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.TIPO_PRODUTO == "A" &&
                                                                            x.UM == model.UnidadeMedida &&
                                                                            x.ARTIGO == null
                                                                      ).OrderByDescending(x => x.PADRAO).ToList();
                    }

                    //Se Não encontrou AINDA na tabela, PEGA O PRIMEIRO VALOR APENAS DO TIPO DE PRODUTO e mesma UM
                    if (model.TamanhoPadrao == null || model.TamanhoPadrao.Count() == 0)
                    {
                        model.TamanhoPadrao = ctxTI.REGRA_PADRAO.Where(x => x.STATUS == true && x.TIPO_PRODUTO == "A" &&
                                                                            x.UM == model.UnidadeMedida
                                                                       ).OrderByDescending(x => x.PADRAO).ToList();
                    }
                }
            }
        }

        private void ObterPrevisaoEntrega(InserirNoCarrinhoViewModel model)
        {
            if ((model.Tipo != Enums.ItemType.Reserva) && (model.Tipo != Enums.ItemType.Reacabamento))
            {
                model.NumeroMaximoDiasDataSolicitacao = int.Parse(ConfigurationManager.AppSettings["DIAS_DATA_SOLICITACAO"]);

                if (base.Session_Carrinho != null && (base.Session_Carrinho.IDTipoPedido == 6 || base.Session_Carrinho.IDTipoPedido == 7))
                {
                    base.Session_Carrinho.DataEntrega = DateTime.Today.AddDays(10);

                    model.DtItemSolicitada = DateTime.Today.AddDays(10);
                    model.DataEntregaItem = model.DtItemSolicitada;
                    return;
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
                        model.DataEntregaItem = DateTime.Today.AddDays(20); 
                    //ALTERAÇÃO NO NUMERO DE DIAS POR SOLICITAÇÃO DO JUNIOR EM 27/07/2016 15:00 HRS.
                    //A PEDIDO DO SR. LUDOVIT PARA VERIFICAR O MOTIVO DO PEDIDO DA IONA TER SIDO LIBERADO COM DISPONIBILIDADE DE 45 DIAS - QUE ERA COMO ESTAVA ATE ESTE DIA.
                    //PEDIDO DA IONA: 223258

                    if (base.Session_Carrinho != null && base.Session_Carrinho.DataEntrega < model.DataEntregaItem)
                        base.Session_Carrinho.DataEntrega = model.DataEntregaItem;
                }
            }
        }

        [HttpPost]
        public ActionResult ExcluirItemCarrinho(InserirNoCarrinhoViewModel model)
        {
            if (base.Session_Carrinho != null && base.Session_Carrinho.Itens != null)
            {
                InserirNoCarrinhoViewModel objExcluir = base.Session_Carrinho.Itens.First(i => i.Equals(model));
                if (!objExcluir.PreExistente)
                {
                    if (base.Session_Carrinho.Itens.Remove(objExcluir))
                    {
                        if (base.Session_Carrinho.Itens.Where(i => !i.Excluir).ToList().Count == default(int))
                        {
                            base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();
                            base.Session_Carrinho.IDTipoPedido = -1;
                        }
                        return RedirectToAction("Carrinho");
                    }
                    else
                    {
                        return RedirectToAction("Error", new { message = "Este item não foi encontrado no carrinho para excluir.", title = "EXCLUSÃO DO CARRINHO" });
                    }
                }
                else
                {
                    objExcluir.Excluir = true;
                    if (base.Session_Carrinho.Itens.Where(i => !i.Excluir).ToList().Count == default(int))
                    {
                        base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();
                        base.Session_Carrinho.IDTipoPedido = -1;
                    }
                    return RedirectToAction("Carrinho");
                }
            }
            else
            {
                return RedirectToAction("Error", new { message = "Não há itens no carrinho para excluir.", title = "EXCLUSÃO DO CARRINHO" });
            }
        }

        public ActionResult Carrinho(string errorMsg)
        {            
            if (base.Session_Carrinho == null)
            {
                return View();
            }

            ViewBag.Carrinho = base.Session_Carrinho;
            ViewBag.UrlDesenhos = ConfigurationManager.AppSettings["PASTA_DESENHOS"];
            ViewBag.UrlReservas = ConfigurationManager.AppSettings["PASTA_RESERVAS"];
        
            foreach (var item in base.Session_Carrinho.Itens) //oda -- 08/11/2016 - ver com cassiano ----
            {
                if (item.Tipo == Enums.ItemType.ProntaEntrega)
                {
                    ViewBag.TipoPedido = "PE";
                    break;
                }
            }
            
            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                ModelState.AddModelError("", errorMsg);                
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public void AtualizaCompose(string compose, string sequencial)
        {
            int iSequencial = int.Parse(sequencial);
            InserirNoCarrinhoViewModel objAtualizar = base.Session_Carrinho.Itens.Where(x => x.NumeroSequencial == iSequencial).First();
            objAtualizar.Compose = int.Parse(compose);
        }
      
        public ActionResult AcertaCompose()
        {
            foreach (var item in base.Session_Carrinho.Itens)
            {
                item.Compose = 0; 
            }
            return RedirectToAction("Carrinho", "Pedido");
        }


        public ActionResult EsvaziarCarrinho()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EsvaziarCarrinho(object model)
        {
            base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();
            base.Session_Carrinho.IDTipoPedido = -1;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LimparPedido()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LimparPedido(object model)
        {
            base.Session_Carrinho = new ConclusaoPedidoViewModel();
            base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();
            return RedirectToAction("Index", "Home");
        }
        private ConclusaoPedidoViewModel ConclusaoPedidoCarregarEscolhas(ConclusaoPedidoViewModel model)
        {
            model.Representante = base.Session_Carrinho.Representante;
            model.ClienteFatura = base.Session_Carrinho.ClienteFatura;
            model.ClienteEntrega = base.Session_Carrinho.ClienteEntrega;
            model.Transportadora = base.Session_Carrinho.Transportadora;
            model.QualidadeComercial = base.Session_Carrinho.QualidadeComercial;
            model.Moeda = base.Session_Carrinho.Moeda;
            model.CondicaoPagto = base.Session_Carrinho.CondicaoPagto;
            model.CanailVenda = base.Session_Carrinho.CanailVenda;
            model.ViaTransporte = base.Session_Carrinho.ViaTransporte;
            model.Frete = base.Session_Carrinho.Frete;
            model.TipoAtendimento = base.Session_Carrinho.TipoAtendimento;

            return model;
        }

        public ActionResult ConclusaoPedido()
        {
            ConclusaoPedidoViewModel model = new ConclusaoPedidoViewModel();
           
            if (Session_Carrinho != null)
            {
                model.IDTipoPedido = base.Session_Carrinho.IDTipoPedido;
                model.Observacoes = base.Session_Carrinho.Observacoes;
                model.PedidoCliente = base.Session_Carrinho.PedidoCliente;
                if (base.Session_Carrinho.Itens != null)
                {
                    decimal totalPedido = 0;
                    base.Session_Carrinho.Itens.ForEach(delegate(InserirNoCarrinhoViewModel item)
                    {
                        if (!item.Excluir)
                        {
                            totalPedido += item.ValorTotalItem;
                        }
                    });
                    model.TotalPedido = base.Session_Carrinho.TotalPedido = totalPedido;
                }

                using (var ctx = new DalutexContext())
                {
                    model.DescTipoPedido = ctx.COML_TIPOSPEDIDOS.Find(model.IDTipoPedido).DESCRICAO.Trim();
                }

                ConclusaoPedidoCarregarEscolhas(model);
                ViewBag.CarrinhoVazio = false;
            }
            else
            {
                ViewBag.CarrinhoVazio = true;
            }


            //oda-- 03/12/2015 --- acertar a data dos itens pelo tipo de atendimento --------------------------- 
            if (model.IDTipoPedido != (int)Enums.TiposPedido.RESERVA)
            {
                if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.EstampaCompleta))
                {
                    List<KeyValuePair<string, DateTime>> lstConsolidada = base.Session_Carrinho.Itens
                                        .GroupBy(g => g.Desenho)
                                        .Select(consolidado => new KeyValuePair<string, DateTime>(consolidado.First().Desenho, consolidado.Max(s => s.DtItemSolicitada)))
                                        .ToList();

                    foreach (KeyValuePair<string, DateTime> item in lstConsolidada)
                    {
                        foreach (InserirNoCarrinhoViewModel it in Session_Carrinho.Itens)
                        {
                            if (it.Desenho == item.Key)
                            {
                                it.DtItemSolicitada = item.Value;
                            }
                        }
                    }
                }
                else if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.PedidoCompleto))
                {
                    List<KeyValuePair<string, DateTime>> lstConsolidada = base.Session_Carrinho.Itens
                         .GroupBy(g => g.Artigo).Select(consolidado => new KeyValuePair<string, DateTime>(consolidado.First().Artigo, consolidado.Max(s => s.DtItemSolicitada))).ToList();

                    foreach (KeyValuePair<string, DateTime> item in lstConsolidada)
                    {
                        foreach (InserirNoCarrinhoViewModel it in Session_Carrinho.Itens)
                        {
                            it.DtItemSolicitada = item.Value;
                        }
                    }
                }
                else if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.CompletoPorArtigo))
                {
                    List<KeyValuePair<string, DateTime>> lstConsolidada = base.Session_Carrinho.Itens
                         .GroupBy(g => g.Artigo).Select(consolidado => new KeyValuePair<string, DateTime>(consolidado.First().Artigo, consolidado.Max(s => s.DtItemSolicitada))).ToList();

                    foreach (KeyValuePair<string, DateTime> item in lstConsolidada)
                    {
                        foreach (InserirNoCarrinhoViewModel it in Session_Carrinho.Itens)
                        {
                            if (it.Artigo == item.Key)
                            {
                                it.DtItemSolicitada = item.Value;
                            }
                        }
                    }
                }
                else if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.ArtigoCompose))
                {
                    List<KeyValuePair<int, DateTime>> lstConsolidada = base.Session_Carrinho.Itens
                         .GroupBy(g => g.Compose).Select(consolidado => new KeyValuePair<int, DateTime>(consolidado.First().Compose, consolidado.Max(s => s.DtItemSolicitada))).ToList();

                    foreach (KeyValuePair<int, DateTime> item in lstConsolidada)
                    {
                        foreach (InserirNoCarrinhoViewModel it in Session_Carrinho.Itens)
                        {
                            if (it.Compose == item.Key)
                            {
                                it.DtItemSolicitada = item.Value;
                            }
                        }
                    }
                }

            } 
            return View(model);
        }

        private void RecarregarPedido(decimal NumeroPedidoBloco, bool ComItens = true)
        {
            using(var ctx = new DalutexContext()){
                using(var ctxTI = new TIDalutexContext()){
                    PRE_PEDIDO objPrePedidoSalvo = ctxTI.PRE_PEDIDO.Where(x => x.NUMERO_PEDIDO_BLOCO == NumeroPedidoBloco).First();

                    base.Session_Carrinho = new ConclusaoPedidoViewModel(){
                        ID = (int)objPrePedidoSalvo.NUMERO_PEDIDO_BLOCO,
                        IDTipoPedido = (int)objPrePedidoSalvo.TIPO_PEDIDO.GetValueOrDefault(),
                        Representante = ctx.REPRESENTANTES.Find((int)objPrePedidoSalvo.ID_REPRESENTANTE),
                        ClienteFatura = ctxTI.VW_CLIENTE_TRANSP.Where(c => c.ID_CLIENTE == (int)objPrePedidoSalvo.ID_CLIENTE).First(),
                        QualidadeComercial = new KeyValuePair<string,string>(objPrePedidoSalvo.QUALIDADE_COM,objPrePedidoSalvo.QUALIDADE_COM),
                        CondicaoPagto = ctxTI.VW_CONDICAO_PGTO.Find((int)objPrePedidoSalvo.COD_COND_PGTO),
                        Observacoes = objPrePedidoSalvo.OBSERVACOES,
                        DataEntrega = objPrePedidoSalvo.DATA_ENTREGA.GetValueOrDefault(),
                        ClienteEntrega = ctxTI.VW_CLIENTE_TRANSP.Where(c => c.ID_CLIENTE == (int)objPrePedidoSalvo.ID_CLIENTE_ENTREGA).First(),
                        Transportadora = ctx.TRANSPORTADORAS.Find((int)objPrePedidoSalvo.ID_TRANSPORTADORA),
                        IDLocaisVenda = (int)objPrePedidoSalvo.ID_LOCAL.GetValueOrDefault(),
                        Moeda = ctx.CADASTRO_MOEDAS.Find((int)objPrePedidoSalvo.COD_MOEDA),
                        CanailVenda = ctx.CANAIS_VENDA.Find((short)objPrePedidoSalvo.CANAL_VENDAS),
                        TipoAtendimento = ctxTI.PRE_PEDIDO_ATEND.Find(objPrePedidoSalvo.ATENDIMENTO),
                        Frete = ctx.COML_TIPOSFRETE.Find((int)objPrePedidoSalvo.TIPOFRETE),
                        ViaTransporte = ctx.COML_VIASTRANSPORTE.Find((int)objPrePedidoSalvo.VIATRANSPORTE),
                        PorcentagemComissao = objPrePedidoSalvo.COMISSAO.GetValueOrDefault(),
                        PedidoCliente = (int)objPrePedidoSalvo.PEDIDO_CLIENTE.GetValueOrDefault(),
                        StatusPedido = (int)objPrePedidoSalvo.STATUS_PEDIDO.GetValueOrDefault()
                    };

                    base.Session_Carrinho.Itens = new List<InserirNoCarrinhoViewModel>();

                    if (ComItens)
                    {
                        List<PRE_PEDIDO_ITENS> lstItens = ctxTI.PRE_PEDIDO_ITENS.Where(i => i.NUMERO_PEDIDO_BLOCO == NumeroPedidoBloco).ToList();

                        foreach (var item in lstItens)
                        {
                            string vColecao = ctxTI.VW_GRUPO_COL_RED.Where(x => x.REDUZIDO == item.REDUZIDO_ITEM).FirstOrDefault().COLECAO;

                            base.Session_Carrinho.Itens.Add(new InserirNoCarrinhoViewModel()
                            {
                                ID = item.ITEM,
                                PreExistente = true,
                                Artigo = item.ARTIGO,
                                DataEntregaItem = item.DATA_ENTREGA.GetValueOrDefault(),
                                Desenho = item.DESENHO,
                                TecnologiaPorExtenso = item.LISO_ESTAMP,
                                Preco = item.PRECO_UNIT.GetValueOrDefault(),
                                PrecoTabela = item.PRECOLISTA,
                                Pecas = (int)item.QTDEPC,
                                Quantidade = item.QUANTIDADE.GetValueOrDefault(),
                                Reduzido = (int)item.REDUZIDO_ITEM,
                                UnidadeMedida = item.UM,
                                Variante = item.VARIANTE,
                                Compose = (int)item.COD_COMPOSE,
                                DtItemSolicitada = item.DATA_ENTREGA_DIGI.GetValueOrDefault(),
                                ValorTotalItem = item.QUANTIDADE.GetValueOrDefault() * item.PRECO_UNIT.GetValueOrDefault(),
                                
                                NumeroSequencial = item.ITEM,//oda-- 03/11/2016 -- erro na edição do item ao setar compose ...
                                Cor = item.COR,

                                Colecao = vColecao,                                 
                                                                
                                Tipo = (
                                    objPrePedidoSalvo.CANAL_VENDAS == (int)Enums.CanaisVenda.TELEVENDAS
                                        && item.PE == "S") ? Enums.ItemType.ProntaEntrega : (
                                            ctxTI.PED_RESERVA_VENDA.Where(r => r.PEDIDO_VENDA == NumeroPedidoBloco).FirstOrDefault() != null ? Enums.ItemType.ValidacaoReserva : (
                                                objPrePedidoSalvo.TIPO_PEDIDO == (int)Enums.TiposPedido.RESERVA ? Enums.ItemType.Reserva : (
                                                    item.LISO_ESTAMP == "L" ? Enums.ItemType.Liso : Enums.ItemType.Estampado
                                            )
                                        )
                                )
                            });
                        }
                    }
                }
            }
        }

        public JsonResult ObterItensCarrinho()
        {
            return Json(base.Session_Carrinho.Itens.Where(i => !i.Excluir).OrderBy(x => x.Compose), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConclusaoPedido(ConclusaoPedidoViewModel model)
        {
            using (var ctx = new DalutexContext())
            {
                model.DescTipoPedido = ctx.COML_TIPOSPEDIDOS.Find(model.IDTipoPedido).DESCRICAO.Trim();
            }

            if (base.Session_Carrinho.DescTipoPedido == null) { base.Session_Carrinho.DescTipoPedido = model.DescTipoPedido; }
            try
            {
                ConclusaoPedidoCarregarEscolhas(model);

                // oda -- 10/09/2016 -- validação do campo nº NFRefat e PedidoRefat para pedido do tipo Refaturamento (ESPECIAL);
                if (model.IDTipoPedido == (int)Enums.TiposPedido.ESPECIAL)
                {
                    if (((model.NFRefat == null) || (String.IsNullOrEmpty(model.NFRefat))) || ((model.PedidoRefat == null) || (String.IsNullOrEmpty(model.PedidoRefat))))
                    {
                        ModelState.AddModelError("", "Para este tipo de pedido os Campos [NF Refat] e [Pedido Refat] devem ser informados.");
                        return View(model);
                    }
                    else
                    {
                        int ipedido = int.Parse(model.PedidoRefat);
                        int inf = int.Parse(model.NFRefat);

                        int cliente = base.Session_Carrinho.ClienteFatura.ID_CLIENTE;

                        using (TIDalutexContext ctx = new TIDalutexContext())
                        {
                            VW_NF_PEDIDO_CLIENTE objresult = ctx.VW_NF_PEDIDO_CLIENTE.Where(x => x.ID_CLIENTE == cliente && x.PEDIDO == ipedido && x.NF == inf).OrderBy(x => x.NF).FirstOrDefault();

                            if (objresult == null)
                            {
                                ModelState.AddModelError("", "Campos [NF Refat] e [Pedido Refat] informados não são referentes ao cliente selecionado.");
                                return View(model);
                            }
                        }
                    }
                }

                if (model.IDTipoPedido == (int)Enums.TiposPedido.RESERVA)
                {
                    model.QualidadeComercial = new KeyValuePair<string,string>(Enums.QualidadeComercial.A.ToString(), Enums.QualidadeComercial.A.ToString());
                    model.CondicaoPagto = new VW_CONDICAO_PGTO() { ID_COND = (int)Enums.CondicoesPagamento.CORTESIA };                    
                }

                if (Session_Carrinho == null)
                {
                    ViewBag.CarrinhoVazio = true;
                    return View(model);
                }
                else
                {
                    ViewBag.CarrinhoVazio = false;
                }

                if (ModelState.IsValid)
                {
                    #region Validações                    
                    if (Session_Carrinho.Itens == null || Session_Carrinho.Itens.Count == 0)
                    {
                        ModelState.AddModelError("", "Não é permitido concluir o pedido sem itens.");
                        return View(model);
                    }

                    foreach (InserirNoCarrinhoViewModel item in Session_Carrinho.Itens)
                    {
                        model.TotalPedido += item.ValorTotalItem;
                    }
                    
                    if (model.IDTipoPedido != (int)Enums.TiposPedido.RESERVA && Session_Carrinho.Itens.Exists(x => x.Reduzido == 0))
                    {
                        ModelState.AddModelError("", "Este carrinho possuem itens com o código reduzido zerado. Favor entrar em contato com o TI.");
                        return View(model);
                    }

                    bool hasErrors = false;

                    if ((model.IDTipoPedido != (int)Enums.TiposPedido.RESERVA) && (model.IDTipoPedido != (int)Enums.TiposPedido.ESPECIAL))
                    {                        
                        if (hasErrors)
                        {
                            return View(model);
                        }

                        if (base.Session_Carrinho.IDTipoPedido.Equals((int)Enums.TiposPedido.VENDA)
                            && !model.CanailVenda.CANAL_VENDA.Equals((int)Enums.CanaisVenda.TELEVENDAS)
                            && !model.CondicaoPagto.ID_COND.Equals((int)Enums.CondicoesPagamento.CORTESIA))
                        {

                            int numParcelas = 0;
                            using (var ctx = new TIDalutexContext())
                            {
                                numParcelas = ctx.VW_CONDICAO_PGTO.Find(model.CondicaoPagto.ID_COND).PARCELAS;
                            }

                            int fatorMultiplicacao = 0;
                            decimal valorMinimoParcelas = decimal.Parse(ConfigurationManager.AppSettings["VALOR_PARCELA_MINIMA"]);

                            if (model.QualidadeComercial.Key == Enums.QualidadeComercial.A.ToString())
                            {
                                fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.A;
                            }
                            else if (model.QualidadeComercial.Key == Enums.QualidadeComercial.B.ToString())
                            {
                                fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.B;
                            }
                            else if (model.QualidadeComercial.Key == Enums.QualidadeComercial.C.ToString())
                            {
                                fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.C;
                            }

                            if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.EstampaCompleta))
                            {
                                List<KeyValuePair<string, decimal>> lstConsolidada = base.Session_Carrinho.Itens
                                    .GroupBy(g => g.Desenho)
                                    .Select(consolidado => new KeyValuePair<string, decimal>(consolidado.First().Desenho, consolidado.Sum(s => s.ValorTotalItem)))
                                    .ToList();

                                bool isValid = true;

                                foreach (KeyValuePair<string, decimal> item in lstConsolidada)
                                {
                                    if (((item.Value / fatorMultiplicacao) / numParcelas) < valorMinimoParcelas)
                                    {
                                        ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C") + " para o desenho: " + item.Key);
                                        isValid = false;
                                    }
                                }

                                if (!isValid)
                                {
                                    hasErrors = true;
                                }
                            }
                            else if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.PedidoCompleto))
                            {
                                if (((model.TotalPedido / fatorMultiplicacao) / numParcelas) < valorMinimoParcelas)
                                {
                                    ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C"));
                                    hasErrors = true;
                                }
                            }
                            else if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.CompletoPorArtigo))
                            {
                                List<KeyValuePair<string, decimal>> lstConsolidada = base.Session_Carrinho.Itens
                                    .GroupBy(g => g.Artigo)
                                    .Select(consolidado => new KeyValuePair<string, decimal>(consolidado.First().Artigo, consolidado.Sum(s => s.ValorTotalItem)))
                                    .ToList();

                                bool isValid = true;

                                foreach (KeyValuePair<string, decimal> item in lstConsolidada)
                                {
                                    if (((item.Value / fatorMultiplicacao) / numParcelas) < valorMinimoParcelas)
                                    {
                                        ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C") + " para o artigo: " + item.Key);
                                        isValid = false;
                                    }
                                }

                                if (!isValid)
                                {
                                    hasErrors = true;
                                }
                            }
                            else if (model.TipoAtendimento.COD_ATEND.Equals((int)Enums.TiposAtendimento.PedidoIncompleto))
                            {
                                bool isValid = true;

                                foreach (InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                                {
                                    if (((item.ValorTotalItem / fatorMultiplicacao) / numParcelas) < valorMinimoParcelas)
                                    {
                                        ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C") + " para o item: Desenho=" + item.Desenho + " Variante=" + item.Variante);
                                        isValid = false;
                                    }
                                }

                                if (!isValid)
                                {
                                    hasErrors = true;
                                }
                            }

                        }
                        //-- oda -- 08/11/2016 --- se o tipo de pedido - no caso CANAL DE VENDAS, for diferente de televendas -- pedidos feitos na loja PE -------------
                        if (!model.CanailVenda.CANAL_VENDA.Equals((int)Enums.CanaisVenda.TELEVENDAS)) //televendas PE ----
                        {
                            // -- oda -- 05/11/2015 --- regra de validação de qtde por desenho ------------------------------------------------------------------------------------------------------------------
                            using (var ctxTI = new TIDalutexContext())
                            {
                                if (model.IDTipoPedido == (int)Enums.TiposPedido.VENDA)
                                {
                                    foreach (InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                                    {
                                        if (item.Desenho == null)
                                        {
                                            item.Desenho = "0000";
                                        }
                                    }

                                    List<KeyValuePair<string, decimal>> lstGrupoDesenho = base.Session_Carrinho.Itens
                                        .GroupBy(g => g.Desenho + g.Tecnologia.Substring(0, 1) + g.IDGrupoColecao)
                                        .Select(consolidado => new KeyValuePair<string, decimal>(consolidado.First().Desenho +
                                                                                                 consolidado.First().Tecnologia.Substring(0, 1) +
                                                                                                 consolidado.First().IDGrupoColecao,
                                                                                                 consolidado.Sum(s => s.QuantidadeConvertida))
                                                                                                 ).ToList();

                                    bool isValidDes = true;
                                    decimal QtdeMaxima = 0;
                                    decimal QtdeMinima = 999999;

                                    REGRAS_QTD_PEDIDOX objMinMaxX = new REGRAS_QTD_PEDIDOX();
                                    objMinMaxX = null;

                                    decimal idGrCol = 0;

                                    #region nova regra validação em MT
                                    //oda-- 19/05/2016 --- mudanca na regra de qtde pela tabela nova Validação sempre em MTs ----------------------------------------------------------------
                                    if (ConfigurationManager.AppSettings["NOVA_REGRA_QTDE_ATIVA"] == "1")
                                    {
                                        foreach (KeyValuePair<string, decimal> item in lstGrupoDesenho)
                                        {
                                            if (item.Key.Substring(4, 1) != "L")
                                            {
                                                decimal? objDesenhoPronto = 0;
                                                bool desenhoPronto = false;
                                                idGrCol = decimal.Parse(item.Key.Substring(5, 1));

                                                try { objDesenhoPronto = ctxTI.VW_DESENHOS_POR_COLECAO.Where(x => x.DESENHO == item.Key.Substring(0, 4)).FirstOrDefault().DESENHO_PRONTO; }
                                                catch { objDesenhoPronto = null; }

                                                if (objDesenhoPronto != null)
                                                { desenhoPronto = (objDesenhoPronto == 1); }

                                                var qryQtdeMinMaxX =
                                                    from Qtde in ctxTI.REGRAS_QTD_PEDIDOX
                                                    where
                                                           Qtde.TECNOLOGIA_DESTINO == item.Key.Substring(4, 1)//tecnologia
                                                        && Qtde.GRUPO_COLECAO == idGrCol
                                                        && Qtde.DESENHO_PRONTO == desenhoPronto
                                                        && Qtde.TIPO_PEDIDO == model.IDTipoPedido
                                                        && Qtde.UM == "MT"
                                                        && Qtde.EXCLUIDO == false
                                                    select
                                                        Qtde;

                                                objMinMaxX = qryQtdeMinMaxX.FirstOrDefault();

                                                if (objMinMaxX != null)
                                                {
                                                    QtdeMaxima = objMinMaxX.QTD_MAX_DES;
                                                    QtdeMinima = objMinMaxX.QTD_MIN_DES;

                                                    if (item.Value < QtdeMinima)
                                                    {
                                                        ModelState.AddModelError("", "A QUANTIDADE MÍNIMA POR DESENHO NÃO PODE SER MENOR QUE: " + QtdeMinima.ToString() +
                                                                      " | item: " + item.Key +
                                                                      " | TOTAL (KG CONVERTIDO + MT): " + item.Value.ToString() +
                                                                      " MTs");
                                                        hasErrors = true;
                                                    }
                                                    else if (item.Value > QtdeMaxima)
                                                    {
                                                        ModelState.AddModelError("", "A QUANTIDADE MÁXIMA POR DESENHO NÃO PODE SER MAIOR QUE: " + QtdeMaxima.ToString() +
                                                                      " | item: " + item.Key +
                                                                      " | TOTAL (KG CONVERTIDO + MT): " + item.Value.ToString() +
                                                                      " MTs");
                                                        hasErrors = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    if (!isValidDes)
                                    {
                                        hasErrors = true;
                                    }
                                }
                                // -- oda -- 05/112015 --- regra de validação de qtde por desenho ------------------------------------------------------------------------------------------------------------------

                                #region Validação Lisos
                                //<<< oda -- 17/10/2016 --- se for LISO; nova regra de qtdes ----------------------------------------------------------------------                            
                                List<KeyValuePair<string, decimal>> lstGrupoCor = base.Session_Carrinho.Itens
                                        .GroupBy(g => g.Cor + g.Tecnologia.Substring(0, 1) + g.IDGrupoColecao + g.UnidadeMedida)
                                        .Select(consolidado => new KeyValuePair<string, decimal>(consolidado.First().Cor +
                                                                                                 consolidado.First().Tecnologia.Substring(0, 1) +
                                                                                                 consolidado.First().IDGrupoColecao +
                                                                                                 consolidado.First().UnidadeMedida,
                                                                                                 consolidado.Sum(s => s.Quantidade))
                                                                                                 ).ToList();
                                foreach (KeyValuePair<string, decimal> item in lstGrupoCor)
                                {
                                    decimal idGrCol = 0;
                                    decimal QtdeMaxima = 0;
                                    decimal QtdeMinima = 999999;
                                    REGRAS_QTD_PEDIDOX objMinMaxX = new REGRAS_QTD_PEDIDOX();
                                    objMinMaxX = null;

                                    //C2MT
                                    if (item.Key.Length > 7)
                                    {
                                        if (item.Key.Substring(7, 1) == "L")
                                        {
                                            idGrCol = decimal.Parse(item.Key.Substring(8, 1));

                                            var qryQtdeMinMaxX =
                                                from Qtde in ctxTI.REGRAS_QTD_PEDIDOX
                                                where Qtde.EXCLUIDO == false
                                                   && Qtde.TECNOLOGIA_DESTINO == "L"
                                                   && (Qtde.GRUPO_COLECAO == idGrCol || Qtde.GRUPO_COLECAO == 0)
                                                   && Qtde.TIPO_PEDIDO == model.IDTipoPedido
                                                   && Qtde.UM == item.Key.Substring(9, 2)
                                                   && Qtde.COR == item.Key.Substring(0, 7)
                                                select
                                                    Qtde;

                                            objMinMaxX = qryQtdeMinMaxX.FirstOrDefault();

                                            if (objMinMaxX != null)
                                            {
                                                QtdeMaxima = objMinMaxX.QTD_MAX_DES;
                                                QtdeMinima = objMinMaxX.QTD_MIN_DES;

                                                if (item.Value < QtdeMinima)
                                                {
                                                    ModelState.AddModelError("", "A QUANTIDADE MÍNIMA POR COR NÃO PODE SER MENOR QUE: " + QtdeMinima.ToString() +
                                                                  " | item: " + item.Key +
                                                                  " | TOTAL: " + item.Value.ToString() +
                                                                  item.Key.Substring(9, 2));
                                                    hasErrors = true;
                                                }
                                                else if (item.Value > QtdeMaxima)
                                                {
                                                    ModelState.AddModelError("", "A QUANTIDADE MÁXIMA POR COR NÃO PODE SER MAIOR QUE: " + QtdeMaxima.ToString() +
                                                                  " | item: " + item.Key +
                                                                  " | TOTAL (KG CONVERTIDO + MT): " + item.Value.ToString() +
                                                                  item.Key.Substring(9, 2));
                                                    hasErrors = true;
                                                }
                                            }
                                        }
                                    }
                                }//>>>> oda -- 17/10/2016 --- se for LISO; nova regra de qtdes ---------------------------------------------------------------------- 
                                #endregion
                            }
                        }                                                              
                    }

                    if (hasErrors)
                    {
                        this.ConclusaoPedidoCarregarEscolhas(model);
                        return View(model);
                    }

                    #endregion

                    #region Persistir na sessão

                    base.Session_Carrinho.Moeda = model.Moeda;
                    base.Session_Carrinho.CondicaoPagto = model.CondicaoPagto;
                    base.Session_Carrinho.CanailVenda = model.CanailVenda;
                    base.Session_Carrinho.ViaTransporte = model.ViaTransporte;
                    base.Session_Carrinho.Frete = model.Frete;
                    base.Session_Carrinho.TipoAtendimento = model.TipoAtendimento;
                    base.Session_Carrinho.Observacoes = model.Observacoes;
                    base.Session_Carrinho.PedidoCliente = model.PedidoCliente;
                    base.Session_Carrinho.NFRefat = model.NFRefat;
                    base.Session_Carrinho.PedidoRefat = model.PedidoRefat;

                    #endregion

                    //-- oda -- 08/11/2016 -- pegar preços apenas se o pedido não for edição de PE ------------
                    if ( (model.IDTipoPedido != (int)Enums.TiposPedido.RESERVA) && (!model.CanailVenda.CANAL_VENDA.Equals((int)Enums.CanaisVenda.TELEVENDAS)) )
                    {                    
                        #region Obter Preço
                        //int? iColecaoAtual = 0;
                        int iCodCondPgto = 0;

                        #region Pegar Código Condição de Pagto - Call Function Oracle
                        using(var ti_ctx = new TIDalutexContext())
                        {                                                                                                   
                            //iCodCondPgto = ti_ctx.Database.SqlQuery<int>("select ti_dalutex.pega_consicao_pgto(:p0) from dual", model.CondicaoPagto.ID_COND).FirstOrDefault();
                            try{iCodCondPgto = ti_ctx.LINK_GRUPO_COND_PGTO.Where(x => x.COD_COND == model.CondicaoPagto.ID_COND).FirstOrDefault().ID_GRUPO_COND;} catch { iCodCondPgto = 1; }// 1: AVISTA
                        }
                        #endregion


                        foreach (InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                        {
                            using (var ctxTI = new TIDalutexContext())
                            {
                                VW_TABELA_PRECO_NOVA objPrecoNovo = null;                                                                         
                                if (item.Reduzido > 0)
                                {
                                    using (var ctxDlx = new DalutexContext())
                                    {
                                        decimal dReduzido = item.Reduzido;                           

                                        VW_GRUPO_COL_RED grCol = ctxTI.VW_GRUPO_COL_RED.Where(x => x.REDUZIDO == item.Reduzido).FirstOrDefault();

                                        int iTipoCol = grCol == null ? 1 : Convert.ToInt32(grCol.TIPO_COL);

                                        VMASCARAPRODUTOACABADO vm = ctxDlx.VMASCARAPRODUTOACABADO.Where(x => x.CODIGO_REDUZIDO == item.Reduzido).FirstOrDefault();
                                        string vClassif = null;
                                        if (vm != null)
                                        {
                                            vClassif = vm.CLASSIF_COR;
                                        }

                                        objPrecoNovo = ctxTI.VW_TABELA_PRECO_NOVA.Where(x =>
                                                             x.TIPO == iTipoCol// 1: coleção/exclusivo; 2: flash/pocket
                                                             && 
                                                             ( 
                                                              (x.TECNOLOGIA == item.Tecnologia && x.TECNOLOGIA != "L")
                                                             ||
                                                              (x.TECNOLOGIA == item.Tecnologia && item.Tecnologia == "L" && x.CLASSIFICACAO == vClassif) 
                                                             )
                                                             && x.CONDICAO_PAGAMENTO == iCodCondPgto
                                                             && x.ARTIGO == item.Artigo
                                                             && x.QUALIDADE == 1
                                                             && x.QUALIDADE_COMERCIAL ==  Session_Carrinho.QualidadeComercial.Key
                                                             && x.TAMANHO_PECA == "G"
                                                             && x.COMISSAO == (iTipoCol == 2 ? 3 : 4)

                                            ).FirstOrDefault();                                                                          
                                    }
                                }
                                else//se não tem reduzido ----
                                {
                                    if (model.IDTipoPedido != (int)Enums.TiposPedido.RESERVA)
                                    {

                                        int iIDColecao = int.Parse(item.IDColecao);

                                        VW_GRUPO_COL_RED grCol = ctxTI.VW_GRUPO_COL_RED.Where(x => x.ID_COL == iIDColecao).FirstOrDefault();

                                        int iTipoCol = grCol == null ? 1 : Convert.ToInt32(grCol.TIPO_COL);

                                        objPrecoNovo = ctxTI.VW_TABELA_PRECO_NOVA.Where(x =>
                                                             x.TIPO == iTipoCol// 1: coleção/exclusivo; 2: flash/pocket
                                                             && x.TECNOLOGIA == item.Tecnologia                                                                                                                               
                                                             && x.CONDICAO_PAGAMENTO == iCodCondPgto
                                                             && x.ARTIGO == item.Artigo
                                                             && x.QUALIDADE == 1
                                                             && x.QUALIDADE_COMERCIAL == Session_Carrinho.QualidadeComercial.Key
                                                             && x.TAMANHO_PECA == "G"
                                                             && x.COMISSAO == (iTipoCol == 2 ? 3 : 4)

                                            ).FirstOrDefault();                                       
                                    }

                                }

                                if (objPrecoNovo != null)
                                {
                                    item.PrecoTabela = decimal.Round(objPrecoNovo.PRECO.GetValueOrDefault(), 2, MidpointRounding.ToEven);
                                }                   
                            }                       
                        }
                        #endregion
                    }
                    return RedirectToAction("Preview", "Pedido");
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }

            this.ConclusaoPedidoCarregarEscolhas(model);
            return View(model);
        }

        public ActionResult ConfirmacaoPedido(string numeropedido)
        {
            ViewBag.NumeroPedido = numeropedido;
            return View();
        }

        public EspelhoPedidoPdf EspelhoPedido(string numeropedido)
        {
            try
            {
                return new EspelhoPedidoPdf() { IDPedidoBloco = decimal.Parse(numeropedido) };
            }
            catch (Exception ex)
            {
                base.Handle(ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult EnviarEmail(string numeropedido)
        {
            EnviarEmailViewModel model = new EnviarEmailViewModel();
            PrepararModelEmailPedido(numeropedido, model, true);

            return View(model);
        }

        [HttpPost]
        public ActionResult EnviarEmail(EnviarEmailViewModel model)
        {
            try
            {
                Utilitarios utils = new Utilitarios();
 

                byte[] buffer;
                MemoryStream pdfStream;

                EspelhoPedidoPdf espelho = new EspelhoPedidoPdf();
                espelho.IDPedidoBloco = decimal.Parse(model.ChaveAnexo);
                espelho.CreatePdfStream(out buffer, out pdfStream);

                Attachment anexo = new Attachment(pdfStream, "Pedido_" + model.ChaveAnexo, "application/pdf");


                utils.EnviaEmail(model.De, model.Para, model.ComCopia, model.Titulo, model.Conteudo.Replace(Environment.NewLine,@"<BR />"), anexo);
                
                pdfStream.Close();

                ViewBag.SendResult = "e-mail enviado com sucesso.";
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.SendResult = "Houve uma falha ao enviar o e-mail." + Environment.NewLine + ex.Message;
                base.Handle(ex);
                return null;
            }
        }

        public JsonResult TotalCarrinho()
        {
            if (base.Session_Carrinho != null && base.Session_Carrinho.Itens != null)
            {
                int iCount = base.Session_Carrinho.Itens.Where(i => !i.Excluir).ToList().Count;
                if(iCount > 0)
                {
                    return Json(iCount, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Preview()
        {
            PreviewViewModel model = new PreviewViewModel();

            model.Carrinho = base.Session_Carrinho;
            model.UrlDesenhos = ConfigurationManager.AppSettings["PASTA_DESENHOS"];
            model.UrlReservas = ConfigurationManager.AppSettings["PASTA_RESERVAS"];
            model.Observacoes = Session_Carrinho.Observacoes;
            model.QualidadeCom = Session_Carrinho.QualidadeComercial.Key;
            model.DescrTipoPedido = base.Session_Carrinho.DescTipoPedido;
            
            if (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA)
            {            
                using (var ctx = new DalutexContext())
                {
                    model.Representante = ctx.REPRESENTANTES.Find(base.Session_Carrinho.Representante.IDREPRESENTANTE).NOME;
                    model.TipoPedido = ctx.COML_TIPOSPEDIDOS.Where(x => x.TIPOPEDIDO == Session_Carrinho.IDTipoPedido).First().DESCRICAO;
                              
                    model.Transportadora = ctx.TRANSPORTADORAS.Find(Session_Carrinho.Transportadora.IDTRANSPORTADORA).NOME;                   
                    model.CondPagto = ctx.COML_CONDICOESPAGAME.Where(x => x.CONDICAO == Session_Carrinho.CondicaoPagto.ID_COND).First().DESCRICAO;
                    model.Frete = ctx.COML_TIPOSFRETE.Where(x => x.TIPOFRETE == Session_Carrinho.Frete.TIPOFRETE).First().DESCRICAO;                
                }            
           
                using (var ctx = new TIDalutexContext())
                {
                    model.ClienteEntrega = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == Session_Carrinho.ClienteEntrega.ID_CLIENTE).First().NOME;
                    model.ClienteFatura= ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == Session_Carrinho.ClienteFatura.ID_CLIENTE).First().NOME;
                    model.TipoAtendimento = ctx.PRE_PEDIDO_ATEND.Where(x => x.COD_ATEND == Session_Carrinho.TipoAtendimento.COD_ATEND).First().DESCRI_ATEND;               
                }
            }
            else
            {
                using (var ctx = new DalutexContext())
                {
                    model.Representante = ctx.REPRESENTANTES.Find(Session_Carrinho.Representante.IDREPRESENTANTE).NOME;
                    model.TipoPedido = ctx.COML_TIPOSPEDIDOS.Where(x => x.TIPOPEDIDO == Session_Carrinho.IDTipoPedido).First().DESCRICAO;
                }

                using (var ctx = new TIDalutexContext())
                {
                    model.ClienteFatura = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == Session_Carrinho.ClienteFatura.ID_CLIENTE).First().NOME;                    
                }
            }
                        
            return View(model);
        }

        [HttpPost]
        public ActionResult Preview(PreviewViewModel model)
        {
            try
            {
                bool estaEditando = base.Session_Carrinho.Editando;

                int iNUMERO_PEDIDO_BLOCO = default(int);

                if (!estaEditando)
                {
                    while (iNUMERO_PEDIDO_BLOCO == default(int))
                    {
                        using (var ctx = new TIDalutexContext())
                        {
                            PROXIMO_NUMERO_PEDIDO reservar = ctx.PROXIMO_NUMERO_PEDIDO.Where(x => x.DISPONIVEL == 0).OrderBy(x => x.NUMERO_PEDIDO).First();

                            iNUMERO_PEDIDO_BLOCO = reservar.NUMERO_PEDIDO;

                            reservar.DISPONIVEL = 1;
                            reservar.ROTINA = 2;
                            reservar.DATA_RESERVA_SID = DateTime.Today;
                            ctx.SaveChanges();
                        }
                    }
                }
                else
                {
                    iNUMERO_PEDIDO_BLOCO = base.Session_Carrinho.ID;
                }
                
                using (var ctx = new TIDalutexContext())
                {
                    #region Grava Pedido

                    PRE_PEDIDO objPrePedido = null;

                    if (!estaEditando)
                    {
                        objPrePedido = new PRE_PEDIDO();
                    }
                    else
                    {
                        objPrePedido = ctx.PRE_PEDIDO.Where(p => p.NUMERO_PEDIDO_BLOCO == iNUMERO_PEDIDO_BLOCO).First();
                    }

                    
                    objPrePedido.NUMERO_PEDIDO_BLOCO = iNUMERO_PEDIDO_BLOCO;
                    objPrePedido.TIPO_PEDIDO = base.Session_Carrinho.IDTipoPedido;
                    objPrePedido.ID_REPRESENTANTE = base.Session_Carrinho.Representante.IDREPRESENTANTE;
                    objPrePedido.ID_CLIENTE = base.Session_Carrinho.ClienteFatura.ID_CLIENTE;
                    objPrePedido.QUALIDADE_COM = base.Session_Carrinho.QualidadeComercial.Key;
                    objPrePedido.COD_COND_PGTO = base.Session_Carrinho.CondicaoPagto.ID_COND;
                    objPrePedido.OBSERVACOES = base.Session_Carrinho.Observacoes;
                    objPrePedido.DATA_ENTREGA = base.Session_Carrinho.DataEntrega;                   
                    objPrePedido.ID_CLIENTE_ENTREGA = (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA ? base.Session_Carrinho.ClienteEntrega.ID_CLIENTE : base.Session_Carrinho.ClienteFatura.ID_CLIENTE);
                    objPrePedido.ID_TRANSPORTADORA = (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA ? base.Session_Carrinho.Transportadora.IDTRANSPORTADORA : -1);
                    objPrePedido.USUARIO_INICIO = base.Session_Usuario.NOME_USU;
                    objPrePedido.ID_LOCAL = (base.Session_Carrinho.IDLocaisVenda == null ? 5 : base.Session_Carrinho.IDLocaisVenda);//todo: verificar com marcio se precisa de uma view para este tipo                    
                    objPrePedido.COD_MOEDA = base.Session_Carrinho.Moeda.CODIGOMOEDA;
                    objPrePedido.CANAL_VENDAS = base.Session_Carrinho.CanailVenda.CANAL_VENDA;
                    objPrePedido.ATENDIMENTO = (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA ? base.Session_Carrinho.TipoAtendimento.COD_ATEND : 0);                    
                    objPrePedido.TIPOFRETE = base.Session_Carrinho.Frete.TIPOFRETE;
                    //GERENTE = model.IDGerentesVenda,// não é necessario gravar neste campo para pedidos <> de PE                    
                    objPrePedido.VIATRANSPORTE = base.Session_Carrinho.ViaTransporte.VIATRANSPORTE;
                    objPrePedido.COMISSAO = Session_Carrinho.PorcentagemComissao;
                    objPrePedido.ORIGEM = "PW"; // APENAS PRA INFORMAR QUE ESTE PEDIDO VEIO DO PEDIDO WEB NOVO. 
                    objPrePedido.PEDIDO_CLIENTE = base.Session_Carrinho.PedidoCliente;

                    objPrePedido.NF_REFAT = (base.Session_Carrinho.NFRefat);
                    objPrePedido.PEDIDO_REFAT = base.Session_Carrinho.PedidoRefat;

                    if(!estaEditando)
                    {
                        objPrePedido.STATUS_PEDIDO = 1; //embora esteja definido no banco como padrão "1", esta gravando nulo, então, deixar explicito.....  
                        objPrePedido.DATA_EMISSAO = DateTime.Now;
                        objPrePedido.DATA_INICIO = DateTime.Now;
                        objPrePedido.DATA_FINAL = DateTime.Now;                        
                        objPrePedido.DATA_EMISSAO_DT = DateTime.Today;
                    }
                    if (base.Session_Carrinho.IDTipoPedido == (int)Enums.TiposPedido.RESERVA)
                    {
                        objPrePedido.DATA_ENTREGA = DateTime.Now;
                        objPrePedido.DATA_ENTREGA_DIGI = DateTime.Now;
                    }

                    #endregion

                    if(!objPrePedido.CANAL_VENDAS.Equals((int)Enums.CanaisVenda.TELEVENDAS))//!model.CanailVenda.CANAL_VENDA.Equals((int)Enums.CanaisVenda.TELEVENDAS)
                    {
                        #region Edição de Itens do Pedido
                        using (var transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                List<PRE_PEDIDO_ITENS> lstItens = null;
                                if (!estaEditando)
                                {
                                    lstItens = new List<PRE_PEDIDO_ITENS>();
                                }
                                else
                                {
                                    lstItens = ctx.PRE_PEDIDO_ITENS.Where(i => i.NUMERO_PEDIDO_BLOCO == iNUMERO_PEDIDO_BLOCO).ToList();
                                }

                                int iNumeroItem = 0;
                                if (estaEditando)
                                {
                                    iNumeroItem = lstItens.Max(i => i.ITEM);
                                }

                                foreach (InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                                {
                                    if (item.Excluir)
                                        continue;

                                    if (base.Session_Carrinho.IDTipoPedido == (int)Enums.TiposPedido.RESERVA)
                                    {
                                        item.Artigo = "0000";
                                        item.Quantidade = 100;
                                        item.Pecas = 1;
                                        item.TecnologiaPorExtenso = null;
                                        item.DataEntregaItem = DateTime.Now;
                                        item.DtItemSolicitada = DateTime.Now;

                                        #region Reserva - Item sem reduzido (id_controle)

                                        if (item.Reduzido <= default(int) && item.ID == 0)
                                        {
                                            item.Reduzido = ctx.Database.SqlQuery<int>("SELECT SEQ_ID_CONTROLE_DESENV.NEXTVAL FROM DUAL", 1).FirstOrDefault();

                                            CONTROLE_DESENV objInsert = new CONTROLE_DESENV()
                                            {
                                                ID_CONTROLE_DESENV = item.Reduzido,
                                                DT_ENT_ATEND = DateTime.Now,
                                                ID_USUARIO = base.Session_Usuario.COD_USU,
                                                ID_CLIENTE = base.Session_Carrinho.ClienteFatura.ID_CLIENTE.ToString("000000"),
                                                ID_REP = base.Session_Carrinho.Representante.IDREPRESENTANTE,
                                                ID_STUDIO = item.IDStudio,
                                                ID_ITEM_STUDIO = item.IDItemStudio,
                                                STATUS_GERAL = 1
                                            };

                                            ctx.CONTROLE_DESENV.Add(objInsert);
                                        }

                                        CONTROLE_DESENV_ITEM_STUDIO objUpdate = ctx.CONTROLE_DESENV_ITEM_STUDIO.Find(item.IDItemStudio);
                                        objUpdate.STATUS = 1; //INDISPONÍVEL

                                        #endregion
                                    }

                                    if(!item.PreExistente)
                                        iNumeroItem++;

                                    string origem = "";

                                    #region Inserir a reserva do item se necessário

                                    if (item.Tipo == Enums.ItemType.ValidacaoReserva)
                                    {
                                        origem = "E";

                                        PED_RESERVA_VENDA objPedReservaVenda = null;
                                        if(!item.PreExistente)//Se é um item novo
                                        {
                                            int _id = ctx.Database.SqlQuery<int>("SELECT SEQ_ID_PED_RES_VENDA.NEXTVAL FROM DUAL", 1).FirstOrDefault();

                                            objPedReservaVenda = new PED_RESERVA_VENDA()
                                            {
                                                PEDIDO_RESERVA = item.PedidoReserva,
                                                ITEM_PED_RESERVA = item.ItemPedidoReserva,
                                                ID_VAR_PED_RESERVA = item.IDVariante,
                                                PEDIDO_VENDA = iNUMERO_PEDIDO_BLOCO,
                                                ITEM_PED_VENDA = iNumeroItem,
                                                ID_PED_RESERVA_VENDA = _id
                                            };

                                            ctx.PED_RESERVA_VENDA.Add(objPedReservaVenda);
                                        }
                                        else
                                        {
                                            try {// oda -- 05/10/2016 -- ver com cassiano: não entendi o pq desta alteração ---------------------------
                                                objPedReservaVenda = ctx.PED_RESERVA_VENDA.Where(
                                                    r => r.PEDIDO_VENDA == iNUMERO_PEDIDO_BLOCO
                                                    && r.ITEM_PED_VENDA == item.ID
                                                    && r.PEDIDO_RESERVA == item.PedidoReserva
                                                    && r.ITEM_PED_RESERVA == item.ItemPedidoReserva).First();

                                                objPedReservaVenda.ID_VAR_PED_RESERVA = item.IDVariante;
                                            } catch{ }
                                        }
                                   
                                        ctx.SaveChanges();
                                    }

                                    #endregion

                                    string _cor ;
                                    if (item.Tipo == Enums.ItemType.ValidacaoReserva)
                                    {
                                        _cor = "E000000";
                                    }
                                    else if (item.Cor == null) 
                                    {
                                         _cor = "0000000";
                                    }
                                    else
                                    {
                                        _cor = item.Cor;
                                    }

                                    PRE_PEDIDO_ITENS objItemSalvar = null;

                                    if(!item.PreExistente)
                                    {
                                        objItemSalvar = new PRE_PEDIDO_ITENS();
                                        objItemSalvar.ITEM = iNumeroItem;
                                        objItemSalvar.NUMERO_PEDIDO_BLOCO = iNUMERO_PEDIDO_BLOCO;
                                        objItemSalvar.Novo = true;
                                    }
                                    else
                                    {
                                        objItemSalvar = lstItens.Where(i => i.ITEM == item.ID).First();
                                    }

                                    objItemSalvar.ARTIGO = item.Artigo;
                                    objItemSalvar.COR = _cor;                     
                                    objItemSalvar.DATA_ENTREGA = item.DataEntregaItem;
                                    objItemSalvar.DESENHO = item.Desenho;
                                    objItemSalvar.LISO_ESTAMP = item.Tecnologia;
                                    objItemSalvar.PE = "N";
                                    objItemSalvar.PRECO_UNIT = item.Preco;
                                    objItemSalvar.PRECOLISTA = (item.PrecoTabela == null ? 0 : item.PrecoTabela);
                                    objItemSalvar.QTDEPC = item.Pecas;
                                    objItemSalvar.QUANTIDADE = item.Quantidade;
                                    objItemSalvar.REDUZIDO_ITEM = item.Reduzido;
                                    objItemSalvar.UM = item.UnidadeMedida;
                                    objItemSalvar.VALOR_TOTAL = item.Preco * item.Quantidade;
                                    objItemSalvar.VARIANTE = item.Variante;
                                    objItemSalvar.COD_COMPOSE = item.Compose;
                                    objItemSalvar.ORIGEM = origem;
                                    objItemSalvar.MALHA_PLANO = item.UnidadeMedida == "MT"? "P": "M";
                                    objItemSalvar.MODA_DECORACAO = "M";
                                    objItemSalvar.DATA_ENTREGA_DIGI = item.DtItemSolicitada;
                                    objItemSalvar.ID_TAB_PRECO = -1;     //todo: se nulop, -1
                                    objItemSalvar.STATUS_ITEM = 15;
                                    objItemSalvar.PRECODIGITADOMOEDA = 0;//todo: apontar este campo para preco correto em caso de este existir                                    
                                    objItemSalvar.TEM_RESTRICAO = (item.TemRestricao == true ? 1 : 0);
                                    objItemSalvar.RESTRICAO = item.Restricao;

                                    if ((item.Tipo == Enums.ItemType.ValidacaoReserva || item.Tipo == Enums.ItemType.Estampado) && !item.PreExistente)
                                    {
                                        if (item.TecnologiaOriginal != item.TecnologiaPorExtenso)
                                        {
                                            objItemSalvar.TROCA_TECNOLOGIA = "Troca de " + item.TecnologiaOriginal + " para " + item.TecnologiaPorExtenso;
                                        }
                                    }

                                    if (objItemSalvar.Novo)
                                    {
                                        lstItens.Add(objItemSalvar);
                                    }
                                }

                                if(!estaEditando)
                                {
                                    ctx.PRE_PEDIDO.Add(objPrePedido);
                                }

                                if (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA)
                                {
                                    #region Criticas

                                    #region Liberação financeira

                                    if(!estaEditando)
                                    {
                                        ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                                        {
                                            NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                                            COD_CRITICA = (decimal)Enums.TiposCritica.LiberacaoFinanceira,
                                            FLG_STATUS = "C"
                                        });
                                    }

                                    #endregion

                                    #region Item sem reduzido

                                    bool hasItemSemReduzido = false;

                                    foreach (PRE_PEDIDO_ITENS item in lstItens)
                                    {
                                        if (!hasItemSemReduzido && item.REDUZIDO_ITEM.GetValueOrDefault() == -2)
                                        {
                                            hasItemSemReduzido = true;
                                        }
                                    }

                                    if (hasItemSemReduzido)
                                    {
                                        PRE_PEDIDO_CRITICA objCriticaAnterior = null;

                                        if(estaEditando)
                                        {
                                            ctx.PRE_PEDIDO_CRITICA
                                            .Where(p => 
                                                p.NUMERO_PRE_PEDIDO == objPrePedido.NUMERO_PEDIDO_BLOCO
                                                && p.COD_CRITICA == (decimal)Enums.TiposCritica.SemReduzido
                                            ).FirstOrDefault();
                                        }

                                        if(objCriticaAnterior == null)
                                        {
                                            ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                                            {
                                                NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                                                COD_CRITICA = (decimal)Enums.TiposCritica.SemReduzido,
                                                FLG_STATUS = "C"
                                            });
                                        }
                                    }

                                    #endregion

                                    #region Preço divergente
                             
                                    foreach (PRE_PEDIDO_ITENS item in lstItens)
                                    {
                                        //if (item.REDUZIDO_ITEM > 0)
                                        //{
                                            //Se não tem preço, critica. Se tem preço e ele é diferente do informado pelo representante, critica.
                                            if (item.PRECOLISTA == null || item.PRECOLISTA.GetValueOrDefault() != item.PRECO_UNIT.GetValueOrDefault())
                                            {
                                                PRE_PEDIDO_CRITICA objCriticaAnterior = null;

                                                if(estaEditando)
                                                {
                                                    ctx.PRE_PEDIDO_CRITICA
                                                    .Where(p => 
                                                        p.NUMERO_PRE_PEDIDO == objPrePedido.NUMERO_PEDIDO_BLOCO
                                                        && p.COD_CRITICA == (decimal)Enums.TiposCritica.PrecoDiferente
                                                        && p.ITEM_PEDIDO == item.ITEM
                                                    ).FirstOrDefault();
                                                }

                                                if(objCriticaAnterior == null)
                                                {
                                                    ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                                                    {
                                                        NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                                                        COD_CRITICA = (decimal)Enums.TiposCritica.PrecoDiferente,
                                                        FLG_STATUS = "C",
                                                        ITEM_PEDIDO = item.ITEM,
                                                        VALOR_TAB = item.PRECOLISTA,
                                                        VALOR_ITEM = item.PRECO_UNIT
                                                    });
                                                }
                                            }
                                        //}
                                    }

                                    #endregion

                                    #region Item com Restrição
                                    foreach (PRE_PEDIDO_ITENS item in lstItens)
                                    {
                                        if (item.TEM_RESTRICAO == 1)
                                        {
                                            PRE_PEDIDO_CRITICA objCriticaAnterior = null;

                                            if (estaEditando)
                                            {
                                                ctx.PRE_PEDIDO_CRITICA
                                                .Where(p =>
                                                    p.NUMERO_PRE_PEDIDO == objPrePedido.NUMERO_PEDIDO_BLOCO
                                                    && p.COD_CRITICA == (decimal)Enums.TiposCritica.Restricao
                                                    && p.ITEM_PEDIDO == item.ITEM
                                                ).FirstOrDefault();
                                            }

                                            if (objCriticaAnterior == null)
                                            {
                                                ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                                                {
                                                    NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                                                    COD_CRITICA = (decimal)Enums.TiposCritica.Restricao,
                                                    FLG_STATUS = "C",
                                                    ITEM_PEDIDO = item.ITEM
                                                });
                                            }
                                        }                                    
                                    }
                                    #endregion

                                    #endregion
                                }

                                foreach (PRE_PEDIDO_ITENS item in lstItens)
                                {
                                    if (item.Novo)
                                    {
                                        ctx.PRE_PEDIDO_ITENS.Add(item);
                                    }
                                }

                                foreach (InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                                {
                                    if(item.Excluir)
                                    {
                                        PRE_PEDIDO_ITENS objExcluir = ctx.PRE_PEDIDO_ITENS.First(i => i.ITEM == item.ID && i.NUMERO_PEDIDO_BLOCO == iNUMERO_PEDIDO_BLOCO);
                                        ctx.PRE_PEDIDO_ITENS.Remove(objExcluir);
                                    }
                                }

                                ctx.SaveChanges();

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw ex;
                            }
                        }
                        #endregion
                    }
                }
                 
                #region EnviarPDF
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["ENVIAR_EMAIL"]) == true)
                {
                    //Envio silencioso
                    EnviarEmailViewModel emailModel = new EnviarEmailViewModel();
                    PrepararModelEmailPedido(iNUMERO_PEDIDO_BLOCO.ToString(), emailModel, false);
                    EnviarEmail(emailModel);
                }
                #endregion

                base.Session_Carrinho = null;

                return RedirectToAction("ConfirmacaoPedido", "Pedido", new { numeropedido = iNUMERO_PEDIDO_BLOCO.ToString() });
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            
            return View(model.Sucesso = false);
        }

        private void PrepararModelEmailPedido(string chave, EnviarEmailViewModel emailModel, bool enviarCliente)
        {
            emailModel.ChaveAnexo = chave;
            emailModel.Titulo = "Pedido " + chave;
            if (DateTime.Now.Hour < 12)
                emailModel.Conteudo += "Bom dia." + Environment.NewLine;
            else if (DateTime.Now.Hour < 19)
                emailModel.Conteudo += "Boa tarde." + Environment.NewLine;
            else
                emailModel.Conteudo += "Boa noite." + Environment.NewLine;

            emailModel.De = "dalutex@dalutex.com.br";

            using (var ctxTI = new TIDalutexContext())
            {
                decimal dNumeroPedido = decimal.Parse(chave);
                PRE_PEDIDO objPedido = ctxTI.PRE_PEDIDO.Where(x => x.NUMERO_PEDIDO_BLOCO == dNumeroPedido).First();

                decimal idclientesgt = objPedido.ID_CLIENTE.GetValueOrDefault();
                if(idclientesgt > 0)
                {
                    VW_EMAILS objEmails = ctxTI.VW_EMAILS.Where(x => x.CLIENTESGT == idclientesgt).FirstOrDefault();
                    if(objEmails != null)
                    {
                        emailModel.Titulo += " - Cliente: " + objEmails.NOME;

                        if (objPedido.DATA_EMISSAO_DT != null)
                            emailModel.Titulo += " - Data: " + objPedido.DATA_EMISSAO_DT.GetValueOrDefault().ToString("dd/MM/yy");

                        emailModel.Para = objEmails.EMAIL_REP + "; ";
                        if (enviarCliente)
                        {
                            emailModel.ComCopia = objEmails.EMAIL_CLIENTE + "; ";
                        }
                        else
                        {
                            emailModel.ComCopia = ConfigurationManager.AppSettings["EMAIL_COMERCIAL_PEDIDO"] + "; ";
                        }
                    }
                }
            }

            emailModel.Conteudo += "Segue anexo o pedido Nº: " + emailModel.ChaveAnexo +"" + Environment.NewLine;
            emailModel.Conteudo += Environment.NewLine;
            emailModel.Conteudo += "Esta mensagem foi gerada automaticamente. Favor não respondê-la." + Environment.NewLine;
            emailModel.Conteudo += Environment.NewLine; emailModel.Conteudo += "AVISO LEGAL: Esta mensagem (incluindo qualquer anexo) e os arquivos nela contidos é confidencial e legalmente protegida, somente podendo ser usada pelo indivíduo ou entidade a quem foi endereçada. Caso você a tenha recebido por engano, deverá devolvê-la ao remetente e, posteriormente, apagá-la, pois, a disseminação, encaminhamento, uso, impressão ou cópia do conteúdo desta mensagem são expressamente proibidos.";
        }

        [HttpGet]
        public ActionResult PesquisaPedido(string pedido, string cliente, string representante, string pagina, string totalpaginas)
        {
            PesquisaPedidoViewModel model = new PesquisaPedidoViewModel();
            model.FiltroData = true;
            model.FiltroDataInicial = DateTime.Today.AddDays(-7);
            model.FiltroDataFinal = DateTime.Today;

            if (pagina != null)
            {
                int iIDPedido;

                if (int.TryParse(pedido, out iIDPedido) && iIDPedido > 0)
                {
                    model.FiltroPedido = iIDPedido.ToString();
                }

                model.FiltroCliente = cliente;
                model.FiltroRepresentante = representante;

                int iPagina;
                if (int.TryParse(pagina, out iPagina) && iPagina > 0)
                {
                    model.Pagina = iPagina;
                }

                int iTotalPaginas;
                if (int.TryParse(totalpaginas, out iTotalPaginas) && iTotalPaginas > 0)
                {
                    model.TotalPaginas = iTotalPaginas;
                }

                ObterPedidos(model);
            }

            return View(model);
        }

        public void ObterPedidos(PesquisaPedidoViewModel model)
        {
            int iItensPorPagina = 10;

            using(var ctxTI = new TIDalutexContext())
            {
                decimal dFiltroPedido = 0;
                decimal.TryParse(model.FiltroPedido, out dFiltroPedido);
                if (model.FiltroCliente != null)
                    model.FiltroCliente = model.FiltroCliente.ToUpper();

                if (model.FiltroRepresentante != null)
                    model.FiltroRepresentante = model.FiltroRepresentante.ToUpper();

                var result = (from p in ctxTI.VW_PESQUISA_PEDIDO
                             where (p.PEDIDO == dFiltroPedido || dFiltroPedido <= 0)
                                && (model.FiltroCliente == null || p.CLIENTE.ToUpper().Contains(model.FiltroCliente))
                                && (model.FiltroRepresentante == null || p.REPRESENTANTE.ToUpper().Contains(model.FiltroRepresentante))
                                && ( (model.FiltroData == false) || (p.DATA_EMISSAO >= model.FiltroDataInicial && p.DATA_EMISSAO <= model.FiltroDataFinal) )
                           orderby p.DATA_EMISSAO descending  
                            select p).ToList();


                decimal dTotal = result.Count / (decimal)iItensPorPagina;
                model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                if (model.TotalPaginas == 0)
                    model.TotalPaginas = 1;

                model.Pedidos = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();
            }
        }

        [HttpPost]
        public ActionResult PesquisaPedido(PesquisaPedidoViewModel model)
        {
            model.Pagina = 1;
            ObterPedidos(model);

            return View(model);
        }

        public ActionResult ManterPedido(string IDPedido)
        {
            if(!string.IsNullOrWhiteSpace(IDPedido))
            {
                ManterPedidoViewModel model = new ManterPedidoViewModel();
                using (var ctxTI = new TIDalutexContext())
                {
                    int iIDPedido = int.Parse(IDPedido);

                    VW_PESQUISA_PEDIDO objPedido = ctxTI.VW_PESQUISA_PEDIDO.Find(iIDPedido);

                    model.Pedido = objPedido.PEDIDO;
                    model.DataHoraEmissao = objPedido.DATA_H_EMIS;

                    model.Status = objPedido.STATUS_PEDIDO;

                    model.Representante = objPedido.REPRESENTANTE;

                    model.Cliente = objPedido.CLIENTE;//cliente fatura
                    model.CNPJFat = objPedido.CNPJ_FAT;
                    model.EndFat = objPedido.END_FAT;
                    
                    model.ClienteEntrega = objPedido.CLIENTE_ENTREGA;
                    model.EndEntrega = objPedido.END_ENTREGA;
                    model.CNPJEntrega = objPedido.CNPJ_ENT;
                    
                    if(objPedido.STATUS_PEDIDO == 10)/*Migrou para o SGT*/
                    {
                        model.PodeEditarPedido = false; /*VER COM O ODAIR base.Session_Usuario.PodeEditarPedidoAvancado;*/
                    }
                    else
                    {
                        model.PodeEditarPedido = base.Session_Usuario.PodeEditarPedidoNormal || base.Session_Usuario.PodeEditarPedidoAvancado;
                        model.PodeCancelarItens = false; /*VER COM O ODAIR base.Session_Usuario.PodeCancelarItens;*/
                    }
                }

               

                return View(model);
            }
            else
            {
                return RedirectToAction("PesquisaPedido");
            }
        }

        public ActionResult DuplicarCabecalho(string numeropedido)
        {
            this.RecarregarPedido(decimal.Parse(numeropedido), false);
            base.Session_Carrinho.ID = 0;

            return RedirectToAction("ConclusaoPedido");
        }

        public ActionResult DuplicarPedido(string numeropedido)
        {
            this.RecarregarPedido(decimal.Parse(numeropedido));
            base.Session_Carrinho.ID = 0;
            foreach (var item in base.Session_Carrinho.Itens)
                item.ID = 0;

            return RedirectToAction("ConclusaoPedido");
        }

        public ActionResult EditarPedido(string numeropedido)
        {
            if (base.Session_Usuario.PodeEditarPedidoNormal || base.Session_Usuario.PodeEditarPedidoAvancado)
            {
                this.RecarregarPedido(decimal.Parse(numeropedido));
                base.Session_Carrinho.Editando = true;

                if (Session_Carrinho.StatusPedido == 10)/*Migrou para o SGT*/
                {
                    if(!base.Session_Usuario.PodeEditarPedidoAvancado)
                    {
                        Session_Carrinho =  new ConclusaoPedidoViewModel();;
                        return RedirectToAction("Message", new { message = "Este pedido já migrou para o SGT. Não é permitido alterá-lo.", title = "Edição de pedido." });
                    }
                    else
                    {
                        return RedirectToAction("ConclusaoPedido");
                    }
                }
                else
                {
                    return RedirectToAction("ConclusaoPedido");
                }
            }
            else
            {
                return RedirectToAction("Message", new { message = "O seu usuário não tem acesso para editar pedidos.", title = "Edição de pedido." });
            }
        }

        #endregion

        #region Coleções

        public ActionResult MenuColecoes(string idcolecao, string nmcolecao, string pagina, string totalpaginas)
        {
            MenuColecoesViewModel model = new MenuColecoesViewModel();
            model.Filtro = nmcolecao;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            ActionResult objValidatorResult = this.ValidarTipoPedido(model);
            if(objValidatorResult != null)
            {
                return objValidatorResult;
            }
            
            int iIDColecao;

            if (int.TryParse(idcolecao, out iIDColecao))
            {
                if(iIDColecao > 0)
                {
                    using (var ctx = new TIDalutexContext())
                    {
                        VW_COLECAO objColecao = ctx.VW_COLECAO.Where(x => x.ID_COLECAO == iIDColecao).First();
                        model.Colecoes = new List<VW_COLECAO>();
                        model.Colecoes.Add(objColecao);
                        model.Filtro = objColecao.NOME_COLECAO;
                        model.Pagina = 1;
                    }
                }
            }
            else if (!string.IsNullOrWhiteSpace(model.Filtro))
            {
                ObterColecoes(model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuColecoes(MenuColecoesViewModel model)
        {
            model.Pagina = 1;
            ObterColecoes(model);

            return View(model);
        }

        private void ObterColecoes(MenuColecoesViewModel model)
        {
            int iItensPorPagina = 10;

            using (var ctx = new TIDalutexContext())
            {
                List<VW_COLECAO> result = null;

                if(model.TotalPaginas == 0)
                {
                    result = ctx.VW_COLECAO
                                    .Where(x => x.NOME_COLECAO.ToUpper().Contains(model.Filtro.ToUpper()))
                                    .OrderBy(x => x.NOME_COLECAO)
                                    .ToList();
                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int) Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.Colecoes = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();
                }
                else
                {
                    model.Colecoes = ctx.VW_COLECAO
                                    .Where(x => x.NOME_COLECAO.ToUpper().Contains(model.Filtro.ToUpper()))
                                    .OrderBy(x => x.NOME_COLECAO)
                                    .Skip((model.Pagina - 1) * iItensPorPagina)
                                    .Take(iItensPorPagina).ToList();
                }
            }
        }

        public ActionResult Desenhos(string idcolecao, string nmcolecao, string filtro, string pagina, string totalpaginas)
        {
            DesenhosViewModel model = new DesenhosViewModel();
            model.NMColecao = nmcolecao;
            model.FiltroDesenho = filtro;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            ActionResult objValidatorResult = this.ValidarTipoPedido(model);
            if (objValidatorResult != null)
            {
                return objValidatorResult;
            }

            using (var ctx = new TIDalutexContext())
            {
                if (idcolecao == "ATUAL")
                {
                    CONFIG_GERAL objResult = ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Atual);
                    model.IDColecao = int.Parse(objResult.PARAMETRO1);
                    model.NMColecao = objResult.PARAMETRO2;
                }
                else if (idcolecao == "POCKET")
                {
                    //oda-- 25/02/2016 --- alteração para pegar a pocket vigente -------------------------------------
                    model.NMColecao = "Flash";
                }
                else if (idcolecao == "DESENHOS")
                {
                    model.IDColecao = -1;
                    model.NMColecao = "DESENHOS";
                }
                else if (idcolecao.ToUpper() == "EXCLUSIVOS")
                {
                    model.IDColecao = 23;
                    model.NMColecao = "EXCLUSIVOS";
                }
                else if (idcolecao == null)
                {
                    ModelState.AddModelError("", "Coleção não informada.");
                    return View(model);
                }
                else
                {
                    model.IDColecao = int.Parse(idcolecao);
                }
            }
            
            if (model.TotalPaginas > 0 || model.IDColecao > 0 || idcolecao == "POCKET")            
            {            
                ObterDesenhos(model);            
            }

            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];

            if (base.Session_Carrinho != null && base.Session_Carrinho.Itens.Count() > 0 )
            {
                ViewBag.Carrinho = base.Session_Carrinho;
            }            

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Desenhos(DesenhosViewModel model)
        {
            model.Pagina = 1;
            this.ObterDesenhos(model);
            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];

            return View(model);
        }

        private void ObterDesenhos(DesenhosViewModel model)
        {
            int iItensPorPagina = 24;

            using (var ctx = new TIDalutexContext())
            {
                List<DesenhoVariante> result = null;
                
                var preQuery =
                    from dc in ctx.VW_DESENHOS_POR_COLECAO
                    where
                        ((model.IDColecao <= 0 ) || (dc.COLECAO == model.IDColecao))
                        && (dc.DESENHO.StartsWith(model.FiltroDesenho.ToUpper()) || model.FiltroDesenho == null)
                        && (dc.TECNOLOGIA.StartsWith(model.FiltroTecnologia.ToUpper()) || model.FiltroTecnologia == null)
                        && (dc.ARTIGO.StartsWith(model.FiltroArtigo.ToUpper()) || model.FiltroArtigo == null)
                       select dc;
                    
                List<VW_DESENHOS_POR_COLECAO> preResult = preQuery.ToList<VW_DESENHOS_POR_COLECAO>();

                if (model.IDColecao == 0 && model.NMColecao != null && model.NMColecao.ToUpper() == "FLASH")
                {
                    List<VW_POCKET_ATUAL> lstColecoesFlash = ctx.VW_POCKET_ATUAL.ToList();
                    if (lstColecoesFlash.Count > 0)
                        model.IDColecao = lstColecoesFlash.First().ID_COLECAO;

                    preResult = (from pr in preResult
                                 join fs in lstColecoesFlash on pr.COLECAO equals fs.ID_COLECAO
                                 select pr).ToList();
                }

                var query = 
                    from pr in preResult
                    group pr by
                    new
                    {
                        pr.DESENHO,
                        pr.VARIANTE
                    }
                    into dv
                    select new DesenhoVariante
                    {
                        Desenho = dv.Key.DESENHO,
                        Variante = dv.Key.VARIANTE
                    };

                if (model.TotalPaginas == 0)
                {
                    result = query.OrderBy(x => x.Desenho)
                                        .ThenBy(x => x.Variante)
                                        .ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.Galeria = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();
                }
                else
                {
                    model.Galeria = query.OrderBy(x => x.Desenho)
                                        .ThenBy(x => x.Variante)
                                        .Skip((model.Pagina - 1) * iItensPorPagina)
                                        .Take(iItensPorPagina)
                                        .ToList();
                }
            }
        }

        public ActionResult Lisos(string idcolecao, string nmcolecao, string pagina, string totalpaginas)
        {
            LisosViewModel model = new LisosViewModel();
            model.NMColecao = nmcolecao;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            ActionResult objValidatorResult = this.ValidarTipoPedido(model);
            if (objValidatorResult != null)
            {
                return objValidatorResult;
            }

            using (var ctx = new TIDalutexContext())
            {
                if (idcolecao == "ATUAL")
                {
                    CONFIG_GERAL objResult = ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Atual);
                    model.IDColecao = int.Parse(objResult.PARAMETRO1);
                    model.NMColecao = objResult.PARAMETRO2;
                }
                else if (idcolecao == "POCKET")
                {
                    CONFIG_GERAL objResult = ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Pocket);
                    model.IDColecao = int.Parse(objResult.INT1.ToString());
                    model.NMColecao = objResult.PARAMETRO2;
                }
                else if (idcolecao == "LISOS")
                {
                    model.IDColecao = -1;
                    model.NMColecao = "LISOS";
                }
                else if (idcolecao == null)
                {
                    ModelState.AddModelError("", "Coleção não informada.");
                    return View(model);
                }
                else
                {
                    model.IDColecao = int.Parse(idcolecao);
                }
            }

            if (model.TotalPaginas > 0 || model.IDColecao > 0)
            {
                ObterLisos(model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lisos(LisosViewModel model)
        {
            model.Pagina = 1;
            this.ObterLisos(model);

            return View(model);
        }

        private void ObterLisos(LisosViewModel model)
        {
            Utilitarios utils = new Utilitarios();

            int iItensPorPagina = 24;

            using (TIDalutexContext ctx = new TIDalutexContext())
            {
                int iFiltroReduzido = -1;
                int.TryParse(model.FiltroReduzido, out iFiltroReduzido);
 
                List<Liso> result = null;
                var query =
                    from dc in ctx.VW_LISOS_POR_COLECAO
                    where
                        (dc.COLECAO == model.IDColecao || model.IDColecao == -1)
                        && (dc.COR.Contains(model.FiltroCor) || model.FiltroCor == null)
                        && (dc.ARTIGO.Contains(model.FiltroArtigo) || model.FiltroArtigo == null)
                        && (dc.CODIGO_REDUZIDO == iFiltroReduzido || iFiltroReduzido <= 0)
                    select new Liso
                    {
                        Reduzido = dc.CODIGO_REDUZIDO,
                        Artigo = dc.ARTIGO,
                        Cor = dc.COR,
                        RGB = dc.CAMINHO,
                        ArtigoAtivo = dc.ARTIGO_ATIVO
                    };


                if (model.TotalPaginas == 0)
                {
                    result = query.OrderBy(x => x.Cor).ThenBy(x => x.Artigo).ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.Galeria = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();
                }
                else
                {
                    model.Galeria = query.OrderBy(x => x.Cor)
                                            .ThenBy(x => x.Artigo)
                                            .Skip((model.Pagina - 1) * iItensPorPagina)
                                            .Take(iItensPorPagina)
                                            .ToList();
                }

                model.Galeria.ForEach(delegate(Liso item)
                {
                    item.RGB = utils.RGBConverter(ColorTranslator.FromWin32(int.Parse(item.RGB)));
                    item.ArtigoAtivo = item.ArtigoAtivo;
                });

                if(base.Session_Carrinho != null)
                {
                    model.Galeria.ForEach(delegate(Liso item)
                    {
                        if (base.Session_Carrinho.Itens.Exists(
                            delegate(InserirNoCarrinhoViewModel incluido)
                            {
                                return incluido.Reduzido == item.Reduzido;
                            }))
                        {
                            item.TemNoCarrinho = true;                            
                        }                
                    });
                }
            }
        }

        private void CarregarTiposPedidos(InserirNoCarrinhoViewModel model)
        {
            //using (DalutexContext ctxDalutex = new DalutexContext())
            //{
            //    int[] tiposPedidos = new int[] 
            //    { 
            //        (int)Enums.TiposPedido.AMOSTRA,
            //        (int)Enums.TiposPedido.PILOTAGEM,
            //        (int)Enums.TiposPedido.VENDA,
            //        //(int)Enums.TiposPedido.MOSTRUARIO,
            //    };
               
            //    //model.TiposPedido = new SelectList(ctxDalutex.COML_TIPOSPEDIDOS.Where(x => tiposPedidos.Any(tipo => x.TIPOPEDIDO.Equals(tipo))).ToList().Select(x => new SelectListItem() { Text = x.DESCRICAO, Value = x.TIPOPEDIDO.ToString() }));
            //    model.TiposPedido = ctxDalutex.COML_TIPOSPEDIDOS.Where(x => tiposPedidos.Any(tipo => x.TIPOPEDIDO.Equals(tipo))).ToList();
            //}

            List<TIPO_PEDIDO_USUARIO> tp = new List<TIPO_PEDIDO_USUARIO>();

            List<int> tiposPedidos = new List<int>();

            int idUsuario = (int)base.Session_Usuario.COD_USU;

            using (TIDalutexContext ctx = new TIDalutexContext())
            {
                tp = ctx.TIPO_PEDIDO_USUARIO.Where(a => a.ID_USUARIO == idUsuario).ToList();

                foreach (var item in tp)
                {
                    tiposPedidos.Add(item.TIPO_PEDIDO);
                }
            }

            using(DalutexContext ctxDalutex = new DalutexContext())
            {
                if (!model.EhReacabamento)
                {
                    model.TiposPedido = ctxDalutex.COML_TIPOSPEDIDOS.Where(x => tiposPedidos.Contains(x.TIPOPEDIDO)).ToList();
                }
                else
                {
                    model.TiposPedido = ctxDalutex.COML_TIPOSPEDIDOS.Where(x => x.TIPOPEDIDO == 2).ToList();
                }
            }            
        }

        #endregion

        #region Pedido Reserva

        public ActionResult ItensParaReserva(string filtrocodstudio, string filtrocoddal, string filtrodesenho, string pagina, string totalpaginas)
        {
            ItensParaReservaViewModel model = new ItensParaReservaViewModel();
            model.FiltroCodStudio = filtrocodstudio;
            model.FiltroCodDal = filtrocoddal;
            model.FiltroDesenho = filtrodesenho;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            ActionResult objValidatorResult = this.ValidarTipoPedido(model);
            if (objValidatorResult != null)
            {
                return objValidatorResult;
            }

            if(Session_Carrinho == null)
            {
                Session_Carrinho = new ConclusaoPedidoViewModel();
                Session_Carrinho.IDTipoPedido = (int)Enums.TiposPedido.RESERVA;
            }
            else
            {
                Session_Carrinho.IDTipoPedido = (int)Enums.TiposPedido.RESERVA;
            }

            ObterItensParaReserva(model);

            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_RESERVAS"];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItensParaReserva(ItensParaReservaViewModel model)
        {
            model.Pagina = 1;
            this.ObterItensParaReserva(model);
            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_RESERVAS"];

            return View(model);
        }

        private void ObterItensParaReserva(ItensParaReservaViewModel model)
        {
            int iItensPorPagina = 24;

            using (var ctx = new TIDalutexContext())
            {
                List<ItemReserva> result = null;

                var query =
                        from dc in ctx.VW_DESENHOS_DISP_RESERVA
                        where
                            (dc.DESENHO.StartsWith(model.FiltroDesenho.ToUpper()) || model.FiltroDesenho == null)
                            && (dc.COD_STUDIO.ToUpper().StartsWith(model.FiltroCodStudio.ToUpper()) || model.FiltroCodStudio == null)
                            && (dc.COD_DAL.ToUpper().Contains(model.FiltroCodDal.ToUpper()) || model.FiltroCodDal == null)
                        group dc by
                            new
                            {
                                dc.DESENHO,
                                dc.COD_STUDIO,
                                dc.COD_DAL,
                                dc.ID_CONTROLE_DESENV,
                                dc.ID_STUDIO,
                                dc.ID_ITEM_STUDIO
                            }
                            into dv
                            select new ItemReserva
                            {
                                Desenho = dv.Key.DESENHO.ToUpper(),
                                CodStudio = dv.Key.COD_STUDIO.ToUpper(),
                                CodDal = dv.Key.COD_DAL.ToUpper(),
                                IDControleDesenvolvimento = dv.Key.ID_CONTROLE_DESENV,
                                IDStudio = (int)dv.Key.ID_STUDIO,
                                IDItemStudio = (int)dv.Key.ID_ITEM_STUDIO
                            };


                if (model.TotalPaginas == 0)
                {
                    result = query.OrderByDescending(x => x.CodDal).ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.Galeria = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();
                }
                else
                {
                    model.Galeria = query.OrderByDescending(x => x.CodDal)
                                            .Skip((model.Pagina - 1) * iItensPorPagina)
                                            .Take(iItensPorPagina)
                                            .ToList();
                }
            }
        }

        public ActionResult DesenhosValidaReserva(int pedidoreserva, string pagina, string totalpaginas)
        {
            DesenhosValidaReservaViewModel model = new DesenhosValidaReservaViewModel();
            model.PedidoReserva = pedidoreserva;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];

            int iItensPorPagina = 24;

            List<VW_ITENS_VALIDAR_RESERVA> result = null;

            using (TIDalutexContext ctx = new TIDalutexContext())
            {
                if (model.TotalPaginas == 0)
                {
                    result = ctx.VW_ITENS_VALIDAR_RESERVA
                                        .Where(x => x.PEDIDO_RESERVA == pedidoreserva)
                                        .OrderBy(x => x.DESENHO)
                                        .ThenBy(x => x.VARIANTE)
                                        .ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    
                    

                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.Galeria = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();               
                }
                else
                {
                    model.Galeria = ctx.VW_ITENS_VALIDAR_RESERVA
                                        .Where(x => x.PEDIDO_RESERVA == pedidoreserva)
                                        .OrderBy(x => x.DESENHO)
                                        .ThenBy(x => x.VARIANTE)
                                        .Skip((model.Pagina - 1) * iItensPorPagina)
                                        .Take(iItensPorPagina)
                                        .ToList();
                }
            }

            return View(model);
        }

        public ActionResult ValidaPedidoReserva(
            string filtropedidoreserva
            , string filtrocliente
            , string filtrorepresentante
            , string filtrocodstudio
            , string filtrocoddal
            , string filtrodesenho
            , string pagina
            , string totalpaginas)
        {
            ValidaPedidoReservaViewModel model = new ValidaPedidoReservaViewModel();
            model.FiltroPedidoReserva = filtropedidoreserva;
            model.FiltroCliente = filtrocliente;
            model.FiltroRepresentante = filtrorepresentante;
            model.FiltroCodStudio = filtrocodstudio;
            model.FiltroCodDal = filtrocoddal;
            model.FiltroDesenho = filtrodesenho;

            ActionResult objValidatorResult = this.ValidarTipoPedido(model);
            if (objValidatorResult != null)
            {
                return objValidatorResult;
            }

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
            {
                model.Pagina = 1;

                if (base.Session_Usuario.ID_REPRES > default(int))
                {
                    int IDRepresentante = base.Session_Usuario.ID_REPRES.GetValueOrDefault();
                    using (var ctx = new DalutexContext())
                    {
                        model.FiltroRepresentante = ctx.REPRESENTANTES.Find(IDRepresentante).NOME.Trim();
                    }

                    this.ObterItensValidacaoReserva(model);
                }
            }
            else
            {
                model.Pagina = int.Parse(pagina);

                this.ObterItensValidacaoReserva(model);
            }

            
            return View(model);
        }

        [HttpPost]
        public ActionResult ValidaPedidoReserva(ValidaPedidoReservaViewModel model)
        {
            model.Pagina = 1;
            this.ObterItensValidacaoReserva(model);

            return View(model);
        }

        private void ObterItensValidacaoReserva(ValidaPedidoReservaViewModel model)
        {
            int PedidoReserva = 0;
            int.TryParse(model.FiltroPedidoReserva, out PedidoReserva);
            int iItensPorPagina = 6;

            using (var ctx = new TIDalutexContext())
            {
                List<VW_VALIDAR_RESERVA> result = null;

                if (model.TotalPaginas == 0)
                {
                    result = ctx.VW_VALIDAR_RESERVA
                                .Where(
                                    x =>
                                    (model.FiltroRepresentante == null || x.REPRESENTANTE.StartsWith(model.FiltroRepresentante.ToUpper()))
                                    &&
                                    (model.FiltroCliente == null || x.CLIENTE.Contains(model.FiltroCliente.ToUpper()))
                                    &&
                                    (model.FiltroCodDal == null || x.COD_DAL.Contains(model.FiltroCodDal.ToUpper()))
                                    &&
                                    (model.FiltroDesenho == null || x.DESENHO.StartsWith(model.FiltroDesenho.ToUpper()))
                                    &&
                                    (model.FiltroCodStudio == null || x.COD_STUDIO.StartsWith(model.FiltroCodStudio.ToUpper()))
                                    &&
                                    (PedidoReserva == 0 || x.PEDIDO.Equals(PedidoReserva))
                                ).OrderByDescending(x => x.DATA_EMISSAO)
                                .ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.ListaValidaReserva = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();

                    if ((PedidoReserva != 0) && (result.Count() == 0))
                    {
                        ModelState.AddModelError("", "Pedido informado não tem VARIANTES (coloração) cadastradas no Desenvolvimento");
                    }
                }
                else
                {
                    model.ListaValidaReserva = ctx.VW_VALIDAR_RESERVA
                                .Where(
                                    x =>
                                    (model.FiltroRepresentante == null || x.REPRESENTANTE.StartsWith(model.FiltroRepresentante.ToUpper()))
                                    &&
                                    (model.FiltroCliente == null || x.CLIENTE.StartsWith(model.FiltroCliente.ToUpper()))
                                    &&
                                    (model.FiltroCodDal == null || x.COD_DAL.Contains(model.FiltroCodDal.ToUpper()))
                                    &&
                                    (model.FiltroDesenho == null || x.DESENHO.StartsWith(model.FiltroDesenho.ToUpper()))
                                    &&
                                    (model.FiltroCodStudio == null || x.COD_STUDIO.StartsWith(model.FiltroCodStudio.ToUpper()))
                                    &&
                                    (PedidoReserva == 0 || x.PEDIDO.Equals(PedidoReserva))
                                ).OrderByDescending(x => x.DATA_EMISSAO)
                                .Skip((model.Pagina - 1) * iItensPorPagina)
                                .Take(iItensPorPagina)
                                .ToList();

                    if ((PedidoReserva != 0) && (result.Count() == 0))
                    {
                        ModelState.AddModelError("", "Pedido informado não tem VARIANTES (coloração) cadastrada no Desenvolvimento.");
                    }
                }
            }
        }

        #endregion

        public ActionResult Reacabamento(              
              string FiltroReduzido 
            , string FiltroCodigo    
            , string FiltroArtigo    
            , string FiltroCor    
            , string FiltroTecnologia   
            , string FiltroDesenho
            , string FiltroVariante
            , string pagina
            , string totalpaginas
            )
        {
            ReacabamentoViewModel model = new ReacabamentoViewModel();
            model.FiltroReduzido = FiltroReduzido;
            model.FiltroCodigo = FiltroCodigo;
            model.FiltroArtigo = FiltroArtigo;
            model.FiltroCor = FiltroCor;
            model.FiltroTecnologia = FiltroTecnologia;
            model.FiltroDesenho = FiltroDesenho;
            model.FiltroVariante = FiltroVariante;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
            { 
                model.Pagina = 1; 
            }
            else
            { 
                model.Pagina = int.Parse(pagina);
                this.ObterItensReacabamento(model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Reacabamento(ReacabamentoViewModel model)
        {
            model.Pagina = 1;
            this.ObterItensReacabamento(model);

            return View(model);
        }

        private void ObterItensReacabamento(ReacabamentoViewModel model)
        {
            int iItensPorPagina = 6;

            using (var ctx = new DalutexContext())
            {
                List<VMASCARAPRODUTOACABADO> result = null;

                int iReduzido = 0;

                if (!string.IsNullOrWhiteSpace(model.FiltroReduzido)) 
                {
                    int.TryParse(model.FiltroReduzido, out iReduzido);
                }
       

                if (model.TotalPaginas == 0)
                {
                    result = ctx.VMASCARAPRODUTOACABADO
                                .Where(
                                        x =>
                                        (
                                            (iReduzido == 0 || x.CODIGO_REDUZIDO.Equals(iReduzido))
                                            &&
                                            (model.FiltroCodigo == null || x.COD_COMERCIAL.Contains(model.FiltroCodigo.ToUpper()))
                                            &&
                                            (model.FiltroArtigo == null || x.ARTIGO.Contains(model.FiltroArtigo.ToUpper()))
                                            &&
                                            (model.FiltroCor == null || x.COR.StartsWith(model.FiltroCor.ToUpper()))
                                            &&
                                            (model.FiltroTecnologia == null || x.MAQUINA.StartsWith(model.FiltroTecnologia.ToUpper()))
                                            &&
                                            (model.FiltroDesenho == null || x.DESENHO.StartsWith(model.FiltroDesenho.ToUpper()))
                                            &&
                                            (model.FiltroVariante == null || x.VARIANTE.StartsWith(model.FiltroVariante.ToUpper()))
                                        )                                 
                                ).OrderByDescending(x => x.CODIGO_REDUZIDO)
                                .ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.ListaItensReacabamento = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();

                    if  (result.Count() == 0)
                    {
                        ModelState.AddModelError("", "Item nao encontrado");
                    }
                }
                else
                {
                    model.ListaItensReacabamento = ctx.VMASCARAPRODUTOACABADO
                                .Where(
                                    x =>
                                    (
                                        (iReduzido == 0 || x.CODIGO_REDUZIDO.Equals(iReduzido))
                                        &&
                                        (model.FiltroCodigo == null || x.COD_COMERCIAL.Contains(model.FiltroCodigo.ToUpper()))
                                        &&
                                        (model.FiltroArtigo == null || x.ARTIGO.Contains(model.FiltroArtigo.ToUpper()))
                                        &&
                                        (model.FiltroCor == null || x.COR.StartsWith(model.FiltroCor.ToUpper()))
                                        &&
                                        (model.FiltroTecnologia == null || x.MAQUINA.StartsWith(model.FiltroTecnologia.ToUpper()))
                                        &&
                                        (model.FiltroDesenho == null || x.DESENHO.StartsWith(model.FiltroDesenho.ToUpper()))
                                        &&
                                        (model.FiltroVariante == null || x.VARIANTE.StartsWith(model.FiltroVariante.ToUpper())))
                                ).OrderBy(x => x.COD_COMERCIAL)
                                .Skip((model.Pagina - 1) * iItensPorPagina)
                                .Take(iItensPorPagina)
                                .ToList();

                    //if (result.Count() == 0){ModelState.AddModelError("", "Item nao encontrado.");}
                }
            }
        }
        

        public JsonResult ObterPedidoRefat(string nf)
        {
            string strResult = "";
            
            int inf = int.Parse(nf);

            int cliente = base.Session_Carrinho.ClienteFatura.ID_CLIENTE;
 
            using (TIDalutexContext ctx = new TIDalutexContext())
            {
                VW_NF_PEDIDO_CLIENTE objresult = ctx.VW_NF_PEDIDO_CLIENTE.Where(
                    x => x.ID_CLIENTE == cliente && x.NF == inf).OrderBy(x => x.PEDIDO).FirstOrDefault();

                if(objresult != null)
                {
                    strResult = objresult.PEDIDO.ToString();
                }
            }

            return Json(strResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterNFRefat(string pedido)
        {
            string strResult = "";

            int ipedido = int.Parse(pedido);

            int cliente = base.Session_Carrinho.ClienteFatura.ID_CLIENTE;

            using (TIDalutexContext ctx = new TIDalutexContext())
            {
                VW_NF_PEDIDO_CLIENTE objresult = ctx.VW_NF_PEDIDO_CLIENTE.Where(
                    x => x.ID_CLIENTE == cliente && x.PEDIDO == ipedido).OrderBy(x => x.NF).FirstOrDefault();

                if (objresult != null)
                {
                    strResult = objresult.NF.ToString();
                }
            }

            return Json(strResult, JsonRequestBehavior.AllowGet);
        }

        #region Pronta Entrega

        public ActionResult ItensProntaEntrega( string pagina, string totalpaginas)
        {
            ItensProntaEntregaViewModel model = new ItensProntaEntregaViewModel();
            model.Tipo = Enums.ItemType.ProntaEntrega;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);
            
            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];
	
            this.ObterDesenhosProntaEntrega(model);

            return View(model);
        }

        private void ObterDesenhosProntaEntrega(ItensProntaEntregaViewModel model)
        {            
            int iItensPorPagina = 24;

            using (var ctx = new TIDalutexContext())
            {
                List<VW_ITENS_PE> result = null;

                if (model.TotalPaginas == 0)
                {
                    result = ctx.VW_ITENS_PE.OrderByDescending(x => x.DESENHO)
                                .ThenBy(x => x.VARIANTE)
                                .ToList();

                    decimal dTotal = result.Count / (decimal)iItensPorPagina;
                    model.TotalPaginas = (int)Decimal.Ceiling(dTotal);
                    if (model.TotalPaginas == 0)
                        model.TotalPaginas = 1;

                    model.Galeria = result.Skip((model.Pagina - 1) * iItensPorPagina).Take(iItensPorPagina).ToList();
                }
                else
                {
                    model.Galeria = ctx.VW_ITENS_PE.OrderByDescending(x => x.DESENHO)
                                .ThenBy(x => x.VARIANTE)
                                .Skip((model.Pagina - 1) * iItensPorPagina)
                                .Take(iItensPorPagina)
                                .ToList();
                }
            }
        }

        public ActionResult DetalhesPE(int reduzido)
        {
            DetalhesPEViewModel model = new DetalhesPEViewModel();

            using(TIDalutexContext ctx = new TIDalutexContext())
            {
                model.ListaPecasPE = ctx.VW_LISTA_PECAS_PE.Where(x => x.REDUZIDO == reduzido).OrderBy(x => x.QUALIDADE).ToList();

                model.DetalhesReduzido = ctx.VW_ITENS_PE.Where(x => x.REDUZIDO == reduzido).ToList();
            }

            model.UrlImagens = model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"]; ;
            model.Desenho = model.DetalhesReduzido.First(item => item.REDUZIDO == reduzido).DESENHO;
            model.Variante = model.DetalhesReduzido.First(item => item.REDUZIDO == reduzido).VARIANTE;
           
            return View(model);
        }

        #endregion
    }
}