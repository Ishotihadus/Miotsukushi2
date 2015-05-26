using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class ItemgetEoValue
    {
        public int usemst;
        public int id;
        public int getcount;

        public static ItemgetEoValue fromDynamic(dynamic json)
        {
            ItemgetEoValue ret = new ItemgetEoValue();
            ret.usemst = (int)json.api_usemst;
            ret.id = (int)json.api_id;
            ret.getcount = (int)json.api_getcount;
            return ret;
        }
    }
}
