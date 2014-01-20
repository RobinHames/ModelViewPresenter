using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ModelViewPresenter.Demo.Web;

using Castle.Windsor.Installer;
using Castle.Windsor;

namespace ModelViewPresenter.Demo.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BootstrapContainer();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        public static IWindsorContainer container { get; private set; }

        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());
        }
    }
}
