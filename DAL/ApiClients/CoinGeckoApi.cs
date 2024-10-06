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

            return GetResponse(url);
        }

        public string GetTopCryptos(int limit, string vsCurrency = "usd", int pageNumber = 1)
        {
            string url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency={vsCurrency}&order=market_cap_desc&per_page={limit}&page={pageNumber}&sparkline=false";

            return GetResponse(url);
        }

        private string GetResponse (string url)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                JArray jsonArray = JArray.Parse(responseBody);

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