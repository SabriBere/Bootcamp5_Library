using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Library
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }

        public ICollection<LibraryBranch> LibraryBranches { get; set; }
        //public ICollection<User> Users { get; set; }
        public ICollection<Membership> Members { get; set; } = new List<Membership>();
    }

    //Una sucursal de biblioteca tiene muchos libros (Navegación de ida)
    //y el libro pertenece a una sola sucursal de biblioteca (Navegación de vuelta)

    //Sucursal - libros -> sucursal tiene lista de libros
    //libro - Sucursal -> libro tiene un id de sucursal 

    //Al agregar "Branch" como entidad podemos desglosar cuando hay varias sucursales de una misma biblioteca
    //Ejemplo: UNNE tiene varias sucursales
    //UNNE
    ////Corrientes Centro
    ////Sede Regional Chaco
    ////Corrientes Campus
    //Ejemplo: UTN tiene varias sucursales
    //UTN
    ////Sede Regional Resistencia
    ////Sede Buenos Aires
    ////Sede Córdoba
}