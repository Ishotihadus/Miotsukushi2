using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_map
{
    public class StartRequest : RequestBase
    {
        /// <summary>
        /// 海域ID
        /// </summary>
        public int maparea_id;

        /// <summary>
        /// マップID
        /// </summary>
        public int mapinfo_no;

        /// <summary>
        /// 艦隊番号（第1艦隊なら1）
        /// </summary>
        public int deck_id;

        /// <summary>
        /// 1固定
        /// </summary>
        public int formation_id;

        public StartRequest(string request) : base(request)
        {
            maparea_id = _Get_Request_int("api_maparea_id").Value;
            mapinfo_no = _Get_Request_int("api_mapinfo_no").Value;
            deck_id = _Get_Request_int("api_deck_id").Value;
            formation_id = _Get_Request_int("api_formation_id").Value;
        }
    }
}
