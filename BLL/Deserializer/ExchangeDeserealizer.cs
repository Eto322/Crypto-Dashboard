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
        private DeserializerHelper _helper;
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
            _helper=new DeserializerHelper();
            var tickers = data["tickers"];
            if (tickers != null && tickers.Any())
            {
                var item = tickers.First();
                return new ExchangeModel
                {
                    Id = _helper.GetStringValue(item, "market", "identifier"),
                    Name = _helper.GetStringValue(item, "market", "name"),
                    Price = _helper.GetDecimalValue(item, "last"),
                    TradeUrl = _helper.GetStringValue(item, "trade_url"),
                    Image = _helper.GetStringValue(item, "market", "logo"),
                    CoinId = coinId
                };
            }

            return new ExchangeModel(); 
        }
    }
}
