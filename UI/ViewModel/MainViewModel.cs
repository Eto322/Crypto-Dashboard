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

        private ICommand _getDetailedInfo;

        public ICommand GetDetailedInfo
        {
            get
            {
                if (_getDetailedInfo == null)
                {
                    _getDetailedInfo = new RelayCommand(param =>
                    {
                        SelectedIndex = 1;
                    });
                }

                return _getDetailedInfo;
            }
        }

        private ICommand _searchTopCoins;

        public ICommand SearchTopCoinsCommand
        {
            get
            {
                if (_searchTopCoins == null)
                {
                    _searchTopCoins = new RelayCommand(param =>
                    {
                        Task.Run(() => TopCurrencies = TopCoinsSearch());
                    });
                }

                return _searchTopCoins;
            }
        }

        #endregion

        #region SettingsRegion

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

        private CryptoInfoManager cryptoInfoManager;
        private ModelConvertor convertor;

        #region SearchHelpers

        
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
                convertor.BlltoUIConvertor(cryptoInfoManager.GetTopNCryptos(SelectedNumberOfTopCurrencies)));
        }

        public ObservableCollection<CryptoCurrencyModel> SearchCoin()
        {
            return new ObservableCollection<CryptoCurrencyModel>(
                convertor.BlltoUIConvertor(cryptoInfoManager.GetSearchCryptocurrencies(TopCoinsSearchBar)));
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


        #endregion

        public MainViewModel()
        {
            IsDarkTheme = true;
            SelectedNumberOfTopCurrencies = TopCurrenciesOptions[1];
            cryptoInfoManager = new CryptoInfoManager(new CredentialManager());
            convertor = new ModelConvertor();

            TopCurrencies = TopCoinsSearchByTop();
        }
    }
}
