using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models
{
    public class UserLoginResponse :Document
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
    }

}