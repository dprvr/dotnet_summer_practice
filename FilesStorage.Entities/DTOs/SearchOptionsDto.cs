using System.Collections.Generic;

namespace FilesStorage.Entities.DTOs
{
    public class SearchOptionsDto
    {
        public int StorageId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public IEnumerable<string> TagsNames { get; set; }
    }
}
