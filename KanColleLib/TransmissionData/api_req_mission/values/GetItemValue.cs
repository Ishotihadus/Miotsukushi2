using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_mission.values
{
    public class GetItemValue
    {
        public int useitem_id;
        public string useitem_name;
        public int useitem_count;

        public static GetItemValue fromDynamic(dynamic json)
        {
            GetItemValue getitem = new GetItemValue();

            getitem.useitem_id = (int)json.api_useitem_id;
            getitem.useitem_name = json.api_useitem_name as string;
            getitem.useitem_count = (int)json.api_useitem_count;

            return getitem;
        }

        // {"api_useitem_id":10,"api_useitem_name":"\u5bb6\u5177\u7bb1\uff08\u5c0f\uff09","api_useitem_count":1}
    }
}
