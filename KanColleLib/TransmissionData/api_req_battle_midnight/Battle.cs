using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_sortie.values;

namespace KanColleLib.TransmissionData.api_req_battle_midnight
{
    public class Battle
    {
        /// <summary>
        /// 艦隊番号（1から）
        /// </summary>
        public int deck_id;

        public int[] nowhps;

        public int[] ship_ke;

        public int[] ship_lv;

        public int[] maxhps;

        public int[][] eSlot;

        public int[][] eKyouka;

        public int[] fParam;

        public int[] eParam;

        public int[] touch_plane;

        public int[] flare_pos;

        public HougekiValue hougeki;

        public static Battle fromDynamic(dynamic json)
        {
            int deck_id = 0;
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
