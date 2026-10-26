using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public int yearOfBirth { get; set; }
    }
}
