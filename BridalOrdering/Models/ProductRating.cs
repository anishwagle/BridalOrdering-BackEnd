using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class ProductRating : Document
    {
        public String UserName { get; set; }
        public String ProductId { get; set; }
        public String Message { get; set; } 
        public int Rating { get; set; }
        
    }

}