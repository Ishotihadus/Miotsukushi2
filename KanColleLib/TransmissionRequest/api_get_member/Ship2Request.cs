using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_get_member
{
    public class Ship2Request : RequestBase
    {
        public int sort_key;
        public int sort_order;

        public Ship2Request(string request)
            : base(request)
        {
            sort_key = int.Parse(_Get_Reqest("api_sort_key"));
            sort_order = int.Parse(_Get_Reqest("spi_sort_order"));
        }
    }
}
