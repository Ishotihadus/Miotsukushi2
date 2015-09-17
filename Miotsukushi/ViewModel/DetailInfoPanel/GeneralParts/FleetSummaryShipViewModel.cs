using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.DetailInfoPanel.GeneralParts
{
    class FleetSummaryShipViewModel : ViewModelBase
    {
        // ほぼ Miotsukushi.ViewModel.DetailInfoPanel.Fleets.ShipViewModel のコピペ

        private int shipid;
        private int charaid;
        private KanColleModel model;
        private ShipData _shipdata;
        private ShipData shipdata
        {
            get
            {
                if (_shipdata == null)
                {
                    if (model.Shipdata != null)
                        _shipdata = model.Shipdata.FirstOrDefault(_ => _.Shipid == shipid);
                }
                return _shipdata;
            }
        }

        private string _ShipName;
        public string ShipName
        {
            get
            {
                return Tools.ResourceStringGetter.GetShipNameResourceString(_ShipName);
            }

            set
            {
                if (_ShipName != value)
                {
                    _ShipName = value;
                    OnPropertyChanged(() => ShipName);
                }
            }
        }

        private int _ShipLevel;
        public int ShipLevel
        {
            get
            {
                return _ShipLevel;
            }

            set
            {
                if (_ShipLevel != value)
                {
                    _ShipLevel = value;
                    OnPropertyChanged(() => ShipLevel);
                }
            }
        }

        private int _HpNow;
        public int HpNow
        {
            get
            {
                return _HpNow;
            }

            set
            {
                if (_HpNow != value)
                {
                    _HpNow = value;
                    OnPropertyChanged(() => HpNow);
                }
            }
        }

        private int _HpMax;
        public int HpMax
        {
            get
            {
                return _HpMax;
            }

            set
            {
                if (_HpMax != value)
                {
                    _HpMax = value;
                    OnPropertyChanged(() => HpMax);
                }
            }
        }

        private int _Cond;
        public int Cond
        {
            get
            {
                return _Cond;
            }

            set
            {
                if (_Cond != value)
                {
                    _Cond = value;
                    OnPropertyChanged(() => Cond);
                }
            }
        }

        private int _FuelNow;
        public int FuelNow
        {
            get
            {
                return _FuelNow;
            }

            set
            {
                if (_FuelNow != value)
                {
                    _FuelNow = value;
                    OnPropertyChanged(() => FuelNow);
                }
            }
        }

        private int _FuelMax;
        public int FuelMax
        {
            get
            {
                return _FuelMax;
            }

            set
            {
                if (_FuelMax != value)
                {
                    _FuelMax = value;
                    OnPropertyChanged(() => FuelMax);
                }
            }
        }

        private int _AmmoNow;
        public int AmmoNow
        {
            get
            {
                return _AmmoNow;
            }

            set
            {
                if (_AmmoNow != value)
                {
                    _AmmoNow = value;
                    OnPropertyChanged(() => AmmoNow);
                }
            }
        }

        private int _AmmoMax;
        public int AmmoMax
        {
            get
            {
                return _AmmoMax;
            }

            set
            {
                if (_AmmoMax != value)
                {
                    _AmmoMax = value;
                    OnPropertyChanged(() => AmmoMax);
                }
            }
        }

        private bool _HasDameCon;
        public bool HasDameCon
        {
            get
            {
                return _HasDameCon;
            }

            set
            {
                if (_HasDameCon != value)
                {
                    _HasDameCon = value;
                    OnPropertyChanged("HasDameCon");
                }
            }
        }


        public FleetSummaryShipViewModel(int shipid)
        {
            model = Model.MainModel.Current.KancolleModel;
            this.shipid = shipid;
            if (shipdata != null)
            {
                charaid = shipdata.Characterid;
                character_initialize();
                initialize();
                damecon_append();
                shipdata.PropertyChanged += shipdata_PropertyChanged;
                shipdata.Slots.CollectionChanged += Slots_CollectionChanged;
            }
        }

        private void Slots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            damecon_append();
        }


        /// <summary>
        /// キャラクター情報で確定するものはとりあえずすべて作る
        /// </summary>
        void character_initialize()
        {
            var character = shipdata.Characterinfo;
            if (character != null)
            {
                ShipName = character.Name;
            }
            else
            {
                ShipName = "不明";
            }
        }
        
        /// <summary>
        /// 艦娘固有のデータを初期化する
        /// </summary>
        void initialize()
        {
            ShipLevel = shipdata.Level;
            HpNow = shipdata.HpNow;
            HpMax = shipdata.HpMax;
            Cond = shipdata.Condition;
            fuel_append();
            ammo_append();
        }

        void fuel_append()
        {
            var fuel_max = 0;
            if (model.Charamaster.ContainsKey(charaid))
            {
                fuel_max = model.Charamaster[charaid].FuelMax;
            }

            int real_fuel_now;
            int real_fuel_max;
            Tools.KanColleTools.ShipResource(ShipLevel, shipdata.Fuel, fuel_max, out real_fuel_now, out real_fuel_max);
            FuelNow = real_fuel_now;
            FuelMax = real_fuel_max;
        }

        void ammo_append()
        {
            var ammo_max = 0;
            if (model.Charamaster.ContainsKey(charaid))
            {
                ammo_max = model.Charamaster[charaid].AmmoMax;
            }

            int real_ammo_max;
            int real_ammo_now;
            Tools.KanColleTools.ShipResource(ShipLevel, shipdata.Ammo, ammo_max, out real_ammo_now, out real_ammo_max);
            AmmoNow = real_ammo_now;
            AmmoMax = real_ammo_max;
        }


        void shipdata_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Shipid":
                    // ありえない
                    break;
                case "Characterid":
                    character_initialize();
                    fuel_append();
                    ammo_append();
                    break;
                case "Level":
                    ShipLevel = shipdata.Level;
                    break;
                case "HpNow":
                    HpNow = shipdata.HpNow;
                    break;
                case "HpMax":
                    HpMax = shipdata.HpMax;
                    break;
                case "Condition":
                    Cond = shipdata.Condition;
                    break;
                case "Fuel":
                    fuel_append();
                    break;
                case "Ammo":
                    ammo_append();
                    break;
                case "ExSlot":
                    damecon_append();
                    break;
            }
        }

        void damecon_append()
        {
            var _hasdamecon = false;

            var exitem = model.Slotdata.FirstOrDefault(_ => _.Id == shipdata.ExSlot);
            if (exitem?.Iteminfo != null && exitem.Iteminfo.TypeEquiptype == 23)
            {
                _hasdamecon = true;
            }

            if(!_hasdamecon)
                if (shipdata.Slots.Select(slot => model.Slotdata.FirstOrDefault(_ => _.Id == slot)).Any(item => item?.Iteminfo != null && item.Iteminfo.TypeEquiptype == 23))
                {
                    _hasdamecon = true;
                }
            
            HasDameCon = _hasdamecon;
        }
    }
}
