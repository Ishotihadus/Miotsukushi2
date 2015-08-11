using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_kaisou
{
    public class RemodelingRequest : RequestBase
    {
        /// <summary>
        /// 改造する艦
        /// </summary>
        public int id;

        public RemodelingRequest(string request)
            :base (request)
        {
            id = (int)_Get_Request_int("api_id");
        }
    }
}
