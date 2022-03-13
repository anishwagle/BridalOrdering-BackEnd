using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class OrderedProduct : Document
    {
       public Product Product { get; set; }
       public int Quantity { get; set; }
        
    }

}