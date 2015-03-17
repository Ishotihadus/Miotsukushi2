using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Miotsukushi.Tools;

namespace Miotsukushi.Model.KanColle
{
    class FleetData : NotifyModelBase
    {
        #region プロパティ定義

        private string _DeckName;
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
        public int FlagShipLevel
        {
            get
            {
                return _FlagShipLevel;
            }

            private set
            {
                if (_FlagShipLevel != value)
                {
                    _FlagShipLevel = value;
                    OnPropertyChanged(() => FlagShipLevel);
                    System.Diagnostics.Debug.WriteLine(string.Format("艦隊「{0}」の旗艦レベルが{1}になったぞ。", DeckName, value));
                }
            }
        }

        private int _SumShipLevel;
        public int SumShipLevel
        {
            get
            {
                return _SumShipLevel;
            }

            private set
            {
                if (_SumShipLevel != value)
                {
                    _SumShipLevel = value;
                    OnPropertyChanged(() => SumShipLevel);
                    System.Diagnostics.Debug.WriteLine(string.Format("艦隊「{0}」の合計レベルが{1}になったぞ。", DeckName, value));
                }
            }
        }

        private int _SumAirMastery;
        /// <summary>
        /// 制空値の合計
        /// </summary>
        public int SumAirMastery
        {
            get
            {
                return _SumAirMastery;
            }

            private set
            {
                if (_SumAirMastery != value)
                {
                    _SumAirMastery = value;
                    OnPropertyChanged(() => SumAirMastery);
                    System.Diagnostics.Debug.WriteLine(string.Format("艦隊「{0}」の合計制空値が{1}になったぞ。", DeckName, value));
                }
            }
        }

        private int _DrumCount;
        /// <summary>
        /// ドラム缶数の合計
        /// </summary>
        public int DrumCount
        {
            get
            {
                return _DrumCount;
            }

            private set
            {
                if (_DrumCount != value)
                {
                    _DrumCount = value;
                    OnPropertyChanged(() => DrumCount);
                    System.Diagnostics.Debug.WriteLine(string.Format("艦隊「{0}」の合計ドラム缶数が{1}になったぞ。", DeckName, value));
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

            private set
            {
                if (_DrumShipCount != value)
                {
                    _DrumShipCount = value;
                    OnPropertyChanged(() => DrumShipCount);
                    System.Diagnostics.Debug.WriteLine(string.Format("艦隊「{0}」のドラム缶積載艦数が{1}になったぞ。", DeckName, value));
                }
            }
        }



        #endregion

        public ObservableCollection<int> ships = new ObservableCollection<int>();

        public FleetExpeditionStatus expedition_status;
        public DateTime expedition_backtime;
        public int expedition_id;

        /// <summary>
        /// 遠征の情報が変更されたときに呼び出されます
        /// </summary>
        public event EventHandler FleetMissionChanged;
        protected virtual void OnFleetMissionChanged(System.EventArgs e) { if (FleetMissionChanged != null) { FleetMissionChanged(this, e); } }

        public void FromDeckValue(KanColleLib.TransmissionData.api_get_member.values.DeckValue data)
        {
            DeckName = data.name;

            for (int i = 0; i < data.ship.Length; i++)
            {
                if (data.ship[i] == -1)
                {
                    if (ships.Count > i)
                        ships.RemoveAt(i);
                }
                else
                {
                    if (i < ships.Count)
                    {
                        if (ships[i] != data.ship[i])
                        {
                            ships[i] = data.ship[i];
                        }
                    }
                    else
                    {
                        ships.Add(data.ship[i]);
                    }
                }
            }

            if (data.mission.Length >= 3)
                ChangeMissionStatus((int)data.mission[0], (int)data.mission[1], data.mission[2]);
            else
                ChangeMissionStatus(-1, -1, -1);

            Recalc();
        }

        /// <summary>
        /// 合計レベルとかの計算
        /// </summary>
        public void Recalc()
        {
            var model = MainModel.Current.kancolleModel;

            int _sumshiplevel = 0;
            int _sumairmastery = 0;
            int _drumcount = 0;
            int _drumshipcount = 0;
            for (int i = 0; i < ships.Count; i++)
            {
                var ship = model.shipdata.FirstOrDefault(_ => _.shipid == ships[i]);
                if (ship != null)
                {
                    _sumshiplevel += ship.level;
                    _sumairmastery += KanColleTools.ShipAirMastery(ship);
                    int thisdrumcount = KanColleTools.ShipDrumCount(ship);
                    _drumcount += thisdrumcount;
                    if (thisdrumcount > 0)
                        ++_drumshipcount;
                }
                if (i == 0)
                    if (ship != null)
                        FlagShipLevel = ship.level;
                    else
                        FlagShipLevel = 0;
            }
            SumShipLevel = _sumshiplevel;
            SumAirMastery = _sumairmastery;
            DrumCount = _drumcount;
            DrumShipCount = _drumshipcount;
        }

        public void ChangeMissionStatus(int status, int id, long time)
        {
            bool missionchanged = false;

            FleetExpeditionStatus _st =
                status == 0 ? FleetExpeditionStatus.at_home :
                status == 1 ? FleetExpeditionStatus.on_expedition :
                status == 2 ? FleetExpeditionStatus.expedition_complete :
                status == 3 ? FleetExpeditionStatus.force_backing :
                FleetExpeditionStatus.unknown;
            if (_st != expedition_status)
            {
                missionchanged = true;
                expedition_status = _st;
            }

            int _id = id;
            if (_id != expedition_id)
            {
                missionchanged = true;
                expedition_id = _id;
            }

            DateTime _tm = Miotsukushi.Tools.TimeParser.ParseTimeFromLong(time);
            if (_tm != expedition_backtime)
            {
                missionchanged = true;
                expedition_backtime = _tm;
            }

            if (missionchanged)
                OnFleetMissionChanged(new System.EventArgs());
        }
    }
}
