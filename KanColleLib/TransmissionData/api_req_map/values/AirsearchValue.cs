using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class AirsearchValue
    {
        public int plane_type;
        public int result;

        public static AirsearchValue fromDynamic(dynamic json)
        {
            return new AirsearchValue()
            {
                plane_type = (int)json.api_plane_type,
                result = (int)json.api_result
            };
        }
    }
}
