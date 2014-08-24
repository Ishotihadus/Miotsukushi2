using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_hensei
{
    public class LockRequest : RequestBase
    {
        public int ship_id;

        public LockRequest(string request)
            : base(request)
        {
            ship_id = (int)_Get_Reqest_int("api_ship_id");
        }
    }
}
