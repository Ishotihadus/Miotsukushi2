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
        /// ただしindex0は必ず-1
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
        /// 艦娘の経験値情報（経験値が入る前の累計経験値/次のレベルまでの累計経験値/さらに次のレベルまでの累計経験値/……）
        /// レベルがあがると要素数が増えることになる（最後のレベルまでの経験値がわからないといけない）
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
            Result result = new Result();

            result.ship_id = json.api_ship_id.Deserialize<int[]>();
            result.clear_result = (int)json.api_clear_result;
            result.get_exp = (int)json.api_get_exp;
            result.member_lv = (int)json.api_member_lv;
            result.member_exp = (int)json.api_member_exp;
            result.get_ship_exp = json.api_get_ship_exp.Deserialize<int[]>();
            result.get_exp_lvup = json.api_get_exp_lvup.Deserialize<int[][]>();
            result.maparea_name = json.api_maparea_name as string;
            result.detail = json.api_detail as string;
            result.quest_name = json.api_quest_name as string;
            result.quest_level = (int)json.api_quest_level;

            if(json.api_get_material is Codeplex.Data.DynamicJson)
                result.get_material = json.api_get_material.Deserialize<int[]>();
            else
                result.get_material = null;

            result.useitem_flag = json.api_useitem_flag.Deserialize<int[]>();

            if (json.api_get_item1())
                result.get_item1 = values.GetItemValue.fromDynamic(json.api_get_item1);
            else
                result.get_item1 = null;

            if (json.api_get_item2())
                result.get_item2 = values.GetItemValue.fromDynamic(json.api_get_item2);
            else
                result.get_item2 = null;

            return result;
        }

    }
}
