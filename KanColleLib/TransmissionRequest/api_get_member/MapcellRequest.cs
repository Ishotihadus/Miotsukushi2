using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_get_member
{
    public class MapcellRequest : RequestBase
    {
        public int maparea_id;
        public int mapinfo_no;

        public MapcellRequest(string request)
            : base(request)
        {
            maparea_id = int.Parse(_Get_Reqest("api_maparea_id"));
            mapinfo_no = int.Parse(_Get_Reqest("api_mapinfo_no"));
        }


    }
}
