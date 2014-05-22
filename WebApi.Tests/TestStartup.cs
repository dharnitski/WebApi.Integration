using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Web.WebApi;
using Owin;
using TripFiles.Mobile;
using WebApi.App_Start;

namespace WebApi.Tests
{
    public class TestsStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage(); // See Microsoft.Owin.Diagnostics
            app.UseWelcomePage("/Welcome"); // See Microsoft.Owin.Diagnostics
            
            var config = new HttpConfiguration();

            var startup = new Startup();
            startup.ConfigureAuth(app);

            config.DependencyResolver = GetDependencyResolver();

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            NinjectWebCommon.RegisterServices(kernel);
            return kernel;
        }

        public IDependencyResolver GetDependencyResolver()
        {
            var kernel = CreateKernel();

            return new NinjectDependencyResolver(kernel);
        }
    }
}
