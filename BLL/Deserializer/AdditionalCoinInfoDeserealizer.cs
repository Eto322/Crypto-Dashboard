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
        private DeserializerHelper _helper;
        
        public AdditionalCoinInfoModel Deserializer ( string json)
        {
            _helper = new DeserializerHelper();
            
            if (string.IsNullOrEmpty(json))
            {
                return null; 
            }

            // Парсинг JSON ответа
            var jsonData = JObject.Parse(json);

            // Извлечение id
            var id = _helper.GetStringValue(jsonData, "id");

            var homepage = _helper.GetStringValue(jsonData["links"], "homepage")?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

            // Извлечение первой ссылки на репозиторий
            var repos = jsonData["links"]["repos_url"]["github"];
            var firstRepository = repos?.FirstOrDefault()?.ToString();

            return new AdditionalCoinInfoModel(id, homepage, firstRepository);
        }
    }
}
