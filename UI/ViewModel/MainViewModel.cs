using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BLL.Manager;
using DAL.Credentials;
using UI.Inf;
using UI.Model;
using OxyPlot;

namespace UI.ViewModel
{
    public class MainViewModel:NotifyPropertyChanged
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

        #region SettingsRegion

        private string _selectedLanguage;

        public string SelectedLanguage
        {
            get => _selectedLanguage;

            set
            {
                if (_selectedLanguage!=value)
                {
                    _selectedLanguage= value;
                    ChangeLanguage(_selectedLanguage);
                    NotifyOfPropertyChanged();
                }
            }
        }
        
        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;

            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    NotifyOfPropertyChanged();
                    ChangeTheme(); 
                }
            }
        }

        private int _selectedNumberOfTopCurrencies;
        public int SelectedNumberOfTopCurrencies
        {
            get => _selectedNumberOfTopCurrencies;
            set
            {
                if (_selectedNumberOfTopCurrencies != value)
                {
                    _selectedNumberOfTopCurrencies = value;
                    NotifyOfPropertyChanged();
                }
            }
        }

        public List<int> TopCurrenciesOptions { get; } = new List<int> { 1,10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };


        #endregion

        #region SettingsCommandRegion

        private ICommand _toggleThemeCommand;
        public ICommand ToggleThemeCommand
        {
            get
            {
                if (_toggleThemeCommand == null)
                {
                    _toggleThemeCommand = new RelayCommand(param =>
                    {
                        IsDarkTheme = !IsDarkTheme; 
                    }, null);
                }

                return _toggleThemeCommand;
            }
        }

        #endregion

        #region Helpers

        private CryptoInfoManager _cryptoInfoManager;
        

        #region SearchHelpers

        public DetailedInfoModel FindCurrrencyForDetailedView (string id)
        {
            var currency = TopCurrencies.FirstOrDefault(c => c.IdGecko == id);
            if (currency==null)
            {
                MessageBox.Show("Cant find selected coin Gecko Api");
                return null;
            }

            return ModelConvertor.CryptoConcurrenceToDetailedInfoModel(currency);
        }
        
        public ObservableCollection<CryptoCurrencyModel> TopCoinsSearch()
        {
            if (string.IsNullOrEmpty(TopCoinsSearchBar))
            {

                return TopCoinsSearchByTop();
            }

            return SearchCoin();

        }

        public ObservableCollection<CryptoCurrencyModel> TopCoinsSearchByTop()
        {
            return new ObservableCollection<CryptoCurrencyModel>(
                ModelConvertor.ConvertBllToUiCryptoCurrencies(_cryptoInfoManager.GetTopNCryptos(SelectedNumberOfTopCurrencies)));
        }

        public ObservableCollection<CryptoCurrencyModel> SearchCoin()
        {
            return new ObservableCollection<CryptoCurrencyModel>(
                ModelConvertor.ConvertBllToUiCryptoCurrencies(_cryptoInfoManager.GetSearchCryptocurrencies(TopCoinsSearchBar)));
        }

        #endregion


        #region ThemeHelper

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        private void ChangeTheme()
        {


            if (IsDarkTheme)
            {
                _paletteHelper.SetTheme(MaterialDesignThemes.Wpf.Theme.Create(BaseTheme.Dark,
                    (Color)Application.Current.Resources["PrimaryColorDark"],
                    (Color)Application.Current.Resources["SecondaryColorDark"])
                );
            }
            else
            {
                _paletteHelper.SetTheme(MaterialDesignThemes.Wpf.Theme.Create(BaseTheme.Light,
                    (Color)Application.Current.Resources["PrimaryColorLight"],
                    (Color)Application.Current.Resources["SecondaryColorLight"])
                );
            }
        }

        #endregion

        #region LanguageHelper

        public void ChangeLanguage(string language)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (language)
            {
                case "uk":
                    dictionary.Source = new Uri("pack://application:,,,/UI;component/Resource/strings.uk.xaml");
                    break;
                case "en":
                    dictionary.Source = new Uri("pack://application:,,,/UI;component/Resource/strings.en.xaml");
                    break;
            }

            var existingDictionary = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("strings."));

            if (existingDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(existingDictionary);
            }

            
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }



        #endregion
        #endregion

        #region DetailedInfoRegion

        private DetailedInfoModel _detailedInfoModel;

        public DetailedInfoModel DetailedInfoModel
        {
            get => _detailedInfoModel ;

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

            if (CandlestickItemsForDraw!=null)
            {
                CandlestickItemsForDraw.Clear();
            }
           

            var candlestickItemModel = ModelConvertor.ConvertToCandleStickItems(candlestickModels);
            foreach (var item in candlestickItemModel)
            {
                CandlestickItemsForDraw.Add(item);
            }

            CandlestickPlot = CandlestickPlotModel.CreateCandlestickPlotModel(CandlestickItemsForDraw.ToList(),IsDarkTheme);
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
        public MainViewModel()
        {
            IsDarkTheme = true;
            SelectedNumberOfTopCurrencies = TopCurrenciesOptions[1];
            _cryptoInfoManager = new CryptoInfoManager(new CredentialManager());
            

            TopCurrencies = TopCoinsSearchByTop();

            DetailedInfoModel = ModelConvertor.CryptoConcurrenceToDetailedInfoModel(TopCurrencies[0]);
            CandlestickItemsForDraw = new ObservableCollection<CandleStickItemModel>();

            LoadCandlestickData(TopCurrencies[0]);
            
            Console.WriteLine();
           
        }
    }
}
