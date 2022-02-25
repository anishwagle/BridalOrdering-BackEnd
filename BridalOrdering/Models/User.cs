using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public enum UserType{
        USER,
        ADMIN
    }
    public class User : Document
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string PasswordHash { get; set; }
    }

}