using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class NDock
    {
        public List<NDockValue> ndocks;

        public static NDock fromDynamic(dynamic json)
        {
            NDock ndock = new NDock();
            ndock.ndocks = new List<NDockValue>();
            foreach (var data in json)
                ndock.ndocks.Add(NDockValue.fromDynamic(data));
            return ndock;
        }
    }
}
