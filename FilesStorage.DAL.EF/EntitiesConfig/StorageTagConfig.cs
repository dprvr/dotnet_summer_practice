using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EntitiesConfig
{
    public class StorageTagConfig : EntityTypeConfiguration<StorageTag>
    {
        public StorageTagConfig()
        {
            HasKey(e => e.Id).Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            //HasRequired(t => t.Storage)
            //    .WithMany(s => s.StorageTags);
        }
    }
}
