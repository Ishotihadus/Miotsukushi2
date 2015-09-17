using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;
using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class ShipViewModel : ViewModelBase
    {
        private int shipid;
        private int charaid;
        private ShipData _shipdata;
        private ShipData shipdata
        {
            get
            {
                if (_shipdata == null)
                {
                    if(model.shipdata != null)
                        _shipdata = model.shipdata.FirstOrDefault(_ => _.shipid == shipid);
                }
                return _shipdata;
            }
        }
        private KanColleModel model;

        #region プロパティ定義

        private string _ShipType;
        public string ShipType
        {
            get
            {
                return Tools.ResourceStringGetter.GetShipTypeNameResourceString(_ShipType);
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

        // 索敵
        private int _Reconnaissance;
        public int Reconnaissance
        {
            get
            {
                return _Reconnaissance;
            }

            set
            {
                if (_Reconnaissance != value)
                {
                    _Reconnaissance = value;
                    OnPropertyChanged(() => Reconnaissance);
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

        private bool _ExSlotOpened;
        public bool ExSlotOpened
        {
            get
            {
                return _ExSlotOpened;
            }

            set
            {
                if (_ExSlotOpened != value)
                {
                    _ExSlotOpened = value;
                    OnPropertyChanged(() => ExSlotOpened);
                }
            }
        }

        private SlotViewModel _ExSlot;
        public SlotViewModel ExSlot
        {
            get
            {
                return _ExSlot;
            }

            set
            {
                if (_ExSlot != value)
                {
                    _ExSlot = value;
                    OnPropertyChanged(() => ExSlot);
                }
            }
        }

        #endregion

        public ShipViewModel(int shipid)
        {
            Slots = new ObservableCollection<SlotViewModel>();
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(Slots, new object());
            model = Model.MainModel.Current.kancolleModel;
            this.shipid = shipid;
            if (shipdata != null)
            {
                charaid = shipdata.characterid;
                character_initialize();
                initialize();
                shipdata.PropertyChanged += shipdata_PropertyChanged;
                shipdata.Slots.CollectionChanged += Slots_CollectionChanged;
                shipdata.OnSlotCount.CollectionChanged += OnSlotCount_CollectionChanged;
            }
        }

        // 仮実装
        void OnSlotCount_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnSlotCountAppend();
        }

        // 仮実装
        void Slots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (shipdata == null)
                return;

            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        if (e.NewItems.Count > 0)
                        {
                            var slotid = shipdata.Slots[e.NewStartingIndex];
                            Slots.Insert(e.NewStartingIndex, new SlotViewModel(slotid));
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        // たぶん起きない
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        Slots.RemoveAt(e.OldStartingIndex);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        Slots[e.OldStartingIndex] = new SlotViewModel(shipdata.Slots[e.NewStartingIndex]);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        // たぶん起きない
                        break;
                }

                OnSlotCountAppend();
            }));
        }

        void OnSlotCountAppend()
        {
            if (shipdata == null)
                return;

            for (var i = 0; i < shipdata.OnSlotCount.Count; i++)
            {
                if (i < Slots.Count)
                    Slots[i].OnSlotCount = shipdata.OnSlotCount[i];
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
                case "reconnaissance":
                    Reconnaissance = shipdata.reconnaissance;
                    break;
                case "luck":
                    Luck = shipdata.luck;
                    break;
                case "ExSlotOpened":
                    ExSlotOpened = shipdata.ExSlotOpened;
                    break;
                case "ExSlot":
                    make_exslot_viewmodel();
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
            Reconnaissance = shipdata.reconnaissance;
            Luck = shipdata.luck;
            ExSlotOpened = shipdata.ExSlotOpened;
            make_exslot_viewmodel();
            fuel_append();
            ammo_append();

            foreach (var id in shipdata.Slots)
                Slots.Add(new SlotViewModel(id));
            OnSlotCountAppend();
        }

        void fuel_append()
        {
            var fuel_max = 0;
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
            var ammo_max = 0;
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

        void make_exslot_viewmodel()
        {
            ExSlot = new SlotViewModel(shipdata.ExSlot);
        }
    }
}
