using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 装備アイテムのマスターデータ
    /// </summary>
    public class MstSlotitem
    {
        /// <summary>
        /// 装備アイテムID
        /// </summary>
        public int id;

        /// <summary>
        /// 図鑑番号
        /// </summary>
        public int sortno;

        /// <summary>
        /// 名前
        /// </summary>
        public string name;

        /// <summary>
        /// 種類
        /// index:1がcardtype
        /// index:2がequiptype
        /// index:3がicontype
        /// </summary>
        public int[] type;

        /// <summary>
        /// 耐久
        /// </summary>
        public int taik;

        /// <summary>
        /// 装甲
        /// </summary>
        public int souk;

        /// <summary>
        /// 火力
        /// </summary>
        public int houg;

        /// <summary>
        /// 雷装
        /// </summary>
        public int raig;

        /// <summary>
        /// 速度
        /// </summary>
        public int soku;

        /// <summary>
        /// 爆装
        /// </summary>
        public int baku;

        /// <summary>
        /// 対空
        /// </summary>
        public int tyku;

        /// <summary>
        /// 対潜
        /// </summary>
        public int tais;

        /// <summary>
        /// 不明（未使用）
        /// </summary>
        public int atap;

        /// <summary>
        /// 砲撃命中
        /// </summary>
        public int houm;

        /// <summary>
        /// 雷撃命中
        /// </summary>
        public int raim;
        
        /// <summary>
        /// 砲撃回避
        /// </summary>
        public int houk;

        /// <summary>
        /// 雷撃回避
        /// </summary>
        public int raik;

        /// <summary>
        /// 爆撃回避
        /// </summary>
        public int bakk;

        /// <summary>
        /// 索敵
        /// </summary>
        public int saku;

        /// <summary>
        /// 不明（未使用）
        /// </summary>
        public int sakb;

        /// <summary>
        /// 運
        /// </summary>
        public int luck;

        /// <summary>
        /// 射程
        /// </summary>
        public int leng;

        /// <summary>
        /// レアリティ
        /// </summary>
        public int rare;

        /// <summary>
        /// 廃棄時取得資源（燃料/弾薬/鋼材/ボーキ）
        /// </summary>
        public int[] broken;

        /// <summary>
        /// 装備品説明
        /// </summary>
        public string info;

        /// <summary>
        /// 不明（未実装）
        /// </summary>
        public string usebull;

        public static MstSlotitem fromDynamic(dynamic json)
        {
            MstSlotitem slotitem = new MstSlotitem();

            slotitem.id = (int)json.api_id;
            slotitem.sortno = (int)json.api_sortno;
            slotitem.name = json.api_name as string;
            slotitem.type = json.api_type.Deserialize<int[]>();
            slotitem.taik = (int)json.api_taik;
            slotitem.souk = (int)json.api_souk;
            slotitem.houg = (int)json.api_houg;
            slotitem.raig = (int)json.api_raig;
            slotitem.soku = (int)json.api_soku;
            slotitem.baku = (int)json.api_baku;
            slotitem.tyku = (int)json.api_tyku;
            slotitem.tais = (int)json.api_tais;
            slotitem.atap = (int)json.api_atap;
            slotitem.houm = (int)json.api_houm;
            slotitem.raim = (int)json.api_raim;
            slotitem.houk = (int)json.api_houk;
            slotitem.raik = (int)json.api_raik;
            slotitem.bakk = (int)json.api_bakk;
            slotitem.saku = (int)json.api_saku;
            slotitem.sakb = (int)json.api_sakb;
            slotitem.luck = (int)json.api_luck;
            slotitem.leng = (int)json.api_leng;
            slotitem.rare = (int)json.api_rare;
            slotitem.info = json.api_info as string;
            slotitem.usebull = json.api_usebull as string;

            return slotitem;
        }

        // {"api_id":1,"api_sortno":1,"api_name":"12cm\u5358\u88c5\u7832","api_type":[1,1,1,1],"api_taik":0,"api_souk":0,"api_houg":1,"api_raig":0,"api_soku":0,"api_baku":0,"api_tyku":1,"api_tais":0,
        // "api_atap":0,"api_houm":0,"api_raim":0,"api_houk":0,"api_raik":0,"api_bakk":0,"api_saku":0,"api_sakb":0,"api_luck":0,"api_leng":1,"api_rare":0,"api_broken":[0,1,1,0],
        // "api_info":"\u65e7\u578b\u306e\u5c0f\u578b\u7832\u3067\u3059\u3002<br>\u65e7\u578b\u99c6\u9010\u8266\u306b\u6a19\u6e96\u7684\u4e3b\u7832\u3068\u3057\u3066\u642d\u8f09\u3055\u308c\u307e\u3057\u305f\u3002<br>\u88c5\u586b\u30fb\u64cd\u7832\u3082\u4eba\u529b\u3067\u3059\u304c\u3001\u30b7\u30f3\u30d7\u30eb\u306a\u69cb\u9020\u3067\u7d4c\u6e08\u6027\u3082\u9ad8\u304f\u3001\u99c6\u9010\u8266\u3084\u6d77\u9632\u8266\u7b49\u306e\u4e3b\u7832\u3068\u3057\u3066\u3001\u9577\u304f\u4f7f\u308f\u308c\u307e\u3057\u305f\u3002<br>\u5bfe\u7a7a\u5c04\u6483\u306b\u306f\u4e0d\u5411\u304d\u3067\u3059\u3002",
        // "api_usebull":"0"}
    }
}
