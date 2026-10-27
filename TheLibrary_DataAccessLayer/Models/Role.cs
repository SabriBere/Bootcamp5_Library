using System;
using System.Collections.Generic;
using System.Text;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_DataAccessLayer.Models
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } =  new List<User>();
    }
}
