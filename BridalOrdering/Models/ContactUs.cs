using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class ContactUs : Document
    {
        public string Name { get; set; }
        public string  Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }

}