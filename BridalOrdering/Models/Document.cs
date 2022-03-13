using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System;
namespace BridalOrdering.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
         string  Id { get; set; }
         DateTime CreatedAt{get; set;}
    }

    public abstract class Document : IDocument
    {
        public string Id { get; set; }
        public DateTime CreatedAt {get; set;}
       public Document(){
            this.Id=Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Today;
        }
    }
}