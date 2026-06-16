using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Core;

namespace Catalog.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            if(product==null) return null;
            
            return new ProductResponse
            {
              Id=product.Id,
              Name=product.Name,
              Summary=product.Summary,
              Description=product.Description,
              ImageFile=product.ImageFile,
              Price=product.Price,
              Brand=product.Brand,
              Type=product.Type,
              CreatedDate=product.CreatedDate              
            };
        }

        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> products)
        =>new Pagination<ProductResponse>(
            products.PageIndex,
            products.PageSize,
            products.Count,
            products.Data.Select(p=>p.ToResponse()).ToList()
        );

        public static IList<ProductResponse> ToResponseList(this IEnumerable<Product> products)
        {
            return products.Select(p=>p.ToResponse()).ToList();
        }

        public static Product ToEntity(this CreateProductCommand command,ProductBrand brand,ProductType type)=>
        new Product
        {
          Name=command.Name,
          Summary=command.Summary,
          Description=command.Description,
          ImageFile=command.ImageFile,
          Brand=brand,
          Type=type,
          Price=command.Price,
          CreatedDate=DateTimeOffset.UtcNow  
        };

        public static Product ToUpdateEntiy(this UpdateProductCommand command,Product existing,ProductBrand brand,ProductType type)
        {
            return new Product
             {
              Id=existing.Id,
              Name=command.Name,
              Summary=command.Summary,
              ImageFile=command.ImageFile,
              Brand=brand,
              Type=type,
              Price=command.Price,
              CreatedDate=existing.CreatedDate  
            };
        }

        public static ProductDto ToDto(this ProductResponse product)
        {
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            (
                product.Id,
                product.Name,
                product.Summary,
                product.Description,
                product.ImageFile,
                new BrandDto(product.Brand.Id,product.Brand.Name),
                new TypeDto(product.Type.Id,product.Type.Name),
                product.Price,
                DateTimeOffset.UtcNow
            );
        }

        public static UpdateProductCommand ToCommand(this UpdateProductDto dto,string id)
        {
            return new UpdateProductCommand
            {
                Id=id,
                Name=dto.Name,
                Summary=dto.Sumamry,
                Description=dto.Description,
                ImageFile=dto.ImageFile,
                Price=dto.Price,
                BrandId=dto.BrandId,
                TypeId=dto.TypeId

            };
        }

        public static CreateProductDto ToCommand(this CreateProductDto dto)
        {
            return new CreateProductDto
            {
              Name=dto.Name,
              Sumary=dto.Sumary,  
              Description=dto.Description,
              ImageFile=dto.ImageFile,
              BrandId=dto.BrandId,
              TypeId=dto.TypeId,
              Price=dto.Price
            };
        }
        
    }
}