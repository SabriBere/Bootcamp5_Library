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

        //Usuario puede ser socio de una o varias bibliotecas
        //public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();
        public ICollection<Membership> Subscriptions { get; set; } = new List<Membership>();
        public ICollection<Role> Roles { get; set; }

        //public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
