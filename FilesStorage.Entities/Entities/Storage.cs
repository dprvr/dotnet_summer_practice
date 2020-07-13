using System.Collections.Generic;

namespace FilesStorage.Entities.Entities
{
    public class Storage 
    {
        public int Id { get; set; }
        public string Something { get; set; }
        public virtual ICollection<StorageFile> StorageFiles { get; set; }
        public virtual ICollection<StorageTag> StorageTags { get; set; }
        //public virtual Account Account { get; set; }
    }
}
