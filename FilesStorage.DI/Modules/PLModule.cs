using Autofac;

using FilesStorage.Entities.Mappers;

namespace FilesStorage.DI.Modules
{
    public class PLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(MappersFactory.CreateDtosAndViewModelsMapper())
                .As<IPLMapper>()
                .SingleInstance();
        }
    }
}
