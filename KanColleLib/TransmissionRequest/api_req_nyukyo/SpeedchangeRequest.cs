using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_nyukyo
{
    public class SpeedchangeRequest : RequestBase
    {
        /// <summary>
        /// 入渠ドック番号（1つめなら1）
        /// </summary>
        public int ndock_id;

        public SpeedchangeRequest(string request)
            : base(request)
        {
            ndock_id = (int)_Get_Request_int("api_ndock_id");
        }
    }
}
