using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class SlotItemValue
    {
        /// <summary>
        /// スロットID
        /// </summary>
        public int id;

        /// <summary>
        /// 装備アイテムID
        /// </summary>
        public int slotitem_id;

        /// <summary>
        /// 装備がロックされているか
        /// </summary>
        public bool locked;

        /// <summary>
        /// 改修レベル
        /// </summary>
        public int level;

        /// <summary>
        /// 艦載機熟練度
        /// </summary>
        public int? alv;

        public static SlotItemValue fromDynamic(dynamic json)
        {
            SlotItemValue slotitem = new SlotItemValue();

            slotitem.id = (int)json.api_id;
            slotitem.slotitem_id = (int)json.api_slotitem_id;
            slotitem.locked = (int)json.api_locked == 1;
            slotitem.level = (int)json.api_level;
            slotitem.alv = json.api_alv() ? (int)json.api_alv : (int?)null;

            return slotitem;
        }

        // {"api_id":1,"api_slotitem_id":42,"api_locked":0}
    }
}
