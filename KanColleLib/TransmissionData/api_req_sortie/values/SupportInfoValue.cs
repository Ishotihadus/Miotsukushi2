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

        public static SupportInfoValue fromDynamic(dynamic json)
        {
            return new SupportInfoValue()
            {
                support_airatack = json.api_support_airatack() && json.api_support_airatack != null ? SupportAiratackValue.fromDynamic(json.api_support_airatack) : null,
                support_hourai = json.api_support_hourai() && json.api_support_hourai != null ? SupportHouraiValue.fromDynamic(json.api_support_hourai) : null
            };
        }
    }
}
