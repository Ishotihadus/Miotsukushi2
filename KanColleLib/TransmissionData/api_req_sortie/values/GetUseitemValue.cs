using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class GetUseitemValue
    {
        public int useitem_id;

        public static GetUseitemValue fromDynamic(dynamic json)
        {
            return new GetUseitemValue()
            {
                useitem_id = (int)json.api_useitem_id
            };
        }
    }
}
