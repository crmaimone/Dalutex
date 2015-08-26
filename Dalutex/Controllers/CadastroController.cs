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
        public ActionResult Representantes()
        {
            PesquisaRepresentantesViewModel model = new PesquisaRepresentantesViewModel();
            model.Representantes = new List<REPRESENTANTES>();
            int IDRepresentante  = 0;

            if (base.Session_Carrinho.IDRepresentante > default(int))
            {
                IDRepresentante = base.Session_Carrinho.IDRepresentante;
            }
            else
            {
                if(base.Session_Usuario.ID_REPRES > default(int))
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
            using(var ctx = new DalutexContext())
            {
                model.Representantes = ctx.REPRESENTANTES.Where(x => x.NOME.Contains(model.Filtro.ToUpper())).OrderBy(x => x.NOME).ToList();
            }

            return View(model);
        }


        public ActionResult ClientesFatura(string IDRepresentante, string IDClienteFatura)
        {
            PesquisaClientesFaturaViewModel model = new PesquisaClientesFaturaViewModel();
            model.IDTipoPedido = Session_Carrinho.IDTipoPedido;

            int iIDClienteFatura;

            if (int.TryParse(IDClienteFatura, out iIDClienteFatura))
            {
                using (var ctx = new TIDalutexContext())
                {
                    int idRepresentante = base.Session_Carrinho.IDRepresentante;
                    VW_CLIENTE_TRANSP objCliente = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == iIDClienteFatura && x.ID_REP == idRepresentante).First();
                    model.ClientesFatura = new List<VW_CLIENTE_TRANSP>();
                    model.ClientesFatura.Add(objCliente);
                    model.Filtro = objCliente.NOME;
                }
            }

            Session_Carrinho.IDRepresentante = int.Parse(IDRepresentante);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientesFatura(PesquisaClientesFaturaViewModel model)
        {
            model.IDTipoPedido = Session_Carrinho.IDTipoPedido;

            using (var ctx = new TIDalutexContext())
            {
                int idRepresentante = base.Session_Carrinho.IDRepresentante;
                model.ClientesFatura = ctx.VW_CLIENTE_TRANSP.Where(x => x.NOME.Contains(model.Filtro.ToUpper()) && x.ID_REP == idRepresentante).OrderBy(x => x.NOME).ToList();
            }

            return View(model);
        }


        public ActionResult ClientesEntrega(string IDClienteFatura, string IDClienteEntrega)
        {
            PesquisaClientesEntregaViewModel model = new PesquisaClientesEntregaViewModel();

            int iIDClienteEntrega;

            if (int.TryParse(IDClienteEntrega, out iIDClienteEntrega))
            {
                using (var ctx = new TIDalutexContext())
                {
                    VW_CLIENTE_TRANSP _ClienteEntrega = ctx.VW_CLIENTE_TRANSP.Where(x => x.ID_CLIENTE == iIDClienteEntrega).First();

                    model.ClientesEntrega = new List<VW_CLIENTE_TRANSP>();

                    model.ClientesEntrega.Add(_ClienteEntrega);

                    model.Filtro = _ClienteEntrega.NOME;
                }
            }

            Session_Carrinho.IDClienteFatura = int.Parse(IDClienteFatura);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientesEntrega(PesquisaClientesEntregaViewModel model)
        {
            using (var ctx = new TIDalutexContext())
            {
                int idRepresentante = base.Session_Carrinho.IDRepresentante;
                model.ClientesEntrega = ctx.VW_CLIENTE_TRANSP.Where(x => x.NOME.Contains(model.Filtro.ToUpper()) && x.ID_REP == idRepresentante).OrderBy(x => x.NOME).ToList();
            }

            return View(model);
        }

        public ActionResult Transportadora(string IDClienteEntrega, string IDTransportadora)
        {
            PesquisaTransportadoraViewModel model = new PesquisaTransportadoraViewModel();

            int iIDTransportadora;

            if (int.TryParse(IDTransportadora, out iIDTransportadora))
            {
                using (var ctx = new DalutexContext())
                {
                    TRANSPORTADORAS _transportadora = ctx.TRANSPORTADORAS.Find(iIDTransportadora);

                    model.Transportadoras = new List<TRANSPORTADORAS>();
                    model.Transportadoras.Add(_transportadora);

                    model.Filtro = _transportadora.NOME;
                }
            }

            Session_Carrinho.IDClienteEntrega = int.Parse(IDClienteEntrega);

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

        [AllowAnonymous]
        public ActionResult UploadImage(string cod_studio, string cod_dal, string studio, string desenho )
        {
            UploadImageModelView model = new UploadImageModelView();
            
            model.CodStudio = cod_studio;
            model.CodDal = cod_dal;
            model.Studio = studio;
            model.Desenho = desenho;

            //ViewBag.CodStudio = cod_studio;

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
            catch(Exception ex)
            {
                ModelState.AddModelError("","Falha ao fazer upload do arquivo." + ex.Message);
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

        
    }
}
                