using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_mission
{
    public class ReturnInstruction
    {
        public long[] mission;

        public static ReturnInstruction fromDynamic(dynamic json)
        {
            ReturnInstruction returninstruction = new ReturnInstruction();
            returninstruction.mission = json.api_mission.Deserialize<long[]>();
            return returninstruction;
        }
    }
}
