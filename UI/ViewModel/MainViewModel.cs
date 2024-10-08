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
    public partial class MainViewModel:NotifyPropertyChanged
    {


        public MainViewModel()
        {
            IsDarkTheme = true;
            SelectedNumberOfTopCurrencies = TopCurrenciesOptions[1];
            _cryptoInfoManager = new CryptoInfoManager(new CredentialManager());
            TopCurrencies = TopCoinsSearchByTop();
            DetailedInfoModel = ModelConvertor.CryptoConcurrenceToDetailedInfoModel(TopCurrencies[0]);
            CandlestickItemsForDraw = new ObservableCollection<CandleStickItemModel>();
            SelectedLanguage = "en";

            LoadCandlestickData(TopCurrencies[0]);
            
            Console.WriteLine();
           
        }
    }
}
