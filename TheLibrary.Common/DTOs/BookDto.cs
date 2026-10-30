using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary.Common.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }

        // Propiedades de navegación
        public string LibraryBranchName { get; set; }
        public string LibraryName { get; set; }
    }
}
