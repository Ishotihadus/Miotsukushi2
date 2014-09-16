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
        public KDockStatus status
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
                    OnPropertyChanged(() => status);
                }
            }
        }

        private int _charaid;
        public int charaid
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
                    OnPropertyChanged(() => charaid);
                }
            }
        }

        private DateTime _complete_time;
        public DateTime complete_time
        {
            get
            {
                return _complete_time;
            }

            set
            {
                if (_complete_time != value)
                {
                    _complete_time = value;
                    OnPropertyChanged(() => complete_time);
                }
            }
        }

        public void FromKDockValue(KanColleLib.TransmissionData.api_get_member.values.KDockValue data)
        {
            switch (data.state)
            {
                case -1:
                    status = KDockStatus.Locked;
                    break;
                case 0:
                    status = KDockStatus.Empty;
                    break;
                case 2:
                    status = KDockStatus.Building;
                    break;
                case 3:
                    status = KDockStatus.Complete;
                    break;
                default:
                    status = KDockStatus.Unknown;
                    break;
            
            }

            charaid = data.created_ship_id;

            complete_time = Tools.TimeParser.ParseTimeFromLong(data.complete_time);
        }

        public void GetShip()
        {
            status = KDockStatus.Empty;
            charaid = 0;
            complete_time = new DateTime();
        }
    }
}
