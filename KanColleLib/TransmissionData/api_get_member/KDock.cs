using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class KDock
    {
        public List<KDockValue> kdocks;

        public static KDock fromDynamic(dynamic json)
        {
            var kdock = new KDock();

            kdock.kdocks = new List<KDockValue>();
            foreach (var data in json)
                kdock.kdocks.Add(KDockValue.fromDynamic(data));

            return kdock;
        }
    }
}
