using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class NextCellData
    {
        /// <summary>
        /// らしんばんが登場するか
        /// </summary>
        public bool rashin_flg;

        /// <summary>
        /// 羅針盤娘ID
        /// 0:なし　1:眠そう　2:桃の髪飾り　3:ひよこ　4:魔法使い
        /// </summary>
        public int rashin_id;

        /// <summary>
        /// 海域ID
        /// </summary>
        public int maparea_id;

        /// <summary>
        /// マップID
        /// </summary>
        public int mapinfo_no;

        /// <summary>
        /// セルID
        /// </summary>
        public int no;

        /// <summary>
        /// グラフィック用ID
        /// 0:出発地 1:?　2:補給マス　3:うずしお　4:通常戦闘（or気のせい）　5:ボス　6:夜戦マス　7:夜昼戦
        /// </summary>
        public int color_no;

        /// <summary>
        /// 0:出発地　1:?　2:補給マス　3:うずしお　4:通常戦闘　5:ボス戦闘　6:気のせいだった　7:航空戦　8:船団護衛成功
        /// </summary>
        public int event_id;

        /// <summary>
        /// 0:戦闘なし　1:昼戦　2:夜戦　3:夜昼戦　4:連合艦隊航空戦　6:能動分岐
        /// event_id=6のときは0:気のせいだった　1:敵影を見ず　2:能動分岐
        /// event_id=7のときは0:航空偵察　4:航空戦
        /// </summary>
        public int event_kind;

        /// <summary>
        /// 次の分岐本数　これが0以下なら終点
        /// </summary>
        public int next;

        /// <summary>
        /// ボスマスID
        /// </summary>
        public int bosscell_no;

        /// <summary>
        /// ボス撃破済みかどうか
        /// </summary>
        public bool bosscomp;

        /// <summary>
        /// 吹き出し
        /// 0:なし　1:敵艦隊発見　2:攻撃目標発見
        /// </summary>
        public int? comment_kind;

        /// <summary>
        /// 索敵フェイズ
        /// 0:なし　1:索敵フェイズ発生
        /// </summary>
        public int? production_kind;

        /// <summary>
        /// 取得アイテム情報 
        /// </summary>
        public ItemgetValue itemget;

        /// <summary>
        /// 能動分岐情報
        /// </summary>
        public SelectRouteValue select_route;

        /// <summary>
        /// イベントマップ情報
        /// </summary>
        public EventmapValue eventmap;

        /// <summary>
        /// うずしお情報
        /// </summary>
        public HappeningValue happening;

        /// <summary>
        /// 敵艦隊情報
        /// 2015年7月17日のメンテに伴って削除？
        /// </summary>
        public EnemyValue enemy;
        
        /// <summary>
        /// 船団護衛成功マスのときの獲得資源情報
        /// </summary>
        public ItemgetEoValue itemget_eo_comment;

        /// <summary>
        /// 船団護衛成功マスのときの獲得アイテム情報
        /// </summary>
        public ItemgetEoValue itemget_eo_result;

        /// <summary>
        /// 船団護衛成功マスのときの獲得結果
        /// </summary>
        public int? get_eo_rate;

        /// <summary>
        /// 航空偵察マス情報
        /// </summary>
        public AirsearchValue airsearch;

        public static NextCellData fromDynamic(dynamic json)
        {
            var ret = new NextCellData();

            ret.rashin_flg = (int)json.api_rashin_flg == 1;
            ret.rashin_id = (int)json.api_rashin_id;
            ret.maparea_id = (int)json.api_maparea_id;
            ret.mapinfo_no = (int)json.api_mapinfo_no;
            ret.no = (int)json.api_no;
            ret.color_no = (int)json.api_color_no;
            ret.event_id = (int)json.api_event_id;
            ret.event_kind = (int)json.api_event_kind;
            ret.next = (int)json.api_next;
            ret.bosscell_no = (int)json.api_bosscell_no;
            ret.bosscomp = (int)json.api_bosscomp == 1;
            ret.comment_kind = json.api_comment_kind() ? (int)json.api_comment_kind : (int?)null;
            ret.production_kind = json.api_production_kind() ? (int)json.api_production_kind : (int?)null;
            ret.itemget = json.api_itemget() ? ItemgetValue.fromDynamic(json.api_itemget) : null;
            ret.select_route = json.api_select_route() ? SelectRouteValue.fromDynamic(json.api_select_route) : null;
            ret.eventmap = json.api_eventmap() ? EventmapValue.fromDynamic(json.api_eventmap) : null;
            ret.happening = json.api_happening() ? HappeningValue.fromDynamic(json.api_happening) : null;
            ret.enemy = json.api_enemy() ? EnemyValue.fromDynamic(json.api_enemy) : null;
            ret.itemget_eo_comment = json.api_itemget_eo_comment() ? ItemgetEoValue.fromDynamic(json.api_itemget_eo_comment) : null;
            ret.itemget_eo_result = json.api_itemget_eo_result() ? ItemgetEoValue.fromDynamic(json.api_itemget_eo_result) : null;
            ret.get_eo_rate = json.api_get_eo_rate() ? (int)json.api_get_eo_rate : (int?)null;
            ret.airsearch = json.api_airsearch() ? AirsearchValue.fromDynamic(json.api_airsearch) : null;

            return ret;
        }

        // svdata={"api_result":1,"api_result_msg":"\u6210\u529f","api_data":{"api_rashin_flg":1,"api_rashin_id":4,"api_maparea_id":1,"api_mapinfo_no":1,"api_no":2,
        // "api_color_no":4,"api_event_id":4,"api_event_kind":1,"api_next":0,"api_bosscell_no":3,"api_bosscomp":1,"api_comment_kind":0,"api_production_kind":0,
        // "api_airsearch":{"api_plane_type":0,"api_result":0}}}
    }
}
