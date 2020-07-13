using System;
using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.DTOs
{
    public class UserSignUpDto
    {
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
    }
}
