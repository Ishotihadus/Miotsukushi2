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
        /// 艦隊ID
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
                    OnPropertyChanged(() => DeckID);
                }
            }
        }


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



        private int _Status;

        /// <summary>
        /// 遠征のステータス
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
                    OnPropertyChanged(() => Status);
                }
            }
        }


        private int _MissionID;

        /// <summary>
        /// 遠征ID
        /// </summary>
        public int MissionID
        {
            get
            {
                return _MissionID;
            }

            set
            {
                if (_MissionID != value)
                {
                    _MissionID = value;
                    OnPropertyChanged(() => MissionID);
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


        private string _MissionName;

        /// <summary>
        /// 遠征名
        /// </summary>
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



        private TimeSpan _MissionLength;

        /// <summary>
        /// 遠征時間
        /// </summary>
        public TimeSpan MissionLength
        {
            get
            {
                return _MissionLength;
            }

            set
            {
                if (_MissionLength != value)
                {
                    _MissionLength = value;
                    OnPropertyChanged(() => MissionLength);
                }
            }
        }


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
                    OnPropertyChanged(() => RemainLength);
                }
            }
        }


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
        }

        private void Fleetdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if(e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == DeckID)
            {
                model.fleetdata.ExListChanged -= Fleetdata_ExListChanged;
                fleet = model.fleetdata[DeckID];
                initialize();
            }
        }

        void initialize()
        {
            // 初期化
            DeckName = fleet.DeckName;

            fleet.PropertyChanged += Fleet_PropertyChanged;
            fleet.ships.CollectionChanged += Ships_CollectionChanged;
        }

        private void Fleet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeckName")
                DeckName = fleet.DeckName;
        }

        void AppendParameters()
        {

        }

        private void Ships_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
