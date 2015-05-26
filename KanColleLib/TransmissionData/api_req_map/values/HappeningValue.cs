using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class HappeningValue
    {
        public int type;
        public int count;
        public int usemst;
        public int mst_id;
        public bool dentan;

        public static HappeningValue fromDynamic(dynamic json)
        {
            return new HappeningValue()
            {
                type = (int)json.api_type,
                count = (int)json.api_count,
                usemst = (int)json.api_usemst,
                mst_id = (int)json.api_mst_id,
                dentan = (int)json.api_dentan == 1
            };
        }
    }
}
