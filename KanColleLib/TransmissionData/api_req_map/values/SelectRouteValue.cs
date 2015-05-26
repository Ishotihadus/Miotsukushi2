using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class SelectRouteValue
    {
        public int[] select_cells;

        public static SelectRouteValue fromDynamic(dynamic json)
        {
            return new SelectRouteValue()
            {
                select_cells = json.api_select_cells.Deserialize<int[]>()
            };
        }
    }
}
