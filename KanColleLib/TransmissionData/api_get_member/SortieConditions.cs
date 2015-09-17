using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class SortieConditions
    {
        public WarValue war;

        public static SortieConditions fromDynamic(dynamic json)
        {
            return new SortieConditions()
            {
                war = WarValue.fromDynamic(json.api_war)
            };
        }
    }
}
