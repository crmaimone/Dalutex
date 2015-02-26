using Dalutex.Models;
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
            MemoryStream ms = new MemoryStream(new Relatorios().GerarEspelhoPedido());
            Attachment anexo = new Attachment(ms, "Pedido_" + 1.ToString() + ".pdf", "application/pdf");
            Utilitarios util = new Utilitarios();
            util.EnviaEmail("crmaimone@gmail.com", "Novo pedido", "Segue novo pedido", anexo);

            return View();
        }
    }
}