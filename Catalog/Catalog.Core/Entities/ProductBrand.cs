using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core
{
    public class ProductBrand:BaseEntity
    {
        [BsonElement("Name")]
        public string Name{get;set;}
    }
}
