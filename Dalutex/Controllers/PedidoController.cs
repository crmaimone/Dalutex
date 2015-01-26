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

            //select ie.codigo_reduzido, 
            //       substr(codigo, 17, 4) desenho,
            //       substr(codigo, 21, 2) variante
            //  from dalutex.itens_estoque ie 
            // where ie.colecao = (select cf.parametro1 from CONFIG_GERAL cf where cf.id_config = 5)

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

        // GET: Pedido
        public ActionResult Ampliacao(string imagem, string desenho)
        {
            ViewBag.ImgURL = imagem;
            ViewBag.Desenho = desenho;
            return View();
        }


        public ActionResult ArtigosDisponiveis(string desenho)
        {
            return View();
        }

        // teste oda ---
        public ActionResult MenuColecao()
        {         
            return View();
        }
    }
}