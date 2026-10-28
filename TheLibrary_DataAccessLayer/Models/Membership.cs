using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } //Expiration Date
        public bool IsActive { get; set; }

        //Navigation properties
        public int MemberId { get; set; }
        public User Member { get; set; } = null!;
        public int LibraryId { get; set; }
        public Library Library { get; set; } = null!;
    }
}
