using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EntitiesConfig
{
    public class StorageFileConfig : EntityTypeConfiguration<StorageFile>
    {
        public StorageFileConfig()
        {
            HasKey(e => e.Id).Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(f => f.CreationDate)
                .IsRequired();

            Property(f => f.Description)
                .HasMaxLength(200);

            Property(f => f.FileType)
                .IsRequired();

            //HasRequired(f => f.Storage)
            //    .WithMany(s => s.StorageFiles);
        }
    }
}
