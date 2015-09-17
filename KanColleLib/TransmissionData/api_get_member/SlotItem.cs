using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class SlotItem
    {
        public List<SlotItemValue> slotitems;

        public static SlotItem fromDynamic(dynamic json)
        {
            var slotitem = new SlotItem();
            slotitem.slotitems = new List<SlotItemValue>();
            foreach (var data in json)
                slotitem.slotitems.Add(SlotItemValue.fromDynamic(data));
            return slotitem;
        }
    }
}
