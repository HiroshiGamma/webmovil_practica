using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api01.source.data;
using api01.source.models;
using api01.source.Mappers;
using Microsoft.EntityFrameworkCore;
using api01.source.Dtos;

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
       public async Task<ActionResult> GetAll()
       {

            var products = await _context.Products.ToListAsync();
            var productDto = products.Select(p => p.toProductDto());
            return Ok(productDto);
            
       }


       [HttpGet("{id}")]
       public async Task<IActionResult> GetById([FromRoute] int id)
       {

            var products = await _context.Products.FindAsync(id);
            if(products == null)
            {
                return NotFound();
            }
            return Ok(products.toProductDto());
       }

       [HttpPost]
       public async Task<IActionResult> Post([FromBody] CreateProductRequestDto productDto)
       {

            var productModel = productDto.toProductFromCreateDto();
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new{ id = productModel.Id}, productModel.toProductDto());

       }

       [HttpPut("{id}")]
       public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProductRequestDto updateDto)
       {

            var productModel = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(productModel == null)
            {
                return NotFound();
            }
            
            productModel.Name = updateDto.Name;
            productModel.Price = updateDto.Price;
            await _context.SaveChangesAsync();
            return Ok(productModel.toProductDto());
       }

       [HttpDelete("{id}")]

       public async Task<IActionResult> Delete([FromRoute] int id)
       {
            var product = await _context.Products.FirstOrDefaultAsync( p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
       }

    }
}