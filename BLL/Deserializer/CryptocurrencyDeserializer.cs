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
       
        public List<CryptocurrencyModel> Deserialize(string json, bool isCoinCap)
        {
            
            if (string.IsNullOrEmpty(json))
            {
                return new List<CryptocurrencyModel>();
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

        private List<CryptocurrencyModel> ConvertCoinCapToCryptocurrency(JToken data)
        {
            var cryptocurrencies = new List<CryptocurrencyModel>();

            foreach (var item in data)
            {
                cryptocurrencies.Add(new CryptocurrencyModel
                { 
                    IdCap = DeserializerHelper.GetStringValue(item, "id"),
                    Symbol = DeserializerHelper.GetStringValue(item, "symbol").ToUpper(),
                    Name = DeserializerHelper.GetStringValue(item, "name"),
                    CirculatingSupply = DeserializerHelper.GetDecimalValue(item, "supply"),
                    MaxSupply = DeserializerHelper.GetDecimalValue(item, "maxSupply"),
                    MarketCapUsd = DeserializerHelper.GetDecimalValue(item, "marketCapUsd"),
                    VolumeUsd24Hr = DeserializerHelper.GetDecimalValue(item, "volumeUsd24Hr"),
                    CurrentPrice = DeserializerHelper.GetDecimalValue(item, "priceUsd"),
                    ChangePercent24Hr = DeserializerHelper.GetDecimalValue(item, "changePercent24Hr"),
                    Explorer = DeserializerHelper.GetStringValue(item, "explorer"),
                    MarketCapRank =  DeserializerHelper.GetDecimalValue(item,"rank")

                });
            }
            
            return cryptocurrencies;
        }

        private List<CryptocurrencyModel> ConvertCoinGeckoToCryptocurrency(JToken data)
        {
            var cryptocurrencies = new List<CryptocurrencyModel>();

            foreach (var item in data)
            {
                cryptocurrencies.Add(new CryptocurrencyModel
                {
                    IdGecko = DeserializerHelper.GetStringValue(item, "id"),
                    Symbol = DeserializerHelper.GetStringValue(item, "symbol").ToUpper(),
                    Name = DeserializerHelper.GetStringValue(item, "name"),
                    CirculatingSupply = DeserializerHelper.GetDecimalValue(item, "circulating_supply"),
                    TotalSupply = DeserializerHelper.GetDecimalValue(item, "total_supply"),
                    MaxSupply = DeserializerHelper.GetDecimalValue(item, "max_supply"),
                    MarketCap = DeserializerHelper.GetDecimalValue(item, "market_cap"),
                    TotalVolume = DeserializerHelper.GetDecimalValue(item, "total_volume"),
                    CurrentPrice = DeserializerHelper.GetDecimalValue(item, "current_price"),
                    PriceChange24H = DeserializerHelper.GetDecimalValue(item, "price_change_24h"),
                    PriceChangePercentage24H = DeserializerHelper.GetDecimalValue(item, "price_change_percentage_24h"),
                    MarketCapRank = DeserializerHelper.GetDecimalValue(item, "market_cap_rank"),
                    Image = DeserializerHelper.GetStringValue(item, "image"),
                    LastUpdated = DeserializerHelper.GetStringValue(item, "last_updated")
                });
            }

            return cryptocurrencies;
        }
    }
}
