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
        /// 不明
        /// </summary>
        public int count;

        public GetIncentive fromDynamic(dynamic json)
        {
            return new GetIncentive() { count = (int)json.api_count };
        }
    }
}
