using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.models;

namespace api01.source.Dtos
{
    public class UserDto
    {
        public int Id {get;set;}

        public String Name { get; set; } = string.Empty;

        public String Rut { get; set; } = string.Empty;

        public String Email { get; set; } = string.Empty;
        
        public int RolId {get; set;} 
        public Rol Rol {get; set; } = null!;

    }
}