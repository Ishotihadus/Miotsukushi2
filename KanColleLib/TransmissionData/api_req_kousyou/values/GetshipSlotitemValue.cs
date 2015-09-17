using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_kousyou.values
{
    public class GetshipSlotitemValue
    {
        /// <summary>
        /// スロットID
        /// </summary>
        public int id;

        /// <summary>
        /// 装備アイテムID
        /// </summary>
        public int slotitem_id;

        public static GetshipSlotitemValue fromDynamic(dynamic json)
        {
            var slotitem = new GetshipSlotitemValue();
            slotitem.id = (int)json.api_id;
            slotitem.slotitem_id = (int)json.api_slotitem_id;
            return slotitem;
        }
    }
}
