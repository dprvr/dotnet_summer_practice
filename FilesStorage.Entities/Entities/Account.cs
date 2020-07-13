using System;

namespace FilesStorage.Entities.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }

        public int UserId { get; set; }
        public int StorageId { get; set; }

        public virtual User User { get; set; }
        public virtual Storage Storage { get; set; }

        public Account() 
        {
            CreationDate = DateTime.Now;
        }
    }
}
