using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listeo.App_Code
{
    public static class Sentinel
    {
        public static bool IsLoggedIn()
        {
            return (HttpContext.Current.Session["User"] != null);
        }
    }
}