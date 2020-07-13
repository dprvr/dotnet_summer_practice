using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FilesStorage.DI;

namespace FilesStorage.PL.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var resolver = AppDependencyResolver.GetMVCDependencyResolver();
            DependencyResolver.SetResolver(resolver);
        }
    }
}
