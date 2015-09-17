using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class DockingViewModel : ViewModelBase
    {
        int id;
        KanColleModel model;

        #region プロパティ定義

        public string ShipName
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    if (model.Charamaster != null && model.Shipdata != null)
                    {
                        var index = model.Shipdata.FirstOrDefault(_ => _.Shipid == model.Ndockdata[id].Shipid);
                        if(index != null && model.Charamaster.ContainsKey(index.Characterid))
                            return Tools.ResourceStringGetter.GetShipNameResourceString(model.Charamaster[index.Characterid].Name);
                        else
                            return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Unknown");
                    }
                    else
                        return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Unknown");
                else
                    return null;
            }
        }

        public string ShipLevel
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    if (model.Charamaster != null && model.Shipdata != null)
                    {
                        var index = model.Shipdata.FirstOrDefault(_ => _.Shipid == model.Ndockdata[id].Shipid);
                        if (index != null)
                            return index.Level + "";
                        else
                            return "?";
                    }
                    else
                        return "?";
                else
                    return null;
            }
        }

        public TimeSpan RemainTime
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    return model.Ndockdata[id].CompleteTime - DateTime.Now;
                else
                    return new TimeSpan();
            }
        }

        public DateTime CompleteTime
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    return model.Ndockdata[id].CompleteTime;
                else
                    return new DateTime();
            }
        }

        public double ProgressValue
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    if (model.Charamaster != null && model.Shipdata != null)
                    {
                        var index = model.Shipdata.FirstOrDefault(_ => _.Shipid == model.Ndockdata[id].Shipid);
                        if (index != null)
                            return (index.NdockTime - RemainTime).TotalSeconds;
                        else
                            return 0;
                    }
                    else
                        return 0;
                else
                    return 0;
            }
        }

        public double ProgressMax
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    if (model.Charamaster != null && model.Shipdata != null)
                    {
                        var index = model.Shipdata.FirstOrDefault(_ => _.Shipid == model.Ndockdata[id].Shipid);
                        if (index != null)
                            return index.NdockTime.TotalSeconds;
                        else
                            return 0;
                    }
                    else
                        return 0;
                else
                    return 0;
            }
        }

        public string Message
        {
            get
            {
                if(model.Ndockdata != null)
                    if(model.Ndockdata.Count > id)
                        switch (model.Ndockdata[id].Status)
                        {
                            case NDockStatus.Unknown:
                                return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Unknown");
                            case NDockStatus.Locked:
                                return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Locked");
                            case NDockStatus.Empty:
                                return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Empty");
                            default:
                                return null;
                        }
                    else
                        return Tools.ResourceStringGetter.GetResourceString("DockingStatus_NotLoaded");
                else
                    return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Unknown");
            }
        }

        public bool DetailVisibility
        {
            get
            {
                if (model.Ndockdata != null && model.Ndockdata.Count > id)
                    switch (model.Ndockdata[id].Status)
                    {
                        case NDockStatus.Docking:
                            return true;
                        default:
                            return false;
                    }
                else
                    return false;
            }
        }

        private Brush _BorderBrush = Brushes.Gray;

        public Brush BorderBrush
        {
            get
            {
                return _BorderBrush;
            }
        }

        #endregion

        public DockingViewModel(int id)
        {
            this.id = id;
            model = Model.MainModel.Current.KancolleModel;
            model.InitializeComplete += model_InitializeComplete;
            model.Ndockdata.ExListChanged += Ndockdata_ExListChanged;
            Model.MainModel.Current.TimerModel.TimerElapsed += timerModel_TimerElapsed;
        }

        private void Ndockdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if(e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == id)
            {
                model.Ndockdata.ExListChanged -= Ndockdata_ExListChanged;
                model.Ndockdata[id].NDockChanged += DockingViewModel_NDockChanged;
                UpdateBorderBrush();
                OnPropertyChanged("");
            }
        }

        void DockingViewModel_NDockChanged(object sender, EventArgs e)
        {
            UpdateBorderBrush();
            OnPropertyChanged("");
        }

        void model_InitializeComplete(object sender, EventArgs e)
        {
            OnPropertyChanged("");
        }

        void timerModel_TimerElapsed(object sender, EventArgs e)
        {
            OnPropertyChanged(() => RemainTime);
            OnPropertyChanged(() => ProgressValue);
            UpdateBorderBrush();
        }

        void UpdateBorderBrush()
        {
            var nowbrush = GetBorderBrush();
            if (_BorderBrush != nowbrush)
            {
                _BorderBrush = nowbrush;
                OnPropertyChanged(() => BorderBrush);
            }
        }

        Brush GetBorderBrush()
        {
            if (model.Ndockdata != null && model.Ndockdata.Count > id)
                switch (model.Ndockdata[id].Status)
                {
                    case NDockStatus.Empty:
                        return Brushes.SpringGreen;
                    case NDockStatus.Docking:
                        if (RemainTime.TotalMinutes < 0)
                            return Brushes.OrangeRed;
                        else if(RemainTime.TotalMinutes < 1)
                            return Brushes.DarkOrange;
                        else
                            return Brushes.SlateBlue;
                    default:
                        return Brushes.Gray;
                }
            else
                return Brushes.Gray;
        }
    }
}
