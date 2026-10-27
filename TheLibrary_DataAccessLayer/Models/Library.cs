using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Library
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public required string Name { get; set; }
        public required string Address { get; set; }

        //listado de sucursales
        public List<LibraryBranch> LibraryBranches { get; set; } = new();

        // N:M - Miembros - EF crea la tabla intermedia con las FK de ambos
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    // Una biblioteca puede tener muchas sucursales (navegación de ida)
    // y cada sucursal pertenece a una sola biblioteca (navegación de vuelta).

    // Sucursal -> Books: la sucursal contiene una lista de libros.
    // Book -> Sucursal: el libro contiene el Id de su sucursal.

    // Agregar Branch como entidad permite representar varias sucursales
    // pertenecientes a una misma biblioteca.

    // Ejemplo: UNNE tiene varias sucursales
    //// Corrientes Centro
    //// Sede Regional Chaco
    //// Corrientes Campus

    // Ejemplo: UTN tiene varias sucursales
    //// Sede Regional Resistencia
    //// Sede Buenos Aires
    //// Sede Córdoba
}
