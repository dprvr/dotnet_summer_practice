using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.ViewModels
{
    public class CreateEditFileView
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 30)]
        public string Description { get; set; }        
        
        [Required]
        [EnumDataType(typeof(FileType))]
        public FileType FileType { get; set; }

        public List<TagsView> Tags { get; set; }
    }
}
