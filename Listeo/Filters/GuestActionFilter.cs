using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Listeo.Filters
{
    public class GuestActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = (User)filterContext.HttpContext.Session["User"];

            if (user != null && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Account")
            {
                filterContext.HttpContext.Response.Redirect("/");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}