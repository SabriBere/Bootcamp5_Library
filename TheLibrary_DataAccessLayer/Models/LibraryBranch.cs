namespace TheLibrary_DataAccessLayer.Models
{
    public class LibraryBranch
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }

        // Relaciones
        public int LibraryId { get; set; }
        public Library Library { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>(); // 1:N
        public ICollection<User> Users { get; set; } = new List<User>(); // N:M
    }
}
