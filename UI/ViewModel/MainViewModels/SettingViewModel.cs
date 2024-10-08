using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Inf;

namespace UI.ViewModel
{
    public partial class MainViewModel
    {
        #region SettingsRegion

        private string _selectedLanguage;

        public string SelectedLanguage
        {
            get => _selectedLanguage;

            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
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

        public List<int> TopCurrenciesOptions { get; } = new List<int> { 1, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };


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
    }
}
