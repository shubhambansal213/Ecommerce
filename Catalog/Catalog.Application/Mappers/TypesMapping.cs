using System;
using Catalog.Core;

namespace Catalog.Application
{
    //extension method
    public static class TypesMapping
    {
        public static TypeResponse ToResponse(this ProductType type)
        {
            return new TypeResponse
            {
                Id=type.Id,
                Name=type.Name,
            };
        }

        public static IList<TypeResponse> ToResponseList(this IEnumerable<ProductType> types)
        {
            return types.Select(x=>x.ToResponse()).ToList();
        }
    }
}
