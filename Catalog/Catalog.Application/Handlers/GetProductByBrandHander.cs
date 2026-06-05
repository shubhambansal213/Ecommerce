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
    public class GetProductByBrandHander : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandHander(IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList=await _productRepository.GetProductByBrand(request.BrandName);
            return productList.ToResponseList();
        }
    }
}