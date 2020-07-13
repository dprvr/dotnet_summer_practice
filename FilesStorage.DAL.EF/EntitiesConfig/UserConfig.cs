using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EntitiesConfig
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(e => e.Id).Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(100);

            Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
