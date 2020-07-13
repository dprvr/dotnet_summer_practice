using System.Collections.Generic;

namespace FilesStorage.Entities.Entities
{
    public class StorageTag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Storage Storage { get; set; }
        public virtual ICollection<StorageFile> Files { get; set; }

        public StorageTag() 
        {

        }
    }
}
