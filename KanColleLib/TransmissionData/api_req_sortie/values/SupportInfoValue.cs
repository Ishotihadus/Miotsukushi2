using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class SupportInfoValue
    {
        // http://jbbs.shitaraba.net/bbs/read_archive.cgi/netgame/12394/1389318021/837-853

        /// <summary>
        /// 航空支援
        /// </summary>
        public SupportAiratackValue support_airatack;

        /// <summary>
        /// 砲撃・雷撃支援
        /// </summary>
        public SupportHouraiValue support_hourai;
    }
}
