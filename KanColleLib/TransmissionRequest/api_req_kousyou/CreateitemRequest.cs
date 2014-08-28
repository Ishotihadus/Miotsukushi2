using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_kousyou
{
    public class CreateitemRequest : RequestBase
    {
        /// <summary>
        /// 燃料
        /// </summary>
        public int item1;

        /// <summary>
        /// 弾薬
        /// </summary>
        public int item2;

        /// <summary>
        /// 鋼材
        /// </summary>
        public int item3;

        /// <summary>
        /// ボーキ
        /// </summary>
        public int item4;

        public CreateitemRequest(string request)
            : base(request)
        {
            item1 = (int)_Get_Request_int("api_item1");
            item2 = (int)_Get_Request_int("api_item2");
            item3 = (int)_Get_Request_int("api_item3");
            item4 = (int)_Get_Request_int("api_item4");
        }
    }
}
