using System.Collections.Generic;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.DTOs
{
    public class FileWithTagsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FileType FileType { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
    }
}
