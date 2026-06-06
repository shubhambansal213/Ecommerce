using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameHandler(IProductRepository productRepository)
        {
           _productRepository =productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList=await _productRepository.GetProductByName(request.name);
            var ProductResponse=productList.ToResponseList();
            return ProductResponse;
        }
    }
}