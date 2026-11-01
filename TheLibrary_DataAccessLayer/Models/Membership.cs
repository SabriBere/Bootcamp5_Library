namespace TheLibrary_DataAccessLayer.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } //Expiration Date
        public bool IsActive { get; set; }

        //Navigation properties
        public string MemberId { get; set; } = string.Empty;
        public User Member { get; set; } = null!;
        public int LibraryId { get; set; }
        public Library Library { get; set; } = null!;
    }
}
