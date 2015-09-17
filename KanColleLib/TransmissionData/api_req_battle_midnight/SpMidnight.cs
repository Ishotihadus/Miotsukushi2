using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_battle_midnight.values;

namespace KanColleLib.TransmissionData.api_req_battle_midnight
{
    public class SpMidnight
    {
        /// <summary>
        /// 艦隊番号（1から）
        /// </summary>
        public int deck_id;

        /// <summary>
        /// 夜戦開始時のHP（1から6が自艦隊、7から12が敵艦隊）
        /// </summary>
        public int[] nowhps;

        /// <summary>
        /// 敵艦ID（インデックスは1から）
        /// </summary>
        public int[] ship_ke;

        /// <summary>
        /// 敵艦レベル（インデックスは1から）
        /// </summary>
        public int[] ship_lv;

        /// <summary>
        /// 最大HP（1から6が自艦隊、7から12が敵艦隊）
        /// </summary>
        public int[] maxhps;

        /// <summary>
        /// 敵のスロット情報（インデックスはともに0から、なければ-1）
        /// </summary>
        public int[][] eSlot;

        /// <summary>
        /// 敵の改装情報（インデックスはともに0から、なければ0）
        /// </summary>
        public int[][] eKyouka;

        /// <summary>
        /// 自艦隊パラメータ（火力/雷装/対空/装甲の順、インデックスは0から、なければ0）
        /// </summary>
        public int[][] fParam;

        /// <summary>
        /// 敵艦隊パラメータ（火力/雷装/対空/装甲の順、インデックスは0から、なければ0）
        /// </summary>
        public int[][] eParam;

        /// <summary>
        /// 交戦情報（自艦隊/敵艦隊/交戦形態の順）
        /// 1:単縦 2:複縦 3:輪形 4:梯形 5:単横 11:第一警戒航行序列（対潜警戒） 12:第二警戒航行序列（前方警戒） 13:第三警戒航行序列（輪形陣） 14:第四警戒航行序列（戦闘隊形）
        /// 1:同航戦 2:反航戦 3:T字有利 4:T字不利
        /// </summary>
        public int[] formation;

        /// <summary>
        /// 触接機のID
        /// </summary>
        public int[] touch_plane;

        /// <summary>
        /// 照明弾の発射艦（1-6）
        /// </summary>
        public int[] flare_pos;

        /// <summary>
        /// 砲雷撃戦の情報
        /// </summary>
        public HougekiValue hougeki;

        public static SpMidnight fromDynamic(dynamic json)
        {
            var deck_id = 0;
            int.TryParse(json.api_deck_id as string, out deck_id);

            return new SpMidnight()
            {
                deck_id = deck_id,
                nowhps = json.api_nowhps.Deserialize<int[]>(),
                ship_ke = json.api_ship_ke.Deserialize<int[]>(),
                ship_lv = json.api_ship_lv.Deserialize<int[]>(),
                maxhps = json.api_maxhps.Deserialize<int[]>(),
                eSlot = json.api_eSlot.Deserialize<int[][]>(),
                eKyouka = json.api_eKyouka.Deserialize<int[][]>(),
                fParam = json.api_fParam.Deserialize<int[][]>(),
                eParam = json.api_eParam.Deserialize<int[][]>(),
                formation = json.api_formation.Deserialize<int[]>(),
                touch_plane = json.api_touch_plane.Deserialize<int[]>(),
                flare_pos = json.api_flare_pos.Deserialize<int[]>(),
                hougeki = json.api_hougeki() && json.api_hougeki != null ? HougekiValue.fromDynamic(json.api_hougeki) : null
            };
        }
    }

    // svdata={"api_result":1,"api_result_msg":"\u6210\u529f", "api_data":{
    //  "api_deck_id":1,
    //  "api_ship_ke":[-1,527,527,522,554,521,553],
    //  "api_ship_lv":[-1,1,1,1,1,1,1],
    //  "api_nowhps":[-1,13,-1,-1,-1,-1,-1,76,76,60,53,50,47],
    //  "api_maxhps":[-1,13,-1,-1,-1,-1,-1,76,76,60,53,50,47],
    //  "api_eSlot":[[505,506,515,525,-1],[505,506,515,525,-1],[505,506,525,525,-1],[504,542,543,-1,-1],[506,514,514,-1,-1],[502,515,542,-1,-1]],
    //  "api_eKyouka":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],
    //  "api_fParam":[[13,69,0,21],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],
    //  "api_eParam":[[68,48,40,70],[68,48,40,70],[58,42,30,60],[42,72,27,36],[35,72,20,34],[30,60,24,27]],
    //  "api_formation":[1,1,1],"api_touch_plane":[-1,-1],"api_flare_pos":[-1,-1],
    //  "api_hougeki":{"api_at_list":[-1,1,11,12],"api_df_list":[-1,[10],[1],[1]],"api_si_list":[-1,[127],[-1],[-1]],"api_cl_list":[-1,[1],[1],[0]],"api_sp_list":[-1,0,0,0],"api_damage":[-1,[86],[1],[0]]}}}
}
