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
            var kcmodel = Model.MainModel.Current.kancolleModel;
            kcmodel.sortiemodel.SortieStarted += Sortiemodel_SortieStarted;
            kcmodel.sortiemodel.CellAdvanced += Sortiemodel_CellAdvanced;
            kcmodel.sortiemodel.GoBackPort += Sortiemodel_GoBackPort;
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(ShipsMe, new object());
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(ShipsCombined, new object());
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(Comments, new object());
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
        }

        void CellAppend()
        {
            var kcmodel = Model.MainModel.Current.kancolleModel;
            var cellinfo = kcmodel.sortiemodel.now_cell;

            if (cellinfo != null)
            {
                CellID = cellinfo.cell_no;
                switch (cellinfo.cell_type)
                {
                    case Model.KanColle.BattleModels.CellModel.CellType.start:
                        CellInfo = "始点";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.battle:
                        CellInfo = "戦闘";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.night_sp_battle:
                        CellInfo = "開幕夜戦";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.night_to_day_battle:
                        CellInfo = "夜戦→昼戦";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.air_battle:
                        CellInfo = "航空戦";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.success_ship_guard:
                        CellInfo = "船団護衛成功";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.air_search:
                        CellInfo = "航空偵察";
                        switch (cellinfo.cell_event_content_id)
                        {
                            case 0: CellInfo += " 失敗"; break;
                            case 1: CellInfo += " 成功"; break;
                            case 2: CellInfo += " 大成功"; break;
                        }
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.no_battle:
                        switch (cellinfo.cell_event_content_id)
                        {
                            case 1: CellInfo = "敵影を見ず"; break;
                            default: CellInfo = "気のせいだった"; break;
                        }
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.route_choice:
                        CellInfo = "能動分岐";
                        if(cellinfo.cell_event_content_values != null)
                            CellInfo += "（" + string.Join(", ", cellinfo.cell_event_content_values) + "）";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.supply:
                        CellInfo = "補給";
                        break;
                    case Model.KanColle.BattleModels.CellModel.CellType.happening:
                        CellInfo = "うずしお";
                        break;
                    default:
                        CellInfo = "不明";
                        break;
                }

                if(cellinfo.is_boss_battle)
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
            var kcmodel = Model.MainModel.Current.kancolleModel;
            var mapinfo = kcmodel.sortiemodel.now_map;
            Comments.Clear();

            if(mapinfo != null)
            {
                AreaID = mapinfo.maparea_id;
                MapID = mapinfo.map_no;
                MapName = mapinfo.map_name;
                OpeName = mapinfo.ope_title;
                OpeInfo = mapinfo.ope_info.Replace("<br>", "");
                if (mapinfo.selected_rank.HasValue)
                {
                    switch (mapinfo.selected_rank.Value)
                    {
                        case 0: OpeName += "（難易度未設定）"; break;
                        case 1: OpeName += "（丙作戦）"; break;
                        case 2: OpeName += "（乙作戦）"; break;
                        case 3: OpeName += "（甲作戦）"; break;
                    }
                }

                if(mapinfo.required_defeat_count.HasValue)
                    Comments.Add(string.Format("撃破回数: {0}/{1}", mapinfo.now_defeat_count.Value, mapinfo.required_defeat_count.Value));
                if (mapinfo.max_hp.HasValue)
                    Comments.Add(string.Format("マップHP: {0}/{1}", mapinfo.now_hp.Value, mapinfo.max_hp.Value));
            }
            CellAppend();

            ShipsMe.Clear();
            sortiing_deck = kcmodel.fleetdata[kcmodel.sortiemodel.sortiing_deck - 1];
            sortiing_deck.PropertyChanged += Sortiing_deck_PropertyChanged;
            foreach (var ship in kcmodel.fleetdata[kcmodel.sortiemodel.sortiing_deck - 1].ships)
                ShipsMe.Add(new SortieShipViewModel(kcmodel.shipdata.FirstOrDefault(_ => _.shipid == ship)));

            ShipsCombined.Clear();
            if (kcmodel.combined_flag > 0 && kcmodel.sortiemodel.sortiing_deck == 1)
            {
                sortiing_deck_combined = kcmodel.fleetdata[1];
                foreach (var ship in kcmodel.fleetdata[1].ships)
                    ShipsCombined.Add(new SortieShipViewModel(kcmodel.shipdata.FirstOrDefault(_ => _.shipid == ship)));
                sortiing_deck_combined.PropertyChanged += Sortiing_deck_combined_PropertyChanged;
            }
            else
            {
                sortiing_deck_combined = null;
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
                HasTaihaShip = sortiing_deck.HasTaihaShip || sortiing_deck_combined.HasTaihaShip;
            }
        }
    }
}
