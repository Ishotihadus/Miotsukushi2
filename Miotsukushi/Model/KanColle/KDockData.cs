using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class KDockData : NotifyModelBase
    {
        private KDockStatus _status = KDockStatus.Unknown;
        public KDockStatus Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        private int _charaid;
        public int Charaid
        {
            get
            {
                return _charaid;
            }

            set
            {
                if (_charaid != value)
                {
                    _charaid = value;
                    OnPropertyChanged(() => Charaid);
                }
            }
        }

        private DateTime _completeTime;
        public DateTime CompleteTime
        {
            get
            {
                return _completeTime;
            }

            set
            {
                if (_completeTime != value)
                {
                    _completeTime = value;
                    OnPropertyChanged(() => CompleteTime);
                }
            }
        }

        public void FromKDockValue(KanColleLib.TransmissionData.api_get_member.values.KDockValue data)
        {
            switch (data.state)
            {
                case -1:
                    Status = KDockStatus.Locked;
                    break;
                case 0:
                    Status = KDockStatus.Empty;
                    break;
                case 2:
                    Status = KDockStatus.Building;
                    break;
                case 3:
                    Status = KDockStatus.Complete;
                    break;
                default:
                    Status = KDockStatus.Unknown;
                    break;
            
            }

            Charaid = data.created_ship_id;

            CompleteTime = Tools.TimeParser.ParseTimeFromLong(data.complete_time);
        }

        public void GetShip()
        {
            Status = KDockStatus.Empty;
            Charaid = 0;
            CompleteTime = new DateTime();
        }
    }
}
