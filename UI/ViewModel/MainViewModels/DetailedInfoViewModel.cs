using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Inf;
using UI.Model;

namespace UI.ViewModel
{
   public partial class MainViewModel
   {

        #region DetailedInfoRegion

        private DetailedInfoModel _detailedInfoModel;

        public DetailedInfoModel DetailedInfoModel
        {
            get => _detailedInfoModel;

            set
            {
                _detailedInfoModel = value;
                NotifyOfPropertyChanged();
            }
        }

        private ObservableCollection<CandleStickItemModel> _candlestickItemsForDraw;

        public ObservableCollection<CandleStickItemModel> CandlestickItemsForDraw
        {
            get => _candlestickItemsForDraw;
            set
            {
                _candlestickItemsForDraw = value;
                NotifyOfPropertyChanged();
            }
        }

        private PlotModel _candlestickPlot;
        public PlotModel CandlestickPlot
        {
            get => _candlestickPlot;
            set
            {
                _candlestickPlot = value;
                NotifyOfPropertyChanged();
            }
        }

        private ObservableCollection<ExchangeModel> _exchangeModels;

        public ObservableCollection<ExchangeModel> ExchangeModels
        {
            get => _exchangeModels;
            set
            {
                _exchangeModels = value;
                NotifyOfPropertyChanged();
            }
        }

        #region MarketsHelper

        private async Task<ObservableCollection<ExchangeModel>> LoadMarketsForCoinAsync(string id) //takes too loong so async
        {

            var exchangeModels = await Task.Run(() =>
            {

                var bllExchanges = _cryptoInfoManager.GetExchangeModels(id);
                return new ObservableCollection<ExchangeModel>(
                    ModelConvertor.ConvertBllToUiExchanges(bllExchanges));
            });

            return exchangeModels;
        }


        #endregion

        #region CandlesstickHelper

        private void LoadCandlestickData(CryptoCurrencyModel model)
        {

            var candlestickModels = _cryptoInfoManager.GetCandelsById(model.IdGecko);

            if (CandlestickItemsForDraw != null)
            {
                CandlestickItemsForDraw.Clear();
            }


            var candlestickItemModel = ModelConvertor.ConvertToCandleStickItems(candlestickModels);
            foreach (var item in candlestickItemModel)
            {
                CandlestickItemsForDraw.Add(item);
            }

            CandlestickPlot = CandlestickPlotModel.CreateCandlestickPlotModel(CandlestickItemsForDraw.ToList(), IsDarkTheme);
            Console.WriteLine();
        }


        #endregion
        #endregion

        #region DetailedInfoCommands



        private ICommand _goTotheCoinHomePageCommand;

        public ICommand GoTotheCoinHomePageCommand
        {
            get
            {
                if (_goTotheCoinHomePageCommand == null)
                {
                    _goTotheCoinHomePageCommand = new RelayCommand(param =>
                    {
                        var url = DetailedInfoModel.HomePageLink;
                        var sInfo = new System.Diagnostics.ProcessStartInfo(url)
                        {
                            UseShellExecute = true,
                        };
                        System.Diagnostics.Process.Start(sInfo);
                    });
                }
                return _goTotheCoinHomePageCommand;
            }
        }

        private ICommand _goTotheCoinRepoCommand;

        public ICommand GoTotheCoinCoinRepoCommand
        {
            get
            {
                if (_goTotheCoinRepoCommand == null)
                {
                    _goTotheCoinRepoCommand = new RelayCommand(param =>
                    {
                        var url = DetailedInfoModel.RepositoryLink;
                        var sInfo = new System.Diagnostics.ProcessStartInfo(url)
                        {
                            UseShellExecute = true,
                        };
                        System.Diagnostics.Process.Start(sInfo);
                    });
                }
                return _goTotheCoinRepoCommand;
            }
        }

        private ICommand _goToTheCoinExchangeCommand;

        public ICommand GoTotTheCoinExchangeCommand
        {
            get
            {
                if (_goToTheCoinExchangeCommand == null)
                {
                    _goToTheCoinExchangeCommand = new RelayCommand(param =>
                    {

                        var url = param.ToString();
                        var sInfo = new System.Diagnostics.ProcessStartInfo(url)
                        {
                            UseShellExecute = true,
                        };
                        System.Diagnostics.Process.Start(sInfo);
                    });
                }

                return _goToTheCoinExchangeCommand;
            }
        }

        #endregion
    }
}
