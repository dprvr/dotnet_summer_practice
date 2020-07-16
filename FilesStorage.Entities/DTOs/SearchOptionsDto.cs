using System.Collections.Generic;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.DTOs
{
    public class SearchOptionsDto
    {
        public int StorageId { get; set; }
        public string FileName { get; set; }
        public FileType? FileType { get; set; }
        public IEnumerable<string> TagsNames { get; set; }
    }
}
