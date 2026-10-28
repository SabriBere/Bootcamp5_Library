using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class LibraryBranch
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }

        //Relationships
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public ICollection<Book> Books { get; set; } //1:N
    }
}
