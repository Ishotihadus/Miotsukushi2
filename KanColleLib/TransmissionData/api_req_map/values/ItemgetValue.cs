using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class ItemgetValue
    {
        public int usemst;
        public int id;
        public int getcount;
        public string name;
        public int icon_id;

        public static ItemgetValue fromDynamic(dynamic json)
        {
            return new ItemgetValue()
            {
                usemst = (int)json.api_usemst,
                id = (int)json.api_id,
                getcount = (int)json.api_getcount,
                name = json.api_name as string,
                icon_id = (int)json.api_icon_id
            };
        }
    }
}
