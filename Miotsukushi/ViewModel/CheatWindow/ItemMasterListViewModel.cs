﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class ItemMasterListViewModel : ViewModelBase
    {
        public ObservableCollection<ItemMasterItem> ItemList { get; set; }

        public ItemMasterListViewModel()
        {
            ItemList = new ObservableCollection<ItemMasterItem>();
            var kcmodel = Model.MainModel.Current.kancolleModel;
            if (kcmodel.initializeCompleted)
                MakeTable();
            else
                kcmodel.InitializeComplete += (_, __) => MakeTable();
        }

        private int? ZeroToNull(int parameter) => parameter != 0 ? parameter : (int?)null;

        private string GetRangeStr(int range)
        {
            switch(range)
            {
                case 0: return null;
                case 1: return "短";
                case 2: return "中";
                case 3: return "長";
                case 4: return "超長";
                default: return "不明";
            }
        }

        private void MakeTable()
        {
            var kcmodel = Model.MainModel.Current.kancolleModel;
            foreach (var c in kcmodel.slotitemmaster)
                ItemList.Add(new ItemMasterItem()
                {
                    ID = c.Key,
                    Name = c.Value.name,
                    EquiptypeID = c.Value.type_equiptype,
                    Equiptype = kcmodel.slotitem_equiptypemaster.ContainsKey(c.Value.type_equiptype) ? kcmodel.slotitem_equiptypemaster[c.Value.type_equiptype].name : null,
                    EquiptypeColor = Tools.KanColleTools.GetSlotItemEquipTypeColor(c.Value.type_equiptype),
                    Firepower = ZeroToNull(c.Value.firepower),
                    Torpedo = ZeroToNull(c.Value.torpedo),
                    Bombing = ZeroToNull(c.Value.bombing),
                    AntiAir = ZeroToNull(c.Value.anti_air),
                    AntiSubmarines = ZeroToNull(c.Value.anti_submarines),
                    Reconnaissance = ZeroToNull(c.Value.reconnaissance),
                    HitRate = ZeroToNull(c.Value.hit_rate),
                    Evasion = ZeroToNull(c.Value.evasion),
                    Armor = ZeroToNull(c.Value.armor),
                    RangeInt = c.Value.range,
                    Range = GetRangeStr(c.Value.range)
                });
        }
    }

    class ItemMasterItem : ViewModelBase
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


        private int _EquiptypeID;
        public int EquiptypeID
        {
            get
            {
                return _EquiptypeID;
            }

            set
            {
                if (_EquiptypeID != value)
                {
                    _EquiptypeID = value;
                    OnPropertyChanged(() => EquiptypeID);
                }
            }
        }


        private string _Equiptype;
        public string Equiptype
        {
            get
            {
                return _Equiptype;
            }

            set
            {
                if (_Equiptype != value)
                {
                    _Equiptype = value;
                    OnPropertyChanged(() => Equiptype);
                }
            }
        }


        private Color _EquiptypeColor;
        public Color EquiptypeColor
        {
            get
            {
                return _EquiptypeColor;
            }

            set
            {
                if (_EquiptypeColor != value)
                {
                    _EquiptypeColor = value;
                    OnPropertyChanged(() => EquiptypeColor);
                }
            }
        }



        private int? _Firepower;
        public int? Firepower
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


        private int? _Torpedo;
        public int? Torpedo
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


        private int? _Bombing;
        public int? Bombing
        {
            get
            {
                return _Bombing;
            }

            set
            {
                if (_Bombing != value)
                {
                    _Bombing = value;
                    OnPropertyChanged(() => Bombing);
                }
            }
        }


        private int? _AntiAir;
        public int? AntiAir
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


        private int? _AntiSubmarines;
        public int? AntiSubmarines
        {
            get
            {
                return _AntiSubmarines;
            }

            set
            {
                if (_AntiSubmarines != value)
                {
                    _AntiSubmarines = value;
                    OnPropertyChanged(() => AntiSubmarines);
                }
            }
        }


        private int? _Reconnaissance;
        public int? Reconnaissance
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


        private int? _HitRate;
        public int? HitRate
        {
            get
            {
                return _HitRate;
            }

            set
            {
                if (_HitRate != value)
                {
                    _HitRate = value;
                    OnPropertyChanged(() => HitRate);
                }
            }
        }


        private int? _Evasion;
        public int? Evasion
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


        private int? _Armor;
        public int? Armor
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


        private int _RangeInt;
        public int RangeInt
        {
            get
            {
                return _RangeInt;
            }

            set
            {
                if (_RangeInt != value)
                {
                    _RangeInt = value;
                    OnPropertyChanged(() => RangeInt);
                }
            }
        }


        private string _Range;
        public string Range
        {
            get
            {
                return _Range;
            }

            set
            {
                if (_Range != value)
                {
                    _Range = value;
                    OnPropertyChanged(() => Range);
                }
            }
        }

    }
}