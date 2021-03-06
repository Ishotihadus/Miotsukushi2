﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class KoukuStage1
    {
        // {"api_f_count":96,"api_f_lostcount":1,"api_e_count":0,"api_e_lostcount":0,"api_disp_seiku":1,"api_touch_plane":[59,-1]}

        /// <summary>
        /// 自艦隊の飛ばした航空機数
        /// </summary>
        public int f_count;

        /// <summary>
        /// 撃墜された航空機数
        /// </summary>
        public int f_lostcount;

        /// <summary>
        /// 敵艦隊の飛ばした航空機数
        /// </summary>
        public int e_count;

        /// <summary>
        /// 撃墜した航空機数
        /// </summary>
        public int e_lostcount;

        /// <summary>
        /// 制空状態（0:均衡　1:確保　2:優勢　3:劣勢　4:喪失）
        /// </summary>
        public int disp_seiku;

        /// <summary>
        /// 自艦隊、敵艦隊がそれぞれ触接に用いた艦載機の種類
        /// </summary>
        public int[] touch_plane;

        public static KoukuStage1 fromDynamic(dynamic json)
        {
            return new KoukuStage1()
            {
                f_count = (int)json.api_f_count,
                f_lostcount = (int)json.api_f_lostcount,
                e_count = (int)json.api_e_count,
                e_lostcount = (int)json.api_e_lostcount,
                disp_seiku = (int)json.api_disp_seiku,
                touch_plane = json.api_touch_plane.Deserialize<int[]>()
            };
        }
    }
}
