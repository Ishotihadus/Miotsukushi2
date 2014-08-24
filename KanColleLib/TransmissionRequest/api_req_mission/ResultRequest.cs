using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_mission
{
    public class ResultRequest : RequestBase
    {
        public int deck_id;

        public ResultRequest(string request)
            : base(request)
        {
            deck_id = (int)_Get_Request_int("api_deck_id");
        }
    }
}
