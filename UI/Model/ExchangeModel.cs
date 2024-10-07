using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Inf;

namespace UI.Model
{
    public class ExchangeModel : NotifyPropertyChanged
    {

        private string _id;
        private string _name;
        private string _coinId;
        private decimal _price;
        private string _tradeUrl;
        private string _image;

        public string Id
        {
            get => _id;
            set { _id = value; NotifyOfPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; NotifyOfPropertyChanged(); }
        }

        public string CoinId
        {
            get => _coinId;
            set { _coinId = value; NotifyOfPropertyChanged(); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; NotifyOfPropertyChanged(); }
        }

        public string TradeUrl
        {
            get => _tradeUrl;
            set { _tradeUrl = value; NotifyOfPropertyChanged(); }
        }

        public string Image
        {
            get => _image;
            set { _image = value; NotifyOfPropertyChanged(); }
        }


    }
}
