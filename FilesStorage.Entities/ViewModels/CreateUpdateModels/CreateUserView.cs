using System;
using System.ComponentModel.DataAnnotations;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.ViewModels
{
    public class CreateUserView
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords mismatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "FirstName")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "LastName")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Please enter your gender")]
        [Display(Name = "Gender")]
        [EnumDataType(typeof(Gender))]        
        public Gender Gender { get; set; }

        [Display(Name ="BirthDate")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
