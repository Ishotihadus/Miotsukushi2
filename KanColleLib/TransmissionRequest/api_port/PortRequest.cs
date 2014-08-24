using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_port
{
    public class PortRequest : RequestBase
    {
        public string port;
        public int sort_key;
        public int sort_order;

        public PortRequest(string request)
            : base(request)
        {
            port = _Get_Request("api_port");
            sort_key = (int)_Get_Request_int("api_sort_key");
            sort_order = (int)_Get_Request_int("spi_sort_order");
        }
    }
}
