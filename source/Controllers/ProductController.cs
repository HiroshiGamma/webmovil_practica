using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api01.source.data;
using Microsoft.EntityFrameworkCore;
using api01.source.models;

namespace api01.source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

       private readonly ApplicationDBContext _context;

       public ProductController(ApplicationDBContext context)
       {

            _context = context;

       }

       [HttpGet]
       public IActionResult Get()
       {
            var products = _context.Products.ToList();
            return Ok(products);
       }


       [HttpGet("{id}")]
       public IActionResult GetById([FromRoute] int id)
       {

            var products = _context.Products.FirstOrDefault(p => p.Id == id);
            if(products == null)
            {
                return NotFound();
            }
            return Ok(products);
       }

       [HttpPost]
       public IActionResult Post([FromBody] Product product)
       {

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);

       }

       [HttpPut("{id}")]
       public IActionResult Put([FromRoute] int id, [FromBody] Product product)
       {

            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if(existingProduct == null)
            {
                return NotFound();
            }
            
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            _context.SaveChanges();
            return Ok(existingProduct);
       }

       [HttpDelete("{id}")]

       public IActionResult Delete([FromRoute] int id)
       {
            var product = _context.Products.FirstOrDefault( p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Producto Deleted");
       }

    }
}