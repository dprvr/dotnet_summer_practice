using System.Collections.Generic;

namespace FilesStorage.Entities.ViewModels
{
    public class FilesViewModel
    {
        public string SearchString { get; set; }
        public IEnumerable<FileView> filesViews { get; set; }

        public FilesViewModel()
        {
            SearchString = "";
        }
    }
}
