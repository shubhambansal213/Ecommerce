using System;
using MediatR;

namespace Catalog.Application
{
    public record GetAllBrandsQueries:IRequest<IList<BrandResponse>>
    {
        
    }
}
