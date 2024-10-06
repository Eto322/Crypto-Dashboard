using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Helper
{
    public class CryptocurrencyMerger
    {
        public List<Cryptocurrency> MergeCryptocurrencyData(List<Cryptocurrency> capData, List<Cryptocurrency> geckoData)
        {
            var mergedData = new List<Cryptocurrency>();

            //  Dictionary for quick lookup of CoinGecko data by IdGecko
            var geckoDictionary = geckoData.ToDictionary(c => c.Symbol.ToUpper());

            // Iterate over CoinCap data and merge
            foreach (var capCrypto in capData)
            {
                // Cryptocurrency object to store merged data
                var mergedCrypto = new Cryptocurrency
                {
                    IdCap = capCrypto.IdCap,
                    Symbol = capCrypto.Symbol,
                    Name = capCrypto.Name,
                    CirculatingSupply = capCrypto.CirculatingSupply,
                    MaxSupply = capCrypto.MaxSupply,
                    MarketCapUsd = capCrypto.MarketCapUsd,
                    VolumeUsd24Hr = capCrypto.VolumeUsd24Hr,
                    CurrentPrice = capCrypto.CurrentPrice,
                    ChangePercent24Hr = capCrypto.ChangePercent24Hr,
                    Explorer = capCrypto.Explorer,
                    MarketCapRank = capCrypto.MarketCapRank,
                };

                // Check if the corresponding CoinGecko data exists
                if (geckoDictionary.TryGetValue(capCrypto.Symbol.ToUpper(), out var geckoCrypto))
                {
                    
                    mergedCrypto.IdGecko = geckoCrypto.IdGecko;
                    mergedCrypto.MarketCap = geckoCrypto.MarketCap;
                    mergedCrypto.TotalVolume = geckoCrypto.TotalVolume;
                    mergedCrypto.PriceChange24h = geckoCrypto.PriceChange24h;
                    mergedCrypto.PriceChangePercentage24h = geckoCrypto.PriceChangePercentage24h;
                    mergedCrypto.TotalSupply = geckoCrypto.TotalSupply;
                    mergedCrypto.Image = geckoCrypto.Image;
                    mergedCrypto.LastUpdated = geckoCrypto.LastUpdated;
                }

                mergedData.Add(mergedCrypto);
            }

            // Additional data Check which don't correspond .
            /*foreach (var geckoCrypto in geckoData)
            {
                if (!capData.Any(c => c.IdGecko == geckoCrypto.IdGecko))
                {
                    mergedData.Add(geckoCrypto); 
                }
            }*/

            return mergedData;
        }
    }
}
