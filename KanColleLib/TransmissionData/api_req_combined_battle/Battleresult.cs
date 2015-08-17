using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_sortie.values;

namespace KanColleLib.TransmissionData.api_req_combined_battle
{
    public class Battleresult
    {

        /// <summary>
        /// 敵艦ID（1から始まるデータ，0は-1で固定，いなければ-1）
        /// </summary>
        public int[] ship_id;

        /// <summary>
        /// 勝利ランク　完全SとSの区別がない
        /// </summary>
        public string win_rank;

        /// <summary>
        /// 獲得提督経験値
        /// </summary>
        public int get_exp;

        /// <summary>
        /// MVP艦（1から始まるインデックス，いなければ-1になる）
        /// </summary>
        public int mvp;

        /// <summary>
        /// MVP艦（随伴艦隊、1から始まる）
        /// </summary>
        public int mvp_combined;

        /// <summary>
        /// 提督レベル
        /// </summary>
        public int member_lv;

        /// <summary>
        /// 経験値獲得後の合計提督経験値
        /// </summary>
        public int member_exp;

        /// <summary>
        /// 基本経験値
        /// </summary>
        public int get_base_exp;

        /// <summary>
        /// 各艦娘の獲得経験値（インデックス0は-1で固定，いなければ-1）
        /// </summary>
        public int[] get_ship_exp;

        /// <summary>
        /// 随伴艦隊の各艦娘の獲得経験値（インデックス0は-1で固定，いなければ-1）
        /// </summary>
        public int[] get_ship_exp_combined;

        /// <summary>
        /// 獲得前の経験値，次のレベルに達する経験値の組, （レベルアップした場合）その次のレベルに達する経験値, （以下同様）（インデックスは0から）
        /// </summary>
        public int[][] get_exp_lvup;

        /// <summary>
        /// 随伴艦隊の獲得前の経験値，次のレベルに達する経験値の組, （レベルアップした場合）その次のレベルに達する経験値, （以下同様）（インデックスは0から）
        /// </summary>
        public int[][] get_exp_lvup_combined;

        /// <summary>
        /// 敵艦を倒した数
        /// </summary>
        public int dests;

        /// <summary>
        /// 旗艦撃沈フラグ
        /// </summary>
        public bool destsf;

        /// <summary>
        /// 任務名
        /// </summary>
        public string quest_name;

        /// <summary>
        /// 難易度
        /// </summary>
        public int quest_level;

        /// <summary>
        /// 敵艦隊情報
        /// </summary>
        public EnemyInfoValue enemy_info;

        /// <summary>
        /// 初めてクリアしたか
        /// </summary>
        public bool first_clear;

        /// <summary>
        /// ドロップフラグ
        /// 0:アイテム　1:艦娘　2:装備
        /// </summary>
        public bool[] get_flag;

        /// <summary>
        /// ドロップ艦娘
        /// </summary>
        public GetShipValue get_ship;

        /// <summary>
        /// ドロップ装備
        /// </summary>
        public GetSlotitemValue get_slotitem;

        /// <summary>
        /// ドロップアイテム
        /// </summary>
        public GetUseitemValue get_useitem;

        /// <summary>
        /// イベントかどうか? イベントドロップがあるかどうか?（未確認）
        /// </summary>
        public bool? get_eventflag;

        /// <summary>
        /// イベントドロップ
        /// </summary>
        public List<GetEventitemValue> get_eventitem;

        public static Battleresult fromDynamic(dynamic json)
        {
            var ret = new Battleresult();

            ret.ship_id = json.api_ship_id.Deserialize<int[]>();
            ret.win_rank = json.api_win_rank as string;
            ret.get_exp = (int)json.api_get_exp;
            ret.mvp = (int)json.api_mvp;
            ret.mvp_combined = (int)json.api_mvp_combined;
            ret.member_lv = (int)json.api_member_lv;
            ret.member_exp = (int)json.api_member_exp;
            ret.get_base_exp = (int)json.api_get_base_exp;
            ret.get_ship_exp = json.api_get_ship_exp.Deserialize<int[]>();
            ret.get_ship_exp_combined = json.api_get_ship_exp_combined.Deserialize<int[]>();
            ret.get_exp_lvup = json.api_get_exp_lvup.Deserialize<int[][]>();
            ret.get_exp_lvup_combined = json.api_get_exp_lvup_combined.Deserialize<int[][]>();
            ret.dests = (int)json.api_dests;
            ret.destsf = (int)json.api_destsf == 1;
            ret.quest_name = json.api_quest_name as string;
            ret.quest_level = (int)json.api_quest_level;
            ret.enemy_info = EnemyInfoValue.fromDynamic(json.api_enemy_info);
            ret.first_clear = (int)json.api_first_clear == 1;
            ret.get_flag = ((int[])(json.api_get_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray();
            ret.get_ship = json.api_get_ship() ? GetShipValue.fromDynamic(json.api_get_ship) : null;
            ret.get_slotitem = json.api_get_slotitem() ? GetSlotitemValue.fromDynamic(json.api_get_slotitem) : null;
            ret.get_useitem = json.api_get_useitem() ? GetUseitemValue.fromDynamic(json.api_get_useitem) : null;
            ret.get_eventflag = json.api_get_eventflag() ? (int)json.api_get_eventflag == 1 : (bool?)null;

            if (!json.api_get_eventitem())
                ret.get_eventitem = null;
            else
            {
                ret.get_eventitem = new List<GetEventitemValue>();
                if (json.api_get_eventitem.IsArray)
                    foreach (var data in json.api_get_eventitem)
                        ret.get_eventitem.Add(GetEventitemValue.fromDynamic(data));
                else
                    ret.get_eventitem.Add(GetEventitemValue.fromDynamic(json.api_get_eventitem));
            }
            

            return ret;
        }
    }
}
