using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class ConstructionViewModel : ViewModelBase
    {
        readonly int id;
        readonly KanColleModel model;
        static readonly string unknown_text = Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Unknown");

        #region プロパティ定義

        private string _ShipType = unknown_text;
        public string ShipType
        {
            get
            {
                return _ShipType;
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

        private string _ShipName = unknown_text;
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

        private TimeSpan _RemainTime = new TimeSpan();
        public TimeSpan RemainTime
        {
            get
            {
                return _RemainTime;
            }

            set
            {
                if (_RemainTime != value)
                {
                    _RemainTime = value;
                    OnPropertyChanged(() => RemainTime);
                }
            }
        }

        private DateTime? _CompleteTime = null;
        public DateTime? CompleteTime
        {
            get
            {
                return _CompleteTime;
            }

            set
            {
                if (_CompleteTime != value)
                {
                    _CompleteTime = value;
                    OnPropertyChanged(() => CompleteTime);
                }
            }
        }

        private double _ProgressValue = 0;
        public double ProgressValue
        {
            get
            {
                return _ProgressValue;
            }

            set
            {
                if (_ProgressValue != value)
                {
                    _ProgressValue = value;
                    OnPropertyChanged(() => ProgressValue);
                }
            }
        }

        private double _ProgressMax = 0;
        public double ProgressMax
        {
            get
            {
                return _ProgressMax;
            }

            set
            {
                if (_ProgressMax != value)
                {
                    _ProgressMax = value;
                    OnPropertyChanged(() => ProgressMax);
                }
            }
        }

        private string _Message = Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_NotLoaded");
        public string Message
        {
            get
            {
                return _Message;
            }

            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }

        private bool _DetailVisibility = false;
        public bool DetailVisibility
        {
            get
            {
                return _DetailVisibility;
            }

            set
            {
                if (_DetailVisibility != value)
                {
                    _DetailVisibility = value;
                    OnPropertyChanged(() => DetailVisibility);
                }
            }
        }

        private Brush _BorderBrush = Brushes.Gray;
        public Brush BorderBrush
        {
            get
            {
                return _BorderBrush;
            }

            set
            {
                if (_BorderBrush != value)
                {
                    _BorderBrush = value;
                    OnPropertyChanged(() => BorderBrush);
                }
            }
        }


        #endregion

        public ConstructionViewModel(int id)
        {
            this.id = id;
            model = Model.MainModel.Current.KancolleModel;
            model.InitializeComplete += model_InitializeComplete;
            model.Kdockdata.ExListChanged += Kdockdata_ExListChanged;
            Model.MainModel.Current.TimerModel.TimerElapsed += timerModel_TimerElapsed;
        }

        private void Kdockdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if (e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == id)
            {
                model.Kdockdata.ExListChanged -= Kdockdata_ExListChanged;
                model.Kdockdata[id].PropertyChanged += ConstructionViewModel_PropertyChanged;
                all_update();
            }
        }

        void model_InitializeComplete(object sender, EventArgs e)
        {
            all_update();
        }

        void ConstructionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            all_update();
        }

        void timerModel_TimerElapsed(object sender, EventArgs e)
        {
            timer_update();
        }

        void all_update()
        {
            if (id >= model.Kdockdata.Count)
            {
                ShipType = unknown_text;
                ShipName = unknown_text;
                RemainTime = new TimeSpan();
                CompleteTime = null;
                ProgressValue = 0;
                ProgressMax = 0;
                Message = Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_NotLoaded");
                DetailVisibility = false;
                BorderBrush = Brushes.Gray;
            }
            else
            {
                if (model.Charamaster.ContainsKey(model.Kdockdata[id].Charaid))
                {
                    ShipName = Tools.ResourceStringGetter.GetShipNameResourceString(model.Charamaster[model.Kdockdata[id].Charaid].Name);
                    if (model.Shiptypemaster.ContainsKey(model.Charamaster[model.Kdockdata[id].Charaid].Shiptype))
                        ShipType = Tools.ResourceStringGetter.GetShipTypeNameResourceString(model.Shiptypemaster[model.Charamaster[model.Kdockdata[id].Charaid].Shiptype].Name);
                    else
                        ShipType = unknown_text;
                    ProgressMax = model.Charamaster[model.Kdockdata[id].Charaid].Buildingtime;
                }
                else
                {
                    ShipName = unknown_text;
                    ShipType = unknown_text;
                    ProgressMax = 0;
                }

                CompleteTime = model.Kdockdata[id].CompleteTime;

                switch (model.Kdockdata[id].Status)
                {
                    case KDockStatus.Building:
                        DetailVisibility = true;
                        Message = null;
                        break;
                    case KDockStatus.Complete:
                        DetailVisibility = true;
                        Message = null;
                        CompleteTime = null;
                        break;
                    case KDockStatus.Empty:
                        Message = Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Empty");
                        DetailVisibility = false;
                        break;
                    case KDockStatus.Locked:
                        Message = Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Locked");
                        DetailVisibility = false;
                        break;
                    case KDockStatus.Unknown:
                    default:
                        Message = unknown_text;
                        DetailVisibility = false;
                        break;
                }
            }

            timer_update();
        }

        void timer_update()
        {
            if (id >= model.Kdockdata.Count)
            {
                RemainTime = new TimeSpan();
                ProgressValue = 0;
                BorderBrush = Brushes.Gray;
            }
            else
            {
                if (CompleteTime.HasValue)
                {
                    RemainTime = CompleteTime.Value - DateTime.Now;
                    ProgressValue = ProgressMax - RemainTime.TotalMinutes;
                }

                switch (model.Kdockdata[id].Status)
                {
                    case KDockStatus.Building:
                        if (RemainTime.TotalMinutes < 0)
                            BorderBrush = Brushes.OrangeRed;
                        else
                            BorderBrush = Brushes.SlateBlue;
                        break;
                    case KDockStatus.Complete:
                        BorderBrush = Brushes.OrangeRed;
                        break;
                    case KDockStatus.Empty:
                        BorderBrush = Brushes.SpringGreen;
                        break;
                    case KDockStatus.Locked:
                        BorderBrush = Brushes.Gray;
                        break;
                    case KDockStatus.Unknown:
                    default:
                        BorderBrush = Brushes.Gray;
                        break;
                }
            }
        }
    }
}
