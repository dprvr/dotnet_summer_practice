using System;
using System.Collections.Generic;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.Entities
{
    public class StorageFile
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FileType FileType { get; set; }

        public virtual Storage Storage { get; set; }
        public virtual ICollection<StorageTag> Tags { get; set; }

        public StorageFile() 
        {
            CreationDate = DateTime.Now;
        }
    }
}
