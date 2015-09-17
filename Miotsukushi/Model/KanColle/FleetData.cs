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

        private string _deckName;
        public string DeckName
        {
            get
            {
                return _deckName;
            }

            set
            {
                if (_deckName != value)
                {
                    _deckName = value;
                    OnPropertyChanged(() => DeckName);
                }
            }
        }

        private int _flagShipLevel;
        public int FlagShipLevel
        {
            get
            {
                return _flagShipLevel;
            }

            private set
            {
                if (_flagShipLevel != value)
                {
                    _flagShipLevel = value;
                    OnPropertyChanged(() => FlagShipLevel);
                }
            }
        }

        private int _sumShipLevel;
        public int SumShipLevel
        {
            get
            {
                return _sumShipLevel;
            }

            private set
            {
                if (_sumShipLevel != value)
                {
                    _sumShipLevel = value;
                    OnPropertyChanged(() => SumShipLevel);
                }
            }
        }

        private int _sumAirMastery;
        /// <summary>
        /// 制空値の合計
        /// </summary>
        public int SumAirMastery
        {
            get
            {
                return _sumAirMastery;
            }

            private set
            {
                if (_sumAirMastery != value)
                {
                    _sumAirMastery = value;
                    OnPropertyChanged(() => SumAirMastery);
                }
            }
        }

        private int _drumCount;
        /// <summary>
        /// ドラム缶数の合計
        /// </summary>
        public int DrumCount
        {
            get
            {
                return _drumCount;
            }

            private set
            {
                if (_drumCount != value)
                {
                    _drumCount = value;
                    OnPropertyChanged(() => DrumCount);
                }
            }
        }

        private int _drumShipCount;
        /// <summary>
        /// ドラム缶積載艦数
        /// </summary>
        public int DrumShipCount
        {
            get
            {
                return _drumShipCount;
            }

            private set
            {
                if (_drumShipCount != value)
                {
                    _drumShipCount = value;
                    OnPropertyChanged(() => DrumShipCount);
                }
            }
        }


        private double _okinoshimaSearchParameter;
        /// <summary>
        /// いわゆる2-5式
        /// </summary>
        public double OkinoshimaSearchParameter
        {
            get
            {
                return _okinoshimaSearchParameter;
            }

            set
            {
                if (_okinoshimaSearchParameter != value)
                {
                    _okinoshimaSearchParameter = value;
                    OnPropertyChanged(() => OkinoshimaSearchParameter);
                }
            }
        }


        private double _okinoshimaSearchParameterError;
        /// <summary>
        /// いわゆる2-5式のエラー
        /// </summary>
        public double OkinoshimaSearchParameterError
        {
            get
            {
                return _okinoshimaSearchParameterError;
            }

            set
            {
                if (_okinoshimaSearchParameterError != value)
                {
                    _okinoshimaSearchParameterError = value;
                    OnPropertyChanged(() => OkinoshimaSearchParameterError);
                }
            }
        }

        private int _minCond;
        /// <summary>
        /// 最小Cond値（なお1回の遠征で減るCondは3）
        /// </summary>
        public int MinCond
        {
            get
            {
                return _minCond;
            }

            set
            {
                if (_minCond != value)
                {
                    _minCond = value;
                    OnPropertyChanged(() => MinCond);
                    RemainExpeditionCount = (value >= 50) ? (value - 50) / 3 + 1 : 0;
                }
            }
        }

        private int _remainExpeditionCount;
        /// <summary>
        /// 同一編成キラ付けなしで全艦キラキラ遠征に出せる回数
        /// </summary>
        public int RemainExpeditionCount
        {
            get
            {
                return _remainExpeditionCount;
            }

            set
            {
                if (_remainExpeditionCount != value)
                {
                    _remainExpeditionCount = value;
                    OnPropertyChanged(() => RemainExpeditionCount);
                }
            }
        }


        private FleetExpeditionStatus _expeditionStatus;
        public FleetExpeditionStatus ExpeditionStatus
        {
            get
            {
                return _expeditionStatus;
            }

            set
            {
                if (_expeditionStatus != value)
                {
                    _expeditionStatus = value;
                    OnPropertyChanged(() => ExpeditionStatus);
                }
            }
        }


        private DateTime _expeditionBacktime;
        public DateTime ExpeditionBacktime
        {
            get
            {
                return _expeditionBacktime;
            }

            set
            {
                if (_expeditionBacktime != value)
                {
                    _expeditionBacktime = value;
                    OnPropertyChanged(() => ExpeditionBacktime);
                }
            }
        }

        private int _expeditionId;
        public int ExpeditionId
        {
            get
            {
                return _expeditionId;
            }

            set
            {
                if (_expeditionId != value)
                {
                    _expeditionId = value;
                    OnPropertyChanged(() => ExpeditionId);
                }
            }
        }

        private int _dockingShipsCount;
        public int DockingShipsCount
        {
            get
            {
                return _dockingShipsCount;
            }

            set
            {
                if (_dockingShipsCount != value)
                {
                    _dockingShipsCount = value;
                    OnPropertyChanged(() => DockingShipsCount);
                }
            }
        }


        private bool _hasTaihaShip;
        public bool HasTaihaShip
        {
            get
            {
                return _hasTaihaShip;
            }

            set
            {
                if (_hasTaihaShip != value)
                {
                    _hasTaihaShip = value;
                    OnPropertyChanged(() => HasTaihaShip);
                }
            }
        }


        private bool _hasUnsuppliedShip;
        public bool HasUnsuppliedShip
        {
            get
            {
                return _hasUnsuppliedShip;
            }

            set
            {
                if (_hasUnsuppliedShip != value)
                {
                    _hasUnsuppliedShip = value;
                    OnPropertyChanged(() => HasUnsuppliedShip);
                }
            }
        }



        #endregion

        public ObservableCollection<int> Ships = new ObservableCollection<int>();


        public void FromDeckValue(KanColleLib.TransmissionData.api_get_member.values.DeckValue data)
        {
            DeckName = data.name;

            for (var i = 0; i < data.ship.Length; i++)
            {
                if (data.ship[i] == -1)
                {
                    while (Ships.Count > i)
                        Ships.RemoveAt(i);
                    break;
                }
                else
                {
                    if (i < Ships.Count)
                    {
                        if (Ships[i] != data.ship[i])
                        {
                            Ships[i] = data.ship[i];
                        }
                    }
                    else
                    {
                        Ships.Add(data.ship[i]);
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
            var model = MainModel.Current.KancolleModel;

            var sumshiplevel = 0;
            var sumairmastery = 0;
            var drumcount = 0;
            var drumshipcount = 0;
            double okinoshimaparameter = 0;
            double okinoshimaerror = 0;
            int? mincond = null;
            for (var i = 0; i < Ships.Count; i++)
            {
                var ship = model.Shipdata.FirstOrDefault(_ => _.Shipid == Ships[i]);
                if (ship != null)
                {
                    sumshiplevel += ship.Level;
                    sumairmastery += ship.AirMastery;
                    var thisdrumcount = KanColleTools.ShipDrumCount(ship);
                    drumcount += thisdrumcount;
                    if (thisdrumcount > 0)
                        ++drumshipcount;
                    double thisokinoshimaparameter;
                    double thisokinoshimaerror;
                    KanColleTools.ShipOkinoshimaSearchParameter(ship, out thisokinoshimaparameter, out thisokinoshimaerror);
                    okinoshimaparameter += thisokinoshimaparameter;
                    okinoshimaerror += thisokinoshimaerror;

                }
                if (i == 0)
                    if (ship != null)
                        FlagShipLevel = ship.Level;
                    else
                        FlagShipLevel = 0;

                if (!mincond.HasValue || mincond > ship.Condition)
                    mincond = ship.Condition;
            }
            SumShipLevel = sumshiplevel;
            SumAirMastery = sumairmastery;
            DrumCount = drumcount;
            DrumShipCount = drumshipcount;
            OkinoshimaSearchParameter = okinoshimaparameter - 0.6142467 * (int)((model.Basicdata.AdmiralLevel + 4) / 5) * 5;
            OkinoshimaSearchParameterError = okinoshimaerror + 0.03692224 * (int)((model.Basicdata.AdmiralLevel + 4) / 5) * 5;

            MinCond = mincond ?? 0;

            ChangeNDockStatus();
            ChangeSupplyStatus();
        }

        public void ChangeNDockStatus()
        {
            var model = MainModel.Current.KancolleModel;
            DockingShipsCount = model.Ndockdata.Count(_ => Ships.Contains(_.Shipid));

            var hastaihaship = false;
            for (var i = 0; i < Ships.Count; i++)
            {
                var ship = model.Shipdata.FirstOrDefault(_ => _.Shipid == Ships[i]);
                if (ship != null && ship.HpNow <= ship.HpMax * 0.25)
                {
                    hastaihaship = true;
                    break;
                }
            }

            HasTaihaShip = hastaihaship;
        }

        public void ChangeMissionStatus(int status, int id, long time)
        {
            ExpeditionStatus =
                status == 0 ? FleetExpeditionStatus.AtHome :
                status == 1 ? FleetExpeditionStatus.OnExpedition :
                status == 2 ? FleetExpeditionStatus.ExpeditionComplete :
                status == 3 ? FleetExpeditionStatus.ForceBacking :
                FleetExpeditionStatus.Unknown;
            ExpeditionId = id;
            ExpeditionBacktime = Miotsukushi.Tools.TimeParser.ParseTimeFromLong(time);
        }

        public void ChangeSupplyStatus()
        {
            var model = MainModel.Current.KancolleModel;
            var hasunsuppliedship = false;

            for (var i = 0; i < Ships.Count; i++)
            {
                var ship = model.Shipdata.FirstOrDefault(_ => _.Shipid == Ships[i]);
                if (ship != null)
                {
                    if (ship.Characterinfo != null)
                        hasunsuppliedship = hasunsuppliedship || ship.Fuel != ship.Characterinfo.FuelMax || ship.Ammo != ship.Characterinfo.AmmoMax;
                }
            }

            HasUnsuppliedShip = hasunsuppliedship;
        }
    }
}
