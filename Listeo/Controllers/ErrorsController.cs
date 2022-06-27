using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Listeo.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Index()
        {
            return View("404");
        }
        public ActionResult NotFound()
        {
            return View("404");
        }

        public ActionResult ServerError()
        {
            return View("500");
        }
    }
}