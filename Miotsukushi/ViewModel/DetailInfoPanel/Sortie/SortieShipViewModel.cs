using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Sortie
{
    class SortieShipViewModel : ViewModelBase
    {
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

        private int _NowHP;
        public int NowHP
        {
            get
            {
                return _NowHP;
            }

            set
            {
                if (_NowHP != value)
                {
                    _NowHP = value;
                    OnPropertyChanged(() => NowHP);
                }
            }
        }

        private int _MaxHP;
        public int MaxHP
        {
            get
            {
                return _MaxHP;
            }

            set
            {
                if (_MaxHP != value)
                {
                    _MaxHP = value;
                    OnPropertyChanged(() => MaxHP);
                }
            }
        }

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

        public List<SortieShipSlotViewModel> Slot { get; set; }

        public class SortieShipSlotViewModel
        {
            public System.Windows.Media.Color ItemTypeColor { get; set; }
            public string ItemName { get; set; }
        }

        private Model.KanColle.ShipData shipmodel;

        public SortieShipViewModel(Model.KanColle.ShipData shipmodel)
        {
            this.shipmodel = shipmodel;

            if(shipmodel == null)
            {
                ShipName = "不明";
                return;
            }
            
            shipmodel.PropertyChanged += Shipmodel_PropertyChanged;
            
            ShipName = shipmodel.characterinfo != null ? shipmodel.characterinfo.name : "不明";
            ShipLevel = shipmodel.level;
            NowHP = shipmodel.hp_now;
            MaxHP = shipmodel.hp_max;
            Firepower = shipmodel.fire_power;
            Torpedo = shipmodel.torpedo;
            AntiAir = shipmodel.anti_air;
            Armor = shipmodel.armor;
            SlotAppend();
            shipmodel.Slots.CollectionChanged += Slots_CollectionChanged;
        }

        void SlotAppend()
        {
            var tmplist = new List<SortieShipSlotViewModel>();
            foreach(var slot in shipmodel.Slots)
            {
                if(slot == -1)
                {
                    tmplist.Add(new SortieShipSlotViewModel()
                    {
                        ItemTypeColor = System.Windows.Media.Colors.Transparent,
                        ItemName = "空き"
                    });
                }
                else
                {
                    var kcmodel = Model.MainModel.Current.kancolleModel;
                    var slotmodel = kcmodel.slotdata.FirstOrDefault(_ => _.id == slot);
                    if(slotmodel != null && slotmodel.iteminfo != null)
                    {
                        tmplist.Add(new SortieShipSlotViewModel()
                        {
                            ItemTypeColor = Tools.KanColleTools.GetSlotItemEquipTypeColor(slotmodel.iteminfo.type_equiptype),
                            ItemName = slotmodel.iteminfo.name
                        });
                    }
                    else
                    {
                        tmplist.Add(new SortieShipSlotViewModel()
                        {
                            ItemTypeColor = System.Windows.Media.Colors.Transparent,
                            ItemName = "不明"
                        });
                    }
                }
            }
            Slot = tmplist;
        }

        private void Slots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SlotAppend();
        }

        private void Shipmodel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "level":
                    ShipLevel = shipmodel.level;
                    break;
                case "characterid":
                    ShipName = shipmodel.characterinfo != null ? shipmodel.characterinfo.name : "不明";
                    break;
                case "hp_now":
                    NowHP = shipmodel.hp_now;
                    break;
                case "hp_max":
                    MaxHP = shipmodel.hp_max;
                    break;
                case "fire_power":
                    Firepower = shipmodel.fire_power;
                    break;
                case "torpedo":
                    Torpedo = shipmodel.torpedo;
                    break;
                case "anti_air":
                    AntiAir = shipmodel.anti_air;
                    break;
                case "armor":
                    Armor = shipmodel.armor;
                    break;
                case "Slots":
                    SlotAppend();
                    break;
            }
        }
    }
}
