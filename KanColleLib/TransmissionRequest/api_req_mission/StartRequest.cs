using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest.api_req_mission
{
    public class StartRequest : RequestBase
    {
        /// <summary>
        /// BOT撃滅用パラメータ
        /// </summary>
        public string mission;

        /// <summary>
        /// 遠征ID
        /// </summary>
        public int mission_id;

        /// <summary>
        /// 艦隊番号（第2艦隊なら2）
        /// </summary>
        public int deck_id;

        public StartRequest(string request)
            : base(request)
        {
            mission = _Get_Request("api_mission");
            mission_id = (int)_Get_Request_int("api_mission_id");
            deck_id = (int)_Get_Request_int("api_deck_id");
        }
    }
}
