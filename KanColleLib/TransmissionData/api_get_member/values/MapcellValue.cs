using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class MapcellValue
    {
        // {"api_id":1284,"api_passed":0}

        /// <summary>
        /// セルID
        /// </summary>
        public int id;

        /// <summary>
        /// セルを通過したことがあるか
        /// </summary>
        public bool passed;

        public static MapcellValue fromDynamic(dynamic json)
        {
            var mapcell = new MapcellValue();

            mapcell.id = (int)json.api_id;
            mapcell.passed = (int)json.api_passed == 1;

            return mapcell;
        }
    }
}
