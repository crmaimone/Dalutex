using Dalutex.Models;
using Dalutex.Models.DataModels;
using Dalutex.Models.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Dalutex.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Perfil corporativo";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Entre com contato conosco:";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Busca(string value)
        {
            ViewBag.Message = "Busca:"+value;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Mapa()
        {
            ViewBag.Message = "Mapa do site";

            return View();
        }
        public ActionResult SecurePage()
        {
            ViewBag.Message = "Página segura";

            return View();
        }

        public ActionResult Teste()
        {
            //using (var ctx = new TIDalutexContext())
            //{
            //    ctx.PED_RESERVA_VENDA.Add(new PED_RESERVA_VENDA()
            //    {
            //        PEDIDO_RESERVA = 88888888888,
            //        ITEM_PED_RESERVA = 88888888888,
            //        ID_VAR_PED_RESERVA = 88888888888,
            //        PEDIDO_VENDA = 88888888888,
            //        ITEM_PED_VENDA = 88888888888
            //    });

            //    ctx.SaveChanges();
            //}  

            return View();
        }
    }
}