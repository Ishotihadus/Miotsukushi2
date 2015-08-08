using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_member
{
    public class GetIncentive
    {
        /// <summary>
        /// もらえる褒章の個数
        /// </summary>
        public int count;

        // api_item があるという噂

        public static GetIncentive fromDynamic(dynamic json)
        {
            return new GetIncentive() { count = (int)json.api_count };
        }
    }
}
