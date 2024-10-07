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

        public CryptoInfoManager(CredentialManager credentialManager)
        {
            _credentialManager = credentialManager;
            _coinGeckoAPi = new CoinGeckoApi(_credentialManager.GetGeckoApiKey());
            _coinCapAPi = new CoinCapApi(_credentialManager.GetCoinCapApiKey());
            _cryptocurrencyDeserializer = new CryptocurrencyDeserializer();
            _additionalCoinInfoDeserializer= new AdditionalCoinInfoDeserializer();
        }

        public List<Cryptocurrency> GetTopNCryptos(int n)
        {
            
            if (!_credentialManager.IsCoinCapApiExist && !_credentialManager.IsCoinGeckoApiKeyExist)
            {
                
                return new List<Cryptocurrency>(); 
            }

            List<Cryptocurrency> geckoData = FetchTopFromGecko(n);
            List<Cryptocurrency> capData = FetchTopFromCap(n);

            var topCryptos = new CryptocurrencyMerger().MergeCryptocurrencyData(capData, geckoData);


            return topCryptos;
        }

        public List<Cryptocurrency> GetSearchCryptocurrencies(string query)
        {
            if (!_credentialManager.IsCoinCapApiExist && !_credentialManager.IsCoinGeckoApiKeyExist)
            {
                Console.WriteLine("No API keys are available.");
                return new List<Cryptocurrency>();
            }

            List<Cryptocurrency> geckoData = FetchSearchFromGecko(query);
            List<Cryptocurrency> capData = FetchSearchFromCap(query);

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

        #region Helpers

        private AdditionalCoinInfoModel FetchAdditionalCoinInfo (string id)
        {
            var geckoData = _coinGeckoAPi.GetCryptoFullInfoById(id);
            var data = _additionalCoinInfoDeserializer.Deserializer(geckoData);
            return data;

        }
        private List<Cryptocurrency> FetchTopFromGecko(int n)
        {
            var geckoData = _coinGeckoAPi.GetTopCryptos(n);
            var geckoCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(geckoData, false);
            return geckoCryptocurrencies;
        }

        private List<Cryptocurrency> FetchTopFromCap(int n)
        {
            var capData = _coinCapAPi.GetTopCryptos(n);
            var capCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(capData, true);
            return capCryptocurrencies;
        }

        private List<Cryptocurrency> FetchSearchFromGecko(string query)
        {
            var geckoData = _coinGeckoAPi.SearchCoins(query);
            var geckoCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(geckoData, false);
            return geckoCryptocurrencies;
        }

        private List<Cryptocurrency> FetchSearchFromCap(string query)
        {
            var capData = _coinCapAPi.SearchCoinCap(query);
            var capCryptocurrencies = _cryptocurrencyDeserializer.Deserialize(capData, true);
            return capCryptocurrencies;
        }

        
        
        #endregion

    }
}
