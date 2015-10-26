namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipListItemViewModel : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int ShipId
        {
            get
            {
                return _shipId;
            }

            set
            {
                if (_shipId == value) return;
                _shipId = value;
                OnPropertyChanged(() => ShipId);
            }
        }

        private int _shipId;
    }
}
