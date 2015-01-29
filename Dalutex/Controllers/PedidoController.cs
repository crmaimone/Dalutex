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

namespace Dalutex.Controllers
{
    public class PedidoController : BaseController
    {

        public ActionResult Pedido()
        {
            return View();
        }
        
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

        public ActionResult ArtigosDisponiveis(string imagem, string desenho, string variante)
        {
            ArtigosDisponiveisViewModel model = new ArtigosDisponiveisViewModel();
            model.Desenho = desenho;
            model.Variante = variante;
            model.Imagem = imagem;

            List<VW_CARACT_DESENHOS> lstQuery = null;

            using(var ctx = new TIDalutexContext())
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
                        base.Session_Carrinho = new List<InserirNoCarrinhoViewModel>();

                    base.Session_Carrinho.Add(model);

                }
            }
            catch(Exception ex)
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
    }
}