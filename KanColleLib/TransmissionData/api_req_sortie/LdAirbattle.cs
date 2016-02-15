using System;
using System.Linq;
using KanColleLib.TransmissionData.api_req_sortie.values;

namespace KanColleLib.TransmissionData.api_req_sortie
{
    public class LdAirbattle
    {
        /// <summary>
        /// 艦隊番号（1から）
        /// </summary>
        public int dock_id;

        /// <summary>
        /// 敵艦ID（インデックスは1から）
        /// </summary>
        public int[] ship_ke;

        /// <summary>
        /// 敵艦レベル（インデックスは1から）
        /// </summary>
        public int[] ship_lv;

        /// <summary>
        /// 現在のHP（1から6が自艦隊、7から12が敵艦隊）
        /// </summary>
        public int[] nowhps;

        /// <summary>
        /// 最大HP（1から6が自艦隊、7から12が敵艦隊）
        /// </summary>
        public int[] maxhps;

        /// <summary>
        /// 夜戦突入があるかどうか
        /// </summary>
        public bool midnight_flag;

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
        /// 索敵情報（自艦隊/敵艦隊の順、1:索敵成功全機帰還 2:索敵成功未帰還機あり 3:索敵失敗未帰還機あり 4:索敵失敗全機帰還 5:索敵成功索敵機使用せず 6:索敵失敗索敵機使用せず）
        /// </summary>
        public int[] search;

        /// <summary>
        /// 交戦情報（自艦隊/敵艦隊/交戦形態の順）
        /// 1:単縦 2:複縦 3:輪形 4:梯形 5:単横 11:第一警戒航行序列（対潜警戒） 12:第二警戒航行序列（前方警戒） 13:第三警戒航行序列（輪形陣） 14:第四警戒航行序列（戦闘隊形）
        /// 1:同航戦 2:反航戦 3:T字有利 4:T字不利
        /// </summary>
        public int[] formation;

        /// <summary>
        /// 航空戦のステージがそれぞれあるかどうかのフラグ
        /// </summary>
        public bool[] stage_flag;

        /// <summary>
        /// 航空戦の情報
        /// </summary>
        public KoukuValue kouku;

        public static LdAirbattle fromDynamic(dynamic json)
        {
            return new LdAirbattle()
            {
                dock_id = (int)json.api_dock_id,
                ship_ke = json.api_ship_ke.Deserialize<int[]>(),
                ship_lv = json.api_ship_lv.Deserialize<int[]>(),
                nowhps = json.api_nowhps.Deserialize<int[]>(),
                maxhps = json.api_maxhps.Deserialize<int[]>(),
                midnight_flag = (int)json.api_midnight_flag == 1,
                eSlot = json.api_eSlot.Deserialize<int[][]>(),
                eKyouka = json.api_eKyouka.Deserialize<int[][]>(),
                fParam = json.api_fParam.Deserialize<int[][]>(),
                eParam = json.api_eParam.Deserialize<int[][]>(),
                search = json.api_search.Deserialize<int[]>(),
                formation = json.api_formation.Deserialize<int[]>(),
                stage_flag = ((int[])(json.api_stage_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                kouku = json.api_kouku() && json.api_kouku != null ? KoukuValue.fromDynamic(json.api_kouku) : null,
            };
        }
    }
}
