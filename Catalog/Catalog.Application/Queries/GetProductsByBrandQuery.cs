using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductsByBrandQuery(string BrandName):IRequest<IList<ProductResponse>>;
}