namespace TheLibrary_DataAccessLayer.Models
{
    // Un Book representa un ejemplar de un libro
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation properties
        public int LibraryId { get; set; }
        public Library Library { get; set; } = null!;

        public int LibraryBranchId { get; set; }
        public LibraryBranch LibraryBranch { get; set; } = null!;
        //necesito un DTO para recortar el listado de libros de esa sucursal
        //solo devolver libro y info sucursal no más
    }
}
