using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.DetailInfoPanel.GeneralParts
{
    class BasicPanelViewModel : ViewModelBase
    {
        KanColleModel model;

        #region プロパティ定義

        private string _AdmiralName;
        public string AdmiralName
        {
            get
            {
                return _AdmiralName;
            }

            set
            {
                if (_AdmiralName != value)
                {
                    _AdmiralName = value;
                    OnPropertyChanged(() => AdmiralName);
                }
            }
        }

        private string _AdmiralComment;
        public string AdmiralComment
        {
            get
            {
                return _AdmiralComment;
            }

            set
            {
                if (_AdmiralComment != value)
                {
                    _AdmiralComment = value;
                    OnPropertyChanged(() => AdmiralComment);
                }
            }
        }


        private string _AdmiralRank;
        public string AdmiralRank
        {
            get
            {
                return _AdmiralRank;
            }

            set
            {
                if (_AdmiralRank != value)
                {
                    _AdmiralRank = value;
                    OnPropertyChanged(() => AdmiralRank);
                }
            }
        }

        private int? _AdmiralLevel;
        public int? AdmiralLevel
        {
            get
            {
                return _AdmiralLevel;
            }

            set
            {
                if (_AdmiralLevel != value)
                {
                    _AdmiralLevel = value;
                    OnPropertyChanged(() => AdmiralLevel);
                }
            }
        }

        private int? _AdmiralExp;
        public int? AdmiralExp
        {
            get
            {
                return _AdmiralExp;
            }

            set
            {
                if (_AdmiralExp != value)
                {
                    _AdmiralExp = value;
                    OnPropertyChanged(() => AdmiralExp);
                }
            }
        }

        private int? _ShipCount;
        public int? ShipCount
        {
            get
            {
                return _ShipCount;
            }

            set
            {
                if (_ShipCount != value)
                {
                    _ShipCount = value;
                    OnPropertyChanged(() => ShipCount);
                }
            }
        }

        private int? _ShipMax;
        public int? ShipMax
        {
            get
            {
                return _ShipMax;
            }

            set
            {
                if (_ShipMax != value)
                {
                    _ShipMax = value;
                    OnPropertyChanged(() => ShipMax);
                }
            }
        }

        private int? _ItemCount;
        public int? ItemCount
        {
            get
            {
                return _ItemCount;
            }

            set
            {
                if (_ItemCount != value)
                {
                    _ItemCount = value;
                    OnPropertyChanged(() => ItemCount);
                }
            }
        }

        private int? _ItemMax;
        public int? ItemMax
        {
            get
            {
                return _ItemMax;
            }

            set
            {
                if (_ItemMax != value)
                {
                    _ItemMax = value;
                    OnPropertyChanged(() => ItemMax);
                }
            }
        }

        private int? _RemainCount;
        public int? RemainCount
        {
            get
            {
                return _RemainCount;
            }

            set
            {
                if (_RemainCount != value)
                {
                    _RemainCount = value;
                    OnPropertyChanged(() => RemainCount);
                }
            }
        }

        private int? _Fuel;
        public int? Fuel
        {
            get
            {
                return _Fuel;
            }

            set
            {
                if (_Fuel != value)
                {
                    _Fuel = value;
                    OnPropertyChanged(() => Fuel);
                }
            }
        }

        private int? _Ammo;
        public int? Ammo
        {
            get
            {
                return _Ammo;
            }

            set
            {
                if (_Ammo != value)
                {
                    _Ammo = value;
                    OnPropertyChanged(() => Ammo);
                }
            }
        }

        private int? _Steel;
        public int? Steel
        {
            get
            {
                return _Steel;
            }

            set
            {
                if (_Steel != value)
                {
                    _Steel = value;
                    OnPropertyChanged(() => Steel);
                }
            }
        }

        private int? _Bauxite;
        public int? Bauxite
        {
            get
            {
                return _Bauxite;
            }

            set
            {
                if (_Bauxite != value)
                {
                    _Bauxite = value;
                    OnPropertyChanged(() => Bauxite);
                }
            }
        }

        private int? _ResourceCap;
        public int? ResourceCap
        {
            get
            {
                return _ResourceCap;
            }

            set
            {
                if (_ResourceCap != value)
                {
                    _ResourceCap = value;
                    OnPropertyChanged(() => ResourceCap);
                }
            }
        }

        private int? _Bucket;
        public int? Bucket
        {
            get
            {
                return _Bucket;
            }

            set
            {
                if (_Bucket != value)
                {
                    _Bucket = value;
                    OnPropertyChanged(() => Bucket);
                }
            }
        }

        private int? _Burner;
        public int? Burner
        {
            get
            {
                return _Burner;
            }

            set
            {
                if (_Burner != value)
                {
                    _Burner = value;
                    OnPropertyChanged(() => Burner);
                }
            }
        }

        private int? _DevKit;
        public int? DevKit
        {
            get
            {
                return _DevKit;
            }

            set
            {
                if (_DevKit != value)
                {
                    _DevKit = value;
                    OnPropertyChanged(() => DevKit);
                }
            }
        }

        private int? _RevAmp;
        public int? RevAmp
        {
            get
            {
                return _RevAmp;
            }

            set
            {
                if (_RevAmp != value)
                {
                    _RevAmp = value;
                    OnPropertyChanged(() => RevAmp);
                }
            }
        }

        private int? _FurnitureCoin;
        public int? FurnitureCoin
        {
            get
            {
                return _FurnitureCoin;
            }

            set
            {
                if (_FurnitureCoin != value)
                {
                    _FurnitureCoin = value;
                    OnPropertyChanged(() => FurnitureCoin);
                }
            }
        }

        #endregion

        public BasicPanelViewModel()
        {
            model = Model.MainModel.Current.kancolleModel;
            model.basicdata.PropertyChanged += basicdata_PropertyChanged;
        }

        void basicdata_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "admiral_name":
                    AdmiralName = model.basicdata.admiral_name;
                    break;
                case "admiral_comment":
                    AdmiralComment = model.basicdata.admiral_comment;
                    break;
                case "admiral_level":
                    AdmiralLevel = model.basicdata.admiral_level;
                    break;
                case "admiral_exp":
                    AdmiralExp = model.basicdata.admiral_exp;
                    break;
                case "admiral_rank":
                    switch (model.basicdata.admiral_rank)
                    {
                        case Model.KanColle.AdmiralRank.MarshalAdmiral:
                            AdmiralRank = "元帥";
                            break;
                        case Model.KanColle.AdmiralRank.Admiral:
                            AdmiralRank = "大将";
                            break;
                        case Model.KanColle.AdmiralRank.ViceAdmiral:
                            AdmiralRank = "中将";
                            break;
                        case Model.KanColle.AdmiralRank.RearAdmiral:
                            AdmiralRank = "少将";
                            break;
                        case Model.KanColle.AdmiralRank.Captain:
                            AdmiralRank = "大佐";
                            break;
                        case Model.KanColle.AdmiralRank.Commander:
                            AdmiralRank = "中佐";
                            break;
                        case Model.KanColle.AdmiralRank.NoviceCommander:
                            AdmiralRank = "新米中佐";
                            break;
                        case Model.KanColle.AdmiralRank.LieutenantCommander:
                            AdmiralRank = "少佐";
                            break;
                        case Model.KanColle.AdmiralRank.MiddleLieutenantCommander:
                            AdmiralRank = "中堅少佐";
                            break;
                        case Model.KanColle.AdmiralRank.NoviceLieutenantCommander:
                            AdmiralRank = "新米少佐";
                            break;
                    }
                    break;
                case "max_ship":
                    ShipMax = model.basicdata.max_ship;
                    break;
                case "max_equipment":
                    ItemMax = model.basicdata.max_equipment + 3;
                    break;
                case "now_ship_number":
                    ShipCount = model.basicdata.now_ship_number;
                    break;
                case "now_equipment_number":
                    ItemCount = model.basicdata.now_equipment_number;
                    break;
                case "remain_count":
                    RemainCount = model.basicdata.remain_count;
                    break;
                case "fuel":
                    Fuel = model.basicdata.fuel;
                    break;
                case "ammo":
                    Ammo = model.basicdata.ammo;
                    break;
                case "steel":
                    Steel = model.basicdata.steel;
                    break;
                case "bauxite":
                    Bauxite = model.basicdata.bauxite;
                    break;
                case "resource_cap":
                    ResourceCap = model.basicdata.resource_cap;
                    break;
                case "instant_repair":
                    Bucket = model.basicdata.instant_repair;
                    break;
                case "instant_construction":
                    Burner = model.basicdata.instant_construction;
                    break;
                case "development_material":
                    DevKit = model.basicdata.development_material;
                    break;
                case "furniture_coin":
                    FurnitureCoin = model.basicdata.furniture_coin;
                    break;
                case "modding_material":
                    RevAmp = model.basicdata.modding_material;
                    break;
            }
        }
    }
}
