namespace Miotsukushi.ViewModel.CheatWindow
{
    class AboutViewModel : ViewModelBase
    {
        private string _version;
        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(() => Version);
                }
            }
        }

        public AboutViewModel()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Version = version.ToString();
        }
    }
}
