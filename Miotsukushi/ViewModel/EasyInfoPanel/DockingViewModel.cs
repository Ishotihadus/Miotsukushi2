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
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    if (model.charamaster != null && model.shipdata != null)
                    {
                        var index = model.shipdata.FirstOrDefault(_ => _.shipid == model.ndockdata[id].shipid);
                        if(index != null && model.charamaster.ContainsKey(index.characterid))
                            return Tools.ResourceStringGetter.GetNameResourceString(model.charamaster[index.characterid].name);
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
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    if (model.charamaster != null && model.shipdata != null)
                    {
                        var index = model.shipdata.FirstOrDefault(_ => _.shipid == model.ndockdata[id].shipid);
                        if (index != null)
                            return index.level + "";
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
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    return model.ndockdata[id].complete_time - DateTime.Now;
                else
                    return new TimeSpan();
            }
        }

        public DateTime CompleteTime
        {
            get
            {
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    return model.ndockdata[id].complete_time;
                else
                    return new DateTime();
            }
        }

        public double ProgressValue
        {
            get
            {
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    if (model.charamaster != null && model.shipdata != null)
                    {
                        var index = model.shipdata.FirstOrDefault(_ => _.shipid == model.ndockdata[id].shipid);
                        if (index != null)
                            return (index.ndock_time - RemainTime).TotalSeconds;
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
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    if (model.charamaster != null && model.shipdata != null)
                    {
                        var index = model.shipdata.FirstOrDefault(_ => _.shipid == model.ndockdata[id].shipid);
                        if (index != null)
                            return index.ndock_time.TotalSeconds;
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
                if(model.ndockdata != null)
                    if(model.ndockdata.Count > id)
                        switch (model.ndockdata[id].status)
                        {
                            case NDockStatus.unknown:
                                return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Unknown");
                            case NDockStatus.locked:
                                return Tools.ResourceStringGetter.GetResourceString("DockingStatus_Locked");
                            case NDockStatus.empty:
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
                if (model.ndockdata != null && model.ndockdata.Count > id)
                    switch (model.ndockdata[id].status)
                    {
                        case NDockStatus.docking:
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
            model = Model.MainModel.Current.kancolleModel;
            model.InitializeComplete += model_InitializeComplete;
            model.ndockdata.ItemAdded += ndockdata_ItemAdded;
            Model.MainModel.Current.timerModel.TimerElapsed += timerModel_TimerElapsed;
        }

        void ndockdata_ItemAdded(object sender, EventArgs e)
        {
            if(model.ndockdata.Count > id)
            {
                model.ndockdata[id].NDockChanged += DockingViewModel_NDockChanged;
                UpdateBorderBrush();
                OnPropertyChanged("");
                model.ndockdata.ItemAdded -= ndockdata_ItemAdded;
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
            if (model.ndockdata != null && model.ndockdata.Count > id)
                switch (model.ndockdata[id].status)
                {
                    case NDockStatus.empty:
                        return Brushes.SpringGreen;
                    case NDockStatus.docking:
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
