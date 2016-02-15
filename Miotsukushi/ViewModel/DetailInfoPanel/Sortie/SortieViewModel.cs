using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Sortie
{
    class SortieViewModel : ViewModelBase
    {
        private int _areaId;
        public int AreaId
        {
            get
            {
                return _areaId;
            }

            set
            {
                if (_areaId != value)
                {
                    _areaId = value;
                    OnPropertyChanged(() => AreaId);
                }
            }
        }

        private int _mapId;
        public int MapId
        {
            get
            {
                return _mapId;
            }

            set
            {
                if (_mapId != value)
                {
                    _mapId = value;
                    OnPropertyChanged(() => MapId);
                }
            }
        }

        private string _mapName;
        public string MapName
        {
            get
            {
                return _mapName;
            }

            set
            {
                if (_mapName != value)
                {
                    _mapName = value;
                    OnPropertyChanged(() => MapName);
                }
            }
        }

        private int _cellId;
        public int CellId
        {
            get
            {
                return _cellId;
            }

            set
            {
                if (_cellId != value)
                {
                    _cellId = value;
                    OnPropertyChanged(() => CellId);
                }
            }
        }

        private string _cellInfo;
        public string CellInfo
        {
            get
            {
                return _cellInfo;
            }

            set
            {
                if (_cellInfo != value)
                {
                    _cellInfo = value;
                    OnPropertyChanged(() => CellInfo);
                }
            }
        }

        private string _opeName;
        public string OpeName
        {
            get
            {
                return _opeName;
            }

            set
            {
                if (_opeName != value)
                {
                    _opeName = value;
                    OnPropertyChanged(() => OpeName);
                }
            }
        }

        private string _opeInfo;
        public string OpeInfo
        {
            get
            {
                return _opeInfo;
            }

            set
            {
                if (_opeInfo != value)
                {
                    _opeInfo = value;
                    OnPropertyChanged(() => OpeInfo);
                }
            }
        }


        private bool _onSortie;
        public bool OnSortie
        {
            get
            {
                return _onSortie;
            }

            set
            {
                if (_onSortie != value)
                {
                    _onSortie = value;
                    OnPropertyChanged(() => OnSortie);
                }
            }
        }

        private bool _hasTaihaShip;
        public bool HasTaihaShip
        {
            get
            {
                return _hasTaihaShip;
            }

            set
            {
                if (_hasTaihaShip != value)
                {
                    _hasTaihaShip = value;
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
            if (_sortiingDeck != null)
                _sortiingDeck.PropertyChanged -= Sortiing_deck_PropertyChanged;
            if (_sortiingDeckCombined != null)
                _sortiingDeckCombined.PropertyChanged -= Sortiing_deck_combined_PropertyChanged;
            HasTaihaShip = false;

            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                ShipsMe.Clear();
                ShipsCombined.Clear();
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    ShipsMe.Clear();
                    ShipsCombined.Clear();
                });
            }
        }

        void CellAppend()
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;
            var cellinfo = kcmodel.Sortiemodel.NowCell;

            if (cellinfo != null)
            {
                CellId = cellinfo.CellNo;
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
                    case Model.KanColle.BattleModels.CellType.AirRaid:
                        CellInfo = "空襲戦";
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

        Model.KanColle.FleetData _sortiingDeck;
        Model.KanColle.FleetData _sortiingDeckCombined;

        private void Sortiemodel_SortieStarted(object sender, EventArgs e)
        {
            if (System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                Comments.Clear();
                ShipsMe.Clear();
                ShipsCombined.Clear();

                var kcmodel = Model.MainModel.Current.KancolleModel;
                var mapinfo = kcmodel.Sortiemodel.NowMap;

                if (mapinfo != null)
                {
                    AreaId = mapinfo.MapareaId;
                    MapId = mapinfo.MapNo;
                    MapName = mapinfo.MapName;
                    OpeName = mapinfo.OpeTitle;
                    OpeInfo = mapinfo.OpeInfo.Replace("<br>", "");
                    if (mapinfo.SelectedRank.HasValue)
                    {
                        switch (mapinfo.SelectedRank.Value)
                        {
                            case 0:
                                OpeName += "（難易度未設定）";
                                break;
                            case 1:
                                OpeName += "（丙作戦）";
                                break;
                            case 2:
                                OpeName += "（乙作戦）";
                                break;
                            case 3:
                                OpeName += "（甲作戦）";
                                break;
                        }
                    }

                    if (mapinfo.RequiredDefeatCount.HasValue)
                        Comments.Add(
                            $"撃破回数: {(mapinfo.NowDefeatCount.HasValue ? mapinfo.NowDefeatCount.Value + "" : "-")}/{mapinfo.RequiredDefeatCount.Value}");
                    if (mapinfo.MaxHp.HasValue)
                        Comments.Add(
                            $"マップHP: {(mapinfo.NowHp.HasValue ? mapinfo.NowHp.Value + "" : "-")}/{mapinfo.MaxHp.Value}");
                }
                CellAppend();

                _sortiingDeck = kcmodel.Fleetdata[kcmodel.Sortiemodel.SortiingDeck - 1];
                _sortiingDeck.PropertyChanged += Sortiing_deck_PropertyChanged;
                foreach (var ship in kcmodel.Fleetdata[kcmodel.Sortiemodel.SortiingDeck - 1].Ships)
                    ShipsMe.Add(new SortieShipViewModel(kcmodel.Shipdata.FirstOrDefault(_ => _.Shipid == ship)));

                if (kcmodel.CombinedFlag > 0 && kcmodel.Sortiemodel.SortiingDeck == 1)
                {
                    _sortiingDeckCombined = kcmodel.Fleetdata[1];
                    foreach (var ship in kcmodel.Fleetdata[1].Ships)
                        ShipsCombined.Add(new SortieShipViewModel(kcmodel.Shipdata.FirstOrDefault(_ => _.Shipid == ship)));
                    _sortiingDeckCombined.PropertyChanged += Sortiing_deck_combined_PropertyChanged;

                    HasTaihaShip = _sortiingDeck.HasTaihaShip || _sortiingDeckCombined.HasTaihaShip;
                }
                else
                {
                    _sortiingDeckCombined = null;
                    HasTaihaShip = _sortiingDeck.HasTaihaShip;
                }

                OnSortie = true;
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() => Sortiemodel_SortieStarted(sender, e));
            }
        }

        private void Sortiing_deck_combined_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "HasTaihaShip")
            {
                HasTaihaShip = _sortiingDeck.HasTaihaShip || _sortiingDeckCombined.HasTaihaShip;
            }
        }

        private void Sortiing_deck_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasTaihaShip")
            {
                if (_sortiingDeckCombined != null)
                    HasTaihaShip = _sortiingDeck.HasTaihaShip || _sortiingDeckCombined.HasTaihaShip;
                else
                    HasTaihaShip = _sortiingDeck.HasTaihaShip;
            }
        }
    }
}
