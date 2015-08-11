using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class ShipValue
    {
        /// <summary>
        /// 艦娘ID
        /// </summary>
        public int id;

        /// <summary>
        /// 図鑑番号
        /// </summary>
        public int sortno;

        /// <summary>
        /// 艦種ID
        /// </summary>
        public int ship_id;

        /// <summary>
        /// レベル
        /// </summary>
        public int lv;

        /// <summary>
        /// 経験値（累計経験値/次のレベルまでの経験値/今のレベルでの経験値%）
        /// </summary>
        public int[] exp;

        /// <summary>
        /// 今のHP
        /// </summary>
        public int nowhp;

        /// <summary>
        /// 耐久（最大HP）
        /// </summary>
        public int maxhp;

        /// <summary>
        /// 射程
        /// 0:なし 1:短 2:中 3:長 4:超長
        /// </summary>
        public int leng;

        /// <summary>
        /// スロット情報（未装備であれば-1）
        /// </summary>
        public int[] slot;

        /// <summary>
        /// 搭載数
        /// </summary>
        public int[] onslot;

        /// <summary>
        /// 過去に近代化回収により強化されたパラメータ量（火力/雷装/対空/装甲/運）
        /// </summary>
        public int[] kyouka;

        /// <summary>
        /// 背景画像（レア度）
        /// </summary>
        public int backs;

        /// <summary>
        /// 現在の燃料
        /// </summary>
        public int fuel;

        /// <summary>
        /// 現在の弾薬
        /// </summary>
        public int bull;

        /// <summary>
        /// 装備可能スロット数
        /// </summary>
        public int slotnum;

        /// <summary>
        /// 入渠時間（ミリ秒）
        /// </summary>
        public long ndock_time;

        /// <summary>
        /// 入渠に必要な資源（鋼材/燃料）
        /// </summary>
        public int[] ndock_item;

        /// <summary>
        /// パラメータ強化レベル（艦娘詳細画面の星の数、0～5）
        /// </summary>
        public int srate;

        /// <summary>
        /// コンディション（疲労度）
        /// </summary>
        public int cond;

        /// <summary>
        /// 火力（現在値/最大値）
        /// </summary>
        public int[] karyoku;

        /// <summary>
        /// 雷装（現在値/最大値）
        /// </summary>
        public int[] raisou;

        /// <summary>
        /// 対空（現在値/最大値）
        /// </summary>
        public int[] taiku;

        /// <summary>
        /// 装甲（現在値/最大値）
        /// </summary>
        public int[] soukou;

        /// <summary>
        /// 回避（現在値/最大値）
        /// </summary>
        public int[] kaihi;

        /// <summary>
        /// 対潜（現在値/最大値）
        /// </summary>
        public int[] taisen;

        /// <summary>
        /// 索敵（現在値/最大値）
        /// </summary>
        public int[] sakuteki;

        /// <summary>
        /// 運（現在値/最大値）
        /// </summary>
        public int[] lucky;

        /// <summary>
        /// ロックされているか
        /// </summary>
        public bool locked;

        /// <summary>
        /// 装備がロックされているか（未使用）
        /// </summary>
        public bool locked_equip;
        
        /// <summary>
        /// お札
        /// </summary>
        public int? sally_area;

        /// <summary>
        /// 増設スロットに入る装備
        /// 0:増設されていない　-1:増設されているが装備されていない
        /// </summary>
        public int slot_ex;

        public static ShipValue fromDynamic(dynamic json)
        {
            ShipValue ship = new ShipValue();

            ship.id = (int)json.api_id;
            ship.sortno = (int)json.api_sortno;
            ship.ship_id = (int)json.api_ship_id;
            ship.lv = (int)json.api_lv;
            ship.exp = json.api_exp.Deserialize<int[]>();
            ship.nowhp = (int)json.api_nowhp;
            ship.maxhp = (int)json.api_maxhp;
            ship.leng = (int)json.api_leng;
            ship.slot = json.api_slot.Deserialize<int[]>();
            ship.onslot = json.api_onslot.Deserialize<int[]>();
            ship.kyouka = json.api_kyouka.Deserialize<int[]>();
            ship.backs = (int)json.api_backs;
            ship.fuel = (int)json.api_fuel;
            ship.bull = (int)json.api_bull;
            ship.slotnum = (int)json.api_slotnum;
            ship.ndock_time = (long)json.api_ndock_time;
            ship.ndock_item = json.api_ndock_item.Deserialize<int[]>();
            ship.srate = (int)json.api_srate;
            ship.cond = (int)json.api_cond;
            ship.karyoku = json.api_karyoku.Deserialize<int[]>();
            ship.raisou = json.api_raisou.Deserialize<int[]>();
            ship.taiku = json.api_taiku.Deserialize<int[]>();
            ship.soukou = json.api_soukou.Deserialize<int[]>();
            ship.kaihi = json.api_kaihi.Deserialize<int[]>();
            ship.taisen = json.api_taisen.Deserialize<int[]>();
            ship.sakuteki = json.api_sakuteki.Deserialize<int[]>();
            ship.lucky = json.api_lucky.Deserialize<int[]>();
            ship.locked = (int)json.api_locked == 1;
            ship.locked_equip = (int)json.api_locked_equip == 1;
            ship.sally_area = json.api_sally_area() ? (int)json.api_sally_area : (int?)null;
            ship.slot_ex = (int)json.api_slot_ex;

            return ship;
        }

    }
}
