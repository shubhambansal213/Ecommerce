using System;

namespace Catalog.Core
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllType();

        Task<ProductType> GetByIdAsync(string id);

    }
}
