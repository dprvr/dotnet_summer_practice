using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using FilesStorage.DAL.EF.EFConfig;
using FilesStorage.DAL.EF.EntitiesConfig;
using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF
{
    [DbConfigurationType(typeof(AppDbConfig))]
    public class FilesStorageContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageTag> Tags { get; set; }
        public DbSet<StorageFile> Files { get; set; }

        public FilesStorageContext() : base("FilesStorageDb")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new AccountConfig())
                .Add(new UserConfig())
                .Add(new StorageTagConfig())
                .Add(new StorageFileConfig())
                .Add(new StorageConfig());     
        }
    }
}
