using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class CombinedRequest : RequestBase
    {
        /// <summary>
        /// 0: 連合艦隊解除　1: 機動部隊　2: 水上部隊
        /// </summary>
        public int combined_type;

        public CombinedRequest(string request)
            : base(request)
        {
            combined_type = _Get_Request_int("api_combined_type").Value;
        }

    }
}
