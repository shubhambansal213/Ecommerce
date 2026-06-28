using System;
using System.Text.Json;
using Catalog.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace Catalog.Infrastructure
{
    public class DatabaseSeeder
    {
        public static async Task SeedAsync(IOptions<DatabaseSettings> options)
        {
            var setting=options.Value;
            var client=new MongoClient(setting.ConnectionString);
            var db=client.GetDatabase(setting.DatabaseName);
            var brands=db.GetCollection<ProductBrand>(setting.BrandCollectionName);
            var types=db.GetCollection<ProductType>(setting.TypeCollectionName);
            var products=db.GetCollection<Product>(setting.ProductCollectionName);
            
            var SeedBasePath=Path.Combine("Data","SeedData");

            //Seed Brands
            List<ProductBrand> brandList=new List<ProductBrand>();
            if(await brands.CountDocumentsAsync(_ => true) == 0)
            {
                var brandData=await File.ReadAllTextAsync(Path.Combine(SeedBasePath,"brands.json"));
                brandList=JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                await brands.InsertManyAsync(brandList);
            }
            else
            {
                brandList=await brands.Find(_=>true).ToListAsync();
            }

            //Seed Types
            List<ProductType> typeList=new List<ProductType>();
            if(await types.CountDocumentsAsync(_ => true) == 0)
            {
                var typeData=await File.ReadAllTextAsync(Path.Combine(SeedBasePath,"types.json"));
                typeList=JsonSerializer.Deserialize<List<ProductType>>(typeData);
                await types.InsertManyAsync(typeList);
            }
            else
            {
                typeList=await types.Find(_=>true).ToListAsync();
            }

            //Seed Products
            if(await products.CountDocumentsAsync(_ => true) == 0)
            {
                var productData=await File.ReadAllTextAsync(Path.Combine(SeedBasePath,"products.json"));
                var productList=JsonSerializer.Deserialize<List<Product>>(productData);
                foreach(var product in productList)
                {
                    //Reset ID to let Mongo generate one
                     product.Id=null;

                    //Default created date if not set
                    if (product.CreatedDate == default)
                    {
                         product.CreatedDate=DateTimeOffset.UtcNow;
                    }
                }
                await products.InsertManyAsync(productList);
            }
        }
    }
}
