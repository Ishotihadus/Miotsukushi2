using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Mapcell
    {
        public List<MapcellValue> mapcells;

        public static Mapcell fromDynamic(dynamic json)
        {
            Mapcell mapcell = new Mapcell();

            mapcell.mapcells = new List<MapcellValue>();
            foreach (var data in json)
                mapcell.mapcells.Add(MapcellValue.fromDynamic(data));

            return mapcell;
        }
    }
}
