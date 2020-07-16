using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.ViewModels
{
    public class FilesViewModel
    {
        public string SearchString { get; set; }
        [Display(Name = "File")]
        public IEnumerable<FileView> filesViews { get; set; }

        public FilesViewModel()
        {
            SearchString = "";
        }
    }
}
