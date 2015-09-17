using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class MissionValue
    {
        /// <summary>
        /// 遠征ID
        /// </summary>
        public int mission_id;

        /// <summary>
        /// 遠征の状態
        /// 0:行ったことがない1:成功したことがない（行ったことはある） 2:成功したことがある
        /// </summary>
        public int state;

        public static MissionValue fromDynamic(dynamic json)
        {
            var mission = new MissionValue();

            mission.mission_id = (int)json.api_mission_id;
            mission.state = (int)json.api_state;

            return mission;
        }

        // {"api_mission_id":126,"api_state":1}
    }
}
