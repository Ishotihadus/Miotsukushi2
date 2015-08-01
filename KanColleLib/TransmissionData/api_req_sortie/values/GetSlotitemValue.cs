using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class GetSlotitemValue
    {
        public int slotitem_id;

        public static GetSlotitemValue fromDynamic(dynamic json)
        {
            return new GetSlotitemValue()
            {
                slotitem_id = (int)json.api_slotitem_id
            };
        }
    }
}
