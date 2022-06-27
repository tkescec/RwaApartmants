using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Listeo.Controllers
{
    public class BaseController : Controller
    {
        private IRepositories repository = RepositoryFactory.GetRepository();

        internal IRepositories Repository { get => repository; set => repository = value; }


        internal ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal void GetTempData()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                TempData["Message"] = null;
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData["Error"] = null;
            }
        }
    }
}