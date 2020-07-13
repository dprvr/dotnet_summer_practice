using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.DTOs
{
    public class FileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FileType FileType { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}
