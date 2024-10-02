using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.Dtos;
using api01.source.models;

namespace api01.source.Interfaces
{
    public interface iProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product?> GetById(int id);

        Task<Product> Post(Product product);

        Task<Product?> Put(int id, UpdateProductRequestDto productDto);

        Task<Product?> Delete(int id);
    }
}