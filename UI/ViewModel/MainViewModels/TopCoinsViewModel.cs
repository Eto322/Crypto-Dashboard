using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Inf;
using UI.Model;

namespace UI.ViewModel
{
    public partial class MainViewModel : NotifyPropertyChanged
    {

        #region TopCoinsRegion

        private string _topCoinsSearchBar;

        public string TopCoinsSearchBar
        {
            get => _topCoinsSearchBar;
            set
            {
                _topCoinsSearchBar = value;
                NotifyOfPropertyChanged();
            }
        }

        private ObservableCollection<CryptoCurrencyModel> _topCurrencies;

        public ObservableCollection<CryptoCurrencyModel> TopCurrencies
        {
            get => _topCurrencies;
            set
            {
                _topCurrencies = value;
                NotifyOfPropertyChanged();
            }
        }

        #endregion

        #region TopCoinsCommandRegion

        private ICommand _getDetailedInfoCommand;

        public ICommand GetDetailedInfoCommandCommand
        {
            get
            {
                if (_getDetailedInfoCommand == null)
                {
                    _getDetailedInfoCommand = new RelayCommand(async param =>
                    {
                        SelectedIndex = 1;
                        if (param != null)
                        {
                            DetailedInfoModel = FindCurrrencyForDetailedView(param.ToString());
                            LoadCandlestickData(TopCurrencies.FirstOrDefault(c => c.IdGecko == param));
                            ExchangeModels = await LoadMarketsForCoinAsync(param.ToString());//takes too loong so async
                        }
                        else
                        {
                            MessageBox.Show("Can't find selected coin Gecko API");
                        }
                    });
                }

                return _getDetailedInfoCommand;
            }
        }

        private ICommand _searchTopCoinsCommand;

        public ICommand SearchTopCoinsCommandCommand
        {
            get
            {
                if (_searchTopCoinsCommand == null)
                {
                    _searchTopCoinsCommand = new RelayCommand(param =>
                    {
                        Task.Run(() => TopCurrencies = TopCoinsSearch());
                    });
                }

                return _searchTopCoinsCommand;
            }
        }
        #endregion

        
    }
}
