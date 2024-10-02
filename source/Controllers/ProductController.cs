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
using api01.source.Interfaces;
using api01.source.Repository;

namespace api01.source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

       private readonly iProductRepository _productRepository;

       public ProductController(iProductRepository productRepository)
       {
            
            _productRepository = productRepository;
        

       }

       [HttpGet]
       public async Task<ActionResult> GetAll()
       {

            var products = await _productRepository.GetAll();
            var productDto = products.Select(p => p.toProductDto());
            return Ok(productDto);
            
       }


       [HttpGet("{id}")]
       public async Task<IActionResult> GetById([FromRoute] int id)
       {

            var products = await _productRepository.GetById(id);
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
            await _productRepository.Post(productModel);
            return CreatedAtAction(nameof(GetById), new{ id = productModel.Id}, productModel.toProductDto());

       }

       [HttpPut("{id}")]
       public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProductRequestDto updateDto)
       {

            var productModel = await _productRepository.Put(id,updateDto);
            if(productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel.toProductDto());
       }

       [HttpDelete("{id}")]

       public async Task<IActionResult> Delete([FromRoute] int id)
       {
            var product = await _productRepository.Delete(id);
            if(product == null)
            {
                return NotFound();
            }

            
            return NoContent();
       }

    }
}