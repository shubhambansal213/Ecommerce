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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _productRepository.GetProductById(request.Id);

            if(existing == null)
            {
                throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
            }

            var brand=await  _productRepository.GetBrandsByIdAsync(request.BrandId);
            var type=await _productRepository.GetTypeByIdAsync(request.TypeId);

             if(brand==null && type==null)
            {
                throw new ApplicationException("Invalid Brand or Types");
            }

            //Mapper Role
            var updateProduct=request.ToUpdateEntiy(existing,brand,type);

            return await _productRepository.UpdateProduct(updateProduct);

        }

    }
}