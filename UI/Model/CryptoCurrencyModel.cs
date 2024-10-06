using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Inf;

namespace UI.Model
{
    public class CryptoCurrencyModel:NotifyPropertyChanged
    {
        private string _idCap;
        private string _idGecko;
        private string _symbol;
        private string _name;
        private decimal? _currentPrice;
        private decimal? _marketCap;
        private decimal? _marketCapUsd;
        private decimal? _totalVolume;
        private decimal? _volumeUsd24Hr;
        private decimal? _changePercent24Hr;
        private decimal? _priceChange24H;
        private decimal? _priceChangePercentage24H;
        private decimal? _circulatingSupply;
        private decimal? _totalSupply;
        private decimal? _maxSupply;
        private decimal? _marketCapRank;
        private string _image;
        private string _explorer;
        private string _lastUpdated;

        public string IdCap
        {
            get => _idCap;
            set { _idCap = value; NotifyOfPropertyChanged(); }
        }

        public string IdGecko
        {
            get => _idGecko;
            set { _idGecko = value; NotifyOfPropertyChanged(); }
        }

        public string Symbol
        {
            get => _symbol;
            set { _symbol = value; NotifyOfPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; NotifyOfPropertyChanged(); }
        }

        public decimal? CurrentPrice
        {
            get => _currentPrice;
            set { _currentPrice = value; NotifyOfPropertyChanged(); }
        }

        public decimal? MarketCap
        {
            get => _marketCap;
            set { _marketCap = value; NotifyOfPropertyChanged(); }
        }

        public decimal? MarketCapUsd
        {
            get => _marketCapUsd;
            set { _marketCapUsd = value; NotifyOfPropertyChanged(); }
        }

        public decimal? TotalVolume
        {
            get => _totalVolume;
            set { _totalVolume = value; NotifyOfPropertyChanged(); }
        }

        public decimal? VolumeUsd24Hr
        {
            get => _volumeUsd24Hr;
            set { _volumeUsd24Hr = value; NotifyOfPropertyChanged(); }
        }

        public decimal? ChangePercent24Hr
        {
            get => _changePercent24Hr;
            set { _changePercent24Hr = value; NotifyOfPropertyChanged(); }
        }

        public decimal? PriceChange24H
        {
            get => _priceChange24H;
            set { _priceChange24H = value; NotifyOfPropertyChanged(); }
        }

        public decimal? PriceChangePercentage24H
        {
            get => _priceChangePercentage24H;
            set { _priceChangePercentage24H = value; NotifyOfPropertyChanged(); }
        }

        public decimal? CirculatingSupply
        {
            get => _circulatingSupply;
            set { _circulatingSupply = value; NotifyOfPropertyChanged(); }
        }

        public decimal? TotalSupply
        {
            get => _totalSupply;
            set { _totalSupply = value; NotifyOfPropertyChanged(); }
        }

        public decimal? MaxSupply
        {
            get => _maxSupply;
            set { _maxSupply = value; NotifyOfPropertyChanged(); }
        }

        public decimal? MarketCapRank
        {
            get => _marketCapRank;
            set { _marketCapRank = value; NotifyOfPropertyChanged(); }
        }

        public string Image
        {
            get => _image;
            set { _image = value; NotifyOfPropertyChanged(); }
        }

        public string Explorer
        {
            get => _explorer;
            set { _explorer = value; NotifyOfPropertyChanged(); }
        }

        public string LastUpdated
        {
            get => _lastUpdated;
            set { _lastUpdated = value; NotifyOfPropertyChanged(); }
        }
    }
}

