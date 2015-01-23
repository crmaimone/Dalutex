using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Dalutex.Models.DataModels;
using Dalutex.Models;
using System.Configuration;

namespace Dalutex.Controllers
{
    public class PedidoController : BaseController
    {
        // GET: Pedido
        [AllowAnonymous]
        public ActionResult Pedido()
        {
            PedidoViewModel model = new PedidoViewModel();

            using (var ctx = new DalutexDataContext())
            {
                //select ie.codigo_reduzido, 
                //       substr(codigo, 17, 4) desenho,
                //       substr(codigo, 21, 2) variante
                //  from dalutex.itens_estoque ie 
                // where ie.colecao = (select cf.parametro1 from CONFIG_GERAL cf where cf.id_config = 5)

                var queryItens =
                from ie in ctx.ItensEstoque
                join cf in ctx.ConfigGerais on ie.Colecao equals cf.Parametro1
                where (
                    cf.ID_Config == 5
                )
                select ie;

                model.Galeria = queryItens.ToList();
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
    }
}