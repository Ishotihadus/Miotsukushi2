using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map
{
    public class Next : values.NextCellData
    {
        public static Next fromDynamic(dynamic json)
        {
            return _fromDynamic(json);
        }
    }
}
