using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_kousyou.values
{
    public class CreateitemValue
    {
        /// <summary>
        /// スロットID
        /// </summary>
        public int id;

        /// <summary>
        /// 装備アイテムID
        /// </summary>
        public int slotitem_id;

        public static CreateitemValue fromDynamic(dynamic json)
        {
            CreateitemValue createitem = new CreateitemValue();
            createitem.id = (int)json.api_id;
            createitem.slotitem_id = (int)json.api_slotitem_id;
            return createitem;
        }
    }
}
