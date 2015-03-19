using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Expedition
{
    class ExpeditionFleetViewModel : ViewModelBase
    {
        KanColleModel model;

        #region プロパティ定義

        private int _DeckID;

        /// <summary>
        /// 艦隊ID（index, 0から）
        /// </summary>
        public int DeckID
        {
            get
            {
                return _DeckID;
            }

            set
            {
                if (_DeckID != value)
                {
                    _DeckID = value;
                    ID = value + 1;
                    OnPropertyChanged(() => DeckID);
                    OnPropertyChanged(() => ID);
                }
            }
        }

        /// <summary>
        /// 艦隊の番号（1から）
        /// </summary>
        public int ID { get; set; } = 1;


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


        private int _FlagShipLevel;

        /// <summary>
        /// 旗艦レベル
        /// </summary>
        public int FlagShipLevel
        {
            get
            {
                return _FlagShipLevel;
            }

            set
            {
                if (_FlagShipLevel != value)
                {
                    _FlagShipLevel = value;
                    OnPropertyChanged(() => FlagShipLevel);
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


        private int _MinCond;

        /// <summary>
        /// Cond値最小値
        /// </summary>
        public int MinCond
        {
            get
            {
                return _MinCond;
            }

            set
            {
                if (_MinCond != value)
                {
                    _MinCond = value;
                    OnPropertyChanged(() => MinCond);
                }
            }
        }


        private int _RemainExpeditionCount;

        /// <summary>
        /// 遠征の残り回数
        /// </summary>
        public int RemainExpeditionCount
        {
            get
            {
                return _RemainExpeditionCount;
            }

            set
            {
                if (_RemainExpeditionCount != value)
                {
                    _RemainExpeditionCount = value;
                    OnPropertyChanged(() => RemainExpeditionCount);
                }
            }
        }



        private int _Status = -3;

        /// <summary>
        /// 遠征のステータス
        /// -3:未取得 -2:未開放 -1:不明
        /// 0:母港にいる 1:遠征中 2:遠征完了 3:遠征強制帰投中 4:遠征中（1分未満） 5:遠征強制帰投中（1分未満）
        /// </summary>
        public int Status
        {
            get
            {
                return _Status;
            }

            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    DetailVisibility = value >= 1;
                    switch(value)
                    {
                        case -3: Message = "読み込まれていません"; break;
                        case -2: Message = "艦隊が未開放です"; break;
                        case -1: Message = "不明"; break;
                        case 0: Message = "母港で待機中です"; break;
                    }

                    OnPropertyChanged(() => Status);
                    OnPropertyChanged(() => DetailVisibility);
                }
            }
        }

        public bool DetailVisibility { get; set; }

        public string Message { get; set; } = "読み込まれていません";


        private int _ExpeditionID;

        /// <summary>
        /// 遠征ID
        /// </summary>
        public int ExpeditionID
        {
            get
            {
                return _ExpeditionID;
            }

            set
            {
                if (_ExpeditionID != value)
                {
                    _ExpeditionID = value;
                    OnPropertyChanged(() => ExpeditionID);
                }
            }
        }


        private string _AreaName;

        /// <summary>
        /// 海域名
        /// </summary>
        public string AreaName
        {
            get
            {
                return _AreaName;
            }

            set
            {
                if (_AreaName != value)
                {
                    _AreaName = value;
                    OnPropertyChanged(() => AreaName);
                }
            }
        }


        private string _ExpeditionName;

        /// <summary>
        /// 遠征名
        /// </summary>
        public string ExpeditionName
        {
            get
            {
                return _ExpeditionName;
            }

            set
            {
                if (_ExpeditionName != value)
                {
                    _ExpeditionName = value;
                    OnPropertyChanged(() => ExpeditionName);
                }
            }
        }


        private string _Description;

        /// <summary>
        /// 遠征の説明
        /// </summary>
        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }



        private TimeSpan _ExpeditionLength;

        /// <summary>
        /// 遠征時間
        /// </summary>
        public TimeSpan ExpeditionLength
        {
            get
            {
                return _ExpeditionLength;
            }

            set
            {
                if (_ExpeditionLength != value)
                {
                    _ExpeditionLength = value;
                    ExpeditionLengthMinutes = value.TotalMinutes;
                    OnPropertyChanged(() => ExpeditionLength);
                    OnPropertyChanged(() => ExpeditionLengthMinutes);
                }
            }
        }

        public double ExpeditionLengthMinutes { get; set; }


        private TimeSpan _RemainLength;

        /// <summary>
        /// 完了までの残り時間
        /// </summary>
        public TimeSpan RemainLength
        {
            get
            {
                return _RemainLength;
            }

            set
            {
                if (_RemainLength != value)
                {
                    _RemainLength = value;
                    RemainLengthMinutes = value.TotalMinutes;
                    OnPropertyChanged(() => RemainLength);
                    OnPropertyChanged(() => RemainLengthMinutes);
                }
            }
        }

        public double RemainLengthMinutes { get; set; }


        private DateTime _CompleteTime;

        /// <summary>
        /// 完了時刻
        /// </summary>
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

        #endregion

        FleetData fleet;

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="DeckID">艦隊インデックス。第1艦隊なら0で。</param>
        public ExpeditionFleetViewModel(int DeckID)
        {
            this.DeckID = DeckID;
            model = Miotsukushi.Model.MainModel.Current.kancolleModel;
            model.fleetdata.ExListChanged += Fleetdata_ExListChanged;
            model.InitializeComplete += Model_InitializeComplete;
            Miotsukushi.Model.MainModel.Current.timerModel.TimerElapsed += TimerModel_TimerElapsed;
        }

        private void Model_InitializeComplete(object sender, EventArgs e)
        {
            if(model.fleetdata.Count <= DeckID)
            {
                // 艦隊が未開放
                Status = -2;
            }

            model.InitializeComplete -= Model_InitializeComplete;
        }

        private void TimerModel_TimerElapsed(object sender, EventArgs e)
        {
            RemainLength = CompleteTime > DateTime.Now ? CompleteTime - DateTime.Now : TimeSpan.Zero;
            if (RemainLengthMinutes < 1)
            {
                if (Status == 1)
                    Status = 4;
                else if (Status == 3)
                    Status = 5;
            }
            else
            {
                if (Status == 4)
                    Status = 1;
                else if (Status == 5)
                    Status = 3;
            }
        }

        private void Fleetdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if(e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == DeckID)
            {
                model.fleetdata.ExListChanged -= Fleetdata_ExListChanged;
                fleet = model.fleetdata[DeckID];

                DeckName = fleet.DeckName;
                FlagShipLevel = fleet.FlagShipLevel;
                SumShipLevel = fleet.SumShipLevel;
                DrumCount = fleet.DrumCount;
                DrumShipCount = fleet.DrumShipCount;
                MinCond = fleet.MinCond;
                RemainExpeditionCount = fleet.RemainExpeditionCount;

                switch (fleet.ExpeditionStatus)
                {
                    case FleetExpeditionStatus.at_home:
                        Status = 0;
                        break;
                    case FleetExpeditionStatus.on_expedition:
                        Status = 1;
                        break;
                    case FleetExpeditionStatus.expedition_complete:
                        Status = 2;
                        break;
                    case FleetExpeditionStatus.force_backing:
                        Status = 3;
                        break;
                    default:
                        Status = -1;
                        break;
                }

                if (Status >= 1)
                {
                    ExpeditionID = fleet.ExpeditionID;
                    ExpeditionLength = TimeSpan.FromMinutes(model.missionmaster.ContainsKey(ExpeditionID) ? model.missionmaster[ExpeditionID].time_minute : 0);
                    ExpeditionName = model.missionmaster.ContainsKey(ExpeditionID) ? model.missionmaster[ExpeditionID].name : "不明";
                    AreaName = (model.missionmaster.ContainsKey(ExpeditionID) && model.mapareamaster.ContainsKey(model.missionmaster[ExpeditionID].maparea_id) ?
                        model.mapareamaster[model.missionmaster[ExpeditionID].maparea_id].name : "不明");
                    Description = model.missionmaster.ContainsKey(ExpeditionID) ? model.missionmaster[ExpeditionID].details : "不明";
                    CompleteTime = fleet.ExpeditionBacktime;
                    RemainLength = CompleteTime > DateTime.Now ? CompleteTime - DateTime.Now : TimeSpan.Zero;
                    if (RemainLengthMinutes < 1)
                        if (Status == 1)
                            Status = 4;
                        else if (Status == 3)
                            Status = 5;
                }

                fleet.PropertyChanged += Fleet_PropertyChanged;
            }
        }

        private void Fleet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "DeckName":
                    DeckName = fleet.DeckName;
                    break;
                case "FlagShipLevel":
                    FlagShipLevel = fleet.FlagShipLevel;
                    break;
                case "SumShipLevel":
                    SumShipLevel = fleet.SumShipLevel;
                    break;
                case "DrumCount":
                    DrumCount = fleet.DrumCount;
                    break;
                case "DrumShipCount":
                    DrumShipCount = fleet.DrumShipCount;
                    break;
                case "MinCond":
                    MinCond = fleet.MinCond;
                    break;
                case "RemainExpeditionCount":
                    RemainExpeditionCount = fleet.RemainExpeditionCount;
                    break;
                case "ExpeditionStatus":
                    switch (fleet.ExpeditionStatus)
                    {
                        case FleetExpeditionStatus.at_home:
                            Status = 0;
                            break;
                        case FleetExpeditionStatus.on_expedition:
                            if (RemainLengthMinutes < 1)
                                Status = 4;
                            else
                                Status = 1;
                            break;
                        case FleetExpeditionStatus.expedition_complete:
                            Status = 2;
                            break;
                        case FleetExpeditionStatus.force_backing:
                            if (RemainLengthMinutes < 1)
                                Status = 5;
                            else
                                Status = 3;
                            break;
                        default:
                            Status = 0;
                            break;
                    }
                    break;
                case "ExpeditionID":
                    ExpeditionID = fleet.ExpeditionID;
                    ExpeditionLength = TimeSpan.FromMinutes(model.missionmaster.ContainsKey(ExpeditionID) ? model.missionmaster[ExpeditionID].time_minute : 0);
                    ExpeditionName = model.missionmaster.ContainsKey(ExpeditionID) ? model.missionmaster[ExpeditionID].name : "不明";
                    AreaName = (model.missionmaster.ContainsKey(ExpeditionID) && model.mapareamaster.ContainsKey(model.missionmaster[ExpeditionID].maparea_id) ?
                        model.mapareamaster[model.missionmaster[ExpeditionID].maparea_id].name : "不明");
                    Description = model.missionmaster.ContainsKey(ExpeditionID) ? model.missionmaster[ExpeditionID].details : "不明";
                    break;
                case "ExpeditionBacktime":
                    CompleteTime = fleet.ExpeditionBacktime;
                    RemainLength = CompleteTime > DateTime.Now ? CompleteTime - DateTime.Now : TimeSpan.Zero;
                    if (RemainLengthMinutes < 1)
                    {
                        if (Status == 1)
                            Status = 4;
                        else if (Status == 3)
                            Status = 5;
                    }
                    else
                    {
                        if (Status == 4)
                            Status = 1;
                        else if (Status == 5)
                            Status = 3;
                    }
                    break;
            }
        }
    }
}
