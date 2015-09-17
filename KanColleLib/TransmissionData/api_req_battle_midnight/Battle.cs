using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_battle_midnight.values;

namespace KanColleLib.TransmissionData.api_req_battle_midnight
{
    public class Battle
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

        public static Battle fromDynamic(dynamic json)
        {
            var deck_id = 0;
            int.TryParse(json.api_deck_id as string, out deck_id);

            return new Battle()
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
                touch_plane = json.api_touch_plane.Deserialize<int[]>(),
                flare_pos = json.api_flare_pos.Deserialize<int[]>(),
                hougeki = json.api_hougeki() && json.api_hougeki != null ? HougekiValue.fromDynamic(json.api_hougeki) : null
            };
        }
    }
}
