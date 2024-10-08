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
using OxyPlot.Axes;

namespace UI.Inf
{
    public static class ModelConvertor
    {
        public static CryptoCurrencyModel BlltoUiCovnertor(CryptocurrencyModel cryptoCurrencyBll)
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

        public static List<CryptoCurrencyModel> ConvertBllToUiCryptoCurrencies(List<CryptocurrencyModel> cryptoCurrencyBll)
        {
            var cryptoCurrencyModel = new List<CryptoCurrencyModel>();
            foreach (var item in cryptoCurrencyBll)
            {
                cryptoCurrencyModel.Add(BlltoUiCovnertor(item));
            }

            return cryptoCurrencyModel;
        }

        public static UI.Model.ExchangeModel BllToUiConverter(BLL.Model.ExchangeModel exchangeBll)
        {
            return new UI.Model.ExchangeModel
            {
                Id = exchangeBll.Id,
                Name = exchangeBll.Name,
                CoinId = exchangeBll.CoinId,
                Price = exchangeBll.Price ??0,
                TradeUrl = exchangeBll.TradeUrl,
                Image = exchangeBll.Image
            };
        }

        public static List<UI.Model.ExchangeModel> ConvertBllToUiExchanges(List<BLL.Model.ExchangeModel> exchangeBllList)
        {
            var exchangeModels = new List<UI.Model.ExchangeModel>();
            foreach (var item in exchangeBllList)
            {
                exchangeModels.Add(BllToUiConverter(item));
            }

            return exchangeModels;
        }


        public static DetailedInfoModel CryptoConcurrenceToDetailedInfoModel(CryptoCurrencyModel cryptocurrency)
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

        public static List<CandleStickItemModel> ConvertToCandleStickItems(List<CandlestickModel> models)
        {
            var items = new List<CandleStickItemModel>();

            foreach (var model in models)
            {
                items.Add(new CandleStickItemModel()
                {
                    X = DateTimeAxis.ToDouble(new DateTime(1970, 1, 1).AddMilliseconds(model.Timestamp.Value)),
                    Open = (double)model.Open.Value,
                    High = (double)model.High.Value,
                    Low = (double)model.Low.Value,
                    Close = (double)model.Close.Value
                });
            }

            return items;
        }

        #region Helpers

        private static AdditionalCoinInfoModel GetAdditionalCoinInfo (string id)
        {
            var manager = new CryptoInfoManager(new CredentialManager());
            
            return manager.GetAddtionalInfo(id);
        }

        #endregion
    }
}
