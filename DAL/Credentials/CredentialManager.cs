using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DAL.Credentials
{
    public class CredentialManager
    {
        private readonly string _geckoApiKey;
        private readonly string _coincapApiKey;
        public  readonly bool IsCoinCapApiExist = false;
        public  readonly bool IsCoinGeckoApiKeyExist = false;
        public CredentialManager()
        {
            // Get the path to the project's root directory
            string basePath = AppContext.BaseDirectory;

            // Create a configuration builder and specify the path to the configuration file
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); 

            // Build the configuration from the specified sources
            IConfigurationRoot configuration = builder.Build();

            // Retrieve the CoinGecko API key from the configuration settings
            _geckoApiKey = configuration["ApiSettings:CoinGeckoApiKey"];

            // Retrieve the CoinCap API key from the configuration settings
            _coincapApiKey = configuration["ApiSettings:CoinCapApiKey"];

            // Set the flags based on whether the API keys exist
            IsCoinGeckoApiKeyExist = !string.IsNullOrEmpty(_geckoApiKey);
            IsCoinCapApiExist = !string.IsNullOrEmpty(_coincapApiKey);

        }

        public string GetGeckoApiKey()
        {
            return _geckoApiKey;
        }

        public string GetCoinCapApiKey()
        {
            return _coincapApiKey;
        }
    }
}
