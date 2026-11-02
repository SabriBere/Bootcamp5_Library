using System.ComponentModel.DataAnnotations;

namespace TheLibrary.Common.DTOs;

public class BookCreateDTO
{
    [Required]
    [StringLength(100, MinimumLength = 1,
        ErrorMessage = "El título debe tener entre 1 y 100 caracteres.")]
    public required string Title { get; set; }

    [Required]
    public required string Author { get; set; }

    [Required]
    public required string ISBN { get; set; }

    [Required(ErrorMessage = "Debe ingresar un año.")]
    public required string Year { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Debe seleccionar una sucursal válida.")]
    public int LibraryBranchId { get; set; }
}
