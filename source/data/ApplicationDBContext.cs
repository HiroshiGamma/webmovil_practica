using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.models;
using Microsoft.EntityFrameworkCore;

namespace api01.source.data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        
        public DbSet<User> Users {get; set; } = null!; 

        public DbSet<Product> Products {get; set; } = null!;

        public DbSet<Rol> Rols {get; set; } = null!; 


    }
}