namespace TheLibrary_DataAccessLayer.Models
{
    public class Book //Esto represnta un ejemplar
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }

        //Navegation property
        public int LibraryBranchId { get; set; }
        public LibraryBranch LibraryBranch { get; set; } = null!;

        //public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
