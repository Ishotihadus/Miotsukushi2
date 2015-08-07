using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.DetailInfoPanel.GeneralParts
{
    class FleetSummaryViewModel : ViewModelBase
    {
        private Miotsukushi.Model.KanColle.KanColleModel model;
        private int id;
        private Model.KanColle.FleetData fleet;

        /// <summary>
        /// 艦隊番号（1から始まる）
        /// </summary>
        public int ID { get { return id + 1; } }

        private string _DeckName;

        /// <summary>
        /// 艦隊名
        /// </summary>
        public string DeckName
        {
            get
            {
                return _DeckName;
            }

            set
            {
                if (_DeckName != value)
                {
                    _DeckName = value;
                    OnPropertyChanged(() => DeckName);
                }
            }
        }


        private bool _HasDockingShip;

        /// <summary>
        /// 入渠中の艦がいるか
        /// </summary>
        public bool HasDockingShip
        {
            get
            {
                return _HasDockingShip;
            }

            set
            {
                if (_HasDockingShip != value)
                {
                    _HasDockingShip = value;
                    OnPropertyChanged(() => HasDockingShip);
                }
            }
        }


        private bool _HasUnsuppliedShip;

        /// <summary>
        /// 未補給の艦がいるか
        /// </summary>
        public bool HasUnsuppliedShip
        {
            get
            {
                return _HasUnsuppliedShip;
            }

            set
            {
                if (_HasUnsuppliedShip != value)
                {
                    _HasUnsuppliedShip = value;
                    OnPropertyChanged(() => HasUnsuppliedShip);
                }
            }
        }


        private bool _HasTiredShip;

        /// <summary>
        /// 疲労中の艦がいるか
        /// </summary>
        public bool HasTiredShip
        {
            get
            {
                return _HasTiredShip;
            }

            set
            {
                if (_HasTiredShip != value)
                {
                    _HasTiredShip = value;
                    OnPropertyChanged(() => HasTiredShip);
                }
            }
        }


        private int _SumShipLevel;

        /// <summary>
        /// 合計レベル
        /// </summary>
        public int SumShipLevel
        {
            get
            {
                return _SumShipLevel;
            }

            set
            {
                if (_SumShipLevel != value)
                {
                    _SumShipLevel = value;
                    OnPropertyChanged(() => SumShipLevel);
                }
            }
        }


        private int _DrumCount;

        /// <summary>
        /// ドラム缶数
        /// </summary>
        public int DrumCount
        {
            get
            {
                return _DrumCount;
            }

            set
            {
                if (_DrumCount != value)
                {
                    _DrumCount = value;
                    OnPropertyChanged(() => DrumCount);
                }
            }
        }


        private int _DrumShipCount;

        /// <summary>
        /// ドラム缶積載艦数
        /// </summary>
        public int DrumShipCount
        {
            get
            {
                return _DrumShipCount;
            }

            set
            {
                if (_DrumShipCount != value)
                {
                    _DrumShipCount = value;
                    OnPropertyChanged(() => DrumShipCount);
                }
            }
        }


        private int _SumAirMastery;

        /// <summary>
        /// 合計制空値
        /// </summary>
        public int SumAirMastery
        {
            get
            {
                return _SumAirMastery;
            }

            set
            {
                if (_SumAirMastery != value)
                {
                    _SumAirMastery = value;
                    OnPropertyChanged(() => SumAirMastery);
                }
            }
        }


        private int _SumReconnaissance;

        /// <summary>
        /// 合計索敵値
        /// </summary>
        public int SumReconnaissance
        {
            get
            {
                return _SumReconnaissance;
            }

            set
            {
                if (_SumReconnaissance != value)
                {
                    _SumReconnaissance = value;
                    OnPropertyChanged(() => SumReconnaissance);
                }
            }
        }

        public ObservableCollection<FleetSummaryShipViewModel> Ships { get; set; }


        public FleetSummaryViewModel(int id)
        {
            Ships = new ObservableCollection<FleetSummaryShipViewModel>();
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(Ships, new object());

            model = Miotsukushi.Model.MainModel.Current.kancolleModel;
            
            this.id = id;
            if(model.fleetdata.Count > id)
            {
                fleet = model.fleetdata[id];
                ViewModelInitialize();
                model.fleetdata[id].PropertyChanged += FleetSummaryViewModel_PropertyChanged;
            }
            else
                model.fleetdata.ExListChanged += Fleetdata_ExListChanged;
        }

        private void Fleetdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if(e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == id)
            {
                model.fleetdata.ExListChanged -= Fleetdata_ExListChanged;
                fleet = model.fleetdata[id];
                ViewModelInitialize();
                model.fleetdata[id].PropertyChanged += FleetSummaryViewModel_PropertyChanged;
                model.fleetdata[id].ships.CollectionChanged += Ships_CollectionChanged;
            }
        }

        private void Ships_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems.Count > 0)
                        Ships.Insert(e.NewStartingIndex, new FleetSummaryShipViewModel(model.fleetdata[id].ships[e.NewStartingIndex]));
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    Ships.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    Ships.RemoveAt(e.OldStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    Ships[e.OldStartingIndex] = new FleetSummaryShipViewModel(model.fleetdata[id].ships[e.OldStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    for (int i = 0; i < model.fleetdata[id].ships.Count; i++)
                    {
                        if (i < Ships.Count)
                            Ships[i] = new FleetSummaryShipViewModel(model.fleetdata[id].ships[i]);
                        else
                            Ships.Add(new FleetSummaryShipViewModel(model.fleetdata[id].ships[i]));
                    }
                    break;
            }
        }

        private void ViewModelInitialize()
        {
            DeckName = fleet.DeckName;
            HasDockingShip = fleet.DockingShipsCount > 0;
            HasTiredShip = fleet.MinCond < 40 && fleet.ships.Count > 0;
            SumAirMastery = fleet.SumAirMastery;
            SumReconnaissance = (int)fleet.OkinoshimaSearchParameter;
            SumShipLevel = fleet.SumShipLevel;
            DrumShipCount = fleet.DrumShipCount;
            DrumCount = fleet.DrumCount;
        }

        private void FleetSummaryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "DeckName":
                    DeckName = fleet.DeckName;
                    break;
                case "DockingShipsCount":
                    HasDockingShip = fleet.DockingShipsCount > 0;
                    break;
                case "MinCond":
                    HasTiredShip = fleet.MinCond < 40 && fleet.ships.Count > 0;
                    break;
                case "SumAirMastery":
                    SumAirMastery = fleet.SumAirMastery;
                    break;
                case "OkinoshimaSearchParameter":
                    SumReconnaissance = (int)fleet.OkinoshimaSearchParameter;
                    break;
                case "SumShipLevel":
                    SumShipLevel = fleet.SumShipLevel;
                    break;
                case "DrumShipCount":
                    DrumShipCount = fleet.DrumShipCount;
                    break;
                case "DrumCount":
                    DrumCount = fleet.DrumCount;
                    break;
            }
        }
    }
}
