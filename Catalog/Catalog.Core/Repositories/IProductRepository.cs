using System;

namespace Catalog.Core
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Pagination<Product>> GetProduct(CatalogSpecParams specParams);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> GetProductByBrand(string name);

        Task<Product> GetProductById(string c);

        Task<Product> CreateProduct(Product product);

        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(string productId);    

        Task<ProductBrand> GetBrandsByIdAsync(string brandId);

        Task<ProductType> GetTypeByIdAsync(string typeId);
    }
}
