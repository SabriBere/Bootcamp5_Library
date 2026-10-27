using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Employee : User
    {
        public required string WorkShift { get; set; } // Acá podemos ver si es turno mañana, tarde o noche
        public int LibraryId { get; set; } // El lugar donde trabaja el empleado
    }
}
