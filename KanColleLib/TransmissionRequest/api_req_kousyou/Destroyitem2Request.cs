using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_kousyou
{
    public class Destroyitem2Request : RequestBase
    {
        public int[] slotitem_ids;

        public Destroyitem2Request(string request)
            : base(request)
        {
            slotitem_ids = _Get_Request_int_array("api_slotitem_ids").Select(_ => _.HasValue ? _.Value : 0).ToArray();
        }
    }
}
