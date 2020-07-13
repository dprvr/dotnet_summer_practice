using System.ComponentModel.DataAnnotations;

namespace FilesStorage.Entities.ViewModels
{
    public class UserLoginView
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
