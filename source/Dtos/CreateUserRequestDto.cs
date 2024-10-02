using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api01.source.models;

namespace api01.source.Dtos
{
    public class CreateUserRequestDto
    {
        [Required]
        public String Name { get; set; } = string.Empty;
        [Required]
        public String Rut { get; set; } = string.Empty;
        [Required]
        public String Email { get; set; } = string.Empty;
        
        [Required]
        public int RolId {get; set;}
    }
}