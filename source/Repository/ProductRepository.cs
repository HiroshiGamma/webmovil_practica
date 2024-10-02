using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.data;
using api01.source.Dtos;
using api01.source.Interfaces;
using api01.source.models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace api01.source.Repository
{
    public class ProductRepository : iProductRepository
    {

        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
            
        }
        public async Task<Product?> Delete(int id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync( x => x.Id == id);
            if(productModel == null)
            {
                throw new Exception("Product not found");
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;

        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> Post(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> Put(int id, UpdateProductRequestDto productDto)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(productModel == null)
            {
                throw new Exception("Product not found");
            }

            productModel.Name = productDto.Name;
            productModel.Price = productDto.Price;

            await _context.SaveChangesAsync();
            return productModel;
        }
    }
}