using System;

namespace Catalog.Infrastructure
{
    public class DatabaseSettings
    {
        public string ConnectionString {get;set;}
        public string DatabaseName{get;set;}

        public string BrandCollectionName{get;set;}

        public string TypeCollectionName{get;set;}

        public string ProductCollectionName{get;set;}
    }
}


// {
//   "DatabaseSettings":{
//     "ConnectionString": "mongodb://localhost:27017",
//     "DatabaseName":"CatalogDb",
//     "ProductCollectionName":"Products",
//     "BrandCollectionName":"Brands",
//     "TypeCollectionName":"Types"
//   }
// }