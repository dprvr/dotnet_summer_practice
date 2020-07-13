using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EntitiesConfig
{
    public class FilesAndTagsConfig : EntityTypeConfiguration<FileAndTag>
    {
        public FilesAndTagsConfig()
        {
            HasKey(r => r.Id).Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasRequired(r => r.File);
            //HasRequired(r => r.Tag);
        }
    }
}
