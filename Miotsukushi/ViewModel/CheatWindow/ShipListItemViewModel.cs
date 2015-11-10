using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipListItemViewModel : ViewModelBase
    {
        private readonly ShipData _shipModel;
        private readonly KanColleModel _model;

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


        /// <summary>
        /// 
        /// </summary>
        public int CharacterId
        {
            get
            {
                return _characterId;
            }

            set
            {
                if (_characterId == value) return;
                _characterId = value;
                OnPropertyChanged(() => CharacterId);
            }
        }

        private int _characterId;


        /// <summary>
        /// 
        /// </summary>
        public string ShipName
        {
            get
            {
                return _shipName;
            }

            set
            {
                if (_shipName == value) return;
                _shipName = value;
                OnPropertyChanged(() => ShipName);
            }
        }

        private string _shipName;


        /// <summary>
        /// 
        /// </summary>
        public string ShipType
        {
            get
            {
                return _shipType;
            }

            set
            {
                if (_shipType == value) return;
                _shipType = value;
                OnPropertyChanged(() => ShipType);
            }
        }

        private string _shipType;


        /// <summary>
        /// 
        /// </summary>
        public int ShipTypeId
        {
            get
            {
                return _shipTypeId;
            }

            set
            {
                if (_shipTypeId == value) return;
                _shipTypeId = value;
                OnPropertyChanged(() => ShipTypeId);
            }
        }

        private int _shipTypeId;


        /// <summary>
        /// 
        /// </summary>
        public string Yomi
        {
            get
            {
                return _yomi;
            }

            set
            {
                if (_yomi == value) return;
                _yomi = value;
                OnPropertyChanged(() => Yomi);
            }
        }

        private string _yomi;


        /// <summary>
        /// 
        /// </summary>
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                if (_level == value) return;
                _level = value;
                OnPropertyChanged(() => Level);
            }
        }

        private int _level;

        /// <summary>
        /// 
        /// </summary>
        public int ExpTotal
        {
            get
            {
                return _expTotal;
            }

            set
            {
                if (_expTotal == value) return;
                _expTotal = value;
                OnPropertyChanged(() => ExpTotal);
            }
        }

        private int _expTotal;



        /// <summary>
        /// 
        /// </summary>
        public int HpMax
        {
            get
            {
                return _hpMax;
            }

            set
            {
                if (_hpMax == value) return;
                _hpMax = value;
                OnPropertyChanged(() => HpMax);
            }
        }

        private int _hpMax;


        /// <summary>
        /// 
        /// </summary>
        public int HpNow
        {
            get
            {
                return _hpNow;
            }

            set
            {
                if (_hpNow == value) return;
                _hpNow = value;
                OnPropertyChanged(() => HpNow);
            }
        }

        private int _hpNow;


        /// <summary>
        /// 
        /// </summary>
        public int Cond
        {
            get
            {
                return _cond;
            }

            set
            {
                if (_cond == value) return;
                _cond = value;
                OnPropertyChanged(() => Cond);
            }
        }

        private int _cond;



        public ShipListItemViewModel(KanColleModel model, ShipData shipModel)
        {
            _shipModel = shipModel;
            _model = model;
            _shipModel.PropertyChanged += _shipModel_PropertyChanged;
        }

        private void _shipModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Characterid":
                    CharacterId = _shipModel.Characterid;
                    ShipName = _shipModel.Characterinfo?.Name;
                    Yomi = _shipModel.Characterinfo?.NameYomi;
                    if (_shipModel.Characterinfo != null) ShipTypeId = _shipModel.Characterinfo.Shiptype;
                    if (_shipModel.Characterinfo != null && _model.Shiptypemaster.ContainsKey(_shipModel.Characterinfo.Shiptype))
                        ShipType = _model.Shiptypemaster[_shipModel.Characterinfo.Shiptype].Name;
                    break;
                case "Level":
                    Level = _shipModel.Level;
                    break;
                case "ExpTotal":
                    ExpTotal = _shipModel.ExpTotal;
                    break;
                case "HpMax":
                    HpMax = _shipModel.HpMax;
                    break;
                case "HpNow":
                    HpNow = _shipModel.HpNow;
                    break;
                case "Condition":
                    Cond = _shipModel.Condition;
                    break;
            }
        }
    }
}
