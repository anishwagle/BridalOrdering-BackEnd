using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class Package : Document
    {
        public string Name { get; set; }
        public Image Image { get; set; }
        public List<string> Products { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }

}