using Dalutex.Models;
using Dalutex.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalutex.Controllers
{
    //[Authorize]
    public class BaseController : Controller
    {
        [Authorize]
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #region ErrorHandling

        [Authorize]
        public ActionResult ErrorMessage(string message, string title)
        {
            ViewBag.Title = title;
            ViewBag.Message = message;
            return View();
        }

        protected void Handle(Exception ex)
        {
            if (ex is DbEntityValidationException)
                this.HandleDbEntityValidationException(ex as DbEntityValidationException);
            else
                throw new Exception(ex.Message + Environment.NewLine + GetInnerException(ex));
        }

        protected string GetInnerException(Exception e, string strIn = "")
        {
            strIn = strIn + e.Message;

            if (e.InnerException != null)
                return GetInnerException(e.InnerException, strIn + Environment.NewLine);
            else
                return strIn;
        }

        protected void HandleDbEntityValidationException(DbEntityValidationException e)
        {
            foreach (var vErr in e.EntityValidationErrors)
                foreach (var err in vErr.ValidationErrors)
                    ModelState.AddModelError("", err.ErrorMessage);
        }

        #endregion

        #region SessionAtributes

        protected USUARIOS Session_Usuario
        {
            get
            {
                if (Session["SESSION_USUARIO"] == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        using (var ctx = new TIDalutexContext())
                        {
                            Session["SESSION_USUARIO"] = ctx.USUARIOS.Where(x => x.NOME_USU.ToUpper() == User.Identity.Name.ToUpper()).FirstOrDefault();
                            return Session["SESSION_USUARIO"] as USUARIOS;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return Session["SESSION_USUARIO"] as USUARIOS;
                }
            }
            set
            {
                Session["SESSION_USUARIO"] = value;
            }
        }

        protected ConclusaoPedidoViewModel Session_Carrinho
        {
            get
            {
                if (Session["SESSION_CARRINHO"] == null)
                    return null;
                else
                    return Session["SESSION_CARRINHO"] as ConclusaoPedidoViewModel;
            }
            set
            {
                Session["SESSION_CARRINHO"] = value;
            }
        }
        #endregion
    }
}