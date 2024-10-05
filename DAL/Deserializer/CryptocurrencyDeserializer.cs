using DAL.ApiClients;
using DAL.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Deserializer
{
    public class CryptocurrencyDeserializer
    {
        public List<Cryptocurrency> Deserialize(string json, bool isCoinCap)
        {
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
                    IdCap = item["id"].ToString(),
                    Symbol = item["symbol"].ToString().ToUpper(),
                    Name = item["name"].ToString(),
                    CirculatingSupply = decimal.TryParse(item["supply"]?.ToString(), out var supply) ? supply : (decimal?)null,
                    MaxSupply = decimal.TryParse(item["maxSupply"]?.ToString(), out var maxSupply) ? maxSupply : (decimal?)null,
                    MarketCapUsd = decimal.TryParse(item["marketCapUsd"]?.ToString(), out var marketCap) ? marketCap : (decimal?)null,
                    VolumeUsd24Hr = decimal.TryParse(item["volumeUsd24Hr"]?.ToString(), out var volume) ? volume : (decimal?)null,
                    CurrentPrice = decimal.TryParse(item["priceUsd"]?.ToString(), out var price) ? price : (decimal?)null,
                    ChangePercent24Hr = decimal.TryParse(item["changePercent24Hr"]?.ToString(), out var change) ? change : (decimal?)null,
                    Explorer = item["explorer"]?.ToString()
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
