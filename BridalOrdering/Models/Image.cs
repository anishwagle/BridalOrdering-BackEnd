using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public enum ImageType{
        Banner,
        Thumbnail,
        Package
    }
    public class Image : Document
    {
        public ImageType Type { get; set; }
        public string Img { get; set; }
        
    }

}