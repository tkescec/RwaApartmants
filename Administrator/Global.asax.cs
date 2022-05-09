using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Administrator
{
    public class Global : HttpApplication
    {
        private readonly IRepositories _repository;

        public Global()
        {
            _repository = RepositoryFactory.GetRepository();
        }

        void Application_Start(object sender, EventArgs e)
        {
            Application["repository"] = _repository;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}