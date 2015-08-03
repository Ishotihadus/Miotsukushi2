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

        public static KoukuStage2 fromDynamic(dynamic json)
        {
            return new KoukuStage2()
            {
                f_count = (int)json.api_f_count,
                f_lostcount = (int)json.api_f_lostcount,
                e_count = (int)json.api_e_count,
                e_lostcount = (int)json.api_e_lostcount,
                air_fire = json.api_air_fire() ? KoukuStage2AirFire.fromDynamic(json.api_air_fire) : null
            };
        }
    }
}
