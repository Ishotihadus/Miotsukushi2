using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Battle
{
    class BattleViewModel : ViewModelBase
    {
        private int _AreaID;
        public int AreaID
        {
            get
            {
                return _AreaID;
            }

            set
            {
                if (_AreaID != value)
                {
                    _AreaID = value;
                    OnPropertyChanged(() => AreaID);
                }
            }
        }

        private int _MapID;
        public int MapID
        {
            get
            {
                return _MapID;
            }

            set
            {
                if (_MapID != value)
                {
                    _MapID = value;
                    OnPropertyChanged(() => MapID);
                }
            }
        }

        private string _MapName;
        public string MapName
        {
            get
            {
                return _MapName;
            }

            set
            {
                if (_MapName != value)
                {
                    _MapName = value;
                    OnPropertyChanged(() => MapName);
                }
            }
        }

        private int _CellID;
        public int CellID
        {
            get
            {
                return _CellID;
            }

            set
            {
                if (_CellID != value)
                {
                    _CellID = value;
                    OnPropertyChanged(() => CellID);
                }
            }
        }



        private string _FormationMe;
        public string FormationMe
        {
            get
            {
                return _FormationMe;
            }

            set
            {
                if (_FormationMe != value)
                {
                    _FormationMe = value;
                    OnPropertyChanged(() => FormationMe);
                }
            }
        }

        private string _FormationEnemy;
        public string FormationEnemy
        {
            get
            {
                return _FormationEnemy;
            }

            set
            {
                if (_FormationEnemy != value)
                {
                    _FormationEnemy = value;
                    OnPropertyChanged(() => FormationEnemy);
                }
            }
        }

        private string _SearchResultMe;
        public string SearchResultMe
        {
            get
            {
                return _SearchResultMe;
            }

            set
            {
                if (_SearchResultMe != value)
                {
                    _SearchResultMe = value;
                    OnPropertyChanged(() => SearchResultMe);
                }
            }
        }

        private string _SearchResultEnemy;
        public string SearchResultEnemy
        {
            get
            {
                return _SearchResultEnemy;
            }

            set
            {
                if (_SearchResultEnemy != value)
                {
                    _SearchResultEnemy = value;
                    OnPropertyChanged(() => SearchResultEnemy);
                }
            }
        }

        private string _AirMasteryStatus;
        public string AirMasteryStatus
        {
            get
            {
                return _AirMasteryStatus;
            }

            set
            {
                if (_AirMasteryStatus != value)
                {
                    _AirMasteryStatus = value;
                    OnPropertyChanged(() => AirMasteryStatus);
                }
            }
        }
        
        private string _CrossingStatus;
        public string CrossingStatus
        {
            get
            {
                return _CrossingStatus;
            }

            set
            {
                if (_CrossingStatus != value)
                {
                    _CrossingStatus = value;
                    OnPropertyChanged(() => CrossingStatus);
                }
            }
        }

        private double _GaugeMe;
        public double GaugeMe
        {
            get
            {
                return _GaugeMe;
            }

            set
            {
                if (_GaugeMe != value)
                {
                    _GaugeMe = value;
                    OnPropertyChanged(() => GaugeMe);
                }
            }
        }

        private double _GaugeEnemy;
        public double GaugeEnemy
        {
            get
            {
                return _GaugeEnemy;
            }

            set
            {
                if (_GaugeEnemy != value)
                {
                    _GaugeEnemy = value;
                    OnPropertyChanged(() => GaugeEnemy);
                }
            }
        }

        private List<BattleShipViewModel> _ShipsMe;
        public List<BattleShipViewModel> ShipsMe
        {
            get
            {
                return _ShipsMe;
            }

            set
            {
                if (_ShipsMe != value)
                {
                    _ShipsMe = value;
                    OnPropertyChanged(() => ShipsMe);
                }
            }
        }

        private List<BattleShipViewModel> _ShipsCombined;
        public List<BattleShipViewModel> ShipsCombined
        {
            get
            {
                return _ShipsCombined;
            }

            set
            {
                if (_ShipsCombined != value)
                {
                    _ShipsCombined = value;
                    OnPropertyChanged(() => ShipsCombined);
                }
            }
        }

        private List<BattleShipViewModel> _ShipsEnemy;
        public List<BattleShipViewModel> ShipsEnemy
        {
            get
            {
                return _ShipsEnemy;
            }

            set
            {
                if (_ShipsEnemy != value)
                {
                    _ShipsEnemy = value;
                    OnPropertyChanged(() => ShipsEnemy);
                }
            }
        }

        public BattleViewModel()
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;
            kcmodel.Battlemodel.BattleAnalyzed += Battlemodel_BattleAnalyzed;
        }

        private void Battlemodel_BattleAnalyzed(object sender, Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs e)
        {
            var battlemodel = Model.MainModel.Current.KancolleModel.Battlemodel;
            AreaID = battlemodel.AreaId;
            MapID = battlemodel.MapId;
            MapName = battlemodel.MapName;
            CellID = battlemodel.CellId;

            FormationMe = FormationString(e.FriendFormation);
            FormationEnemy = FormationString(e.EnemyFormation);
            AirMasteryStatus = AirMasteryString(e.AirMastery);
            CrossingStatus = CrossingString(e.crossing_type);
            GaugeMe = e.FriendGauge * 100;
            GaugeEnemy = e.EnemyGauge * 100;

            var me_list = new List<BattleShipViewModel>();
            foreach(var ship in e.Friend)
            {
                me_list.Add(new BattleShipViewModel()
                {
                    ShipName = ship.Name,
                    ShipLevel = ship.Level,
                    MaxHP = ship.MaxHp,
                    AfterHP = ship.AfterHp,
                    SumAttack = ship.SumAttack, 
                    SumDamage = ship.BeforeHp - ship.AfterHp,
                    Firepower = ship.FirePower,
                    Torpedo = ship.Torpedo,
                    AntiAir = ship.AntiAir,
                    Armor = ship.Armor,
                    IsFriend = true,
                    Speed = ship.Speed,
                    IsEscaped = ship.Escaped,
                    Slot = (ship.Slot.Select(_ => new BattleShipViewModel.BattleShipSlotViewModel()
                    {
                        ItemName = Model.MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(_) ? Model.MainModel.Current.KancolleModel.Slotitemmaster[_].Name : _ == -1 ? "空き" : "不明",
                        ItemTypeColor = Model.MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(_) ? Tools.KanColleTools.GetSlotItemEquipTypeColor(Model.MainModel.Current.KancolleModel.Slotitemmaster[_].TypeEquiptype) : System.Windows.Media.Colors.Transparent
                    }).ToList())
                });
            }
            ShipsMe = me_list;

            var enemy_list = new List<BattleShipViewModel>();
            foreach (var ship in e.Enemy)
            {
                enemy_list.Add(new BattleShipViewModel()
                {
                    ShipName = ship.Name,
                    ShipLevel = ship.Level,
                    MaxHP = ship.MaxHp,
                    AfterHP = ship.AfterHp,
                    SumAttack = ship.SumAttack,
                    SumDamage = ship.BeforeHp - ship.AfterHp,
                    Firepower = ship.FirePower,
                    Torpedo = ship.Torpedo,
                    AntiAir = ship.AntiAir,
                    Armor = ship.Armor,
                    IsFriend = false,
                    Speed = ship.Speed,
                    Slot = (ship.Slot.Select(_ => new BattleShipViewModel.BattleShipSlotViewModel()
                    {
                        ItemName = Model.MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(_) ? Model.MainModel.Current.KancolleModel.Slotitemmaster[_].Name : _ == -1 ? "空き" : "不明",
                        ItemTypeColor = Model.MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(_) ? Tools.KanColleTools.GetSlotItemEquipTypeColor(Model.MainModel.Current.KancolleModel.Slotitemmaster[_].TypeEquiptype) : System.Windows.Media.Colors.Transparent
                    }).ToList())
                });
            }
            ShipsEnemy = enemy_list;

            if (e.FriendCombined != null)
            {
                var combinedList = new List<BattleShipViewModel>();
                foreach (var ship in e.FriendCombined)
                {
                    combinedList.Add(new BattleShipViewModel()
                    {
                        ShipName = ship.Name,
                        ShipLevel = ship.Level,
                        MaxHP = ship.MaxHp,
                        AfterHP = ship.AfterHp,
                        SumAttack = ship.SumAttack,
                        SumDamage = ship.BeforeHp - ship.AfterHp,
                        Firepower = ship.FirePower,
                        Torpedo = ship.Torpedo,
                        AntiAir = ship.AntiAir,
                        Armor = ship.Armor,
                        IsFriend = true,
                        Speed = ship.Speed,
                        IsEscaped = ship.Escaped,
                        Slot = (ship.Slot.Select(_ => new BattleShipViewModel.BattleShipSlotViewModel()
                        {
                            ItemName = Model.MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(_) ? Model.MainModel.Current.KancolleModel.Slotitemmaster[_].Name : _ == -1 ? "空き" : "不明",
                            ItemTypeColor = Model.MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(_) ? Tools.KanColleTools.GetSlotItemEquipTypeColor(Model.MainModel.Current.KancolleModel.Slotitemmaster[_].TypeEquiptype) : System.Windows.Media.Colors.Transparent
                        }).ToList())
                    });
                }
                ShipsCombined = combinedList;
            }
            else
                ShipsCombined = new List<BattleShipViewModel>();
        }

        private string FormationString(Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation formation)
        {
            switch(formation)
            {
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Tanju: return "単縦陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Fukuju: return "複従陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Rinkei: return "輪形陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Teikei: return "梯形陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Tanou: return "単横陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Daiichikeikai: return "第一警戒航行序列";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Dainikeikai: return "第二警戒航行序列";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Daisankeikai: return "第三警戒航行序列";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.Daiyonkeikai: return "第四警戒航行序列";
                default: return "不明";
            }
        }

        private string CrossingString(Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType crossingType)
        {
            switch(crossingType)
            {
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.Parallel: return "同航戦";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.AntiParallel: return "反航戦";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.CrossAdv: return "T字有利";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.CrossDisadv: return "T字不利";
                default: return "不明";
            }
        }

        private string AirMasteryString(Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus airMastery)
        {
            switch(airMastery)
            {
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.Secure: return "制空権確保";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.Superior: return "制空優勢";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.Tie: return "制空均衡";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.Inferior: return "制空劣勢";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.Lost: return "制空権喪失";
                default: return "不明";
            }
        }
    }
}
