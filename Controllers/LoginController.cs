using SavedMessages.DataAccessLayer;
using SavedMessages.DataAccessLayer.Entities;
using SavedMessages.Helpers;
using SavedMessages.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SavedMessages.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet] //Показываем страницу с формой для логина
        public ActionResult SignIn(string returnUrl)
        {
            var loginVM = new LoginVM
            {
                ReturnUrl = returnUrl
            };

            return View(loginVM);
        }

        [HttpPost] //Логиним пользователя на сайт
        public ActionResult SignIn(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var accountRepository = new AccountRepository<User>();
                var account = accountRepository.GetAccount(loginVM);

                if (account != null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(account.Email, true);
                    Session["UserID"] = account.Id;
                    Session["UserName"] = account.FirstName;
                    SetRefreshTokenCookie(loginVM);
                    if (account.Permission.Id == 2)
                        return RedirectToAction("MyDetails", "AdminArea/Admin");
                    if (account.Permission.Id == 1)
                        return RedirectToAction("MyDetails", "UserArea/User");
                }
                else
                {
                    ModelState.AddModelError("SigninError", "There is no user with such login and password");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(loginVM);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            HttpContext.User = new GenericPrincipal(
                new GenericIdentity(string.Empty), null);

            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();

            return RedirectToLocal();
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        //При вводе логина пользователя будем подсвечивать зеленым
        //если такой логин есть
        public JsonResult IsAccountExists(string login)
        {
            var accountRepository = new AccountRepository<User>();
            return Json(accountRepository.IsAccountExists(login),
            JsonRequestBehavior.AllowGet);
        }

        private void SetRefreshTokenCookie(LoginVM loginVM)
        {
            TokenHelper tokenHelper = new TokenHelper();
            var token = tokenHelper.GetRefreshToken(loginVM.Email, loginVM.Password);

            HttpCookie tokenCookie = new HttpCookie("SavedMessagesCookie");
            tokenCookie.Value = token.RefreshToken;
            tokenCookie.Expires = DateTime.MaxValue;

            Response.Cookies.Add(tokenCookie);
        }

        //GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}