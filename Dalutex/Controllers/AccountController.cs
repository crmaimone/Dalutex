using Dalutex.Models;
using Dalutex.Models.DataModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System;

namespace Dalutex.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
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
            try
            {
                if (ModelState.IsValid)
                {
                    USUARIOS objUsuario = null;

                    using (var ctx = new TIDalutexContext())
                    {
                        objUsuario = ctx.USUARIOS.Where(x => x.LOGIN_USU.ToUpper() == model.Login.ToUpper() && x.SENHA_USU.ToUpper() == model.Password.ToUpper()).FirstOrDefault();
                    }

                    if (objUsuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(objUsuario.NOME_USU, model.RememberMe);
                        objUsuario.SENHA_USU = null;
                        base.Session_Usuario = objUsuario;
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                            return RedirectToLocal(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuário ou senha inválidos.");
                    }
                }

            }
            catch(Exception ex)
            {
                base.Handle(ex);
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
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}