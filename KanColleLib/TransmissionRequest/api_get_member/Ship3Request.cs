using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_get_member
{
    public class Ship3Request : RequestBase
    {
        public int? shipid;
        public int sort_key;
        public int sort_order;

        public Ship3Request(string request)
            : base(request)
        {
            shipid = _Get_Request("api_shipid") != null ? int.Parse(_Get_Request("api_shipid")) : (int?)null;
            sort_key = int.Parse(_Get_Request("api_sort_key"));
            sort_order = int.Parse(_Get_Request("spi_sort_order"));
        }
    }
}
