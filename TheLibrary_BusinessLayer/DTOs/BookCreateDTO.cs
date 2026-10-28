using System.ComponentModel.DataAnnotations;

namespace TheLibrary_BusinessLayer.DTOs
{
    public class BookCreateDTO
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Debe tener un nombre entre 1 y 100 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Debe tener un año")]
        public string Year { get; set; }

        public int LibraryBranchId { get; set; } //es obligatorio el id de la sucursal
    }
}
