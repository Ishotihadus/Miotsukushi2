using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Useitem
    {
        public List<UseitemValue> useitems;

        public static Useitem fromDynamic(dynamic json)
        {
            var ret = new Useitem();
            ret.useitems = new List<UseitemValue>();
            foreach (var data in json)
                ret.useitems.Add(UseitemValue.fromDynamic(data));
            return ret;
        }
    }
}
