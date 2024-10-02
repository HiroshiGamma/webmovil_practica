using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api01.source.Dtos;
using api01.source.models;


namespace api01.source.Mappers
{
    public static class ProductMapper
    {
        
        public static ProductDto toProductDto(this Product productModel)
        {
            return new ProductDto
            {   
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price
            };
        }

        public static Product toProductFromCreateDto(this CreateProductRequestDto createProductRequestDto)
        {
            return new Product
            {
                Name = createProductRequestDto.Name,
                Price = createProductRequestDto.Price
            };
        }
        
    }
}