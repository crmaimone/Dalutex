﻿using Dalutex.Models;
using Dalutex.Models.DataModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace Dalutex.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                USUARIOS objUsuario = null;

                using (var ctx = new TIDalutexContext())
                {
                    objUsuario = ctx.USUARIOS.Where(x => x.LOGIN_USU == model.Login && x.SENHA_USU == model.Password).FirstOrDefault();
                }

                if (objUsuario != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    objUsuario.SENHA_USU = null;
                    base.Session_Usuario = objUsuario;
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToLocal(returnUrl);
                    else
                        return RedirectToAction("Pedido", "Pedido");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

         //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}