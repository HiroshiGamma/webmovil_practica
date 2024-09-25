using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api01.source.models
{
    public class Product
    {
        public String Name { get; set; } = string.Empty;
        
        public int Id { get; set; }

        public int Price { get; set; }


        //Entityframework relationships

        public List<User> Users {get; set;} = [];

        
    }
}