using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Core;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }
        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var existing=await _productRepository.GetProductById(request.Id);

            if (existing == null)
            {
                 throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
            }

            var status=await _productRepository.DeleteProduct(request.Id);
            return status;
            
        }
    }
}