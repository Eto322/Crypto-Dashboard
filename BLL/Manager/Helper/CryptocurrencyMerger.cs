﻿using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager.Helper
{
    public static class CryptocurrencyMerger
    {
        public static List<CryptocurrencyModel> MergeCryptocurrencyData(List<CryptocurrencyModel> capData,
            List<CryptocurrencyModel> geckoData)
        {
            var mergedData = new List<CryptocurrencyModel>();

           


            if (capData != null && capData.Any())
            {
                // Iterate over CoinCap data and merge
                foreach (var capCrypto in capData)
                {
                    var mergedCrypto = new CryptocurrencyModel
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

                    // Check if corresponding CoinGecko data exists and merge
                    var geckoCrypto = geckoData.FirstOrDefault(g =>
                        g.Symbol.Equals(capCrypto.Symbol, StringComparison.OrdinalIgnoreCase));
                    if (geckoCrypto != null)
                    {
                        mergedCrypto.IdGecko = geckoCrypto.IdGecko;
                        mergedCrypto.MarketCap = geckoCrypto.MarketCap;
                        mergedCrypto.TotalVolume = geckoCrypto.TotalVolume;
                        mergedCrypto.PriceChange24H = geckoCrypto.PriceChange24H;
                        mergedCrypto.PriceChangePercentage24H = geckoCrypto.PriceChangePercentage24H;
                        mergedCrypto.TotalSupply = geckoCrypto.TotalSupply;
                        mergedCrypto.Image = geckoCrypto.Image;
                        mergedCrypto.LastUpdated = geckoCrypto.LastUpdated;
                    }

                    mergedData.Add(mergedCrypto);
                }
            }


            if (geckoData != null && geckoData.Any())
            {
                foreach (var geckoCrypto in geckoData)
                {
                    // Only  it doesn't already exist in mergedData
                    if (!mergedData.Any(m => m.Symbol.Equals(geckoCrypto.Symbol, StringComparison.OrdinalIgnoreCase)))
                    {
                        mergedData.Add(new CryptocurrencyModel
                        {
                            IdGecko = geckoCrypto.IdGecko,
                            Symbol = geckoCrypto.Symbol,
                            Name = geckoCrypto.Name,
                            CirculatingSupply = geckoCrypto.CirculatingSupply,
                            TotalSupply = geckoCrypto.TotalSupply,
                            MaxSupply = geckoCrypto.MaxSupply,
                            MarketCap = geckoCrypto.MarketCap,
                            TotalVolume = geckoCrypto.TotalVolume,
                            CurrentPrice = geckoCrypto.CurrentPrice,
                            PriceChange24H = geckoCrypto.PriceChange24H,
                            PriceChangePercentage24H = geckoCrypto.PriceChangePercentage24H,
                            MarketCapRank = geckoCrypto.MarketCapRank,
                            Image = geckoCrypto.Image,
                            LastUpdated = geckoCrypto.LastUpdated
                        });
                    }
                }


            }

            return mergedData;
        }
    }
}
