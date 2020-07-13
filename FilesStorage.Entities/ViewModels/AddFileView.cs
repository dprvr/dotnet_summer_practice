using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.ViewModels
{
    public class AddFileView
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "please enter filename")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "please specify filename b")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 30)]
        public string Description { get; set; }
        
        public FileType FileType { get; set; }

        public int[] SelectedIds { get; set; }

    }
}
