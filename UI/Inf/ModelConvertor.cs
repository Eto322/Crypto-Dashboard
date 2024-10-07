using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Manager;
using BLL.Manager.Helper;
using UI.Model;
using BLL.Model;
using DAL.Credentials;

namespace UI.Inf
{
    public class ModelConvertor
    {
        public CryptoCurrencyModel BlltoUiCovnertor(Cryptocurrency cryptoCurrencyBll)
        {
            return new CryptoCurrencyModel
            {
                IdCap = cryptoCurrencyBll.IdCap,
                IdGecko = cryptoCurrencyBll.IdGecko,
                Symbol = cryptoCurrencyBll.Symbol,
                Name = cryptoCurrencyBll.Name,
                CurrentPrice = cryptoCurrencyBll.CurrentPrice,
                MarketCap = cryptoCurrencyBll.MarketCap,
                MarketCapUsd = cryptoCurrencyBll.MarketCapUsd,
                TotalVolume = cryptoCurrencyBll.TotalVolume,
                VolumeUsd24Hr = cryptoCurrencyBll.VolumeUsd24Hr,
                ChangePercent24Hr = cryptoCurrencyBll.ChangePercent24Hr,
                PriceChange24H = cryptoCurrencyBll.PriceChange24H,
                PriceChangePercentage24H = cryptoCurrencyBll.PriceChangePercentage24H,
                CirculatingSupply = cryptoCurrencyBll.CirculatingSupply,
                TotalSupply = cryptoCurrencyBll.TotalSupply,
                MaxSupply = cryptoCurrencyBll.MaxSupply,
                MarketCapRank = cryptoCurrencyBll.MarketCapRank,
                Image = cryptoCurrencyBll.Image,
                Explorer = cryptoCurrencyBll.Explorer,
                LastUpdated = cryptoCurrencyBll.LastUpdated
            };
        }

        public List<CryptoCurrencyModel> BlltoUiConvertor(List<Cryptocurrency> cryptoCurrencyBll)
        {
            var cryptoCurrencyModel = new List<CryptoCurrencyModel>();
            foreach (var item in cryptoCurrencyBll)
            {
                cryptoCurrencyModel.Add(BlltoUiCovnertor(item));
            }

            return cryptoCurrencyModel;
        }

        public DetailedInfoModel CryptoConcurrenceToDetailedInfoModel(CryptoCurrencyModel cryptocurrency)
        {
            var AdditionalInfo = GetAdditionalCoinInfo(cryptocurrency.IdGecko);
            
            return new DetailedInfoModel
            {
                Id = cryptocurrency.IdGecko, 
                Symbol = cryptocurrency.Symbol,
                Name = cryptocurrency.Name,
                ImageUrl = cryptocurrency.Image,
                CurrentPrice = cryptocurrency.CurrentPrice ?? 0, 
                MarketCap = cryptocurrency.MarketCap ?? 0,
                TotalVolume = cryptocurrency.TotalVolume ?? 0,
                PriceChange24H = cryptocurrency.PriceChange24H ?? 0,
                PriceChangePercentage24H = cryptocurrency.PriceChangePercentage24H ?? 0,
                CirculatingSupply = cryptocurrency.CirculatingSupply ?? 0,
                TotalSupply = cryptocurrency.TotalSupply ?? 0,
                MaxSupply = cryptocurrency.MaxSupply ?? 0,
                HomePageLink = AdditionalInfo.Homepage,
                RepositoryLink = AdditionalInfo.Repository
            };
        }

        #region Helpers

        private AdditionalCoinInfoModel GetAdditionalCoinInfo (string id)
        {
            var manager = new CryptoInfoManager(new CredentialManager());
            
            return manager.GetAddtionalInfo(id);
        }

        #endregion
    }
}
