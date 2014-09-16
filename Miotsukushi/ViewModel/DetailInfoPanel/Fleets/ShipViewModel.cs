using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class ShipViewModel : ViewModelBase
    {
        private int shipid;
        private int charaid;
        private ShipData shipdata;
        private KanColleModel model;

        #region プロパティ定義

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

        private string _ShipName;
        public string ShipName
        {
            get
            {
                return _ShipName;
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

        private int _ExpToNextLv;
        public int ExpToNextLv
        {
            get
            {
                return _ExpToNextLv;
            }

            set
            {
                if (_ExpToNextLv != value)
                {
                    _ExpToNextLv = value;
                    OnPropertyChanged(() => ExpToNextLv);
                }
            }
        }

        private int _ExpPercentage;
        public int ExpPercentage
        {
            get
            {
                return _ExpPercentage;
            }

            set
            {
                if (_ExpPercentage != value)
                {
                    _ExpPercentage = value;
                    OnPropertyChanged(() => ExpPercentage);
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

        #endregion

        public ShipViewModel(int shipid)
        {
            model = Model.MainModel.Current.kancolleModel;
            this.shipid = shipid;
            shipdata = model.shipdata.FirstOrDefault(_ => _.shipid == shipid);
            if (shipdata != null)
            {
                charaid = shipdata.characterid;
                character_initialize();
                initialize();
                shipdata.PropertyChanged += shipdata_PropertyChanged;
            }
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
                case "exp_total":
                    // 未使用
                    break;
                case "exp_to_next_lv":
                    ExpToNextLv = shipdata.exp_to_next_lv;
                    break;
                case "exp_percent_in_this_lv":
                    ExpPercentage = shipdata.exp_percent_in_this_lv;
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
                case "ndock_time":
                    // 未使用
                    break;
            }
        }

        /// <summary>
        /// キャラクター情報で確定するものはとりあえずすべて作る
        /// </summary>
        void character_initialize()
        {
            if (model.charamaster.ContainsKey(charaid))
            {
                var character = model.charamaster[charaid];
                if (model.shiptypemaster.ContainsKey(character.shiptype))
                {
                    ShipType = model.shiptypemaster[character.shiptype].name;
                }
                else
                {
                    ShipType = "不明";
                }

                ShipName = character.name;
            }
            else
            {
                ShipName = "不明";
                ShipType = "不明";
            }
        }

        /// <summary>
        /// 艦娘固有のデータを初期化する
        /// </summary>
        void initialize()
        {
            ShipLevel = shipdata.level;
            ExpToNextLv = shipdata.exp_to_next_lv;
            ExpPercentage = shipdata.exp_percent_in_this_lv;
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
    }
}
