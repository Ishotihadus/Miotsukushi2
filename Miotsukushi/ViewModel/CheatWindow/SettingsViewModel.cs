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

        private string _StatisticsDBToken;
        public string StatisticsDBToken
        {
            get
            {
                return _StatisticsDBToken;
            }

            set
            {
                if (_StatisticsDBToken != value)
                {
                    _StatisticsDBToken = value;
                    OnPropertyChanged(() => StatisticsDBToken);
                    Properties.Settings.Default.StatisticsDBToken = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private bool _StatisticsSendingOn;
        public bool StatisticsSendingOn
        {
            get
            {
                return _StatisticsSendingOn;
            }

            set
            {
                if (_StatisticsSendingOn != value)
                {
                    _StatisticsSendingOn = value;
                    OnPropertyChanged(() => StatisticsSendingOn);
                    Properties.Settings.Default.StatisticsSendingOn = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private bool _SoftwareRendering = false;
        public bool SoftwareRendering
        {
            get
            {
                return _SoftwareRendering;
            }

            set
            {
                if (_SoftwareRendering != value)
                {
                    _SoftwareRendering = value;
                    OnPropertyChanged(() => SoftwareRendering);
                    Properties.Settings.Default.SoftwareRendering = value;
                    Properties.Settings.Default.Save();
                    if (!value)
                        System.Windows.Media.RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.Default;
                    else
                        System.Windows.Media.RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
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
            StatisticsDBToken = Properties.Settings.Default.StatisticsDBToken;
            StatisticsSendingOn = Properties.Settings.Default.StatisticsSendingOn;
            SoftwareRendering = Properties.Settings.Default.SoftwareRendering;
            if(SoftwareRendering)
                System.Windows.Media.RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            else
                System.Windows.Media.RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.Default;
        }
    }
}
