using DAL.ApiClients;
using DAL.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Deserializer.Helper;

namespace DAL.Deserializer
{
    
    public class CryptocurrencyDeserializer
    {
        private DeserializerHelper _helper;
        public List<Cryptocurrency> Deserialize(string json, bool isCoinCap)
        {
            _helper=new DeserializerHelper();
            if (isCoinCap)
            {
                
                var jsonData = JObject.Parse(json)["data"];
                return ConvertCoinCapToCryptocurrency(jsonData);
            }
            else
            {
                var data = JsonConvert.DeserializeObject<List<Cryptocurrency>>(json);
                return ConvertCoinGeckoToCryptocurrency(data);
            }
        }

        private List<Cryptocurrency> ConvertCoinCapToCryptocurrency(JToken data)
        {
            var cryptocurrencies = new List<Cryptocurrency>();

            foreach (var item in data)
            {
                cryptocurrencies.Add(new Cryptocurrency
                {
                    IdCap = _helper.GetStringValue(item, "id"),
                    Symbol = _helper.GetStringValue(item, "symbol").ToUpper(),
                    Name = _helper.GetStringValue(item, "name"),
                    CirculatingSupply = _helper.GetDecimalValue(item, "supply"),
                    MaxSupply = _helper.GetDecimalValue(item, "maxSupply"),
                    MarketCapUsd = _helper.GetDecimalValue(item, "marketCapUsd"),
                    VolumeUsd24Hr = _helper.GetDecimalValue(item, "volumeUsd24Hr"),
                    CurrentPrice = _helper.GetDecimalValue(item, "priceUsd"),
                    ChangePercent24Hr = _helper.GetDecimalValue(item, "changePercent24Hr"),
                    Explorer = _helper.GetStringValue(item, "explorer")
                });
            }
            

            return cryptocurrencies;
        }

        private List<Cryptocurrency> ConvertCoinGeckoToCryptocurrency(List<Cryptocurrency> data)
        {
            throw new NotImplementedException();
        }
    }
}
