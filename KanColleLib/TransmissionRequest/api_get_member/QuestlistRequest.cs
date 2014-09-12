using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_get_member
{
    public class QuestlistRequest : RequestBase
    {
        public int page_no;

        public QuestlistRequest(string request)
            : base(request)
        {
            page_no = int.Parse(_Get_Request("api_page_no"));
        }
    }
}
