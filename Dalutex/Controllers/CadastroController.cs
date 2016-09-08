using Dalutex.Models;
using Dalutex.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalutex.Controllers
{
    public class CadastroController : BaseController
    {
        public ActionResult ProximoPasso(
            string IDRepresentante,
            string IDClienteFatura,
            string IDClienteEntrega,
            string IDTransportadora,
            string IDQualidadeComercial,
            string IDMoeda,
            string IDCondicao,
            string IDCanal,
            string IDVia,
            string IDFrete,
            string IDTipo
        )
        {

            if (IDRepresentante != null)
            {
                using (var ctx = new DalutexContext())
                {
                    Session_Carrinho.Representante = ctx.REPRESENTANTES.Find(int.Parse(IDRepresentante));
                    Session_Carrinho.ClienteFatura = null;
                    Session_Carrinho.ClienteEntrega = null;
                }
            }
            else if (IDClienteFatura != null)
            {
                using (var ctx = new TIDalutexContext())
                {
                    int iIDClienteFatura = int.Parse(IDClienteFatura);
                    Session_Carrinho.ClienteFatura = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == iIDClienteFatura).First();
                }
            }
            else if (IDClienteEntrega != null)
            {
                using (var ctx = new TIDalutexContext())
                {
                    int iIDClienteEntrega = int.Parse(IDClienteEntrega);
                    Session_Carrinho.ClienteEntrega = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == iIDClienteEntrega).First();
                }
            }
            if (IDTransportadora != null) /*Não é ELSE pq os dois parâmetros são passados*/
            {
                using (var ctx = new DalutexContext())
                {
                    Session_Carrinho.Transportadora = ctx.TRANSPORTADORAS.Find(int.Parse(IDTransportadora));
                }
            }
            else if (IDQualidadeComercial != null)
            {
                Session_Carrinho.QualidadeComercial = new KeyValuePair<string, string>(IDQualidadeComercial, IDQualidadeComercial);
            }
            else if (IDMoeda != null)
            {
                using (DalutexContext ctxDalutex = new DalutexContext())
                {
                    Session_Carrinho.Moeda = ctxDalutex.CADASTRO_MOEDAS.Find(int.Parse(IDMoeda));
                }
            }
            else if (IDCondicao != null)
            {
                int iIDCondicao = int.Parse(IDCondicao);
                using (TIDalutexContext ctxTI = new TIDalutexContext())
                {
                    Session_Carrinho.CondicaoPagto = ctxTI.VW_CONDICAO_PGTO.Where(x => x.ID_COND == iIDCondicao).First();
                }
            }
            else if (IDCanal != null)
            {
                using (DalutexContext ctxDalutex = new DalutexContext())
                {
                    Session_Carrinho.CanailVenda = ctxDalutex.CANAIS_VENDA.Find(int.Parse(IDCanal));
                }
            }
            else if (IDVia != null)
            {
                using (DalutexContext ctxDalutex = new DalutexContext())
                {
                    Session_Carrinho.ViaTransporte = ctxDalutex.COML_VIASTRANSPORTE.Find(int.Parse(IDVia));
                }
            }
            else if (IDFrete != null)
            {
                using (DalutexContext ctxDalutex = new DalutexContext())
                {
                    Session_Carrinho.Frete = ctxDalutex.COML_TIPOSFRETE.Find(int.Parse(IDFrete));
                }
            }
            else if (IDTipo != null)
            {
                using (TIDalutexContext ctxTI = new TIDalutexContext())
                {
                    Session_Carrinho.TipoAtendimento = ctxTI.PRE_PEDIDO_ATEND.Find(int.Parse(IDTipo));
                }
            }

            if (base.Session_Carrinho.Representante == null || base.Session_Carrinho.Representante.IDREPRESENTANTE <= 0)
            {
                return RedirectToAction("Representantes");
            }
            else if (base.Session_Carrinho.ClienteFatura == null || base.Session_Carrinho.ClienteFatura.ID_CLIENTE <= 0)
            {
                return RedirectToAction("ClientesFatura");
            }
            else if (base.Session_Carrinho.IDTipoPedido != (int)Enums.TiposPedido.RESERVA)
            {
                if (base.Session_Carrinho.ClienteEntrega == null || base.Session_Carrinho.ClienteEntrega.ID_CLIENTE <= 0)
                {
                    return RedirectToAction("ClientesEntrega");
                }                                                              
                else if  ( (base.Session_Carrinho.ClienteFatura != null) && (base.Session_Carrinho.ClienteEntrega != null))
                {
                    string sClienteEntrega = base.Session_Carrinho.ClienteFatura.ID_CLIENTE.ToString();

                    using (var ctx = new TIDalutexContext())
                    {
                        sClienteEntrega = sClienteEntrega.PadLeft(6, '0');
    
                        bool? EhOpTriangular = ctx.VW_CLIE_OPER_TRINGULAR.Where(x => x.COD_CLIENTE.Trim() == sClienteEntrega).First().OPERACAO_TRIANGULAR;

                        if (EhOpTriangular == true)
                        {
                            if (base.Session_Carrinho.ClienteEntrega.ID_CLIENTE == base.Session_Carrinho.ClienteFatura.ID_CLIENTE)
                            {                                                               
                                return RedirectToAction("ClientesEntrega");
                            }
                        }
                    }
                }

                if (base.Session_Carrinho.Transportadora == null || base.Session_Carrinho.Transportadora.IDTRANSPORTADORA <= 0)
                {
                    return RedirectToAction("Transportadora");
                }
                else if (base.Session_Carrinho.QualidadeComercial.Key == null)
                {
                    return RedirectToAction("QualidadeComercial");
                }
                else if (base.Session_Carrinho.Moeda == null || base.Session_Carrinho.Moeda.CODIGOMOEDA < 0)
                {
                    return RedirectToAction("Moeda");
                }
                else if (base.Session_Carrinho.CondicaoPagto == null || base.Session_Carrinho.CondicaoPagto.ID_COND <= 0)
                {
                    return RedirectToAction("CondicaoPgto");
                }
                else if (base.Session_Carrinho.CanailVenda == null || base.Session_Carrinho.CanailVenda.CANAL_VENDA <= 0)
                {
                    return RedirectToAction("CanalVendas");
                }
                else if (base.Session_Carrinho.ViaTransporte == null || base.Session_Carrinho.ViaTransporte.VIATRANSPORTE <= 0)
                {
                    return RedirectToAction("ViaTransporte");
                }
                else if (base.Session_Carrinho.Frete == null || base.Session_Carrinho.Frete.TIPOFRETE <= 0)
                {
                    return RedirectToAction("Frete");
                }
                else if (base.Session_Carrinho.TipoAtendimento == null || base.Session_Carrinho.TipoAtendimento.COD_ATEND <= 0)
                {
                    return RedirectToAction("TipoAtendimento");
                }
            }

            return RedirectToAction("ConclusaoPedido", "Pedido");
        }

        public ActionResult Representantes()
        {
            PesquisaRepresentantesViewModel model = new PesquisaRepresentantesViewModel();
            model.Representantes = new List<REPRESENTANTES>();
            int IDRepresentante = 0;

            if (base.Session_Carrinho.Representante != null && base.Session_Carrinho.Representante.IDREPRESENTANTE > default(int))
            {
                IDRepresentante = base.Session_Carrinho.Representante.IDREPRESENTANTE;
            }
            else
            {
                if (base.Session_Usuario.ID_REPRES > default(int))
                {
                    IDRepresentante = base.Session_Usuario.ID_REPRES.GetValueOrDefault();
                }
            }

            if (IDRepresentante > default(int))
            {
                using (var ctx = new DalutexContext())
                {
                    REPRESENTANTES objRepresentante = ctx.REPRESENTANTES.Find(IDRepresentante);
                    COML_CONTATOS contato = ctx.COML_CONTATOS.Where(x => x.IDPESSOAFJ == objRepresentante.IDREPRESENTANTE && x.EMAIL.Trim() != "").FirstOrDefault();
                    if (contato != null)
                    {
                        contato.EMAIL.Trim();
                        objRepresentante.CONTATO = contato;
                    }

                    model.Representantes.Add(objRepresentante);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Representantes(PesquisaRepresentantesViewModel model)
        {
            using (var ctx = new DalutexContext())
            {
                model.Representantes = ctx.REPRESENTANTES.Where(x => x.NOME.Contains(model.Filtro.ToUpper())).OrderBy(x => x.NOME).ToList();
            }

            return View(model);
        }

        public ActionResult ClientesFatura(string IDClienteFatura)
        {
            PesquisaClientesFaturaViewModel model = new PesquisaClientesFaturaViewModel();
            model.IDTipoPedido = Session_Carrinho.IDTipoPedido;

            if (IDClienteFatura == null && base.Session_Carrinho.ClienteFatura != null)
                IDClienteFatura = base.Session_Carrinho.ClienteFatura.ID_CLIENTE.ToString();

            int iIDClienteFatura;

            if (int.TryParse(IDClienteFatura, out iIDClienteFatura) && iIDClienteFatura > 0)
            {
                using (var ctx = new TIDalutexContext())
                {
                    int idRepresentante = base.Session_Carrinho.Representante.IDREPRESENTANTE;
                    VW_CLIENTE_TRANSP objCliente = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == iIDClienteFatura && x.ID_REP == idRepresentante).First();
                    model.ClientesFatura = new List<VW_CLIENTE_TRANSP>();
                    model.ClientesFatura.Add(objCliente);
                    model.Filtro = objCliente.NOME;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientesFatura(PesquisaClientesFaturaViewModel model)
        {
            model.IDTipoPedido = Session_Carrinho.IDTipoPedido;

            using (var ctx = new TIDalutexContext())
            {
                int idRepresentante = base.Session_Carrinho.Representante.IDREPRESENTANTE;
                model.ClientesFatura = ctx.VW_CLIENTE_TRANSP.Where(x => x.NOME.Contains(model.Filtro.ToUpper()) && x.ID_REP == idRepresentante).OrderBy(x => x.NOME).ToList();
            }

            return View(model);
        }

        public ActionResult ClientesEntrega(string IDClienteEntrega)
        {           
            PesquisaClientesEntregaViewModel model = new PesquisaClientesEntregaViewModel();

            string sClienteEntrega = base.Session_Carrinho.ClienteFatura.ID_CLIENTE.ToString();

            //PadLeft(5, '0');
            sClienteEntrega = sClienteEntrega.PadLeft(6, '0');

            using (var ctx = new TIDalutexContext())
            {
                bool? EhOpTriangular = ctx.VW_CLIE_OPER_TRINGULAR.Where(x => x.COD_CLIENTE.Trim() == sClienteEntrega).First().OPERACAO_TRIANGULAR;

                if (EhOpTriangular == true)
                {                                                                            
                    ModelState.AddModelError("", "CLIENTE DE FATURA [" + base.Session_Carrinho.ClienteFatura.NOME + "]" + " TEM OPERAÇÃO TRIANGULAR.");
                    ModelState.AddModelError("", "O CLIENTE DE ENTREGA DEVE SER DIFERENTE DO CLIENTE DA FATURA.");
                 
                    return View(model);                         
                }
                else
                {
                    if (IDClienteEntrega == null && base.Session_Carrinho.ClienteEntrega != null)
                        IDClienteEntrega = base.Session_Carrinho.ClienteEntrega.ID_CLIENTE.ToString();

                    int iIDClienteEntrega;
                    int.TryParse(IDClienteEntrega, out iIDClienteEntrega);

                    if (iIDClienteEntrega == 0)
                    {
                        if (base.Session_Carrinho.ClienteFatura != null && base.Session_Carrinho.ClienteFatura.ID_CLIENTE > 0)
                        {
                            iIDClienteEntrega = base.Session_Carrinho.ClienteFatura.ID_CLIENTE;
                        }
                    }

                    if (iIDClienteEntrega > 0)
                    {                       
                        VW_CLIENTE_TRANSP _ClienteEntrega = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == iIDClienteEntrega).First();
                        model.ClientesEntrega = new List<VW_CLIENTE_TRANSP>();
                        model.ClientesEntrega.Add(_ClienteEntrega);
                        model.Filtro = _ClienteEntrega.NOME;                       
                    }                   
                }
            }
            return View(model);                     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientesEntrega(PesquisaClientesEntregaViewModel model)
        {           
            using (var ctx = new TIDalutexContext())
            {
                int idRepresentante = base.Session_Carrinho.Representante.IDREPRESENTANTE;
                model.ClientesEntrega = ctx.VW_CLIENTE_TRANSP.Where(x => x.NOME.Contains(model.Filtro.ToUpper()) && x.ID_REP == idRepresentante).OrderBy(x => x.NOME).ToList();
            }

            return View(model);
        }

        public ActionResult Transportadora(string IDTransportadora)
        {
            PesquisaTransportadoraViewModel model = new PesquisaTransportadoraViewModel();

            if (IDTransportadora == null && base.Session_Carrinho.Transportadora != null)
                IDTransportadora = base.Session_Carrinho.Transportadora.IDTRANSPORTADORA.ToString();

            int iIDTransportadora;

            if (int.TryParse(IDTransportadora, out iIDTransportadora) && iIDTransportadora > 0)
            {
                using (var ctx = new DalutexContext())
                {
                    TRANSPORTADORAS _transportadora = ctx.TRANSPORTADORAS.Find(iIDTransportadora);

                    model.Transportadoras = new List<TRANSPORTADORAS>();
                    model.Transportadoras.Add(_transportadora);

                    model.Filtro = _transportadora.NOME;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transportadora(PesquisaTransportadoraViewModel model)
        {
            using (var ctx = new DalutexContext())
            {
                model.Transportadoras = ctx.TRANSPORTADORAS.Where(x => x.NOME.Contains(model.Filtro.ToUpper())).OrderBy(x => x.NOME).ToList();
            }
            return View(model);
        }

        public ActionResult QualidadeComercial(string Qualidade)
        {
            PesquisaQualidadeComercialViewModel model = new PesquisaQualidadeComercialViewModel();

            model.Qualidades = new List<KeyValuePair<string, string>>();
            model.Qualidades.Add(new KeyValuePair<string, string>(Enums.QualidadeComercial.A.ToString(), Enums.QualidadeComercial.A.ToString()));
            model.Qualidades.Add(new KeyValuePair<string, string>(Enums.QualidadeComercial.B.ToString(), Enums.QualidadeComercial.B.ToString()));
            model.Qualidades.Add(new KeyValuePair<string, string>(Enums.QualidadeComercial.C.ToString(), Enums.QualidadeComercial.C.ToString()));

            if (Qualidade == null && base.Session_Carrinho.QualidadeComercial.Key != null)
                model.QualidadeSelecionada = base.Session_Carrinho.QualidadeComercial.Key.ToString();
            else
                model.QualidadeSelecionada = Qualidade;

            return View(model);
        }

        public ActionResult Moeda(string Moeda)
        {
            PesquisaMoedaViewModel model = new PesquisaMoedaViewModel();

            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                model.Moedas = ctxDalutex.CADASTRO_MOEDAS.ToList();
            }

            if (Moeda == null && base.Session_Carrinho.Moeda != null)
                model.MoedaSelecionada = base.Session_Carrinho.Moeda.CODIGOMOEDA.ToString();
            else
                model.MoedaSelecionada = Moeda;

            return View(model);
        }

        public ActionResult CondicaoPgto(string Condicao)
        {
            PesquisaCondicaoPagamentoViewModel model = new PesquisaCondicaoPagamentoViewModel();

            using (TIDalutexContext ctxTI = new TIDalutexContext())
            {
                model.Condicoes = ctxTI.VW_CONDICAO_PGTO.ToList();
            }

            if (Condicao == null && base.Session_Carrinho.CondicaoPagto != null)
                model.CondicaoSelecionada = base.Session_Carrinho.CondicaoPagto.ID_COND.ToString();
            else
                model.CondicaoSelecionada = Condicao;

            return View(model);
        }

        public ActionResult CanalVendas(string Canal)
        {
            PesquisaCanalVendasViewModel model = new PesquisaCanalVendasViewModel();

            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                model.Canais = ctxDalutex.CANAIS_VENDA.ToList();
            }

            if (Canal == null && base.Session_Carrinho.CanailVenda != null)
                model.CanalSelecionado = base.Session_Carrinho.CanailVenda.CANAL_VENDA.ToString();
            else
                model.CanalSelecionado = Canal;

            return View(model);
        }

        public ActionResult ViaTransporte(string Via)
        {
            PesquisaViaTransporteViewModel model = new PesquisaViaTransporteViewModel();

            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                model.ViasTransporte = ctxDalutex.COML_VIASTRANSPORTE.ToList();
            }

            if (Via == null && base.Session_Carrinho.ViaTransporte != null)
                model.ViaSelecionada = base.Session_Carrinho.ViaTransporte.VIATRANSPORTE.ToString();
            else
                model.ViaSelecionada = Via;

            return View(model);
        }

        public ActionResult Frete(string Frete)
        {
            PesquisaFreteViewModel model = new PesquisaFreteViewModel();

            using (DalutexContext ctxDalutex = new DalutexContext())
            {
                model.Fretes = ctxDalutex.COML_TIPOSFRETE.ToList();
            }

            if (Frete == null && base.Session_Carrinho.Frete != null)
                model.FreteSelecionado = base.Session_Carrinho.Frete.TIPOFRETE.ToString();
            else
                model.FreteSelecionado = Frete;

            return View(model);
        }

        public ActionResult TipoAtendimento(string Tipo)
        {
            PesquisaTipoAtendimentoViewModel model = new PesquisaTipoAtendimentoViewModel();

            using (TIDalutexContext ctxTI = new TIDalutexContext())
            {
                model.TiposAtendimento = ctxTI.PRE_PEDIDO_ATEND.ToList();
            }

            if (Tipo == null && base.Session_Carrinho.TipoAtendimento != null)
                model.TipoSelecionado = base.Session_Carrinho.TipoAtendimento.COD_ATEND.ToString();
            else
                model.TipoSelecionado = Tipo;

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult UploadImage(string cod_studio, string cod_dal, string studio, string desenho)
        {
            UploadImageModelView model = new UploadImageModelView();

            model.CodStudio = cod_studio;
            model.CodDal = cod_dal;
            model.Studio = studio;
            model.Desenho = desenho;

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult UploadSucess()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UploadImage(HttpPostedFileBase desenho, string codStudio)
        {
            try
            {
                //CASSIANO:Se não houver uma verificação aqui, o arquivo será sobrescrito. O que deseja fazer?
                if (desenho != null && desenho.ContentLength > 0)
                {
                    //var x = desenho.ContentType;
                    var fileName = Path.GetFileName(desenho.FileName);//Se precisar
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["PASTA_UPLOAD"]), codStudio + ".jpg");
                    desenho.SaveAs(path);

                    //var img = 
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Falha ao fazer upload do arquivo." + ex.Message);
            }

            return RedirectToAction("UploadSucess");
        }

        [AllowAnonymous]
        public ActionResult DesenhosSemImagem(
            string filtrocodstudio,
            string filtrocoddal,
            string filtrodesenho,
            string pagina,
            string totalpaginas,
            string filtrostudio)
        {
            DesenhosSemImagemModelView model = new DesenhosSemImagemModelView();
            model.FiltroCodStudio = filtrocodstudio;
            model.FiltroCodDal = filtrocoddal;
            model.FiltroDesenho = filtrodesenho;
            model.FiltroStudio = filtrostudio;

            if (!string.IsNullOrWhiteSpace(totalpaginas))
            {
                model.TotalPaginas = int.Parse(totalpaginas);
            }

            if (string.IsNullOrWhiteSpace(pagina))
                model.Pagina = 1;
            else
                model.Pagina = int.Parse(pagina);


            ObterItensParaReserva(model);

            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_RESERVAS"];
            return View(model);
        }

        private void ObterItensParaReserva(DesenhosSemImagemModelView model)
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
                            && (dc.NOME_STUDIO.ToUpper().Contains(model.FiltroStudio.ToUpper()) || model.FiltroStudio == null)
                        group dc by
                            new
                            {
                                dc.DESENHO,
                                dc.COD_STUDIO,
                                dc.COD_DAL,
                                dc.ID_CONTROLE_DESENV,
                                dc.ID_STUDIO,
                                dc.ID_ITEM_STUDIO,
                                dc.NOME_STUDIO
                            }
                            into dv
                            select new ItemReserva
                            {
                                Desenho = dv.Key.DESENHO.ToUpper(),
                                CodStudio = dv.Key.COD_STUDIO.ToUpper(),
                                CodDal = dv.Key.COD_DAL.ToUpper(),
                                IDControleDesenvolvimento = dv.Key.ID_CONTROLE_DESENV,
                                IDStudio = (int)dv.Key.ID_STUDIO,
                                IDItemStudio = (int)dv.Key.ID_ITEM_STUDIO,
                                Studio = dv.Key.NOME_STUDIO
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

        [AllowAnonymous]
        public ActionResult Teste()
        {
            return View();
        }

        public ActionResult RascunhoPedido(string pedido, string cliente, string representante, string pagina, string totalpaginas)
        {
            RascunhoPedidoViewModel model = new RascunhoPedidoViewModel();
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

                ObterPedidosRascunho(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult RascunhoPedido(RascunhoPedidoViewModel model)
        {
            model.Pagina = 1;
            ObterPedidosRascunho(model);

            return View(model);
        }

        public void ObterPedidosRascunho(RascunhoPedidoViewModel model)
        {
            int iItensPorPagina = 10;

            using (var ctxTI = new TIDalutexContext())
            {
                decimal dFiltroPedido = 0;
                decimal.TryParse(model.FiltroPedido, out dFiltroPedido);
                if (model.FiltroCliente != null)
                    model.FiltroCliente = model.FiltroCliente.ToUpper();

                if (model.FiltroRepresentante != null)
                    model.FiltroRepresentante = model.FiltroRepresentante.ToUpper();

                var result = (from p in ctxTI.VW_RASCUNHO_PEDIDOS 
                              where (p.PEDIDO == dFiltroPedido || dFiltroPedido <= 0)
                                 && (model.FiltroCliente == null || p.CLIENTE.ToUpper().Contains(model.FiltroCliente))
                                 && (model.FiltroRepresentante == null || p.REPRESENTANTE.ToUpper().Contains(model.FiltroRepresentante))
                                 && (model.FiltroData == false || p.DATA_EMISSAO >= model.FiltroDataInicial && p.DATA_EMISSAO <= model.FiltroDataFinal)
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
        public ActionResult SalvarRascunhoPedido()
        {
            if (base.Session_Carrinho != null)
            {
                try
                {
                    int vNumeroPedido = 0;

                    using (var ctx = new TIDalutexContext())
                    {
                        #region Grava Pedido
                        vNumeroPedido = ctx.Database.SqlQuery<int>("SELECT SEQ_RASCUNHO_PEDIDO.NEXTVAL FROM DUAL", 1).FirstOrDefault();

                        RASCUNHO_PEDIDO objPrePedido = new RASCUNHO_PEDIDO();

                        objPrePedido.PEDIDO = vNumeroPedido;
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
                        objPrePedido.VIATRANSPORTE = base.Session_Carrinho.ViaTransporte.VIATRANSPORTE;
                        objPrePedido.COMISSAO = Session_Carrinho.PorcentagemComissao;
                        objPrePedido.ORIGEM = "PW"; // APENAS PRA INFORMAR QUE ESTE PEDIDO VEIO DO PEDIDO WEB NOVO. 
                        objPrePedido.PEDIDO_CLIENTE = base.Session_Carrinho.PedidoCliente;
                        objPrePedido.STATUS_PEDIDO = 1; //embora esteja definido no banco como padrão "1", esta gravando nulo, então, deixar explicito.....  
                        objPrePedido.DATA_INICIO = DateTime.Now;
                        objPrePedido.DATA_FINAL = DateTime.Now;
                        objPrePedido.DATA_EMISSAO = DateTime.Now;
                        objPrePedido.DATA_EMISSAO_DT = DateTime.Today;
                        #endregion

                        using (var transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                #region Salvar Itens
                                List<RASCUNHO_PEDIDO_ITEM> lstItens = new List<RASCUNHO_PEDIDO_ITEM>();

                                int iNumeroItem = 0;

                                foreach (InserirNoCarrinhoViewModel item in base.Session_Carrinho.Itens)
                                {
                                    if (base.Session_Carrinho.IDTipoPedido == (int)Enums.TiposPedido.RESERVA)
                                    {
                                        item.Artigo = "0000";
                                        item.Quantidade = 100;
                                        item.Pecas = 1;
                                        item.TecnologiaPorExtenso = null;
                                        item.DataEntregaItem = DateTime.Now;
                                        item.DtItemSolicitada = DateTime.Now;
                                    }

                                    string _cor;
                                    if (item.Tipo == Enums.ItemType.ValidacaoReserva) { _cor = "E000000"; } else if (item.Cor == null) { _cor = "0000000"; } else { _cor = item.Cor; }

                                    RASCUNHO_PEDIDO_ITEM objItemSalvar = new RASCUNHO_PEDIDO_ITEM();

                                    iNumeroItem++;

                                    objItemSalvar.ITEM = iNumeroItem;
                                    objItemSalvar.PEDIDO = vNumeroPedido;
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
                                    objItemSalvar.ORIGEM = "PW";
                                    objItemSalvar.MALHA_PLANO = item.UnidadeMedida == "MT" ? "P" : "M";
                                    objItemSalvar.MODA_DECORACAO = "M";
                                    objItemSalvar.DATA_ENTREGA_DIGI = item.DtItemSolicitada;
                                    objItemSalvar.ID_TAB_PRECO = -1;
                                    objItemSalvar.STATUS_ITEM = 15;
                                    objItemSalvar.PRECODIGITADOMOEDA = 0;
                                    objItemSalvar.TEM_RESTRICAO = item.TemRestricao;
                                    objItemSalvar.RESTRICAO = item.Restricao;

                                    if ((item.Tipo == Enums.ItemType.ValidacaoReserva || item.Tipo == Enums.ItemType.Estampado) && !item.PreExistente)
                                    {
                                        if (item.TecnologiaOriginal != item.TecnologiaPorExtenso)
                                        {
                                            objItemSalvar.TROCA_TECNOLOGIA = "Troca de " + item.TecnologiaOriginal + " para " + item.TecnologiaPorExtenso;
                                        }
                                    }

                                    lstItens.Add(objItemSalvar);
                                }

                                ctx.RASCUNHO_PEDIDO.Add(objPrePedido);


                                foreach (RASCUNHO_PEDIDO_ITEM item in lstItens)
                                {
                                    ctx.RASCUNHO_PEDIDO_ITEM.Add(item);
                                }
                                #endregion

                                ctx.SaveChanges();

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw ex;
                            }
                        }
                    }
                    return RedirectToAction("RascunhoPedidoSucesso", "Cadastro", new { pedido = vNumeroPedido.ToString() });
                }
                catch (Exception ex) { base.Handle(ex); }
            }
            return RedirectToAction("RascunhoPedidoSucesso", "Cadastro", new { pedido = "0" });
        }

        public ActionResult SalvarRascunhoPedido(string pedido)
        {
            return View();
        }

        public ActionResult RascunhoPedidoSucesso(string pedido)
        {
            ViewBag.NumeroPedido = pedido;

            return View();
        }

        public ActionResult TabelaPrecos()
        {
            TabelaPrecosViewModel model = new TabelaPrecosViewModel();

            return View(model);
        }

        public JsonResult ObterListaPrecos()
        {
            List<VW_CUS_CONS_TAB_PRECO> lstResult = null;


            using (var ctx = new TIDalutexContext())
            {
                decimal idUsuario = base.Session_Usuario.COD_USU;
                //decimal idUsuario = 456;

                lstResult = ctx.VW_CUS_CONS_TAB_PRECO.Where(x => x.ID_USUARIO == idUsuario).ToList();
            }

            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }
    }
}
