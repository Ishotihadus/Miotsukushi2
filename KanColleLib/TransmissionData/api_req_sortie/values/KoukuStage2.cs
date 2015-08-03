using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class KoukuStage2
    {
        // "api_stage2":{"api_f_count":30,"api_f_lostcount":1,"api_e_count":0,"api_e_lostcount":0}
        public int f_count;
        public int f_lostcount;
        public int e_count;
        public int e_lostcount;

        /// <summary>
        /// 対空砲火の情報
        /// </summary>
        public KoukuStage2AirFire air_fire;
    }
}
