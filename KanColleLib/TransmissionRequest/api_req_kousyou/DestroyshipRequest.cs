using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_kousyou
{
    public class DestroyshipRequest : RequestBase
    {
        /// <summary>
        /// 解体する艦の番号
        /// </summary>
        public int ship_id;

        public DestroyshipRequest(string request)
            : base(request)
        {
            ship_id = (int)_Get_Request_int("api_ship_id");
        }
    }
}
