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
    }

    //UNNE - Centro
    //Sede Regional Chaco
    //Sede Regional Corrientes

    //UTN
    //Chaco
    //Buenos Aires
}
