using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace DAL.ApiClients
{
    public class CoinGeckoResponse
    {
        public List<Cryptocurrency> Data { get; set; }
    }

    public class CoinGeckoApi
    {
        private readonly string _apikey;
        private readonly HttpClient _client;

        public CoinGeckoApi(string apikey)
        {
            _apikey = apikey;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("x-cg-demo-api-key", _apikey);
        }

        public string GetTopNCryptos(int limit, string vsCurrency = "usd")
        {
            string url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency={vsCurrency}&order=market_cap_desc&per_page={limit}&page=1&sparkline=false";

            try
            {
                HttpResponseMessage response = _client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                JArray jsonArray = JArray.Parse(responseBody);

                
                var cryptocurrencies = jsonArray.ToObject<List<Cryptocurrency>>();

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return e.Message;
            }
        }
    }
}