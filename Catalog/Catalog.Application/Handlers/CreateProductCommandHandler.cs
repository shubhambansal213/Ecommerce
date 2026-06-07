using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            productRepository=_productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var brand=await _productRepository.GetBrandsByIdAsync(request.BrandId);
            var type=await _productRepository.GetTypeByIdAsync(request.TypeId);


            if(brand==null && type==null)
            {
                throw new ApplicationException("Invalid Brand or Types");
            }

            var productEntity=request.ToEntity(brand,type);
            var newProduct=await _productRepository.CreateProduct(productEntity);
            return newProduct.ToResponse();
        }
    }
}