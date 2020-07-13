using System;
using System.Data.Entity;
using System.Linq;
using FilesStorage.DAL.EF.Helpers;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EF
{
    public class FilesStorageDbInitializer : DropCreateDatabaseAlways<FilesStorageContext>
    {
        private readonly EntitiesGenerator _generator;

        public FilesStorageDbInitializer() : base()
        {
            _generator = new EntitiesGenerator();
        }

        protected override void Seed(FilesStorageContext context)
        {
            var storage = new Storage();
            var addedStorage = context.Storages.Add(storage);
            context.SaveChanges();

            var user = _generator.GenerateUser();
            var addedUser = context.Users.Add(user);
            context.SaveChanges();

            var account = _generator.CreateAccount();
            account.StorageId = addedStorage.Id;
            account.UserId = addedUser.Id;
            context.Accounts.Add(account);
            context.SaveChanges();

            
            var tags = _generator.CreateTags(addedStorage);
            context.Tags.AddRange(tags);
            context.SaveChanges();

            var files = _generator.CreateFiles(addedStorage);
            context.Files.AddRange(files);
            context.SaveChanges();

            //null reference : faker error
            //var files = _generator.CreateFakeFiles(20, context.Accounts.FirstOrDefault(e => e.Login == "admin").Storage);                       
            //context.Files.AddRange(files);

        }
    }
}
