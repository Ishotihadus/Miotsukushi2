using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_sortie.values;

namespace KanColleLib.TransmissionData.api_req_sortie
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
        /// 獲得前の経験値，次のレベルに達する経験値の組, （レベルアップした場合）その次のレベルに達する経験値, （以下同様）（インデックスは0から）
        /// </summary>
        public int[][] get_exp_lvup;

        /// <summary>
        /// 敵艦を倒した数
        /// </summary>
        public int dests;

        /// <summary>
        /// 旗艦撃沈フラグ
        /// </summary>
        public bool destsf;

        /// <summary>
        /// 轟沈フラグ（インデックスは1から）
        /// </summary>
        public bool[] lost_flag;

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
        /// 航空偵察作戦報酬獲得フラグ　0:なし 1:獲得
        /// </summary>
        public int mapcell_incentive;

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

        /// <summary>
        /// 獲得戦果
        /// </summary>
        public string get_exmap_rate;

        /// <summary>
        /// 取得アイテムID
        /// </summary>
        public int get_exmap_useitem_id;

        public static Battleresult fromDynamic(dynamic json)
        {
            var ret = new Battleresult();

            ret.ship_id = json.api_ship_id.Deserialize<int[]>();
            ret.win_rank = json.api_win_rank as string;
            ret.get_exp = (int)json.api_get_exp;
            ret.mvp = (int)json.api_mvp;
            ret.member_lv = (int)json.api_member_lv;
            ret.member_exp = (int)json.api_member_exp;
            ret.get_base_exp = (int)json.api_get_base_exp;
            ret.get_ship_exp = json.api_get_ship_exp.Deserialize<int[]>();
            ret.get_exp_lvup = json.api_get_exp_lvup.Deserialize<int[][]>();
            ret.dests = (int)json.api_dests;
            ret.destsf = (int)json.api_destsf == 1;
            ret.lost_flag = ((int[])json.api_lost_flag.Deserialize<int[]>()).Select(_ => _ == 1).ToArray();
            ret.quest_name = json.api_quest_name as string;
            ret.quest_level = (int)json.api_quest_level;
            ret.enemy_info = EnemyInfoValue.fromDynamic(json.api_enemy_info);
            ret.first_clear = (int)json.api_first_clear == 1;
            ret.mapcell_incentive = (int)json.api_mapcell_incentive;
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

            ret.get_exmap_rate = json.api_get_exmap_rate is string ? json.api_get_exmap_rate as string : ((int)json.api_get_exmap_rate).ToString();
            ret.get_exmap_useitem_id = json.api_get_exmap_useitem_id is string ? int.Parse(json.api_get_exmap_useitem_id as string) : (int)json.api_get_exmap_useitem_id;

            return ret;
        }
    }

    // svdata={"api_result":1,"api_result_msg":"\u6210\u529f","api_data":{"api_ship_id":[-1,505,503,502,502,-1,-1],"api_win_rank":"C","api_get_exp":10,"api_mvp":1,"api_member_lv":102,"api_member_exp":2032289,
    // "api_get_base_exp":30,"api_get_ship_exp":[-1,72,-1,-1,-1,-1,-1],"api_get_exp_lvup":[[112918,117600]],"api_dests":1,"api_destsf":0,"api_lost_flag":[-1,0,0,0,0,0,0],
    // "api_quest_name":"\u93ae\u5b88\u5e9c\u6b63\u9762\u6d77\u57df","api_quest_level":1,"api_enemy_info":{"api_level":"","api_rank":"","api_deck_name":"\u6575\u4e3b\u529b\u8266\u968a"},
    // "api_first_clear":0,"api_mapcell_incentive":0,"api_get_flag":[0,0,0],"api_get_eventflag":0,"api_get_exmap_rate":0,"api_get_exmap_useitem_id":0}}

    // svdata={"api_result":1,"api_result_msg":"\u6210\u529f","api_data":{"api_ship_id":[-1,501,-1,-1,-1,-1,-1],"api_win_rank":"S","api_get_exp":10,"api_mvp":1,"api_member_lv":102,"api_member_exp":2032279,
    // "api_get_base_exp":30,"api_get_ship_exp":[-1,108,-1,-1,-1,-1,-1],"api_get_exp_lvup":[[112810,117600]],"api_dests":1,"api_destsf":1,"api_lost_flag":[-1,0,0,0,0,0,0],
    // "api_quest_name":"\u93ae\u5b88\u5e9c\u6b63\u9762\u6d77\u57df","api_quest_level":1,"api_enemy_info":{"api_level":"","api_rank":"","api_deck_name":"\u6575\u5075\u5bdf\u8266"},
    // "api_first_clear":0,"api_mapcell_incentive":0,"api_get_flag":[0,1,0],
    // "api_get_ship":{"api_ship_id":29,"api_ship_type":"\u99c6\u9010\u8266","api_ship_name":"\u6587\u6708","api_ship_getmes":"\u3042\u305f\u3057\u3001\u6587\u6708\u3063\u3066\u3044\u3046\u306e\u3002<br>\u3088\u308d\u3057\u304f\u3045\uff5e\u3002"},
    // "api_get_eventflag":0,"api_get_exmap_rate":0,"api_get_exmap_useitem_id":0}}

}
