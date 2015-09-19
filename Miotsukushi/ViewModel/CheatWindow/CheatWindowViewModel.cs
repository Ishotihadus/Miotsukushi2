using System;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class CheatWindowViewModel : ViewModelBase
    {
        private const string DebuggerPassword = "86e144bfa663d91f68705291cfb5f535";

        private bool _isDebuggerActivated;
        public bool IsDebuggerActivated
        {
            get
            {
                return _isDebuggerActivated;
            }

            set
            {
                if (_isDebuggerActivated != value)
                {
                    _isDebuggerActivated = value;
                    OnPropertyChanged(() => IsDebuggerActivated);
                }
            }
        }


        public CheatWindowViewModel()
        {
            DebuggerActivatedAppend();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "DebuggerPassword")
                DebuggerActivatedAppend();
        }

        private void DebuggerActivatedAppend()
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var bs = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Properties.Settings.Default.DebuggerPassword));
            md5.Clear();
            var result = BitConverter.ToString(bs).ToLower().Replace("-", "");
            IsDebuggerActivated = result == DebuggerPassword;
        }
    }
}