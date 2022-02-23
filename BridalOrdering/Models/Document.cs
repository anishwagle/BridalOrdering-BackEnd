using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
         string  Id { get; set; }
    }

    public abstract class Document : IDocument
    {
        public string Id { get; set; }
    }
}