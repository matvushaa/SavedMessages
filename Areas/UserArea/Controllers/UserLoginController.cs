using SavedMessages.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SavedMessages.Areas.UserArea.Controllers
{
    public class UserLoginController : Controller
    {
        public ActionResult SignOut(LoginVM loginVm)
        {
            FormsAuthentication.SignOut();
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}