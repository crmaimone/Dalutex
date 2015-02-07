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
            if (base.Session_Usuario.ID_REPRES > default(int))
            {
                using (var ctx = new DalutexContext())
                {
                    REPRESENTANTES objRepresentante = ctx.REPRESENTANTES.Find(base.Session_Usuario.ID_REPRES);
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

        public ActionResult ClientesFatura(string IDRepresentante)
        {
            Session_Carrinho.IDRepresentante = int.Parse(IDRepresentante);
            return View();
        }
    }
}