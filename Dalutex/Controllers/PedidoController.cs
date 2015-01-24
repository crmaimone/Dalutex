using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Dalutex.Models;
using System.Configuration;
using Dalutex.Models.DataModels;

namespace Dalutex.Controllers
{
    public class PedidoController : BaseController
    {
        // GET: Pedido
        [AllowAnonymous]
        public ActionResult Pedido()
        {
            PedidoViewModel model = new PedidoViewModel();

            using (var ctxDx = new DalutexContext())
            {
                using (var ctxTI = new TIDalutexContext())
                {
                    //select ie.codigo_reduzido, 
                    //       substr(codigo, 17, 4) desenho,
                    //       substr(codigo, 21, 2) variante
                    //  from dalutex.itens_estoque ie 
                    // where ie.colecao = (select cf.parametro1 from CONFIG_GERAL cf where cf.id_config = 5)

                    var queryItens =
                    from ie in ctxDx.ITENS_ESTOQUE
                    join cf in ctxTI.CONFIG_GERAL on ie.COLECAO.ToString() equals cf.parametro1
                    where (
                        cf.id_config == 5
                    )
                    select ie;

                    model.Galeria = queryItens.ToList();
                }
            }

            model.UrlImagens = ConfigurationManager.AppSettings["PASTA_DESENHOS"];
            return View(model);
        }

        // GET: Pedido
        public ActionResult Ampliacao(string imagem)
        {
            ViewBag.ImgURL = imagem;
            return View();
        }


        public ActionResult ArtigosDisponiveis(string desenho)
        {
            return View();
        }
    }
}