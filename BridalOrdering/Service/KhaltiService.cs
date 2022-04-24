using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BridalOrdering.Models;
using BridalOrdering.Models.Khalti;
using BridalOrdering.Store;
using BridalOrdering.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;


namespace BridalOrdering.Services
{
    public class KhaltiService 
    {
        
        public KhaltiService()
        {
           
        }

        public async Task<KhaltiResponse> VerifyKhaltiPayment(KhaltiReqBody data)
        {

               using var httpClient = new HttpClient();
               httpClient.DefaultRequestHeaders.Add("Authorization", "Key test_secret_key_5ec0ebb9f15047479ef1bcc502c35bd2");

                 var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                var serializedData = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://khalti.com/api/v2/payment/verify/", serializedData);
                var response2 = await response.Content.ReadAsStringAsync();
                /////////////////////////////////////////
                if (!response.IsSuccessStatusCode)
                    {
                         var errorResp = JsonConvert.DeserializeObject<KhaltiErrorResponse>(response2);
                         var er= new KhaltiResponse();
                         er.SuccessResponse=null;
                         er.ErrorResponse=errorResp;
                         return er;
                    }
                    

                var successResp = JsonConvert.DeserializeObject<KhaltiSuccessResponse>(response2);
                var sr= new KhaltiResponse();
                sr.SuccessResponse=successResp;
                sr.ErrorResponse=null;
                return sr;

                ////////////////////////////////////////////
        }  
    }
}
