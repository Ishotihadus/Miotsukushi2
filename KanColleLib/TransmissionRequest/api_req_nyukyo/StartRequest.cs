using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_nyukyo
{
    public class StartRequest : RequestBase
    {
        /// <summary>
        /// 艦娘ID
        /// </summary>
        public int ship_id;

        /// <summary>
        /// 入渠ドック番号（1つめなら1）
        /// </summary>
        public int ndock_id;

        /// <summary>
        /// バケツを使用するか
        /// </summary>
        public bool highspeed;

        public StartRequest(string request)
            : base(request)
        {
            ship_id = (int)_Get_Request_int("api_ship_id");
            ndock_id = (int)_Get_Request_int("api_ndock_id");
            highspeed = _Get_Request_int("api_highspeed") == 1;
        }
    }
}
