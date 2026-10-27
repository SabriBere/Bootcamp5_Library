namespace TheLibrary_DataAccessLayer.Models
{
    public class LibraryBranch
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }

        // Una biblioteca tiene muchos libros
        // pero ese libro pertenece a 1 sucursal

        // sucursal - libro --> sucursal lista de libros
        // libro - sucursal --> libro 1 ID de sucursal

        // No es necesario crear una relación intermedia

        // Establezco navegaciones y relaciones
        public int LibraryId { get; set; }
        public Library Library { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>(); // 1:N
    }
}
