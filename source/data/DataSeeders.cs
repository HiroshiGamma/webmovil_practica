using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.models;
using Bogus;


namespace api01.source.data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDBContext>();
               
                if(!context.Rols.Any()){

                    context.Rols.AddRange( 
                        new Rol {Name = "Admin"},
                        new Rol { Name = "User"} 
                    );
                    context.SaveChanges();

                }

                var existingRuts = new HashSet<string>();

                if(!context.Users.Any()){


                    var userFaker = new Faker<User>()
                    .RuleFor(u => u.Rut, f => GenerateUniqueRandomRut(existingRuts))
                    .RuleFor(u => u.Name, f=> f.Person.FullName)
                    .RuleFor(u => u.Email, f => f.Person.Email)
                    .RuleFor(u => u.RolId, f => f.Random.Number(1,2));

                    var users = userFaker.Generate(10);
                    context.Users.AddRange(users);
                    context.SaveChanges(); 
                }

                if(!context.Products.Any())
                {
                    var productFaker = new Faker<Product>()
                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Price, f => f.Random.Number(1000, 100000));

                    var products = productFaker.Generate(10);
                    context.Products.AddRange(products);
                    context.SaveChanges();
                }

                
                context.SaveChanges();

            }
        }

        private static string GenerateUniqueRandomRut(HashSet<string> existingRuts)
        {
            string rut;
            do 
            {
                rut = GenerateRandomRut();
            } while ( existingRuts.Contains(rut));

            existingRuts.Add(rut);
            return rut;
            
        }


        
        private static string GenerateRandomRut()
        {
            Random random = new Random();
            int number = random.Next(10000000, 99999999); // Genera un número entre 10.000.000 y 99.999.999
            char checkDigit = GenerateCheckDigit(number);
            return $"{number}-{checkDigit}";
        }

        private static char GenerateCheckDigit(int number)
        {
            int sum = 0;
            int factor = 2;
            
            while (number > 0)
            {
                int digit = number % 10;
                sum += digit * factor;
                factor = factor == 7 ? 2 : factor + 1;
                number /= 10;
            }

            int mod = 11 - (sum % 11);
            if (mod == 11) return '0';
            if (mod == 10) return 'K';
            
            return mod.ToString()[0];
        }
    }


}