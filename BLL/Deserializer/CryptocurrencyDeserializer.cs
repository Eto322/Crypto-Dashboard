using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Deserializer.Helper;
using BLL.Model;

namespace BLL.Deserializer
{
    
    public class CryptocurrencyDeserializer
    {
        private DeserializerHelper _helper;
        public List<Cryptocurrency> Deserialize(string json, bool isCoinCap)
        {
            _helper=new DeserializerHelper();
            if (string.IsNullOrEmpty(json))
            {
                return new List<Cryptocurrency>();
            }
            
            if (isCoinCap)
            {
                
                var jsonData = JObject.Parse(json)["data"];
                return ConvertCoinCapToCryptocurrency(jsonData);
            }
            else
            {

                var jsonData = JArray.Parse(json);
                return ConvertCoinGeckoToCryptocurrency(jsonData);
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
                    Explorer = _helper.GetStringValue(item, "explorer"),
                    MarketCapRank =  _helper.GetDecimalValue(item,"rank")

                });
            }
            
            return cryptocurrencies;
        }

        private List<Cryptocurrency> ConvertCoinGeckoToCryptocurrency(JToken data)
        {
            var cryptocurrencies = new List<Cryptocurrency>();

            foreach (var item in data)
            {
                cryptocurrencies.Add(new Cryptocurrency
                {
                    IdGecko = _helper.GetStringValue(item, "id"),
                    Symbol = _helper.GetStringValue(item, "symbol").ToUpper(),
                    Name = _helper.GetStringValue(item, "name"),
                    CirculatingSupply = _helper.GetDecimalValue(item, "circulating_supply"),
                    TotalSupply = _helper.GetDecimalValue(item, "total_supply"),
                    MaxSupply = _helper.GetDecimalValue(item, "max_supply"),
                    MarketCap = _helper.GetDecimalValue(item, "market_cap"),
                    TotalVolume = _helper.GetDecimalValue(item, "total_volume"),
                    CurrentPrice = _helper.GetDecimalValue(item, "current_price"),
                    PriceChange24H = _helper.GetDecimalValue(item, "price_change_24h"),
                    PriceChangePercentage24H = _helper.GetDecimalValue(item, "price_change_percentage_24h"),
                    MarketCapRank = _helper.GetDecimalValue(item, "market_cap_rank"),
                    Image = _helper.GetStringValue(item, "image"),
                    LastUpdated = _helper.GetStringValue(item, "last_updated")
                });
            }

            return cryptocurrencies;
        }
    }
}
