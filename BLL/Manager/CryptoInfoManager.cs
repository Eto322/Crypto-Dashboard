using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Deserializer;
using BLL.Deserializer.Helper;
using BLL.Manager.Helper;
using BLL.Model;
using DAL.ApiClients;
using DAL.Credentials;
using Newtonsoft.Json.Linq;

namespace BLL.Manager
{
    public class CryptoInfoManager
    {
        private readonly CoinGeckoApi _coinGeckoAPi;
        private readonly CoinCapApi _coinCapAPi;
        private readonly CredentialManager _credentialManager;
        private readonly CryptocurrencyDeserializer _cryptocurrencyDeserializer;
        private readonly AdditionalCoinInfoDeserializer _additionalCoinInfoDeserializer;
        private readonly CandlesDeserializer _candlesDeserializer;
        
        public CryptoInfoManager(CredentialManager credentialManager)
        {
            _credentialManager = credentialManager;
            _coinGeckoAPi = new CoinGeckoApi(_credentialManager.GetGeckoApiKey());
            _coinCapAPi = new CoinCapApi(_credentialManager.GetCoinCapApiKey());
            _cryptocurrencyDeserializer = new CryptocurrencyDeserializer();
            _additionalCoinInfoDeserializer= new AdditionalCoinInfoDeserializer();
            _candlesDeserializer = new CandlesDeserializer();
        }

        public List<CryptocurrencyModel> GetTopNCryptos(int n)
        {
            
            if (!_credentialManager.IsCoinCapApiExist && !_credentialManager.IsCoinGeckoApiKeyExist)
            {
                
                return new List<CryptocurrencyModel>(); 
            }

            List<CryptocurrencyModel> geckoData = FetchTopFromGecko(n);
            List<CryptocurrencyModel> capData = FetchTopFromCap(n);

            var topCryptos = new CryptocurrencyMerger().MergeCryptocurrencyData(capData, geckoData);


            return topCryptos;
        }

        public List<CryptocurrencyModel> GetSearchCryptocurrencies(string query)
        {
            if (!_credentialManager.IsCoinCapApiExist && !_credentialManager.IsCoinGeckoApiKeyExist)
            {
                Console.WriteLine("No API keys are available.");
                return new List<CryptocurrencyModel>();
            }

            List<CryptocurrencyModel> geckoData = FetchSearchFromGecko(query);
            List<CryptocurrencyModel> capData = FetchSearchFromCap(query);

            var searched = new CryptocurrencyMerger().MergeCryptocurrencyData(capData, geckoData);

            return searched;
        }

        public AdditionalCoinInfoModel GetAddtionalInfo(string id)
        {
            if (!_credentialManager.IsCoinGeckoApiKeyExist)
            {
                return new AdditionalCoinInfoModel();
            }

            var Additionalinfo = FetchAdditionalCoinInfo(id);

            Additionalinfo.Homepage = LinkCleaner.CleanLink(Additionalinfo.Homepage);
            Additionalinfo.Repository = LinkCleaner.CleanLink(Additionalinfo.Repository);
            return Additionalinfo;
        }

        public List<CandlestickModel> GetCandelsById(string id, string days = "7")
        {

            if (!_credentialManager.IsCoinGeckoApiKeyExist)
            {
                return new List<CandlestickModel>();
            }

            return FetchCandlestickModels(id, days);
        }

        public List<ExchangeModel> GetExchanges()
        {
            if (!_credentialManager.IsCoinGeckoApiKeyExist)
            {
                throw new Exception("CoinGecko API key is not set");
            }

            return ExchangeDataLoader.LoadExchangeModels();
        }
        #region Helpers

        private AdditionalCoinInfoModel FetchAdditionalCoinInfo (string id)
        {
            var geckoData = _coinGeckoAPi.GetCryptoFullInfoById(id);
            var data = _additionalCoinInfoDeserializer.Deserializer(geckoData);
            return data;

        }
        private List<CryptocurrencyModel> FetchTopFromGecko(int n)
        {
            var geckoData = _coinGeckoAPi.GetTopCryptos(n);
            var geckoCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(geckoData, false);
            return geckoCryptocurrencies;
        }

        private List<CryptocurrencyModel> FetchTopFromCap(int n)
        {
            var capData = _coinCapAPi.GetTopCryptos(n);
            var capCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(capData, true);
            return capCryptocurrencies;
        }

        private List<CryptocurrencyModel> FetchSearchFromGecko(string query)
        {
            var geckoData = _coinGeckoAPi.SearchCoins(query);
            var geckoCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(geckoData, false);
            return geckoCryptocurrencies;
        }

        private List<CryptocurrencyModel> FetchSearchFromCap(string query)
        {
            var capData = _coinCapAPi.SearchCoinCap(query);
            var capCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(capData, true);
            return capCryptocurrencies;
        }

        private List<CandlestickModel> FetchCandlestickModels(string id, string days = "7")
        {
            var geckoData = _coinGeckoAPi.GetCandelsById(id, days);
            var geckoCandels = _candlesDeserializer.Deserialize(geckoData);
            return geckoCandels;
        }
        
        
        #endregion

    }
}
