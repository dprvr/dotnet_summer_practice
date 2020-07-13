using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EntitiesConfig
{
    public class StorageConfig : EntityTypeConfiguration<Storage>
    {
        public StorageConfig()
        {
            HasKey(s => s.Id).Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
