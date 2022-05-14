using DAL;
using DAL.Repositories;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

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
            Application["Repositories"] = _repository;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}