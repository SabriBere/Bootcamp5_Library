using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        //Navigation properties
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
