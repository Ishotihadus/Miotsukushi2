using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Mission
    {
        public List<MissionValue> missions;

        public static Mission fromDynamic(dynamic json)
        {
            Mission mission = new Mission();
            mission.missions = new List<MissionValue>();
            foreach (var data in json)
                mission.missions.Add(MissionValue.fromDynamic(data));
            return mission;
        }
    }
}
