using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UI.Inf;

namespace UI.ViewModel
{
    public class MainViewModel:NotifyPropertyChanged
    {
        #region SettingsRegion

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

        public MainViewModel()
        {
            IsDarkTheme= true;
        }
    }
}
