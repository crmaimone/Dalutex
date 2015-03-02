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
        [ValidateAntiForgeryToken]
        public ActionResult MenuColecoes(MenuColecoesViewModel model)
        {
            using (var ctx = new TIDalutexContext())
            {
                model.Colecoes = ctx.VW_COLECAO.Where(x => x.NOME_COLECAO.ToUpper().Contains(model.Filtro.ToUpper())).OrderBy(x => x.NOME_COLECAO).ToList();
            }

            return View(model);
        }

        public ActionResult EmConstrucao()
        {
            return View();
        }

        public ActionResult DesenhosPorColecao(string IDColecao, string NMColecao, string pagina)
        {
            DesenhosPorColecaoViewModel model = new DesenhosPorColecaoViewModel();
            model.NMColeao = NMColecao;

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            using (var ctx = new TIDalutexContext())
            {
                if( IDColecao == "ATUAL")
                {
                    model.IDColecao = int.Parse(ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Atual).PARAMETRO1);
                    model.NMColeao = ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Atual).PARAMETRO2;
                }
                else if( IDColecao == "POCKET")
                {
                    model.IDColecao = int.Parse(ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Pocket).INT1.ToString());
                    model.NMColeao = ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Pocket).PARAMETRO2;
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
                        (dc.COLECAO == model.IDColecao)
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

        public ActionResult LisosPorColecao(string IDColecao, string NMColecao, string pagina)
        {
            LisosPorColecaoViewModel model = new LisosPorColecaoViewModel();
            model.NMColeao = NMColecao;

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);

            using (var ctx = new TIDalutexContext())
            {
                if (IDColecao == "ATUAL")
                {
                    model.IDColecao = int.Parse(ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Atual).PARAMETRO1);
                }
                else if (IDColecao == "POCKET")
                {
                    model.IDColecao = int.Parse(ctx.CONFIG_GERAL.Find((int)Enums.TipoColecaoEspecial.Pocket).INT1.ToString());
                }
                else if (IDColecao == null)
                {
                    ModelState.AddModelError("", "Coleção não informada.");
                    return View(model);
                }
                else
                {
                    model.IDColecao = int.Parse(IDColecao);
                }

                Utilitarios utils = new Utilitarios();

                var query =
                    from dc in ctx.VW_LISOS_POR_COLECAO
                    where
                        dc.COLECAO == model.IDColecao
                    select new Liso
                    {
                        Reduzido = dc.CODIGO_REDUZIDO,
                        Artigo = dc.ARTIGO,
                        Cor = dc.COR,
                        RGB = dc.CAMINHO
                    };

                model.Galeria = query.OrderBy(x => x.Cor).ThenBy(x => x.Artigo).Skip((model.Pagina - 1) * 24).Take(24).ToList();
                model.Galeria.ForEach(delegate(Liso item)
                {
                    item.RGB = utils.RGBConverter(ColorTranslator.FromWin32(int.Parse(item.RGB)));
                });
            }

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
                    List<VW_TROCA_TEC> lstTrocas = ctx.VW_TROCA_TEC.Where(x => x.ID_TEC_ORIGINAL == iIDTecnologia).ToList();

                    List<int?> lstTecnologias = new List<int?>();
                    lstTecnologias.Add(iIDTecnologia);

                    foreach (VW_TROCA_TEC item in lstTrocas)
                    {
                        lstTecnologias.Add(item.ID_TEC_NOVA);
                    }

                    var query =
                        from ar in ctx.VW_ARTIGOS_DISPONIVEIS
                        where
                            (lstTecnologias.Contains(ar.ID_TECNOLOGIA))
                            && (ar.ID_CARAC_TEC.Equals(null) || !lstCaracteristicas.Contains(ar.ID_CARAC_TEC))
                        group ar by
                            new
                            {
                                ar.ARTIGO,
                                ar.TECNOLOGIA,
                            }
                            into grp
                            select new ArtigoTecnologia
                            {
                                Artigo = grp.Key.ARTIGO,
                                Tecnologia = grp.Key.TECNOLOGIA
                            };

                    string _sql = query.ToString();

                    model.Artigos = query.OrderBy(x => x.Tecnologia).ThenBy(x => x.Artigo).ToList();
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

        private void CarregarTiposPedidos(InserirNoCarrinhoViewModel model)
        {
            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                int[] tiposPedidos = new int[] 
                { 
                    (int)Enums.TiposPedido.AMOSTRA,
                    (int)Enums.TiposPedido.PILOTAGEM,
                    (int)Enums.TiposPedido.VENDA
                };

                model.TiposPedido = ctxDalutex.COML_TIPOSPEDIDOS.Where(x => tiposPedidos.Any(tipo => x.TIPOPEDIDO.Equals(tipo))).ToList();
            }
        }

        public ActionResult InserirNoCarrinho(
            string IDColecao
            , string NMColecao
            , string pagina
            , string desenho
            , string variante
            , string artigo
            , string tecnologia
            , string cor
            , string modo
            , string rgb
            , int reduzido)
        {
            InserirNoCarrinhoViewModel model = new InserirNoCarrinhoViewModel();
            model.Desenho = desenho;
            model.Variante = variante;
            model.Artigo = artigo;
            model.TecnologiaPorExtenso = tecnologia;
            model.IDColecao = IDColecao;
            model.NMColecao = NMColecao;
            model.Cor = cor;
            model.RGB = rgb;
            model.Reduzido = reduzido;

            if(pagina != null)
                model.Pagina = int.Parse(pagina);

            if (modo == "A")//Alterando item
            {
                model = base.Session_Carrinho.Itens.Where(x => x.Equals(model)).First();
            }

            model.Modo = modo;

            using (var ctxTI = new TIDalutexContext())
            {
                using (var ctx = new DalutexContext())
                {
                    VMASCARAPRODUTOACABADO objReduzido = ctx.VMASCARAPRODUTOACABADO.Where(
                            x =>
                                x.ARTIGO == model.Artigo
                                && x.DESENHO == model.Desenho
                                && x.VARIANTE == model.Variante
                                && x.MAQUINA == model.Tecnologia
                            ).FirstOrDefault();

                    if (objReduzido != null && objReduzido.CODIGO_REDUZIDO > default(int))
                        model.Reduzido = objReduzido.CODIGO_REDUZIDO;
                    else
                        model.Reduzido = -2; //Deixar o JOB buscar mais tarde ou criar o reduzido?
                }

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
                                ModelState.AddModelError("", "UNIDADE DE MEDIDA INVÁLIDA.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "UNIDADE DE MEDIDA NÃO ENCONTRADA.");
                        }
                    }
                }
            }

            this.CarregarTiposPedidos(model);

            model.ObterTipoPedido = base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserirNoCarrinho(InserirNoCarrinhoViewModel model)
        {
            bool hasErrors = false;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ValorPadrao <= 0)
                    {
                        ModelState.AddModelError("", "NÃO É PERMITIDO SALVAR SEM VALOR PADRÃO DEFINIDO.");
                        hasErrors = true;
                    }
                    if (model.Pecas <= 0)
                    {
                        ModelState.AddModelError("", "Campo \"PEÇAS\" NÃO PODE SER MENOR OU IGUAL A ZERO.");
                        hasErrors = true;
                    }
                    if(model.Preco <= 0)
                    {
                        ModelState.AddModelError("", "Campo \"PREÇO\" NÃO PODE SER MENOR OU IGUAL A ZERO.");
                        hasErrors = true;
                    }

                    if(hasErrors)
                    {
                        if(base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                        {
                            model.ObterTipoPedido = true;
                            this.CarregarTiposPedidos(model);
                        }

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

                    if(model.Modo == "I")//Inclusão
                    {
                        if (base.Session_Carrinho.Itens.Contains(model))
                        {
                            ModelState.AddModelError("", "Este item já foi incluído no carrinho.");
                            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                            {
                                model.ObterTipoPedido = true;
                                this.CarregarTiposPedidos(model);
                            } 
                            return View(model);
                        }

                        base.Session_Carrinho.Itens.Add(model);

                        if(!string.IsNullOrWhiteSpace(model.Cor))
                            return RedirectToAction("LisosPorColecao", "Pedido", new { IDColecao = model.IDColecao, NMColecao = model.NMColecao, pagina = model.Pagina});
                        else
                            return RedirectToAction("ArtigosDisponiveis", "Pedido", new { desenho = model.Desenho, variante = model.Variante, });
                    }
                    else
                    {
                        int index = base.Session_Carrinho.Itens.FindIndex(x => x.Equals(model));
                        if (index < 0)
                        {
                            ModelState.AddModelError("", "Este item não foi encontrado no carrinho para alteração.");
                            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
                            {
                                model.ObterTipoPedido = true;
                                this.CarregarTiposPedidos(model);
                            }
                            return View(model);
                        }

                        base.Session_Carrinho.Itens[index] = model;
                        return RedirectToAction("Carrinho", "Pedido");
                    }
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            // If we got this far, something failed, redisplay form
            if (base.Session_Carrinho == null || base.Session_Carrinho.IDTipoPedido < 0)
            {
                model.ObterTipoPedido = true;
                this.CarregarTiposPedidos(model);
            } 
            return View(model);
        }

        [HttpPost]
        public ActionResult ExcluirItemCarrinho(InserirNoCarrinhoViewModel model)
        {
            if(base.Session_Carrinho != null && base.Session_Carrinho.Itens != null)
            {
                if(base.Session_Carrinho.Itens.Remove(model))
                {
                    return RedirectToAction("Carrinho");
                }
                else
                {
                    return RedirectToAction("ErrorMessage", new { message = "Este item não foi encontrado no carrinho para excluir.", title = "EXCLUSÃO DO CARRINHO" });
                }
            }
            else
            {
                return RedirectToAction("ErrorMessage", new { message="Não há itens no carrinho para excluir.", title ="EXCLUSÃO DO CARRINHO" });
            }
        }

        public ActionResult Carrinho()
        {
            ViewBag.Carrinho = base.Session_Carrinho;
            ViewBag.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];

            return View();
        }

        private ConclusaoPedidoViewModel ConclusaoPedidoCarregarListas(ConclusaoPedidoViewModel model)
        {
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

            foreach (InserirNoCarrinhoViewModel item in model.Itens)
            {
                model.TotalPedido += item.ValorTotalItem;
            }
            return model;
        }

        public ActionResult ConclusaoPedido(string IDTransportadora)
        {
            Session_Carrinho.IDTransportadora = int.Parse(IDTransportadora);

            ConclusaoPedidoViewModel model = new ConclusaoPedidoViewModel();
            ConclusaoPedidoCarregarListas(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConclusaoPedido(ConclusaoPedidoViewModel model)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session_Carrinho.Itens == null || Session_Carrinho.Itens.Count == 0)
                    {
                        ModelState.AddModelError("", "Não é permitido concluir o pedido sem itens.");
                        this.ConclusaoPedidoCarregarListas(model);
                        return View(model);
                    }

                    foreach (InserirNoCarrinhoViewModel item in Session_Carrinho.Itens)
                    {
                        model.TotalPedido += item.ValorTotalItem;
                    }

                    #region Validações

                    bool hasErrors = false;

                    if(model.IDCondicoesPagto <= 0)
                    {
                        ModelState.AddModelError("", "Por favor informe a condição de pagamento.");
                        hasErrors = true;
                    }
                    if (model.IDMoedas < 0)
                    {
                        ModelState.AddModelError("", "Por favor informe a moeda.");
                        hasErrors = true;
                    }
                    if (model.IDViasTransporte <= 0)
                    {
                        ModelState.AddModelError("", "Por favor informe a via de transporte.");
                        hasErrors = true;
                    }
                    if (model.IDFretes <= 0)
                    {
                        ModelState.AddModelError("", "Por favor informe o tipo de frete.");
                        hasErrors = true;
                    }
                    if (model.IDCanaisVenda <= 0)
                    {
                        ModelState.AddModelError("", "Por favor informe o canal de venda.");
                        hasErrors = true;
                    }
                    if (model.IDTiposAtendimento <= 0)
                    {
                        ModelState.AddModelError("", "Por favor informe o tipo de atendimento.");
                        hasErrors = true;
                    }
                    if (string.IsNullOrWhiteSpace(model.IDQualidadeComercial))
                    {
                        ModelState.AddModelError("", "Por favor informe a qualidade comercial.");
                        hasErrors = true;
                    }

                    if( base.Session_Carrinho.IDTipoPedido.Equals((int) Enums.TiposPedido.VENDA)
                        && !model.IDCanaisVenda.Equals((int) Enums.CanaisVenda.TELEVENDAS)
                        && !model.IDCondicoesPagto.Equals((int) Enums.CondicoesPagamento.CORTESIA))
                    {
                        
                        int numParcelas = 0;
                        using(var ctx = new TIDalutexContext() )
                        {
                            numParcelas = ctx.VW_CONDICAO_PGTO.Find(model.IDCondicoesPagto).PARCELAS;
                        }
                        
                        int fatorMultiplicacao = 0;
                        decimal valorMinimoParcelas = decimal.Parse(ConfigurationManager.AppSettings["VALOR_PARCELA_MINIMA"]);

                        if (model.IDQualidadeComercial == Enums.QualidadeComercial.A.ToString())
                        {
                            fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.A;
                        }
                        else if (model.IDQualidadeComercial == Enums.QualidadeComercial.B.ToString())
                        {
                            fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.B;
                        }
                        else if (model.IDQualidadeComercial == Enums.QualidadeComercial.C.ToString())
                        {
                            fatorMultiplicacao = (int)Enums.FatorMultiplicacaoQualidadeComercial.C;
                        }

                        if (model.IDTiposAtendimento.Equals((int)Enums.TiposAtendimento.EstampaCompleta))
                        {
                            List<KeyValuePair<string,decimal>> lstConsolidada = base.Session_Carrinho.Itens
                                .GroupBy(g => g.Desenho)
                                .Select(consolidado => new KeyValuePair<string, decimal>(consolidado.First().Desenho, consolidado.Sum(s => s.ValorTotalItem)))
                                .ToList();

                            bool isValid = true;

                            foreach(KeyValuePair<string,decimal> item in lstConsolidada)
                            {
                                if (((item.Value / fatorMultiplicacao ) / numParcelas) < valorMinimoParcelas)
                                {
                                    ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C") + " para o desenho: " + item.Key);
                                    isValid = false;
                                }
                            }

                            if(!isValid)
                            {
                                hasErrors = true;
                            }
                        }
                        else if(model.IDTiposAtendimento.Equals((int)Enums.TiposAtendimento.PedidoCompleto))
                        {
                            if (((model.TotalPedido / fatorMultiplicacao ) / numParcelas) < valorMinimoParcelas)
                            {
                                ModelState.AddModelError("", "Valor mínimo das parcelas é inferior a " + valorMinimoParcelas.ToString("C"));
                                hasErrors = true;
                            }
                        }
                        else if(model.IDTiposAtendimento.Equals((int)Enums.TiposAtendimento.CompletoPorArtigo))
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
                        else if (model.IDTiposAtendimento.Equals((int)Enums.TiposAtendimento.PedidoIncompleto))
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

                        if(hasErrors)
                        {
                            this.ConclusaoPedidoCarregarListas(model);
                            return View(model);
                        }
                    }

                    #endregion

                    int iNUMERO_PEDIDO_BLOCO = default(int);

                    while (iNUMERO_PEDIDO_BLOCO == default(int))
                    {
                        using (var ctx = new TIDalutexContext())
                        {
                            PROXIMO_NUMERO_PEDIDO reservar = ctx.PROXIMO_NUMERO_PEDIDO.Where(x => x.DISPONIVEL == 0).OrderBy(x => x.NUMERO_PEDIDO).First();
                            iNUMERO_PEDIDO_BLOCO = reservar.NUMERO_PEDIDO;
                            reservar.DISPONIVEL = 1;
                            ctx.SaveChanges();
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
                            COR = item.Cor,
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
                            REDUZIDO_ITEM = item.Reduzido, // REDUZIDO_0 -- VER COM CASSIANO;
                            UM = item.UnidadeMedida,
                            VALOR_TOTAL = item.Preco * item.Quantidade,
                            VARIANTE = item.Variante
                        };

                        lstItens.Add(objItem);
                    }

                    using(var ctx = new TIDalutexContext())
                    {
                        ctx.PRE_PEDIDO.Add(objPrePedido);

                        #region Criticas

                        #region Liberação financeira

                        ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                        {
                            NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                            COD_CRITICA = (decimal)Enums.TiposCritica.LiberacaoFinanceira,
                            FLG_STATUS = "C"
                        });

                        #endregion

                        #region Item sem reduzido

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

                        #endregion

                        #region Preço divergente


                        foreach (PRE_PEDIDO_ITENS item in lstItens)
                        {
                            if(item.REDUZIDO_ITEM != -2)//TEM REDUZIDO
                            {
                                decimal dReduzido = item.REDUZIDO_ITEM.GetValueOrDefault();
                                using (var ctxDlx = new DalutexContext())
                                {
                                    var queryParametros = from vw in ctxDlx.VMASCARAPRODUTOACABADO
                                                join co in ctxDlx.COLECOES on vw.COLECAO equals co.COLECAO
                                                where vw.CODIGO_REDUZIDO == dReduzido
                                                select new ParametrosPreco{
                                                    E_Exclusivo = vw.EXCL == "E" ? true : false,
                                                    Comissao = co.ID_COLECAO == "POCK" ? 3 : 4,
                                                    IDColecao = vw.COLECAO
                                                };

                                    ParametrosPreco objParametro = queryParametros.First();


                                    #region Call Function Oracle

                                    int iCodCondPgto = 0;//TODO: BUSCAR NA FUNÇÃO DO ORACLE
                                    iCodCondPgto = ctx.Database.SqlQuery<int>("select ti_dalutex.pega_consicao_pgto(:p0) from dual", 1).FirstOrDefault();
                                        
                                    #endregion

                                    int? iColecaoAtual = int.Parse(ctx.CONFIG_GERAL.Where(y => y.ID_CONFIG == (int)Enums.TipoColecaoEspecial.Atual).First().PARAMETRO1);

                                    TABELAPRECOITEM objPreco = ctx.TABELAPRECOITEM.Where(x =>
                                            x.COLECAO == ( objParametro.E_Exclusivo ? objParametro.IDColecao : iColecaoAtual )
                                            && x.QUALIDADECOMERCIAL == model.IDQualidadeComercial
                                            && x.COD_COND_PAGTO == iCodCondPgto
                                            && x.EST_LISO == "E"
                                            && x.COMISSAO == objParametro.Comissao
                                            && x.ARTIGO == item.ARTIGO
                                        ).FirstOrDefault();

                                    //Se não tem preço, crítica. Se tem preço e ele é diferente do informado pelo representante, crítica.
                                    if(objPreco == null || decimal.Round(objPreco.VALOR.GetValueOrDefault(), 2, MidpointRounding.ToEven) != item.PRECO_UNIT.GetValueOrDefault())
                                    {
                                        ctx.PRE_PEDIDO_CRITICA.Add(new PRE_PEDIDO_CRITICA()
                                        {
                                            NUMERO_PRE_PEDIDO = objPrePedido.NUMERO_PEDIDO_BLOCO,
                                            COD_CRITICA = (decimal)Enums.TiposCritica.PrecoDiferente,
                                            FLG_STATUS = "C",
                                            ITEM_PEDIDO = item.ITEM,
                                            VALOR_TAB = decimal.Round(objPreco != null ? objPreco.VALOR.GetValueOrDefault() : 0, 2, MidpointRounding.ToEven),
                                            VALOR_ITEM = item.PRECO_UNIT
                                        });
                                    }
                                }
                            }
                        }

                        foreach (PRE_PEDIDO_ITENS item in lstItens)
                        {
                            ctx.PRE_PEDIDO_ITENS.Add(item);
                        }

                        #endregion

                        #endregion

                        ctx.SaveChanges();
                    }

                    #region EnviarPDF

                    //MemoryStream ms = new MemoryStream(new Relatorios().GerarEspelhoPedido());
                    //Attachment anexo = new Attachment(ms, "Pedido_" + iNUMERO_PEDIDO_BLOCO.ToString() + ".pdf", "application/pdf");
                    //Utilitarios util = new Utilitarios();
                    //util.EnviaEmail("crmaimone@gmail.com", "Novo pedido", "Segue novo pedido", anexo);

                    #endregion

                    base.Session_Carrinho = null;

                    return RedirectToAction("ConfirmacaoPedido", "Pedido", new { NumeroPedido = iNUMERO_PEDIDO_BLOCO.ToString() });
                }
            }
            catch (Exception ex)
            {
                base.Handle(ex);
            }
            // If we got this far, something failed, redisplay form
            
            this.ConclusaoPedidoCarregarListas(model);
            return View(model);
        }

        public ActionResult ConfirmacaoPedido(string NumeroPedido)
        {
            ViewBag.NumeroPedido = NumeroPedido;
            return View();
        }

        public EspelhoPedidoPdf EspelhoPedido(string NumeroPedido)
        {
            return new EspelhoPedidoPdf() { IDPedidoBloco = decimal.Parse(NumeroPedido) };
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