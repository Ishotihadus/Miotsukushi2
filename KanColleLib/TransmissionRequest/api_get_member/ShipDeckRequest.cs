using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_get_member
{
    public class ShipDeckRequest : RequestBase
    {
        public int?[] deck_rid;

        public ShipDeckRequest(string request)
            :base(request)
        {
            deck_rid = _Get_Request_int_array("api_deck_rid");
        }
    }
}
