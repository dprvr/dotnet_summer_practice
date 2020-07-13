using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilesStorage.Entities.ViewModels
{
    public class FileView
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "File")]
        [ReadOnly(true)]
        public string FullName { get; set; }
    }
}
