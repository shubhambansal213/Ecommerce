using System;
using Catalog.Core;
using MediatR;

namespace Catalog.Application
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQueries, IList<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            _brandRepository=brandRepository;
        }
        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQueries request, CancellationToken cancellationToken)
        {
            var brandList=await _brandRepository.GetAllBrands();
            return brandList.ToResponseList();
        }
    }
}
