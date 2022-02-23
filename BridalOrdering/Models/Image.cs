using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public enum ImageType{
        Banner,
        Thumbnail
    }
    public class Image : Document
    {
        public ImageType Type { get; set; }
        public string Url { get; set; }
        
    }

}