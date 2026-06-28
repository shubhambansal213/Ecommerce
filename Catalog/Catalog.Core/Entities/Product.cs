using System;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id {get;set;}

        public string Name { get; set; }

        public string Summary{get;set;}

        public string Description{get;set;}

        public string ImageFile{get;set;}

        public ProductBrand Brand{get;set;}

        public ProductType Type{get;set;}

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price {get;set;}

        public DateTimeOffset CreatedDate{get;set;}

        

    }
}
