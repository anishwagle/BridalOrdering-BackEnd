using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class Product : Document
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }  
        public List<Image> Images { get; set; }
        public string CategoryId { get; set; }
        public string Package { get; set; }
        
    }

}