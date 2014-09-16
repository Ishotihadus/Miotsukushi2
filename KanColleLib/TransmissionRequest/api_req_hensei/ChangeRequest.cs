using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class ChangeRequest : RequestBase
    {
        /// <summary>
        /// 艦隊番号（1始まり）
        /// </summary>
        public int id;

        /// <summary>
        /// 変更後の艦娘ID
        /// -2ならば旗艦以外全部解除
        /// -1ならばその艦をはずす
        /// </summary>
        public int ship_id;
        
        /// <summary>
        /// インデックス（0始まり）
        /// </summary>
        public int ship_idx;

        public ChangeRequest(string request)
            : base(request)
        {
            id = (int)_Get_Request_int("api_id");
            ship_id = (int)_Get_Request_int("api_ship_id");
            ship_idx = (int)_Get_Request_int("api_ship_idx");
        }

    }
}
