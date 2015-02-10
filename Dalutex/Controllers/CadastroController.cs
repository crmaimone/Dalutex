using Dalutex.Models;
using Dalutex.Models.DataModels;
using System;
using System.Collections.Generic;
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
                    IDRepresentante = base.Session_Usuario.ID_REPRES;
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
        public ActionResult Representantes(PesquisaRepresentantesViewModel model)
        {
            using(var ctx = new DalutexContext())
            {
                model.Representantes = ctx.REPRESENTANTES.Where(x => x.NOME.Contains(model.Filtro)).ToList();
            }

            return View(model);
        }

        public ActionResult ClientesFatura(string IDRepresentante, string IDClienteFatura)
        {
            PesquisaClientesFaturaViewModel model = new PesquisaClientesFaturaViewModel();

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
        public ActionResult ClientesFatura(PesquisaClientesFaturaViewModel model)
        {
            using (var ctx = new TIDalutexContext())
            {
                int idRepresentante = base.Session_Carrinho.IDRepresentante;
                model.ClientesFatura = ctx.VW_CLIENTE_TRANSP.Where(x => x.NOME.Contains(model.Filtro.ToUpper()) && x.ID_REP == idRepresentante).ToList();
            }

            return View(model);
        }


        public ActionResult ClientesEntrega(string IDClienteFatura)
        {
            //PesquisaClientesFaturaViewModel model = new PesquisaClientesFaturaViewModel();

            //int iIDClienteFatura;

            //if (int.TryParse(IDClienteFatura, out iIDClienteFatura))
            //{
            //    using (var ctx = new TIDalutexContext())
            //    {
            //        VW_CLIENTE_TRANSP objCliente = ctx.VW_CLIENTE_TRANSP.Find(iIDClienteFatura);
            //        model.ClientesFatura = new List<VW_CLIENTE_TRANSP>();
            //        model.ClientesFatura.Add(objCliente);
            //        model.Filtro = objCliente.NOME;
            //    }
            //}

            Session_Carrinho.IDClienteFatura = int.Parse(IDClienteFatura);

            return View();
        }
    }
}