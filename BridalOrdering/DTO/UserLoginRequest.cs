using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class UserLoginRequest 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}