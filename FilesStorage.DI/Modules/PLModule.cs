using System.Reflection;

using Autofac;
using Autofac.Integration.Mvc;

using FilesStorage.Entities.Mappers;

namespace FilesStorage.DI.Modules
{
    public class PLModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(MappersFactory.CreateDtosAndViewModelsMapper())
                .As<IPLMapper>()
                .SingleInstance();

            builder.RegisterControllers(Assembly.Load("FilesStorage.PL.Web"));
        }
    }
}
