using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class KoukuValue
    {
        // "api_kouku":{"api_plane_from":[[5,6],[-1]],"api_stage1":{"api_f_count":96,"api_f_lostcount":1,"api_e_count":0,"api_e_lostcount":0,"api_disp_seiku":1,"api_touch_plane":[59,-1]},"api_stage2":{"api_f_count":30,"api_f_lostcount":1,"api_e_count":0,"api_e_lostcount":0},"api_stage3":{"api_frai_flag":[-1,0,0,0,0,0,0],"api_erai_flag":[-1,0,1,0,1,0,0],"api_fbak_flag":[-1,0,0,0,0,0,0],"api_ebak_flag":[-1,0,0,0,0,0,0],"api_fcl_flag":[-1,0,0,0,0,0,0],"api_ecl_flag":[-1,0,0,0,0,0,0],"api_fdam":[-1,0,0,0,0,0,0],"api_edam":[-1,0,0,0,57,0,0]}}

        /// <summary>
        /// 航空機を飛ばせる艦の一覧
        /// いない場合は [-1] が格納される
        /// </summary>
        public int[] plane_from;

        public KoukuStage1 stage1;
        public KoukuStage2 stage2;
        public KoukuStage3 stage3;

    }
}
