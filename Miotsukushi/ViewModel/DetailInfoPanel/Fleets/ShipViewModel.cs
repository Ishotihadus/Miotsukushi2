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
                    if(model.Shipdata != null)
                        _shipdata = model.Shipdata.FirstOrDefault(_ => _.Shipid == shipid);
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
            model = Model.MainModel.Current.KancolleModel;
            this.shipid = shipid;
            if (shipdata != null)
            {
                charaid = shipdata.Characterid;
                character_initialize();
                Initialize();
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
                case "ExpTotal":
                    // 未使用
                    break;
                case "ExpToNextLv":
                    ExpToNextLv = shipdata.ExpToNextLv;
                    break;
                case "ExpPercentInThisLv":
                    ExpPercentage = shipdata.ExpPercentInThisLv;
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
                case "NdockTime":
                    // 未使用
                    break;
                case "AirMastery":
                    AirMastery = shipdata.AirMastery;
                    break;
                case "FirePower":
                    Firepower = shipdata.FirePower;
                    break;
                case "Armor":
                    Armor = shipdata.Armor;
                    break;
                case "Torpedo":
                    Torpedo = shipdata.Torpedo;
                    break;
                case "Evasion":
                    Evasion = shipdata.Evasion;
                    break;
                case "AntiAir":
                    AntiAir = shipdata.AntiAir;
                    break;
                case "AntiSubmarine":
                    AntiSubmarine = shipdata.AntiSubmarine;
                    break;
                case "Reconnaissance":
                    Reconnaissance = shipdata.Reconnaissance;
                    break;
                case "Luck":
                    Luck = shipdata.Luck;
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
            var character = shipdata.Characterinfo;
            if (character != null)
            {
                if (model.Shiptypemaster.ContainsKey(character.Shiptype))
                {
                    ShipType = model.Shiptypemaster[character.Shiptype].Name;
                }
                else
                {
                    ShipType = "不明";
                }
                ShipName = character.Name;
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
        void Initialize()
        {
            ShipLevel = shipdata.Level;
            ExpToNextLv = shipdata.ExpToNextLv;
            ExpPercentage = shipdata.ExpPercentInThisLv;
            HpNow = shipdata.HpNow;
            HpMax = shipdata.HpMax;
            Cond = shipdata.Condition;
            AirMastery = shipdata.AirMastery;
            Firepower = shipdata.FirePower;
            Armor = shipdata.Armor;
            Torpedo = shipdata.Torpedo;
            Evasion = shipdata.Evasion;
            AntiAir = shipdata.AntiAir;
            AntiSubmarine = shipdata.AntiSubmarine;
            Reconnaissance = shipdata.Reconnaissance;
            Luck = shipdata.Luck;
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

        void make_exslot_viewmodel()
        {
            ExSlot = new SlotViewModel(shipdata.ExSlot);
        }
    }
}
