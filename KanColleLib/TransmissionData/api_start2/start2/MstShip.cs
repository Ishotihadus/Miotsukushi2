using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 艦種のマスターデータ
    /// </summary>
    public class MstShip
    {
        /// <summary>
        /// 艦種ID
        /// </summary>
        public int id;

        /// <summary>
        /// 艦種図鑑番号
        /// </summary>
        public int? sortno;

        /// <summary>
        /// 艦名
        /// </summary>
        public string name;

        /// <summary>
        /// 艦名の読み
        /// </summary>
        public string yomi;

        /// <summary>
        /// 艦種の船種ID
        /// </summary>
        public int stype;

        /// <summary>
        /// 改造Lv
        /// </summary>
        public int? afterlv;

        /// <summary>
        /// 改造後艦種ID
        /// </summary>
        public int? aftershipid;

        /// <summary>
        /// 耐久/HP（初期値/最大値）
        /// </summary>
        public int[] taik;
        
        /// <summary>
        /// 装甲（初期値/最大値）
        /// </summary>
        public int[] souk;

        /// <summary>
        /// 火力（初期値/最大値）
        /// </summary>
        public int[] houg;

        /// <summary>
        /// 雷装（初期値/最大値）
        /// </summary>
        public int[] raig;

        /// <summary>
        /// 対空（初期値/最大値）
        /// </summary>
        public int[] tyku;

        /// <summary>
        /// 運（初期値/最大値）
        /// </summary>
        public int[] luck;

        /// <summary>
        /// 速力
        /// 10以上が高速、それ未満が低速
        /// 0（不動）も実装されてはいるが表示には用いられない
        /// 参照: Core.swf/action/common/util/Util.as - public static function getSpeedLevel(param1:int) : int
        /// </summary>
        public int? soku;

        /// <summary>
        /// 射程距離
        /// </summary>
        public int? leng;

        /// <summary>
        /// スロット数
        /// </summary>
        public int? slot_num;

        /// <summary>
        /// 最大搭載数
        /// </summary>
        public int[] maxeq;

        /// <summary>
        /// 建造時間（分）
        /// </summary>
        public int? buildtime;

        /// <summary>
        /// 解体時の純資源（燃料/弾薬/鋼材/ボーキ）
        /// </summary>
        public int[] broken;

        /// <summary>
        /// 近代化改修に利用した際に増加するステータス（火力/雷装/対空/装甲）
        /// </summary>
        public int[] powup;

        /// <summary>
        /// 背景画像/レアリティ
        /// </summary>
        public int? backs;

        /// <summary>
        /// 入手時メッセージ
        /// </summary>
        public string getmes;

        /// <summary>
        /// 改造に必要な燃料
        /// </summary>
        public int? afterfuel;

        /// <summary>
        /// 改造に必要な弾薬
        /// </summary>
        public int? afterbull;

        /// <summary>
        /// 燃料最大値
        /// </summary>
        public int? fuel_max;

        /// <summary>
        /// 弾薬最大値
        /// </summary>
        public int? bull_max;

        /// <summary>
        /// ボイスの有無のフラグ
        /// </summary>
        public int? voicef;

        public static MstShip fromDynamic(dynamic json)
        {
            MstShip ship = new MstShip();

            ship.id = (int)json.api_id;
            ship.sortno = json.api_sortno() ? (int)json.api_sortno : (int?)null;
            ship.name = json.api_name as string;
            ship.yomi = json.api_yomi as string;
            ship.stype = (int)json.api_stype;
            ship.afterlv = json.api_afterlv() ? (int)json.api_afterlv : (int?)null;
            ship.aftershipid = json.api_aftershipid() ? int.Parse(json.api_aftershipid as string) : (int?)null;
            ship.taik = json.api_taik() ? json.api_taik.Deserialize<int[]>() : null;
            ship.souk = json.api_souk() ? json.api_souk.Deserialize<int[]>() : null;
            ship.houg = json.api_houg() ? json.api_houg.Deserialize<int[]>() : null;
            ship.raig = json.api_raig() ? json.api_raig.Deserialize<int[]>() : null;
            ship.tyku = json.api_tyku() ? json.api_tyku.Deserialize<int[]>() : null;
            ship.luck = json.api_luck() ? json.api_luck.Deserialize<int[]>() : null;
            ship.soku = json.api_soku() ? (int)json.api_soku : (int?)null;
            ship.leng = json.api_leng() ? (int)json.api_leng : (int?)null;
            ship.slot_num = json.api_slot_num() ? (int)json.api_slot_num : (int?)null;
            ship.maxeq = json.api_maxeq() ? json.api_maxeq.Deserialize<int[]>() : null;
            ship.buildtime = json.api_buildtime() ? (int)json.api_buildtime : (int?)null;
            ship.broken = json.api_broken() ? json.api_broken.Deserialize<int[]>() : null;
            ship.powup = json.api_powup() ? json.api_powup.Deserialize<int[]>() : null;
            ship.backs = json.api_backs() ? (int)json.api_backs : (int?)null;
            ship.getmes = json.api_getmes() ? json.api_getmes as string : null;
            ship.afterfuel = json.api_afterfuel() ? (int)json.api_afterfuel : (int?)null;
            ship.afterbull = json.api_afterbull() ? (int)json.api_afterbull : (int?)null;
            ship.fuel_max = json.api_fuel_max() ? (int)json.api_fuel_max : (int?)null;
            ship.bull_max = json.api_bull_max() ? (int)json.api_bull_max : (int?)null;
            ship.voicef = json.api_voicef() ? (int)json.api_voicef : (int?)null;

            return ship;
        }

        // "api_id":1,"api_sortno":31,"api_name":"\u7766\u6708","api_yomi":"\u3080\u3064\u304d","api_stype":2,"api_ctype":28,"api_cnum":1,"api_enqflg":"0",
        // "api_afterlv":20,"api_aftershipid":"254","api_taik":[13,24],"api_souk":[5,18],"api_tous":[0,0],"api_houg":[6,29],"api_raig":[18,59],"api_baku":[0,0],"api_tyku":[7,29],"api_atap":[0,0],
        // "api_tais":[16,39],"api_houm":[0,0],"api_kaih":[37,79],"api_houk":[0,0],"api_raik":[0,0],"api_bakk":[0,0],"api_saku":[4,17],"api_sakb":[0,0],"api_luck":[12,49],"api_sokuh":1,"api_soku":10,
        // "api_leng":1,"api_grow":[0,0,0,0,0,0,0,0],"api_slot_num":2,"api_maxeq":[0,0,0,0,0],"api_defeq":[1,37,-1,-1],"api_buildtime":18,"api_broken":[1,1,4,0],"api_powup":[1,1,0,0],"api_gumax":[0,0,0,0],
        // "api_backs":3,"api_getmes":"\u7766\u6708\u3067\u3059\u3002<br>\u306f\u308a\u304d\u3063\u3066\u3001\u307e\u3044\u308a\u307e\u3057\u3087\u30fc\uff01","api_homemes":null,
        // "api_gomes":null,"api_gomes2":null,
        // "api_sinfo":"\u5e1d\u56fd\u6d77\u8ecd\u306e\u99c6\u9010\u8266\u3067\u521d\u3081\u3066\u5927\u578b\u3067\u5f37\u529b\u306a61cm\u9b5a\u96f7\u3092\u642d\u8f09\u3057\u307e\u3057\u305f\u3001\u7766\u6708\u3067\u3059\uff01<br>\u65e7\u5f0f\u306a\u304c\u3089\u3001\u7b2c\u4e00\u7dda\u3067\u9811\u5f35\u3063\u305f\u306e\u3067\u3059\uff01",
        // "api_afterfuel":100,"api_afterbull":100,"api_touchs":[null,null,null],"api_missions":null,"api_systems":null,"api_fuel_max":15,"api_bull_max":15,"api_voicef":0

        // 10月10日のメンテで情報が減少
        // "api_id":1,"api_sortno":31,"api_name":"睦月","api_yomi":"むつき","api_stype":2,"api_afterlv":20,"api_aftershipid":"254","api_taik":[13,24],"api_souk":[5,18],"api_houg":[6,29],"api_raig":[18,59],
        // "api_tyku":[7,29],"api_luck":[12,49],"api_soku":10,"api_leng":1,"api_slot_num":2,"api_maxeq":[0,0,0,0,0],"api_buildtime":18,"api_broken":[1,1,4,0],"api_powup":[1,1,0,0],"api_backs":3,
        // "api_getmes":"睦月です。<br>はりきって、まいりましょー！","api_afterfuel":100,"api_afterbull":100,"api_fuel_max":15,"api_bull_max":15,"api_voicef":0

        // 2015年7月17日のメンテで敵艦情報が減少
        // 
    }
}
