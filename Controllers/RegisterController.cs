using SavedMessages.BusinessLogic.Managers;
using SavedMessages.DataAccessLayer.Entities;
using SavedMessages.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SavedMessages.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IManager<User> _repo;
        private readonly IAccountManager<User> _rep;

        public RegisterController(IManager<User> repo, IAccountManager<User> rep)
        {
            _repo = repo;
            _rep = rep;
        }

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            //ScryptEncoder encoder = new ScryptEncoder();
            user.PermissionId = 1;
            user.IsVerifyed = false;

            //user.Password = encoder.Encode(user.Password);

            _repo.Create(user);

            EmailService.SendMail(user.Email, user.Id.ToString());

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ConfirmEmail(Guid id)
        {
            _rep.EmailConfirm(id);
            return View();
        }
    }
}