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
        // GET: Pedido
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

        public ActionResult ArtigosDisponiveis(string imagem, string desenho)
        {
            ViewBag.ImgURL = imagem;
            ViewBag.Desenho = desenho;

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
                    var query =
                        from ar in ctx.VW_ARTIGOS_DISPONIVEIS
                        where
                            (ar.ID_TECNOLOGIA == null || ar.ID_TECNOLOGIA != iIDTecnologia)
                            && 
                            (ar.ID_CARAC_TEC == null || !lstCaracteristicas.Contains(ar.ID_CARAC_TEC))
                        select ar;


                    //ViewBag.Artigos = ctx.VW_ARTIGOS_DISPONIVEIS.Where(x => (
                    //        x.ID_TECNOLOGIA == null || x.ID_TECNOLOGIA != iIDTecnologia
                    //    )
                    //    && (
                    //        x.ID_CARAC_TEC == null || !lstCaracteristicas.Contains(x.ID_CARAC_TEC)
                    //    )).ToList();

                    //throw new Exception(query.ToString());
                   
                    ViewBag.Artigos = query.ToList();

                }
            }
            return View();
        }

        public ActionResult Pedido()
        {         
            return View();
        }
    }
}