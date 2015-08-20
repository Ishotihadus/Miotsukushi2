using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class SettingsViewModel : ViewModelBase
    {
        public List<string> Themes { get; set; }
        public List<string> AccentColors { get; set; }

        private int _ThemeSelectedIndex;
        public int ThemeSelectedIndex
        {
            get
            {
                return _ThemeSelectedIndex;
            }

            set
            {
                if (_ThemeSelectedIndex != value)
                {
                    _ThemeSelectedIndex = value;
                    Model.MainModel.Current.themeModel.selected_theme = value;
                    OnPropertyChanged(() => ThemeSelectedIndex);
                }
            }
        }

        private int _AccentColorSelectedIndex;
        public int AccentColorSelectedIndex
        {
            get
            {
                return _AccentColorSelectedIndex;
            }

            set
            {
                if (_AccentColorSelectedIndex != value)
                {
                    _AccentColorSelectedIndex = value;
                    Model.MainModel.Current.themeModel.selected_accent = value;
                    OnPropertyChanged(() => AccentColorSelectedIndex);
                }
            }
        }



        public SettingsViewModel()
        {
            var model = Model.MainModel.Current;
            Themes = model.themeModel.theme_name.ToList();
            AccentColors = model.themeModel.accent_name.ToList();
            ThemeSelectedIndex = model.themeModel.selected_theme;
            AccentColorSelectedIndex = model.themeModel.selected_accent;
        }
    }
}
