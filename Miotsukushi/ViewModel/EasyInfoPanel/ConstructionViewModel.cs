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
        int id;
        KanColleModel model;

        #region プロパティ定義

        public string ShipType
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id)
                    if (model.charamaster != null && model.charamaster.ContainsKey(model.kdockdata[id].charaid) &&
                        model.shiptypemaster != null && model.shiptypemaster.ContainsKey(model.charamaster[model.kdockdata[id].charaid].shiptype))
                        return Tools.ResourceStringGetter.GetShipTypeNameResourceString(model.shiptypemaster[model.charamaster[model.kdockdata[id].charaid].shiptype].name);
                    else
                        return Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Unknown");
                else
                    return null;
            }
        }

        public string ShipName
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id)
                    if (model.charamaster != null && model.charamaster.ContainsKey(model.kdockdata[id].charaid))
                        return Tools.ResourceStringGetter.GetShipNameResourceString(model.charamaster[model.kdockdata[id].charaid].name);
                    else
                        return Tools.ResourceStringGetter.GetResourceString("ExpeditionStatus_Unknown");
                else
                    return null;
            }
        }

        public TimeSpan RemainTime
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id)
                    return model.kdockdata[id].complete_time - DateTime.Now;
                else
                    return new TimeSpan();
            }
        }

        public DateTime? CompleteTime
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id)
                    if (model.kdockdata[id].status == KDockStatus.building)
                        return model.kdockdata[id].complete_time;
                    else
                        return null;
                else
                    return null;
            }
        }

        public double ProgressValue
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id && model.charamaster != null && model.charamaster.ContainsKey(model.kdockdata[id].charaid))
                    return model.charamaster[model.kdockdata[id].charaid].buildingtime - RemainTime.TotalMinutes;
                else
                    return 0;
            }
        }

        public double ProgressMax
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id && model.charamaster != null && model.charamaster.ContainsKey(model.kdockdata[id].charaid))
                    return model.charamaster[model.kdockdata[id].charaid].buildingtime;
                else
                    return 0;
            }
        }

        public string Message
        {
            get
            {
                if (model.kdockdata != null)
                    if (model.kdockdata.Count > id)
                        switch (model.kdockdata[id].status)
                        {
                            case KDockStatus.unknown:
                                return Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Unknown");
                            case KDockStatus.locked:
                                return Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Locked");
                            case KDockStatus.empty:
                                return Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Empty");
                            default:
                                return null;
                        }
                    else
                        return Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_NotLoaded");
                else
                    return Tools.ResourceStringGetter.GetResourceString("ConstructionStatus_Unknown");
            }
        }

        public bool DetailVisibility
        {
            get
            {
                if (model.kdockdata != null && model.kdockdata.Count > id)
                    switch (model.kdockdata[id].status)
                    {
                        case KDockStatus.building:
                        case KDockStatus.complete:
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

        public ConstructionViewModel(int id)
        {
            this.id = id;
            model = Model.MainModel.Current.kancolleModel;
            model.InitializeComplete += model_InitializeComplete;
            model.kdockdata.ItemAdded += kdockdata_ItemAdded;
            Model.MainModel.Current.timerModel.TimerElapsed += timerModel_TimerElapsed;
        }

        void model_InitializeComplete(object sender, EventArgs e)
        {
            OnPropertyChanged("");
        }

        void kdockdata_ItemAdded(object sender, EventArgs e)
        {
            if (model.kdockdata.Count > id)
            {
                model.kdockdata[id].KDockChanged += ConstructionViewModel_KDockChanged;
                UpdateBorderBrush();
                OnPropertyChanged("");
                model.kdockdata.ItemAdded -= kdockdata_ItemAdded;
            }
        }

        void timerModel_TimerElapsed(object sender, EventArgs e)
        {
            OnPropertyChanged(() => RemainTime);
            OnPropertyChanged(() => ProgressValue);
            UpdateBorderBrush();
        }

        void ConstructionViewModel_KDockChanged(object sender, EventArgs e)
        {
            UpdateBorderBrush();
            OnPropertyChanged("");
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
            if (model.kdockdata != null && model.kdockdata.Count > id)
                switch (model.kdockdata[id].status)
                {
                    case KDockStatus.empty:
                        return Brushes.SpringGreen;
                    case KDockStatus.building:
                        if (RemainTime.TotalMinutes < 0)
                            return Brushes.OrangeRed;
                        else
                            return Brushes.SlateBlue;
                    case KDockStatus.complete:
                        return Brushes.OrangeRed;
                    default:
                        return Brushes.Gray;
                }
            else
                return Brushes.Gray;
        }

    }
}
