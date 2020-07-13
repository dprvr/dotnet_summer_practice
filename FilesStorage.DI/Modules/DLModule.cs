using System;

using Autofac;

using FilesStorage.DAL.EF;
using FilesStorage.DAL.EF.Helpers;
using FilesStorage.DAL.EF.Repositories;
using FilesStorage.DAL.Interfaces;

namespace FilesStorage.DI.Modules
{
    public class DLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FilesStorageContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UsersRepository>()
                .As<IUsersRepository>()
                .SingleInstance();

            Action<Exception> action = ex => ExceptionsHandler.HandleExceptions(ex);
            builder
                .RegisterInstance(action)
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<AccountsRepository>()
                .As<IAccountsRepository>()
                .SingleInstance();

            builder
                .RegisterType<FilesRepository>()
                .As<IFilesRepository>()
                .SingleInstance();

            builder
                .RegisterType<TagsRepository>()
                .As<ITagsRepository>()
                .SingleInstance();

            builder
                .RegisterType<StoragesRepository>()
                .As<IStorageRepository>()
                .SingleInstance();

            builder
                .RegisterType<FilesAndTagsRepository>()
                .As<IFilesAndTagsRepository>()
                .SingleInstance();
        }

    }
}
