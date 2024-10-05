using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Cryptocurrency
    {
        // Basic properties of the cryptocurrency
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal? CurrentPrice { get; set; } // CoinGecko only
        public decimal? MarketCap { get; set; }
        public decimal? MarketCapUsd { get; set; } // CoinCap
        public decimal? TotalVolume { get; set; } // CoinGecko
        public decimal? VolumeUsd24Hr { get; set; } //  CoinCap
        public decimal? ChangePercent24Hr { get; set; } // CoinCap
        public decimal? PriceChange24h { get; set; } // CoinGecko
        public decimal? PriceChangePercentage24h { get; set; } // ДCoinGecko
        public decimal? CirculatingSupply { get; set; }
        public decimal? TotalSupply { get; set; }
        public decimal? MaxSupply { get; set; }
        public int? MarketCapRank { get; set; }
        public string Image { get; set; } //  CoinGecko
        public string Explorer { get; set; } //  CoinCap
        public string LastUpdated { get; set; } //  CoinGecko
    }
}
