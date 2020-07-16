using System;

using FilesStorage.Entities.Enums;

namespace FilesStorage.Entities.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }        

        public User() { }
    }
}
