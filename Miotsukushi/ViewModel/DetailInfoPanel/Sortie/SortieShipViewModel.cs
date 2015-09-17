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

        private int _Speed;
        public int Speed
        {
            get
            {
                return _Speed;
            }

            set
            {
                if (_Speed != value)
                {
                    _Speed = value;
                    OnPropertyChanged(() => Speed);
                }
            }
        }

        private bool _IsFriend;
        public bool IsFriend
        {
            get
            {
                return _IsFriend;
            }

            set
            {
                if (_IsFriend != value)
                {
                    _IsFriend = value;
                    OnPropertyChanged(() => IsFriend);
                }
            }
        }

        private bool _IsEscaped;

        /// <summary>
        /// 退避済みかどうか
        /// </summary>
        public bool IsEscaped
        {
            get
            {
                return _IsEscaped;
            }

            set
            {
                if (_IsEscaped != value)
                {
                    _IsEscaped = value;
                    OnPropertyChanged(() => IsEscaped);
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
            IsFriend = true; // 敵艦隊がここに現れることはないので

            if(shipmodel == null)
            {
                ShipName = "不明";
                return;
            }
            
            shipmodel.PropertyChanged += Shipmodel_PropertyChanged;
            
            ShipName = shipmodel.Characterinfo != null ? shipmodel.Characterinfo.Name : "不明";
            ShipLevel = shipmodel.Level;
            NowHP = shipmodel.HpNow;
            MaxHP = shipmodel.HpMax;
            Firepower = shipmodel.FirePower;
            Torpedo = shipmodel.Torpedo;
            AntiAir = shipmodel.AntiAir;
            Armor = shipmodel.Armor;
            Speed = shipmodel.Characterinfo != null ? shipmodel.Characterinfo.Speed : 0;
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
                    var kcmodel = Model.MainModel.Current.KancolleModel;
                    var slotmodel = kcmodel.Slotdata.FirstOrDefault(_ => _.Id == slot);
                    if(slotmodel != null && slotmodel.Iteminfo != null)
                    {
                        tmplist.Add(new SortieShipSlotViewModel()
                        {
                            ItemTypeColor = Tools.KanColleTools.GetSlotItemEquipTypeColor(slotmodel.Iteminfo.TypeEquiptype),
                            ItemName = slotmodel.Iteminfo.Name
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
                case "Level":
                    ShipLevel = shipmodel.Level;
                    break;
                case "Characterid":
                    ShipName = shipmodel.Characterinfo != null ? shipmodel.Characterinfo.Name : "不明";
                    Speed = shipmodel.Characterinfo?.Speed ?? 0;
                    break;
                case "HpNow":
                    NowHP = shipmodel.HpNow;
                    break;
                case "HpMax":
                    MaxHP = shipmodel.HpMax;
                    break;
                case "FirePower":
                    Firepower = shipmodel.FirePower;
                    break;
                case "Torpedo":
                    Torpedo = shipmodel.Torpedo;
                    break;
                case "AntiAir":
                    AntiAir = shipmodel.AntiAir;
                    break;
                case "Armor":
                    Armor = shipmodel.Armor;
                    break;
                case "Slots":
                    SlotAppend();
                    break;
            }
        }
    }
}
