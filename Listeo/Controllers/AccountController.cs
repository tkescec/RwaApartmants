using DAL.Models;
using Listeo.Filters;
using Listeo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace Listeo.Controllers
{
    public class AccountController : BaseController
    {
        [GuestActionFilter]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            GetTempData();

            return View();
        }

        [GuestActionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                User user = Repository.AuthRepository.AuthUser(model.Email, Crypto.HashPassword(model.Password));

                if (CheckUser(user))
                {
                    Session["User"] = user;

                    return RedirectToLocal(returnUrl);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "There was a problem. Please contact the site administrator.");
            }

            return View(model);
        }

        [GuestActionFilter]
        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            GetTempData();

            return View();
        }

        [GuestActionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string returnUrl="/Account/Login")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (Repository.AuthRepository.RegisterUser((DAL.Models.ViewModels.RegisterViewModel)model))
                {
                    TempData["Message"] = "You have successfully registered!";

                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(String.Empty, "There was a problem registering the user.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "There was a problem. Please contact the site administrator.");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        private bool CheckUser(User user)
        {
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid E-mail address and/or password.");
                return false;
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(String.Empty, "You have not confirmed your E-mail address. Confirm your E-mail address so you can access the portal!");
                return false;
            }
            if (user.DeletedAt != null)
            {
                ModelState.AddModelError(String.Empty, "Account deactivated!");
                return false;
            }
            if (user.Role != Roles.RoleType.User.ToString())
            {
                ModelState.AddModelError(String.Empty, "You cannot log in as an administrator!");
                return false;
            }

            return true;
        }
    }
}