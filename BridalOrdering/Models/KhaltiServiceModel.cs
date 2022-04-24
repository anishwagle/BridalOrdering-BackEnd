using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
namespace BridalOrdering.Models.Khalti
{
    public class PaymentType{
        public string Idx { get; set; }
        public string Name { get; set; }
    }
    public class State{
        public string Idx { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
    }
    public class KhaltiUser{
        public string Idx { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }

    public class KhaltiSuccessResponse
    {
        public string Idx { get; set; }
        public PaymentType Type { get; set; }
        public State State { get; set; }
        public KhaltiUser User { get; set; }
        public KhaltiUser Merchant { get; set; }
        public string Amount { get; set; }
        public string Fee_Amount { get; set; }
        public string Refunded { get; set; }
        public string Created_On { get; set; }
        public string Ebanker { get; set; }

    }

    public class KhaltiErrorResponse{
        public string Error_Key { get; set; }
    }

    public class KhaltiResponse{
        public KhaltiErrorResponse ErrorResponse { get; set; }
        public KhaltiSuccessResponse SuccessResponse { get; set; }
    }

    /*khalti controller request body*/
    public class KhaltiReqBody{
        public string Token { get; set; }
        public string Amount { get; set; }
    }


}