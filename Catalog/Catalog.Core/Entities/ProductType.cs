using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core
{
    public class ProductType:BaseEntity
    {
        [BsonElement("Name")]
        public string Name{get;set;}
    }
}
