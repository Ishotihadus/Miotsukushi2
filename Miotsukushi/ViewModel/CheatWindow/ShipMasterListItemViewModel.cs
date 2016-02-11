namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipMasterListItemViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }


        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }


        private string _shipType;
        public string ShipType
        {
            get
            {
                return _shipType;
            }

            set
            {
                if (_shipType != value)
                {
                    _shipType = value;
                    OnPropertyChanged(() => ShipType);
                }
            }
        }


        private int _shipTypeId;
        public int ShipTypeId
        {
            get
            {
                return _shipTypeId;
            }

            set
            {
                if (_shipTypeId != value)
                {
                    _shipTypeId = value;
                    OnPropertyChanged(() => ShipTypeId);
                }
            }
        }


        private string _yomi;
        public string Yomi
        {
            get
            {
                return _yomi;
            }

            set
            {
                if (_yomi != value)
                {
                    _yomi = value;
                    OnPropertyChanged(() => Yomi);
                }
            }
        }


        private string _resourceId;
        public string ResourceId
        {
            get
            {
                return _resourceId;
            }

            set
            {
                if (_resourceId != value)
                {
                    _resourceId = value;
                    OnPropertyChanged(() => ResourceId);
                }
            }
        }


        private int? _afterShipLv;
        public int? AfterShipLv
        {
            get
            {
                return _afterShipLv;
            }

            set
            {
                if (_afterShipLv != value)
                {
                    _afterShipLv = value;
                    OnPropertyChanged(() => AfterShipLv);
                }
            }
        }


        private string _afterShipName;
        public string AfterShipName
        {
            get
            {
                return _afterShipName;
            }

            set
            {
                if (_afterShipName != value)
                {
                    _afterShipName = value;
                    OnPropertyChanged(() => AfterShipName);
                }
            }
        }

    }
}
