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
        public DateTime? DateOfBirth { get; set; }

        //puede ser socio de 1 o más bibliotecas
        public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();

        //roles
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
