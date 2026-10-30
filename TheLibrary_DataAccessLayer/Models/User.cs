using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace TheLibrary_DataAccessLayer.Models
{
    public class User: IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //Usuario puede ser socio de una o varias bibliotecas
        //public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();
        public ICollection<Membership> Subscriptions { get; set; } = new List<Membership>();

        //public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
