using System;
using Catalog.Core;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure
{

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<ProductBrand> _brands;
        private readonly IMongoCollection<ProductType> _types;
        private readonly IMongoCollection<Product> _products;
        public ProductRepository(IConfiguration config)
        {
            var client=new MongoClient(config["DatabaseSettings : ConnectionString"]);
            var db= client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _brands=db.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
            _types=db.GetCollection<ProductType>(config["DatabaseSettings:TypeCollectionName"]);
            _products=db.GetCollection<Product>(config["DatabaseSettings:ProductCollectionName"]);
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;

        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var deleteProduct= await _products.DeleteOneAsync(x=>x.Id==productId);
            return deleteProduct.IsAcknowledged && deleteProduct.DeletedCount>0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(p=>true).ToListAsync(); 
        }

        public async Task<ProductBrand> GetBrandsByIdAsync(string brandId)
        {
            return await _brands.Find(b=>b.Id==brandId).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProduct(CatalogSpecParams catalogSpecParams)
        {
            var builder=Builders<Product>.Filter;
            var filter=builder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter&=builder.Where(p=>p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrnadId))
            {
                filter&=builder.Eq(p=>p.Brand.Id,catalogSpecParams.BrnadId);
            }
             if (!string.IsNullOrEmpty(catalogSpecParams.TYpeId))
            {
                filter&=builder.Eq(p=>p.Brand.Id,catalogSpecParams.TYpeId);
            }

            var totalIteam=await _products.CountDocumentsAsync(filter);
            var data=await ApplyDataFilters(catalogSpecParams,filter);
            return new Pagination<Product>(
                catalogSpecParams.PageIndex,
                catalogSpecParams.PageSize,
                (int)totalIteam,
                data
            );
        }

      

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            return await _products.Find(x=>x.Brand.Name.ToLower()==name.ToLower()).ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _products.Find(x=>x.Id==productId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            //substring base search
            var filter =Builders<Product>.Filter.Regex(p=>p.Name,new BsonRegularExpression($".*{name}.*", "i"));
            return await _products.Find(filter).ToListAsync();
        }

        public async Task<ProductType> GetTypeByIdAsync(string typeId)
        {
            return await _types.Find(x=>x.Id==typeId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct=await _products.ReplaceOneAsync(x=>x.Id==product.Id,product);
            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount>0;
        }

        private async Task<IReadOnlyCollection<Product>> ApplyDataFilters(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var softDefn=Builders<Product>.Sort.Ascending("Name");

            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                softDefn=catalogSpecParams.Sort switch
                {
                    "priceAsc"=>Builders<Product>.Sort.Ascending(p=>p.Price),
                    "priceDsc"=>Builders<Product>.Sort.Descending(p=>p.Price),

                    _=>Builders<Product>.Sort.Ascending(p=>p.Name),

                };
            }

            return await _products
            .Find(filter)
            .Sort(softDefn)
            .Skip(catalogSpecParams.PageSize*(catalogSpecParams.PageIndex))
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
        }
    }
}
