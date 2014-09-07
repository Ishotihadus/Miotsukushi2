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
        int id;
        KanColleModel model;

        #region プロパティ定義

        public int ID
        {
            get
            {
                return id + 1;
            }
        }

        public string MissionName
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id && model.missionmaster.ContainsKey(model.fleetdata[id].expedition_id))
                    return model.missionmaster[model.fleetdata[id].expedition_id].name;
                else
                    return null;
            }
        }

        public TimeSpan RemainTime
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id)
                    return model.fleetdata[id].expedition_backtime - DateTime.Now;
                else
                    return new TimeSpan();
            }
        }

        public DateTime CompleteTime
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id)
                    return model.fleetdata[id].expedition_backtime;
                else
                    return new DateTime();
            }
        }

        public double ProgressValue
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id && model.missionmaster.ContainsKey(model.fleetdata[id].expedition_id))
                    return (double)model.missionmaster[model.fleetdata[id].expedition_id].time_minute - RemainTime.TotalMinutes;
                else
                    return 0;
            }
        }

        public double ProgressMax
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id && model.missionmaster.ContainsKey(model.fleetdata[id].expedition_id))
                    return model.missionmaster[model.fleetdata[id].expedition_id].time_minute;
                else
                    return 0;
            }
        }

        public string Message
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id)
                    switch (model.fleetdata[id].expedition_status)
                    {
                        case FleetExpeditionStatus.unknown:
                            return "不明";
                        case FleetExpeditionStatus.at_home:
                            return "遠征に出ていません";
                        default:
                            return null;
                    }
                else
                    return "読み込まれていません";
            }
        }

        public bool DetailVisibility
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id)
                    switch (model.fleetdata[id].expedition_status)
                    {
                        case FleetExpeditionStatus.on_expedition:
                        case FleetExpeditionStatus.force_backing:
                        case FleetExpeditionStatus.expedition_complete:
                            return true;
                        default:
                            return false;
                    }
                else
                    return false;
            }
        }

        public Brush BorderBrush
        {
            get
            {
                if (model.fleetdata != null && model.fleetdata.Count > id)
                    switch (model.fleetdata[id].expedition_status)
                    {
                        case FleetExpeditionStatus.at_home:
                            return Brushes.SpringGreen;
                        case FleetExpeditionStatus.on_expedition:
                            if(RemainTime.TotalMinutes < 0)
                                return Brushes.OrangeRed;
                            else if(RemainTime.TotalMinutes < 1)
                                return Brushes.DarkOrange;
                            else
                                return Brushes.SlateBlue;
                        case FleetExpeditionStatus.force_backing:
                            if (RemainTime.TotalMinutes < 0)
                                return Brushes.OrangeRed;
                            else if (RemainTime.TotalMinutes < 1)
                                return Brushes.Chocolate;
                            else
                                return Brushes.Crimson;
                        case FleetExpeditionStatus.expedition_complete:
                            return Brushes.OrangeRed;
                        default:
                            return Brushes.Gray;
                    }
                else
                    return Brushes.Gray;
            }
        }

        #endregion

        public ExpeditionViewModel(int id)
        {
            this.id = id;
            model = Model.MainModel.Current.kancolleModel;
            model.InitializeComplete += model_InitializeComplete;
            Model.MainModel.Current.timerModel.TimerElapsed += timerModel_TimerElapsed;
        }

        void timerModel_TimerElapsed(object sender, EventArgs e)
        {
            OnPropertyChanged(() => RemainTime);
            OnPropertyChanged(() => ProgressValue);
            OnPropertyChanged(() => BorderBrush);
        }

        void model_InitializeComplete(object sender, EventArgs e)
        {
            if (model.fleetdata != null && model.fleetdata.Count > id)
                model.fleetdata[id].FleetMissionChanged += ExpeditionViewModel_FleetMissionChanged;
            OnPropertyChanged("");
        }

        void ExpeditionViewModel_FleetMissionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("");
        }

    }
}
