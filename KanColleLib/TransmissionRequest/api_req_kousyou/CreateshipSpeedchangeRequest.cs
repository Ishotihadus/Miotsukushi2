using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_kousyou
{
    public class CreateshipSpeedchangeRequest : RequestBase
    {
        /// <summary>
        /// バーナーを使うかどうかのフラグ
        /// 常にtrue
        /// </summary>
        public bool highspeed;

        /// <summary>
        /// 工廠ドックの番号
        /// </summary>
        public int kdock_id;

        public CreateshipSpeedchangeRequest(string request)
            : base(request)
        {
            highspeed = _Get_Request_int("api_highspeed") == 1;
            kdock_id = (int)_Get_Request_int("api_kdock_id");
        }
    }
}
