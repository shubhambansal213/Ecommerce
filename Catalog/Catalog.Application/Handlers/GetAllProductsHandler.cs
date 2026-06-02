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
    public class GetAllProductsHandler:IRequestHandler<GetAllProductsQuery,Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }

        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList=await _productRepository.GetProduct(request.CatalogSpecParams);
            var productResponseList=productList.ToResponse();
            return productResponseList;
        }
    }
}