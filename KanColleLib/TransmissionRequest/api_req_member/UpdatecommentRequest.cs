using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_member
{
    public class UpdatecommentRequest : RequestBase
    {
        public string cmt_id;
        public string cmt;

        public UpdatecommentRequest(string request)
            : base(request)
        {
            cmt = _Get_Request("api_cmt");
            cmt_id = _Get_Request("api_cmt_id");
        }
    }
}
