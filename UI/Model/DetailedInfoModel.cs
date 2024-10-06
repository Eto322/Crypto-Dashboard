using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Inf;

namespace UI.Model
{
    public class DetailedInfoModel:NotifyPropertyChanged
    {
        private string _id;
        private string _symbol;
        private string _name;
        private string _imageUrl;
        private decimal _currentPrice;
        private decimal _marketCap;
        private decimal _totalVolume;
        private decimal _priceChange24H;
        private decimal _priceChangePercentage24H;
        private decimal _circulatingSupply;
        private decimal _totalSupply;
        private decimal _maxSupply;
        private string _homepageLink;
        private string _repositoryLink;
        

        public string Id
        {
            get => _id;
            set { _id = value; NotifyOfPropertyChanged(); }
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

        public string ImageUrl
        {
            get => _imageUrl;
            set { _imageUrl = value; NotifyOfPropertyChanged(); }
        }

        public decimal CurrentPrice
        {
            get => _currentPrice;
            set { _currentPrice = value; NotifyOfPropertyChanged(); }
        }

        public decimal MarketCap
        {
            get => _marketCap;
            set { _marketCap = value; NotifyOfPropertyChanged(); }
        }

        public decimal TotalVolume
        {
            get => _totalVolume;
            set { _totalVolume = value; NotifyOfPropertyChanged(); }
        }

        public decimal PriceChange24H
        {
            get => _priceChange24H;
            set { _priceChange24H = value; NotifyOfPropertyChanged(); }
        }

        public decimal PriceChangePercentage24H
        {
            get => _priceChangePercentage24H;
            set { _priceChangePercentage24H = value; NotifyOfPropertyChanged(); }
        }

        public decimal CirculatingSupply
        {
            get => _circulatingSupply;
            set { _circulatingSupply = value; NotifyOfPropertyChanged(); }
        }

        public decimal TotalSupply
        {
            get => _totalSupply;
            set { _totalSupply = value; NotifyOfPropertyChanged(); }
        }

        public decimal MaxSupply
        {
            get => _maxSupply;
            set { _maxSupply = value; NotifyOfPropertyChanged(); }
        }


        public string HomePageLink
        {
            get => _homepageLink;
            set { _homepageLink = value; NotifyOfPropertyChanged(); }
        }

        public string RepositoryLink
        {
            get => _repositoryLink;
            set { _repositoryLink = value; NotifyOfPropertyChanged(); }
        }

      
    }
}
