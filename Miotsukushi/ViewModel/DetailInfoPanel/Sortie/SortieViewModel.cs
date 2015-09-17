using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Sortie
{
    class SortieViewModel : ViewModelBase
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

        private string _CellInfo;
        public string CellInfo
        {
            get
            {
                return _CellInfo;
            }

            set
            {
                if (_CellInfo != value)
                {
                    _CellInfo = value;
                    OnPropertyChanged(() => CellInfo);
                }
            }
        }

        private string _OpeName;
        public string OpeName
        {
            get
            {
                return _OpeName;
            }

            set
            {
                if (_OpeName != value)
                {
                    _OpeName = value;
                    OnPropertyChanged(() => OpeName);
                }
            }
        }

        private string _OpeInfo;
        public string OpeInfo
        {
            get
            {
                return _OpeInfo;
            }

            set
            {
                if (_OpeInfo != value)
                {
                    _OpeInfo = value;
                    OnPropertyChanged(() => OpeInfo);
                }
            }
        }


        private bool _OnSortie = false;
        public bool OnSortie
        {
            get
            {
                return _OnSortie;
            }

            set
            {
                if (_OnSortie != value)
                {
                    _OnSortie = value;
                    OnPropertyChanged(() => OnSortie);
                }
            }
        }

        private bool _HasTaihaShip = false;
        public bool HasTaihaShip
        {
            get
            {
                return _HasTaihaShip;
            }

            set
            {
                if (_HasTaihaShip != value)
                {
                    _HasTaihaShip = value;
                    OnPropertyChanged(() => HasTaihaShip);
                }
            }
        }



        public ObservableCollection<SortieShipViewModel> ShipsMe { get; set; } = new ObservableCollection<SortieShipViewModel>();

        public ObservableCollection<SortieShipViewModel> ShipsCombined { get; set; } = new ObservableCollection<SortieShipViewModel>();

        public ObservableCollection<string> Comments { get; set; } = new ObservableCollection<string>();

        public SortieViewModel()
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;
            kcmodel.Sortiemodel.SortieStarted += Sortiemodel_SortieStarted;
            kcmodel.Sortiemodel.CellAdvanced += Sortiemodel_CellAdvanced;
            kcmodel.Sortiemodel.GoBackPort += Sortiemodel_GoBackPort;
            kcmodel.Sortiemodel.GobackShips.ExListChanged += Goback_ships_ExListChanged;
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(ShipsMe, new object());
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(ShipsCombined, new object());
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(Comments, new object());
        }

        private void Goback_ships_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;

            for (var i = 0; i < ShipsMe.Count; i++)
            {
                if (kcmodel.Sortiemodel.GobackShips.Contains(i))
                    ShipsMe[i].IsEscaped = true;
                else
                    ShipsMe[i].IsEscaped = false;
            }

            for(var i = 0; i < ShipsCombined.Count; i++)
            {
                if (kcmodel.Sortiemodel.GobackShips.Contains(i + 6))
                    ShipsCombined[i].IsEscaped = true;
                else
                    ShipsCombined[i].IsEscaped = false;
            }
        }

        private void Sortiemodel_GoBackPort(object sender, EventArgs e)
        {
            OnSortie = false;
            ShipsMe.Clear();
            ShipsCombined.Clear();
            if(sortiing_deck != null)
                sortiing_deck.PropertyChanged -= Sortiing_deck_PropertyChanged;
            if(sortiing_deck_combined != null)
                sortiing_deck_combined.PropertyChanged -= Sortiing_deck_combined_PropertyChanged;
            HasTaihaShip = false;
        }

        void CellAppend()
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;
            var cellinfo = kcmodel.Sortiemodel.NowCell;

            if (cellinfo != null)
            {
                CellID = cellinfo.CellNo;
                switch (cellinfo.CellType)
                {
                    case Model.KanColle.BattleModels.CellType.Start:
                        CellInfo = "始点";
                        break;
                    case Model.KanColle.BattleModels.CellType.Battle:
                        CellInfo = "戦闘";
                        break;
                    case Model.KanColle.BattleModels.CellType.NightSpBattle:
                        CellInfo = "開幕夜戦";
                        break;
                    case Model.KanColle.BattleModels.CellType.NightToDayBattle:
                        CellInfo = "夜戦→昼戦";
                        break;
                    case Model.KanColle.BattleModels.CellType.AirBattle:
                        CellInfo = "航空戦";
                        break;
                    case Model.KanColle.BattleModels.CellType.SuccessShipGuard:
                        CellInfo = "船団護衛成功";
                        break;
                    case Model.KanColle.BattleModels.CellType.AirSearch:
                        CellInfo = "航空偵察";
                        switch (cellinfo.CellEventContentId)
                        {
                            case 0: CellInfo += " 失敗"; break;
                            case 1: CellInfo += " 成功"; break;
                            case 2: CellInfo += " 大成功"; break;
                        }
                        break;
                    case Model.KanColle.BattleModels.CellType.NoBattle:
                        switch (cellinfo.CellEventContentId)
                        {
                            case 1: CellInfo = "敵影を見ず"; break;
                            default: CellInfo = "気のせいだった"; break;
                        }
                        break;
                    case Model.KanColle.BattleModels.CellType.RouteChoice:
                        CellInfo = "能動分岐";
                        if(cellinfo.CellEventContentValues != null)
                            CellInfo += "（" + string.Join(", ", cellinfo.CellEventContentValues) + "）";
                        break;
                    case Model.KanColle.BattleModels.CellType.Supply:
                        CellInfo = "補給";
                        break;
                    case Model.KanColle.BattleModels.CellType.Happening:
                        CellInfo = "うずしお";
                        break;
                    default:
                        CellInfo = "不明";
                        break;
                }

                if(cellinfo.IsBossBattle)
                {
                    CellInfo += "（ボス戦）";
                }
            }
        }

        private void Sortiemodel_CellAdvanced(object sender, EventArgs e)
        {
            CellAppend();
        }

        Model.KanColle.FleetData sortiing_deck;
        Model.KanColle.FleetData sortiing_deck_combined;

        private void Sortiemodel_SortieStarted(object sender, EventArgs e)
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;
            var mapinfo = kcmodel.Sortiemodel.NowMap;
            Comments.Clear();

            if(mapinfo != null)
            {
                AreaID = mapinfo.MapareaId;
                MapID = mapinfo.MapNo;
                MapName = mapinfo.MapName;
                OpeName = mapinfo.OpeTitle;
                OpeInfo = mapinfo.OpeInfo.Replace("<br>", "");
                if (mapinfo.SelectedRank.HasValue)
                {
                    switch (mapinfo.SelectedRank.Value)
                    {
                        case 0: OpeName += "（難易度未設定）"; break;
                        case 1: OpeName += "（丙作戦）"; break;
                        case 2: OpeName += "（乙作戦）"; break;
                        case 3: OpeName += "（甲作戦）"; break;
                    }
                }

                if(mapinfo.RequiredDefeatCount.HasValue)
                    Comments.Add(string.Format("撃破回数: {0}/{1}", mapinfo.NowDefeatCount.HasValue ? mapinfo.NowDefeatCount.Value + "" : "-", mapinfo.RequiredDefeatCount.Value));
                if (mapinfo.MaxHp.HasValue)
                    Comments.Add(string.Format("マップHP: {0}/{1}", mapinfo.NowHp.HasValue ? mapinfo.NowHp.Value + "" : "-", mapinfo.MaxHp.Value));
            }
            CellAppend();

            ShipsMe.Clear();
            sortiing_deck = kcmodel.Fleetdata[kcmodel.Sortiemodel.SortiingDeck - 1];
            sortiing_deck.PropertyChanged += Sortiing_deck_PropertyChanged;
            foreach (var ship in kcmodel.Fleetdata[kcmodel.Sortiemodel.SortiingDeck - 1].Ships)
                ShipsMe.Add(new SortieShipViewModel(kcmodel.Shipdata.FirstOrDefault(_ => _.Shipid == ship)));

            ShipsCombined.Clear();
            if (kcmodel.CombinedFlag > 0 && kcmodel.Sortiemodel.SortiingDeck == 1)
            {
                sortiing_deck_combined = kcmodel.Fleetdata[1];
                foreach (var ship in kcmodel.Fleetdata[1].Ships)
                    ShipsCombined.Add(new SortieShipViewModel(kcmodel.Shipdata.FirstOrDefault(_ => _.Shipid == ship)));
                sortiing_deck_combined.PropertyChanged += Sortiing_deck_combined_PropertyChanged;

                HasTaihaShip = sortiing_deck.HasTaihaShip || sortiing_deck_combined.HasTaihaShip;
            }
            else
            {
                sortiing_deck_combined = null;
                HasTaihaShip = sortiing_deck.HasTaihaShip;
            }

            OnSortie = true;
        }

        private void Sortiing_deck_combined_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "HasTaihaShip")
            {
                HasTaihaShip = sortiing_deck.HasTaihaShip || sortiing_deck_combined.HasTaihaShip;
            }
        }

        private void Sortiing_deck_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasTaihaShip")
            {
                if (sortiing_deck_combined != null)
                    HasTaihaShip = sortiing_deck.HasTaihaShip || sortiing_deck_combined.HasTaihaShip;
                else
                    HasTaihaShip = sortiing_deck.HasTaihaShip;
            }
        }
    }
}
