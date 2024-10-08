using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Deserializer.Helper;
using BLL.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BLL.Deserializer
{
    public class ExchangeDeserializer
    {
        
        public List<ExchangeModel> Deserialize(string json)
        {
            var exchanges = JsonConvert.DeserializeObject<List<ExchangeModel>>(json);
            return exchanges;
        }

        public ExchangeModel Deserialize(string json,string coinId)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new ExchangeModel(); 
            }

            var jsonData = JObject.Parse(json);
            var Data = ConvertToExchangeModel(jsonData, coinId);
            return ConvertToExchangeModel(jsonData, coinId);
        }

        private ExchangeModel ConvertToExchangeModel(JToken data,string coinId)
        {
           
            var tickers = data["tickers"];
            if (tickers != null && tickers.Any())
            {
                var item = tickers.First();
                return new ExchangeModel
                {
                    Id = DeserializerHelper.GetStringValue(item, "market", "identifier"),
                    Name = DeserializerHelper.GetStringValue(item, "market", "name"),
                    Price = DeserializerHelper.GetDecimalValue(item, "last"),
                    TradeUrl = DeserializerHelper.GetStringValue(item, "trade_url"),
                    Image = DeserializerHelper.GetStringValue(item, "market", "logo"),
                    CoinId = coinId
                };
            }

            return new ExchangeModel(); 
        }
    }
}
