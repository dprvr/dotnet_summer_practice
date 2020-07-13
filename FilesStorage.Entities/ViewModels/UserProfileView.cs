using System;
using System.ComponentModel.DataAnnotations;

namespace FilesStorage.Entities.ViewModels
{
    public class UserProfileView
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Display(Name = "Connected email")]
        public string Email { get; set; }               
    }
}
