using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class NDockData
    {
        public NDockStatus Status;

        public int Shipid;

        public DateTime CompleteTime;

        public void FromNDockValue(KanColleLib.TransmissionData.api_get_member.values.NDockValue data)
        {
            var changed = false;

            NDockStatus _status;

            switch (data.state)
            {
                case -1:
                    _status = NDockStatus.Locked;
                    break;
                case 0:
                    _status = NDockStatus.Empty;
                    break;
                case 1:
                    _status = NDockStatus.Docking;
                    break;
                default:
                    _status = NDockStatus.Unknown;
                    break;
            }

            if (_status != Status)
            {
                Status = _status;
                changed = true;
            }

            var _shipid = data.ship_id;
            if (_shipid != Shipid)
            {
                Shipid = _shipid;
                changed = true;
            }

            var compTime = Tools.TimeParser.ParseTimeFromLong(data.complete_time);
            if (compTime != CompleteTime)
            {
                CompleteTime = compTime;
                changed = true;
            }

            if (changed)
                OnNDockChanged(new System.EventArgs());
        }

        /// <summary>
        /// 入渠ドックの情報が変更された際に呼び出されます
        /// </summary>
        public event EventHandler NDockChanged;
        protected virtual void OnNDockChanged(System.EventArgs e) { if (NDockChanged != null) { NDockChanged(this, e); } }
    }
}
