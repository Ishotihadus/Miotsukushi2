using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Mapinfo
    {
        public List<MapinfoValue> mapinfos;

        public static Mapinfo fromDynamic(dynamic json)
        {
            Mapinfo mapinfo = new Mapinfo();

            mapinfo.mapinfos = new List<MapinfoValue>();
            foreach (var data in json)
                mapinfo.mapinfos.Add(MapinfoValue.fromDynamic(data));

            return mapinfo;
        }
    }
}
