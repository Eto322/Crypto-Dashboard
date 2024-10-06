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

        public string SearchCoins(string query)
        {
            string url = $"https://api.coingecko.com/api/v3/search?query={query}";
            List<string> ids = ExtractId(GetResponse(url));
            return GetCryptoDetailsById(ids);
        }

        public string GetTopCryptos(int limit, string vsCurrency = "usd", int pageNumber = 1)
        {
            string url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency={vsCurrency}&order=market_cap_desc&per_page={limit}&page={pageNumber}&sparkline=false";

            return GetResponse(url);
        }

        public string GetCryptoFullInfoById(string id)
        {
            string url = $"https://api.coingecko.com/api/v3/coins/{id}";
            return GetResponse(url);
        }
        
        public string GetCryptoDetailsById(List<string>ids, string vsCurrency = "usd")
        {
            string idsString = string.Join(",", ids);
            string url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency={vsCurrency}&ids={idsString}&order=market_cap_desc&per_page=250&page=1&sparkline=false";
            
            return GetResponse(url);
        }

        private List<string> ExtractId(string response)
        {
            var ids = new List<string>();

            try
            {
                JObject jsonResponse = JObject.Parse(response);
                JArray coinsArray = (JArray)jsonResponse["coins"];

                foreach (var coin in coinsArray)
                {
                    string id = coin["id"].ToString();
                    ids.Add(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error parsing response: {e.Message}");
                return new List<string>();
               
            }

            return ids;
        }
        
     
        
        private string GetResponse (string url)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}