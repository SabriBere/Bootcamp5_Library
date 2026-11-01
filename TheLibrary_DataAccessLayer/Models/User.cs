using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TheLibrary_DataAccessLayer.Models
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //puede ser socio de 1 o más bibliotecas
        // public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();

        //roles
        // public ICollection<Role> Roles { get; set; } = new List<Role>();

        //membresías
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();

        //préstamos
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
