using System;

namespace Catalog.Core
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();

        Task<ProductBrand> GetBrandAsync(string id);
    }
}
