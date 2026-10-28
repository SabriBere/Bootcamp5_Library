using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace TheLibrary_BusinessLayer.DTOs
{
    public class BookCreateDTO
    {
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Debe tener un nombre entre 1 y 100 caracteres")]
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Debe tener un año")]
        public string Year { get; set; }

        public int LibraryBranchId { get; set; }
    }
}
