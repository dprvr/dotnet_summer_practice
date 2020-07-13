using System.ComponentModel.DataAnnotations;

namespace FilesStorage.Entities.Enums
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
    }
}
