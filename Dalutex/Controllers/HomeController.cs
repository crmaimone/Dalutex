using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalutex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Perfil corporativo";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Entre com contato conosco:";

            return View();
        }
    }
}