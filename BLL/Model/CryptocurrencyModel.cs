namespace BLL.Model
{
    public class CryptocurrencyModel
    {
        // Basic properties of the cryptocurrency
        public string IdCap { get; set; } //CoinCap
        public string IdGecko { get; set; }//CoinGecko
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal? CurrentPrice { get; set; } // CoinGecko 
        public decimal? MarketCap { get; set; }
        public decimal? MarketCapUsd { get; set; } // CoinCap
        public decimal? TotalVolume { get; set; } // CoinGecko
        public decimal? VolumeUsd24Hr { get; set; } //  CoinCap
        public decimal? ChangePercent24Hr { get; set; } // CoinCap
        public decimal? PriceChange24H { get; set; } // CoinGecko
        public decimal? PriceChangePercentage24H { get; set; } // CoinGecko
        public decimal? CirculatingSupply { get; set; }
        public decimal? TotalSupply { get; set; }
        public decimal? MaxSupply { get; set; }
        public decimal? MarketCapRank { get; set; }
        public string Image { get; set; } //  CoinGecko
        public string Explorer { get; set; } //  CoinCap
        public string LastUpdated { get; set; } //  CoinGecko

            }
}
