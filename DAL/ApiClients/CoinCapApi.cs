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
  

    public class CoinCapApi
    {

        private readonly string _apikey;
        private readonly HttpClient _client;
        public CoinCapApi(string apikey) 
        {
            _apikey = apikey;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apikey}");
        }

        public string GetTopCryptos(int limit)
        {
            string url = $"https://api.coincap.io/v2/assets?limit={limit}";

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
