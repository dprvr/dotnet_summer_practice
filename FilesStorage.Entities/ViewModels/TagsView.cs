using System.ComponentModel.DataAnnotations;

namespace FilesStorage.Entities.ViewModels
{
    public class TagsView
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Tag Name")]
        [StringLength(15, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
