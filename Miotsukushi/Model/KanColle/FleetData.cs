using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class FleetData
    {
        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnFleetNameChanged(new EventArgs());
                }
            }
        }
        List<int> ships = new List<int>();

        public int this[int i] { get { return ships[i]; } }
        public int Count { get { return ships.Count; } }

        public FleetExpeditionStatus expedition_status;
        public DateTime expedition_backtime;
        public int expedition_id;

        /// <summary>
        /// 艦隊名が変更されたときに呼び出されます
        /// </summary>
        public event EventHandler FleetNameChanged;
        protected virtual void OnFleetNameChanged(EventArgs e) { if (FleetNameChanged != null) { FleetNameChanged(this, e); } }

        /// <summary>
        /// 艦隊の編成が変更されたときに呼び出されます
        /// </summary>
        public event EventHandler FleetChanged;
        protected virtual void OnFleetChanged(EventArgs e) { if (FleetChanged != null) { FleetChanged(this, e); } }

        /// <summary>
        /// 遠征の情報が変更されたときに呼び出されます
        /// </summary>
        public event EventHandler FleetMissionChanged;
        protected virtual void OnFleetMissionChanged(EventArgs e) { if (FleetMissionChanged != null) { FleetMissionChanged(this, e); } }

        public void FromDeckValue(KanColleLib.TransmissionData.api_get_member.values.DeckValue data)
        {
            name = data.name;

            bool shipchanged = false;
            for (int i = 0; i < data.ship.Length; i++)
            {
                if (data.ship[i] == -1)
                    break;
                if (i < ships.Count)
                {
                    if (ships[i] != data.ship[i])
                    {
                        ships[i] = data.ship[i];
                        shipchanged = true;
                    }
                }
                else
                {
                    ships.Add(data.ship[i]);
                    shipchanged = true;
                }
            }
            if (shipchanged)
                OnFleetChanged(new EventArgs());

            if (data.mission.Length >= 3)
                ChangeMissionStatus((int)data.mission[0], (int)data.mission[1], data.mission[2]);
            else
                ChangeMissionStatus(-1, -1, -1);
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
                OnFleetMissionChanged(new EventArgs());
        }
    }
}
