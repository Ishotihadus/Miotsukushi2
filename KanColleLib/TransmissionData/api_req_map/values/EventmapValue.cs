using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class EventmapValue
    {
        /// <summary>
        /// マップボス最大HP
        /// </summary>
        public int max_maphp;

        /// <summary>
        /// マップボス現在HP
        /// </summary>
        public int now_maphp;

        /// <summary>
        /// 倍率係数（実際のボスのHP×dmgが上のHPに対応する）
        /// http://jbbs.shitaraba.net/bbs/read_archive.cgi/netgame/12394/1379584776/ の >>903
        /// </summary>
        public int dmg;

        public static EventmapValue fromDynamic(dynamic json)
        {
            return new EventmapValue()
            {
                max_maphp = (int)json.api_max_maphp,
                now_maphp = (int)json.api_now_maphp,
                dmg = (int)json.api_dmg
            };
        }
    }
}
