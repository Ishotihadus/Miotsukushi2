using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_mission
{
    public class Result
    {
        /// <summary>
        /// 遠征から帰投した艦娘たちのID
        /// </summary>
        public int[] ship_id;

        /// <summary>
        /// 結果
        /// 0:失敗 1:成功 2:大成功
        /// </summary>
        public int clear_result;

        /// <summary>
        /// 獲得司令部経験値
        /// </summary>
        public int get_exp;

        /// <summary>
        /// 司令部レベル
        /// </summary>
        public int member_lv;

        /// <summary>
        /// 累計司令部経験値
        /// </summary>
        public int member_exp;

        /// <summary>
        /// 艦娘の取得経験値
        /// </summary>
        public int[] get_ship_exp;

        /// <summary>
        /// 艦娘の経験値情報（現在の累計経験値/次のレベルまでの累計経験値）
        /// </summary>
        public int[][] get_exp_lvup;

        /// <summary>
        /// 海域名
        /// </summary>
        public string maparea_name;

        /// <summary>
        /// 遠征の詳細説明
        /// </summary>
        public string detail;

        /// <summary>
        /// 遠征名
        /// </summary>
        public string quest_name;

        /// <summary>
        /// 遠征の難易度
        /// </summary>
        public int quest_level;
        
        /// <summary>
        /// 獲得資源（燃料/弾薬/鋼材/ボーキ）
        /// なければ-1を送ってくるのでnullで返す
        /// </summary>
        public int[] get_material;

        /// <summary>
        /// 獲得アイテムの情報（1つめ/2つめ）
        /// 0:なし 1:バケツ 2:バーナー 3:開発資材 4:使用アイテム 5:家具コイン
        /// </summary>
        public int[] useitem_flag;

        /// <summary>
        /// 1つめの獲得アイテム（なければnull）
        /// </summary>
        public values.GetItemValue get_item1;

        /// <summary>
        /// 2つめの獲得アイテム（なければnull）
        /// </summary>
        public values.GetItemValue get_item2;

        public static Result fromDynamic(dynamic json)
        {
            // not implemented
        }

        // {"api_ship_id":[-1,8407,4850,1687,14315,9436,1688],"api_clear_result":0,"api_get_exp":15,"api_member_lv":106,"api_member_exp":3718319,"api_get_ship_exp":[97,65,65,65,65,65],
        // "api_get_exp_lvup":[[271995,275000],[70653,74100],[72879,74100],[1662,2100],[67301,70300],[72912,74100]],
        // "api_maparea_name":"\u5357\u65b9\u6d77\u57df","api_detail":"\u6c34\u96f7\u6226\u968a\u306b\u30c9\u30e9\u30e0\u7f36(\u8f38\u9001\u7528)\u3092\u6e80\u8f09\u3057\u3001\u5357\u65b9\u65b9\u9762\u53cb\u8ecd\u3078\u306e\u9f20\u8f38\u9001\u4f5c\u6226\u3092\u9042\u884c\u305b\u3088\uff01",
        // "api_quest_name":"\u6771\u4eac\u6025\u884c","api_quest_level":7,"api_get_material":-1,"api_useitem_flag":[0,0]}
    }
}
