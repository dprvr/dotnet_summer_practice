using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.ViewModels
{
    public class FileFullView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FileType FileType { get; set; }

        public IEnumerable<TagsView> Tags { get; set; }
    }
}
