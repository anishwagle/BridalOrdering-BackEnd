using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class Order : Document
    {
        public string UserId { get; set; }
        public Address Address { get; set; }
        public List<Product> Products { get; set; }
    }

}