using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipMasterListViewModel : ViewModelBase
    {
        public ObservableCollection<ShipMasterItem> ShipList { get; set; }

        public ShipMasterListViewModel()
        {
            ShipList = new ObservableCollection<ShipMasterItem>();
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(ShipList, new object());
            var kcmodel = Model.MainModel.Current.kancolleModel;
            if (kcmodel.initializeCompleted)
                MakeTable();
            else
                kcmodel.InitializeComplete += (_, __) => MakeTable();   
        }

        private void MakeTable()
        {
            var kcmodel = Model.MainModel.Current.kancolleModel;
            foreach (var c in kcmodel.charamaster)
                ShipList.Add(new ShipMasterItem()
                {
                    ID = c.Key,
                    Name = c.Value.name,
                    ShipType = kcmodel.shiptypemaster.ContainsKey(c.Value.shiptype) ? kcmodel.shiptypemaster[c.Value.shiptype].name : "不明",
                    ShipTypeID = c.Value.shiptype,
                    Yomi = c.Value.name_yomi,
                    ResourceID = c.Value.resource_id,
                    AfterShipLv = c.Value.aftership_lv,
                    AfterShipName = c.Value.aftership_id.HasValue ? kcmodel.charamaster[c.Value.aftership_id.Value].name : null
                });
        }
    }

    class ShipMasterItem : ViewModelBase
    {
        private int _ID;
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }


        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }


        private string _ShipType;
        public string ShipType
        {
            get
            {
                return _ShipType;
            }

            set
            {
                if (_ShipType != value)
                {
                    _ShipType = value;
                    OnPropertyChanged(() => ShipType);
                }
            }
        }


        private int _ShipTypeID;
        public int ShipTypeID
        {
            get
            {
                return _ShipTypeID;
            }

            set
            {
                if (_ShipTypeID != value)
                {
                    _ShipTypeID = value;
                    OnPropertyChanged(() => ShipTypeID);
                }
            }
        }


        private string _Yomi;
        public string Yomi
        {
            get
            {
                return _Yomi;
            }

            set
            {
                if (_Yomi != value)
                {
                    _Yomi = value;
                    OnPropertyChanged(() => Yomi);
                }
            }
        }


        private string _ResourceID;
        public string ResourceID
        {
            get
            {
                return _ResourceID;
            }

            set
            {
                if (_ResourceID != value)
                {
                    _ResourceID = value;
                    OnPropertyChanged(() => ResourceID);
                }
            }
        }


        private int? _AfterShipLv;
        public int? AfterShipLv
        {
            get
            {
                return _AfterShipLv;
            }

            set
            {
                if (_AfterShipLv != value)
                {
                    _AfterShipLv = value;
                    OnPropertyChanged(() => AfterShipLv);
                }
            }
        }


        private string _AfterShipName;
        public string AfterShipName
        {
            get
            {
                return _AfterShipName;
            }

            set
            {
                if (_AfterShipName != value)
                {
                    _AfterShipName = value;
                    OnPropertyChanged(() => AfterShipName);
                }
            }
        }

    }
}
