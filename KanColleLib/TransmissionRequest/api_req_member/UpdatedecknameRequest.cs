using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_member
{
    public class UpdatedecknameRequest : RequestBase
    {
        /// <summary>
        /// 艦隊番号（第3艦隊なら3）
        /// </summary>
        public int deck_id;

        /// <summary>
        /// 艦隊名ID
        /// </summary>
        public string name_id;

        /// <summary>
        /// 変更後の艦隊名
        /// </summary>
        public string name;

        public UpdatedecknameRequest(string request)
            : base(request)
        {
            deck_id = (int)_Get_Request_int("api_deck_id");
            name_id = _Get_Request("api_name_id");
            name = _Get_Request("api_name");
        }
    }
}
