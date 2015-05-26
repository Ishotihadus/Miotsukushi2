using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_map.values;

namespace KanColleLib.TransmissionData.api_req_map
{
    public class Next
    {
        public NextCellData next_cell_data;

        public static Next fromDynamic(dynamic json)
        {
            return new Next()
            {
                next_cell_data = NextCellData.fromDynamic(json)
            };
        }
    }
}
