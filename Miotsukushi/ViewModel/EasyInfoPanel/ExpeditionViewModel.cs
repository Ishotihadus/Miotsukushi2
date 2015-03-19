using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class ExpeditionViewModel : ViewModelBase
    {
        KanColleModel model;

        #region プロパティ定義

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



        private string _MissionName;
        public string MissionName
        {
            get
            {
                return _MissionName;
            }

            set
            {
                if (_MissionName != value)
                {
                    _MissionName = value;
                    OnPropertyChanged(() => MissionName);
                }
            }
        }


        private TimeSpan _RemainTime;
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


        private DateTime _CompleteTime;
        public DateTime CompleteTime
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



        private double _ProgressValue;
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


        private double _ProgressMax;
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



        private string _Message;
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


        private bool _DetailVisibility;
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


        FleetData fleet;

        public ExpeditionViewModel(int id)
        {
            this.ID = id + 1;
            model = Model.MainModel.Current.kancolleModel;
            model.InitializeComplete += model_InitializeComplete;
            model.fleetdata.ExListChanged += Fleetdata_ExListChanged;
            Model.MainModel.Current.timerModel.TimerElapsed += timerModel_TimerElapsed;

            DetailVisibility = false;
            Message = Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_NotLoaded");
        }

        private void Fleetdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if(e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == ID - 1)
            {
                model.fleetdata.ExListChanged -= Fleetdata_ExListChanged;
                fleet = model.fleetdata[ID - 1];
                fleet.PropertyChanged += Fleet_PropertyChanged;

                if (fleet.ExpeditionStatus == FleetExpeditionStatus.unknown)
                {
                    DetailVisibility = false;
                    Message = Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_Unknown");
                }
                else if (fleet.ExpeditionStatus == FleetExpeditionStatus.at_home)
                {
                    DetailVisibility = false;
                    Message = Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_AtHome");
                }
                else
                {
                    MissionName = model.missionmaster.ContainsKey(fleet.ExpeditionID) ? model.missionmaster[fleet.ExpeditionID].name : Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_Unknown");
                    CompleteTime = fleet.ExpeditionBacktime;
                    RemainTime = DateTime.Now < CompleteTime ? CompleteTime - DateTime.Now : TimeSpan.Zero;
                    ProgressMax = model.missionmaster.ContainsKey(fleet.ExpeditionID) ? model.missionmaster[fleet.ExpeditionID].time_minute : 0;
                    ProgressValue = ProgressMax - RemainTime.TotalMinutes;
                }
                UpdateBorderBrush();
            }
        }

        private void Fleet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "ExpeditionStatus":
                    if (fleet.ExpeditionStatus == FleetExpeditionStatus.unknown)
                    {
                        DetailVisibility = false;
                        Message = Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_Unknown");
                    }
                    else if(fleet.ExpeditionStatus == FleetExpeditionStatus.at_home)
                    {
                        DetailVisibility = false;
                        Message = Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_AtHome");
                    }
                    else
                        DetailVisibility = true;
                    UpdateBorderBrush();
                    break;
                case "ExpeditionID":
                    MissionName = model.missionmaster.ContainsKey(fleet.ExpeditionID) ? model.missionmaster[fleet.ExpeditionID].name : Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_Unknown");
                    CompleteTime = fleet.ExpeditionBacktime;
                    RemainTime = DateTime.Now < CompleteTime ? CompleteTime - DateTime.Now : TimeSpan.Zero;
                    ProgressMax = model.missionmaster.ContainsKey(fleet.ExpeditionID) ? model.missionmaster[fleet.ExpeditionID].time_minute : 0;
                    ProgressValue = ProgressMax - RemainTime.TotalMinutes;
                    break;
                case "ExpeditionBacktime":
                    CompleteTime = fleet.ExpeditionBacktime;
                    RemainTime = DateTime.Now < CompleteTime ? CompleteTime - DateTime.Now : TimeSpan.Zero;
                    ProgressValue = ProgressMax - RemainTime.TotalMinutes;
                    break;
            }
        }

        void timerModel_TimerElapsed(object sender, EventArgs e)
        {
            RemainTime = DateTime.Now < CompleteTime ? CompleteTime - DateTime.Now : TimeSpan.Zero;
            ProgressValue = ProgressMax - RemainTime.TotalMinutes;
            UpdateBorderBrush();
        }

        void model_InitializeComplete(object sender, EventArgs e)
        {
            if (model.fleetdata.Count <= ID - 1)
            {
                Message = Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_NotOpened");
                DetailVisibility = false;
                UpdateBorderBrush();
            }
            model.InitializeComplete -= model_InitializeComplete;
        }

        void UpdateBorderBrush()
        {
            if (model.fleetdata == null || model.fleetdata.Count <= ID - 1)
                BorderBrush = Brushes.Gray;
            else
                switch(fleet.ExpeditionStatus)
                {
                    case FleetExpeditionStatus.at_home:
                        BorderBrush =  Brushes.SpringGreen;
                        break;
                    case FleetExpeditionStatus.on_expedition:
                        if (RemainTime.TotalMinutes < 0)
                            BorderBrush = Brushes.OrangeRed;
                        else if (RemainTime.TotalMinutes < 1)
                            BorderBrush = Brushes.DarkOrange;
                        else
                            BorderBrush = Brushes.SlateBlue;
                        break;
                    case FleetExpeditionStatus.force_backing:
                        if (RemainTime.TotalMinutes < 0)
                            BorderBrush = Brushes.OrangeRed;
                        else if (RemainTime.TotalMinutes < 1)
                            BorderBrush = Brushes.Chocolate;
                        else
                            BorderBrush = Brushes.Crimson;
                        break;
                    case FleetExpeditionStatus.expedition_complete:
                        BorderBrush = Brushes.OrangeRed;
                        break;
                    default:
                        BorderBrush = Brushes.Gray;
                        break;
                }
        }

    }
}
