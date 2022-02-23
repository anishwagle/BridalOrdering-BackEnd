using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class Carousel : Document
    {
        public Image Image { get; set; }
        
    }

}