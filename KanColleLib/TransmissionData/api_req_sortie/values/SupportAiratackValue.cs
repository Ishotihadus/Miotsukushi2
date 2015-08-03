using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class SupportAiratackValue
    {
        /*
        "api_support_airatack":{"api_deck_id":4,
        "api_ship_id":[134,7122,19742,18959,7028,0],
        "api_undressing_flag":[0,0,0,0,0,0],
        "api_stage_flag":[1,1,1],
        "api_plane_from":[[-1]],
        "api_stage1":{
        "api_f_count":58,"api_f_lostcount":0,
        "api_e_count":0,
        "api_e_lostcount":0},
        "api_stage2":{
        "api_f_count":58,
        "api_f_lostcount":3},
        "api_stage3":{
        "api_erai_flag":[-1,0,1,0,0,0,1],
        "api_ebak_flag":[-1,0,1,1,0,0,0],
        "api_ecl_flag":[-1,0,0,0,0,0,0],
        "api_edam":[-1,0,6,0,0,0,0]}}
        */

        /// <summary>
        /// 艦隊番号
        /// </summary>
        public int deck_id;

        /// <summary>
        /// 艦娘ID（0から始まる、いなければ0）
        /// </summary>
        public int[] ship_id;

        /// <summary>
        /// 各艦がダメージを受けたか（ダメージ受けないんじゃなかったの……?）（0から始まる）
        /// </summary>
        public bool[] undressing_flag;

        /// <summary>
        /// 各ステージの状態
        /// </summary>
        public bool[] stage_flag;

        /// <summary>
        /// 敵艦隊の航空機発艦元（なければ[-1]が入る）
        /// </summary>
        public int[] plane_from;

        public SupportAiratackStage1 stage1;
        public SupportAiratackStage2 stage2;
        public SupportAiratackStage3 stage3;
    }

    public class SupportAiratackStage1
    {
        public int f_count;
        public int f_lostcount;
        public int e_count;
        public int e_lostcount;
    }

    public class SupportAiratackStage2
    {
        public int f_count;
        public int f_lostcount;
    }

    public class SupportAiratackStage3
    {
        /// <summary>
        /// 敵艦隊の被雷撃状況（1から始まる）
        /// </summary>
        public bool[] erai_flag;

        /// <summary>
        /// 敵艦隊の被爆撃情報（1から始まる）
        /// </summary>
        public bool[] ebak_flag;

        /// <summary>
        /// 敵艦隊のクリティカル等情報（1から始まる）
        /// </summary>
        public int[] ecl_flag;

        /// <summary>
        /// 敵艦隊の受けたダメージ（1から始まる）
        /// </summary>
        public double[] edam;
    }
}
