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
                }
            }
        }


        private double _OkinoshimaSearchParameter;
        /// <summary>
        /// いわゆる2-5式
        /// </summary>
        public double OkinoshimaSearchParameter
        {
            get
            {
                return _OkinoshimaSearchParameter;
            }

            set
            {
                if (_OkinoshimaSearchParameter != value)
                {
                    _OkinoshimaSearchParameter = value;
                    OnPropertyChanged(() => OkinoshimaSearchParameter);
                }
            }
        }


        private double _OkinoshimaSearchParameterError;
        /// <summary>
        /// いわゆる2-5式のエラー
        /// </summary>
        public double OkinoshimaSearchParameterError
        {
            get
            {
                return _OkinoshimaSearchParameterError;
            }

            set
            {
                if (_OkinoshimaSearchParameterError != value)
                {
                    _OkinoshimaSearchParameterError = value;
                    OnPropertyChanged(() => OkinoshimaSearchParameterError);
                }
            }
        }

        private int _MinCond;
        /// <summary>
        /// 最小Cond値（なお1回の遠征で減るCondは3）
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
                    RemainExpeditionCount = (value >= 50) ? (value - 49) / 3 + 1 : 0;
                }
            }
        }

        private int _RemainExpeditionCount;
        /// <summary>
        /// 同一編成キラ付けなしで全艦キラキラ遠征に出せる回数
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
            double _okinoshimaparameter = 0;
            double _okinoshimaerror = 0;
            int? _mincond = null;
            for (int i = 0; i < ships.Count; i++)
            {
                var ship = model.shipdata.FirstOrDefault(_ => _.shipid == ships[i]);
                if (ship != null)
                {
                    _sumshiplevel += ship.level;
                    _sumairmastery += ship.air_mastery;
                    int thisdrumcount = KanColleTools.ShipDrumCount(ship);
                    _drumcount += thisdrumcount;
                    if (thisdrumcount > 0)
                        ++_drumshipcount;
                    double thisokinoshimaparameter;
                    double thisokinoshimaerror;
                    KanColleTools.ShipOkinoshimaSearchParameter(ship, out thisokinoshimaparameter, out thisokinoshimaerror);
                    _okinoshimaparameter += thisokinoshimaparameter;
                    _okinoshimaerror += thisokinoshimaerror;
                }
                if (i == 0)
                    if (ship != null)
                        FlagShipLevel = ship.level;
                    else
                        FlagShipLevel = 0;

                if (!_mincond.HasValue || _mincond > ship.condition)
                    _mincond = ship.condition;
            }
            SumShipLevel = _sumshiplevel;
            SumAirMastery = _sumairmastery;
            DrumCount = _drumcount;
            DrumShipCount = _drumshipcount;
            OkinoshimaSearchParameter = _okinoshimaparameter - 0.6142467 * (int)((model.basicdata.admiral_level + 4) / 5) * 5;
            OkinoshimaSearchParameterError = _okinoshimaerror + 0.03692224 * (int)((model.basicdata.admiral_level + 4) / 5) * 5;

            MinCond = _mincond ?? 0;
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
