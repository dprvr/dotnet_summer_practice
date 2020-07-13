using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

using FilesStorage.DI.Modules;

namespace FilesStorage.DI
{
    public class AppDependencyResolver
    {
        private static IContainer _container;

        static AppDependencyResolver()
        {
            InitContainer();
        }
        
        public static void InitContainer()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterModule(new DLModule())
                .RegisterModule(new BLModule())
                .RegisterModule(new PLModule());

            builder.RegisterControllers(Assembly.Load("FilesStorage.PL.Web"));
            _container = builder.Build();
        }

        public static IDependencyResolver GetMVCDependencyResolver()
        {
            return new AutofacDependencyResolver(_container);
        }

    }
}
