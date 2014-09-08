using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class KDockData
    {
        public KDockStatus status = KDockStatus.unknown;

        public int shipid;

        public DateTime complete_time;

        public void FromKDockValue(KanColleLib.TransmissionData.api_get_member.values.KDockValue data)
        {
            bool changed = false;

            KDockStatus _status;
            switch (data.state)
            {
                case -1:
                    _status = KDockStatus.locked;
                    break;
                case 0:
                    _status = KDockStatus.empty;
                    break;
                case 2:
                    _status = KDockStatus.building;
                    break;
                case 3:
                    _status = KDockStatus.complete;
                    break;
                default:
                    _status = KDockStatus.unknown;
                    break;
            
            }
            if (_status != status)
            {
                status = _status;
                changed = true;
            }

            int _shipid = data.created_ship_id;
            if (_shipid != shipid)
            {
                shipid = _shipid;
                changed = true;
            }

            var _complete_time = Tools.TimeParser.ParseTimeFromLong(data.complete_time);
            if (_complete_time != complete_time)
            {
                complete_time = _complete_time;
                changed = true;
            }

            if (changed)
                OnKDockChanged(new EventArgs());
        }

        public void GetShip()
        {
            status = KDockStatus.empty;
            shipid = 0;
            complete_time = new DateTime();
            OnKDockChanged(new EventArgs());
        }

        /// <summary>
        /// 建造ドックの情報が変更された際に呼び出されます
        /// </summary>
        public event EventHandler KDockChanged;
        protected virtual void OnKDockChanged(EventArgs e) { if (KDockChanged != null) { KDockChanged(this, e); } }
    }
}
