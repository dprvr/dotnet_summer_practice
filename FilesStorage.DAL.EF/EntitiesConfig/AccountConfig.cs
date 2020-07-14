using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FilesStorage.Entities.Entities;

namespace FilesStorage.DAL.EF.EntitiesConfig
{
    public class AccountConfig : EntityTypeConfiguration<Account>
    {
        public AccountConfig()
        {
            HasKey(e => e.Id).Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(a => a.Login)
                .IsRequired()
                .HasMaxLength(50);

            HasIndex(a => a.Login)
                .IsUnique();

            Property(a => a.CreationDate)
                .IsRequired();

            Property(a => a.HashedPassword)
                .IsRequired();
        }
    }
}
