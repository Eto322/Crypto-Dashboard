using BLL.Manager;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using UI.Inf;
using UI.Model;

namespace UI.ViewModel
{
    public partial class MainViewModel
    {

        #region Helpers

        private CryptoInfoManager _cryptoInfoManager;


        #region SearchHelpers

        public DetailedInfoModel FindCurrrencyForDetailedView(string id)
        {
            var currency = TopCurrencies.FirstOrDefault(c => c.IdGecko == id);
            if (currency == null)
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
    }

}
