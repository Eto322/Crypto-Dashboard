using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Model;
using BLL.Model;

namespace UI.Inf
{
    public class ModelConvertor
    {
        public CryptoCurrencyModel BlltoUICovnertor(Cryptocurrency _cryptoCurrencyBll)
        {
            return new CryptoCurrencyModel
            {
                IdCap = _cryptoCurrencyBll.IdCap,
                IdGecko = _cryptoCurrencyBll.IdGecko,
                Symbol = _cryptoCurrencyBll.Symbol,
                Name = _cryptoCurrencyBll.Name,
                CurrentPrice = _cryptoCurrencyBll.CurrentPrice,
                MarketCap = _cryptoCurrencyBll.MarketCap,
                MarketCapUsd = _cryptoCurrencyBll.MarketCapUsd,
                TotalVolume = _cryptoCurrencyBll.TotalVolume,
                VolumeUsd24Hr = _cryptoCurrencyBll.VolumeUsd24Hr,
                ChangePercent24Hr = _cryptoCurrencyBll.ChangePercent24Hr,
                PriceChange24h = _cryptoCurrencyBll.PriceChange24h,
                PriceChangePercentage24h = _cryptoCurrencyBll.PriceChangePercentage24h,
                CirculatingSupply = _cryptoCurrencyBll.CirculatingSupply,
                TotalSupply = _cryptoCurrencyBll.TotalSupply,
                MaxSupply = _cryptoCurrencyBll.MaxSupply,
                MarketCapRank = _cryptoCurrencyBll.MarketCapRank,
                Image = _cryptoCurrencyBll.Image,
                Explorer = _cryptoCurrencyBll.Explorer,
                LastUpdated = _cryptoCurrencyBll.LastUpdated
            };
        }

        public List<CryptoCurrencyModel> BlltoUIConvertor(List<Cryptocurrency> _cryptoCurrencyBll)
        {
            var cryptoCurrencyModel = new List<CryptoCurrencyModel>();
            foreach (var item in _cryptoCurrencyBll)
            {
                cryptoCurrencyModel.Add(BlltoUICovnertor(item));
            }

            return cryptoCurrencyModel;
        }
    }
}
