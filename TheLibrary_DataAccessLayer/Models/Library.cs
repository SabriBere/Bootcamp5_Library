using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Library
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
    }
}
