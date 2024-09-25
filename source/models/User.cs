using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api01.source.models
{
    public class User
    {
        public int Id { get; set; }

        public String Name { get; set; } = string.Empty;

        public String Rut { get; set; } = string.Empty;

        public String Email { get; set; } = string.Empty;

        // Entityframework relationships 

        public List<Product> products {get;set;} = [];

        public int RoleId {get;set;} 
        public Rol rol {get;set;} = null; 

        
    }
}