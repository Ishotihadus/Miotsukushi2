using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class ChangeRequest : RequestBase
    {
        public int id;
        public int ship_id;
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
