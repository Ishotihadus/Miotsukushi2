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
                    Model.MainModel.Current.ThemeModel.SelectedTheme = value;
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
                    Model.MainModel.Current.ThemeModel.SelectedAccent = value;
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
                    System.Windows.Media.RenderOptions.ProcessRenderMode = value ? System.Windows.Interop.RenderMode.SoftwareOnly : System.Windows.Interop.RenderMode.Default;
                }
            }
        }

        private bool _logging = true;

        public bool Logging
        {
            get { return _logging; }
            set
            {
                if (_logging != value)
                {
                    _logging = value;
                    OnPropertyChanged(() => Logging);
                    Properties.Settings.Default.Logging = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private string _debuggerPassword;

        public string DebuggerPassword
        {
            get { return _debuggerPassword; }
            set
            {
                if (_debuggerPassword != value)
                {
                    _debuggerPassword = value;
                    OnPropertyChanged(() => DebuggerPassword);
                    Properties.Settings.Default.DebuggerPassword = value;
                    Properties.Settings.Default.Save();
                }
            }
        }

        public SettingsViewModel()
        {
            var model = Model.MainModel.Current;
            Themes = model.ThemeModel.ThemeName.ToList();
            AccentColors = model.ThemeModel.AccentName.ToList();
            ThemeSelectedIndex = model.ThemeModel.SelectedTheme;
            AccentColorSelectedIndex = model.ThemeModel.SelectedAccent;
            StatisticsDBToken = Properties.Settings.Default.StatisticsDBToken;
            StatisticsSendingOn = Properties.Settings.Default.StatisticsSendingOn;
            SoftwareRendering = Properties.Settings.Default.SoftwareRendering;
            DebuggerPassword = Properties.Settings.Default.DebuggerPassword;
            System.Windows.Media.RenderOptions.ProcessRenderMode = SoftwareRendering ? System.Windows.Interop.RenderMode.SoftwareOnly : System.Windows.Interop.RenderMode.Default;
        }
    }
}
