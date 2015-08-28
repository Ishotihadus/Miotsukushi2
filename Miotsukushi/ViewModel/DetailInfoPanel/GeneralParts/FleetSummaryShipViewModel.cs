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
                    if (model.shipdata != null)
                        _shipdata = model.shipdata.FirstOrDefault(_ => _.shipid == shipid);
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
            model = Model.MainModel.Current.kancolleModel;
            this.shipid = shipid;
            if (shipdata != null)
            {
                charaid = shipdata.characterid;
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
            var character = shipdata.characterinfo;
            if (character != null)
            {
                ShipName = character.name;
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
            ShipLevel = shipdata.level;
            HpNow = shipdata.hp_now;
            HpMax = shipdata.hp_max;
            Cond = shipdata.condition;
            fuel_append();
            ammo_append();
        }

        void fuel_append()
        {
            int fuel_max = 0;
            if (model.charamaster.ContainsKey(charaid))
            {
                fuel_max = model.charamaster[charaid].fuel_max;
            }

            int real_fuel_now;
            int real_fuel_max;
            Tools.KanColleTools.ShipResource(ShipLevel, shipdata.fuel, fuel_max, out real_fuel_now, out real_fuel_max);
            FuelNow = real_fuel_now;
            FuelMax = real_fuel_max;
        }

        void ammo_append()
        {
            int ammo_max = 0;
            if (model.charamaster.ContainsKey(charaid))
            {
                ammo_max = model.charamaster[charaid].ammo_max;
            }

            int real_ammo_max;
            int real_ammo_now;
            Tools.KanColleTools.ShipResource(ShipLevel, shipdata.ammo, ammo_max, out real_ammo_now, out real_ammo_max);
            AmmoNow = real_ammo_now;
            AmmoMax = real_ammo_max;
        }


        void shipdata_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "shipid":
                    // ありえない
                    break;
                case "characterid":
                    character_initialize();
                    fuel_append();
                    ammo_append();
                    break;
                case "level":
                    ShipLevel = shipdata.level;
                    break;
                case "hp_now":
                    HpNow = shipdata.hp_now;
                    break;
                case "hp_max":
                    HpMax = shipdata.hp_max;
                    break;
                case "condition":
                    Cond = shipdata.condition;
                    break;
                case "fuel":
                    fuel_append();
                    break;
                case "ammo":
                    ammo_append();
                    break;
                case "ExSlot":
                    damecon_append();
                    break;
            }
        }

        void damecon_append()
        {
            bool _hasdamecon = false;

            var exitem = model.slotdata.FirstOrDefault(_ => _.id == shipdata.ExSlot);
            if (exitem != null && exitem.iteminfo != null && exitem.iteminfo.type_equiptype == 23)
            {
                _hasdamecon = true;
            }

            if(!_hasdamecon)
                foreach (var slot in shipdata.Slots)
                {
                    var item = model.slotdata.FirstOrDefault(_ => _.id == slot);
                    if (item != null && item.iteminfo != null && item.iteminfo.type_equiptype == 23)
                    {
                        _hasdamecon = true;
                        break;
                    }
                }
            
            HasDameCon = _hasdamecon;
        }
    }
}
