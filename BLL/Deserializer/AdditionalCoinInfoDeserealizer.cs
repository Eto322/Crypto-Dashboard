using BLL.Deserializer.Helper;
using BLL.Manager.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Deserializer
{
    public class AdditionalCoinInfoDeserializer
    {
        
        
        public AdditionalCoinInfoModel Deserializer ( string json)
        {
            
            if (string.IsNullOrEmpty(json))
            {
                return null; 
            }

           //Parse Json data
            var jsonData = JObject.Parse(json);
            
           //Get Id
            var id = DeserializerHelper.GetStringValue(jsonData, "id");

            //Getting first homepage link
            var homepage = DeserializerHelper.GetStringValue(jsonData["links"], "homepage")?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            
            //Getting first link
            var repos = jsonData["links"]["repos_url"]["github"];
           
            var firstRepository = repos?.FirstOrDefault()?.ToString();

            return new AdditionalCoinInfoModel(id, homepage, firstRepository);
        }
    }
}
