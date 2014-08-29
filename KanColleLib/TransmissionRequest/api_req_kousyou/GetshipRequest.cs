using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_kousyou
{
    public class GetshipRequest : RequestBase
    {
        public int kdock_id;

        public GetshipRequest(string request)
            : base(request)
        {
            kdock_id = (int)_Get_Request_int("api_kdock_id");
        }
    }
}
