using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;
using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class ShipViewModel : ViewModelBase, IDisposable
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

        public ObservableCollection<SlotViewModel> Slots { get; private set; }

        private int _AirMastery;
        public int AirMastery
        {
            get
            {
                return _AirMastery;
            }

            set
            {
                if (_AirMastery != value)
                {
                    _AirMastery = value;
                    OnPropertyChanged(() => AirMastery);
                }
            }
        }

        // 火力
        private int _Firepower;
        public int Firepower
        {
            get
            {
                return _Firepower;
            }

            set
            {
                if (_Firepower != value)
                {
                    _Firepower = value;
                    OnPropertyChanged(() => Firepower);
                }
            }
        }

        // 装甲
        private int _Armor;
        public int Armor
        {
            get
            {
                return _Armor;
            }

            set
            {
                if (_Armor != value)
                {
                    _Armor = value;
                    OnPropertyChanged(() => Armor);
                }
            }
        }

        // 雷装
        private int _Torpedo;
        public int Torpedo
        {
            get
            {
                return _Torpedo;
            }

            set
            {
                if (_Torpedo != value)
                {
                    _Torpedo = value;
                    OnPropertyChanged(() => Torpedo);
                }
            }
        }

        // 回避
        private int _Evasion;
        public int Evasion
        {
            get
            {
                return _Evasion;
            }

            set
            {
                if (_Evasion != value)
                {
                    _Evasion = value;
                    OnPropertyChanged(() => Evasion);
                }
            }
        }

        private int _AntiAir;
        public int AntiAir
        {
            get
            {
                return _AntiAir;
            }

            set
            {
                if (_AntiAir != value)
                {
                    _AntiAir = value;
                    OnPropertyChanged(() => AntiAir);
                }
            }
        }

        private int _AntiSubmarine;
        public int AntiSubmarine
        {
            get
            {
                return _AntiSubmarine;
            }

            set
            {
                if (_AntiSubmarine != value)
                {
                    _AntiSubmarine = value;
                    OnPropertyChanged(() => AntiSubmarine);
                }
            }
        }

        private int _Luck;
        public int Luck
        {
            get
            {
                return _Luck;
            }

            set
            {
                if (_Luck != value)
                {
                    _Luck = value;
                    OnPropertyChanged(() => Luck);
                }
            }
        }

        #endregion

        public ShipViewModel(int shipid)
        {
            Slots = new ObservableCollection<SlotViewModel>();
            model = Model.MainModel.Current.kancolleModel;
            this.shipid = shipid;
            shipdata = model.shipdata.FirstOrDefault(_ => _.shipid == shipid);
            if (shipdata != null)
            {
                charaid = shipdata.characterid;
                character_initialize();
                initialize();
                SlotAppend();
                shipdata.PropertyChanged += shipdata_PropertyChanged;
                shipdata.Slots.CollectionChanged += Slots_CollectionChanged;
                shipdata.OnSlotCount.CollectionChanged += OnSlotCount_CollectionChanged;
            }
        }

        // 仮実装
        void OnSlotCount_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                for (int i = 0; i < shipdata.OnSlotCount.Count; i++)
                {
                    if (i < Slots.Count)
                        Slots[i].OnSlotCount = shipdata.OnSlotCount[i];
                }
            });
        }

        // 仮実装
        void Slots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SlotAppend();
        }

        void SlotAppend()
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                if (shipdata == null)
                    return;

                for (int i = 0; i < shipdata.Slots.Count; i++)
                {
                    if (i >= Slots.Count)
                    {
                        Slots.Add(new SlotViewModel() { slotid = -1 });
                        if (shipdata.OnSlotCount.Count > i)
                            Slots[i].OnSlotCount = shipdata.OnSlotCount[i];
                    }
                    if (Slots[i].slotid != shipdata.Slots[i])
                    {
                        Slots[i].slotid = shipdata.Slots[i];
                        if (Slots[i].slotid == -1)
                        {
                            Slots[i].IsEmpty = true;
                            Slots[i].ItemName = "空き";
                        }
                        else
                        {
                            Slots[i].IsEmpty = false;
                            var slotmodel = model.slotdata.FirstOrDefault(_ => _.id == Slots[i].slotid);
                            if (slotmodel != null)
                            {
                                var slotitemdata = slotmodel.iteminfo;
                                if (slotitemdata != null)
                                {
                                    Slots[i].ItemTypeBrush = Tools.KanColleTools.GetSlotItemEquipTypeBrush(slotitemdata.type_equiptype);
                                    Slots[i].ItemName = slotitemdata.name;
                                    if (model.slotitem_equiptypemaster.ContainsKey(slotitemdata.type_equiptype))
                                        Slots[i].ItemType = model.slotitem_equiptypemaster[slotitemdata.type_equiptype].name;
                                    else
                                        Slots[i].ItemType = "不明";
                                }
                                else
                                {
                                    Slots[i].ItemTypeBrush = System.Windows.Media.Brushes.Transparent;
                                    Slots[i].ItemName = "不明";
                                    Slots[i].ItemType = "不明";
                                }
                            }
                            else
                            {
                                Slots[i].ItemTypeBrush = System.Windows.Media.Brushes.Transparent;
                                Slots[i].ItemName = "不明";
                                Slots[i].ItemType = "不明";
                            }
                        }
                    }
                }
            });
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
                case "air_mastery":
                    AirMastery = shipdata.air_mastery;
                    break;
                case "fire_power":
                    Firepower = shipdata.fire_power;
                    break;
                case "armor":
                    Armor = shipdata.armor;
                    break;
                case "torpedo":
                    Torpedo = shipdata.torpedo;
                    break;
                case "evasion":
                    Evasion = shipdata.evasion;
                    break;
                case "anti_air":
                    AntiAir = shipdata.anti_air;
                    break;
                case "anti_submarine":
                    AntiSubmarine = shipdata.anti_submarine;
                    break;
                case "luck":
                    Luck = shipdata.luck;
                    break;
            }
        }

        /// <summary>
        /// キャラクター情報で確定するものはとりあえずすべて作る
        /// </summary>
        void character_initialize()
        {
            var character = shipdata.characterinfo;
            if (character != null)
            {
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
            AirMastery = shipdata.air_mastery;
            Firepower = shipdata.fire_power;
            Armor = shipdata.armor;
            Torpedo = shipdata.torpedo;
            Evasion = shipdata.evasion;
            AntiAir = shipdata.anti_air;
            AntiSubmarine = shipdata.anti_submarine;
            Luck = shipdata.luck;
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

        #region IDisposable

        bool disposed = false;

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                if (shipdata != null)
                    shipdata.PropertyChanged -= shipdata_PropertyChanged;
                shipdata = null;
                model = null;
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        ~ShipViewModel()
        {
            Dispose(false);
        }

        #endregion
    }
}
