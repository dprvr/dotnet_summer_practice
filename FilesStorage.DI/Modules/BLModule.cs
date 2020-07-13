using Autofac;
using FilesStorage.BLL;
using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.Mappers;

namespace FilesStorage.DI.Modules
{
    public class BLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(MappersFactory.CreateDtoAndEntityModelsMapper())
                .As<IBLMapper>()
                .SingleInstance();

            builder
                .RegisterType<UsersLogic>()
                .As<IUsersLogic>()
                .SingleInstance();

            builder
                .RegisterType<FilesLogic>()
                .As<IFilesLogic>()
                .SingleInstance();

            builder
                .RegisterType<Hasher>()
                .As<IHasher>()
                .SingleInstance();

            builder
                .RegisterType<TagsLogic>()
                .As<ITagsLogic>()
                .SingleInstance();

            builder
                .RegisterType<SearchQueryParser>()
                .As<ISearchQueryParser>()
                .SingleInstance();
        }
    }
}
