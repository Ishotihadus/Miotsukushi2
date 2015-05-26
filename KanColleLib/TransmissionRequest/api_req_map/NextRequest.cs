using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_map
{
    public class NextRequest : RequestBase
    {
        public int recovery_type;

        /// <summary>
        /// 能動分岐時の選択
        /// </summary>
        public int? cell_id;

        public NextRequest(string request) : base(request)
        {
            recovery_type = _Get_Request_int("api_recovery_type").Value;
            cell_id = _Get_Request_int("api_cell_id");
        }
    }
}
