﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_sortie.values;

namespace KanColleLib.TransmissionData.api_req_sortie
{
    public class Battle
    {
        // 参考: http://blog.kiseki.moe/2015/05/11/kancolle-api-battle/

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

        /// <summary>
        /// 支援艦隊のフラグ（0:なし 1:航空支援 2:砲撃支援 3:雷撃支援）
        /// </summary>
        public int support_flag;

        /// <summary>
        /// 支援の状況
        /// </summary>
        public SupportInfoValue support_info;

        /// <summary>
        /// 開幕雷撃のフラグ
        /// </summary>
        public bool opening_flag;

        /// <summary>
        /// 開幕雷撃
        /// </summary>
        public RaigekiValue opening_atack;

        /// <summary>
        /// 砲撃1巡目、2巡目、3巡目、終幕雷撃があるかどうかのフラグ
        /// </summary>
        public bool[] hourai_flag;

        public HougekiValue hougeki1;
        public HougekiValue hougeki2;
        public HougekiValue hougeki3;

        /// <summary>
        /// 終幕雷撃
        /// </summary>
        public RaigekiValue raigeki;

        public static Battle fromDynamic(dynamic json)
        {
            return new Battle()
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
                support_flag = (int)json.api_support_flag,
                support_info = json.api_support_info() && json.api_support_info != null ? SupportInfoValue.fromDynamic(json.api_support_info) : null,
                opening_flag = (int)json.api_opening_flag == 1,
                opening_atack = json.api_opening_atack() && json.api_opening_atack != null ? RaigekiValue.fromDynamic(json.api_opening_atack) : null,
                hourai_flag = ((int[])(json.api_hourai_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                hougeki1 = json.api_hougeki1() && json.api_hougeki1 != null ? HougekiValue.fromDynamic(json.api_hougeki1) : null,
                hougeki2 = json.api_hougeki2() && json.api_hougeki2 != null ? HougekiValue.fromDynamic(json.api_hougeki2) : null,
                hougeki3 = json.api_hougeki3() && json.api_hougeki3 != null ? HougekiValue.fromDynamic(json.api_hougeki3) : null,
                raigeki = json.api_raigeki() && json.api_raigeki != null ? RaigekiValue.fromDynamic(json.api_raigeki) : null
            };
        }


        // {"api_result":1,"api_result_msg":"\u6210\u529f","api_data":{"api_dock_id":1,"api_ship_ke":[-1,501,-1,-1,-1,-1,-1],"api_ship_lv":[-1,1,-1,-1,-1,-1,-1],
        // "api_nowhps":[-1,32,-1,-1,-1,-1,-1,20,-1,-1,-1,-1,-1],"api_maxhps":[-1,32,-1,-1,-1,-1,-1,20,-1,-1,-1,-1,-1],"api_midnight_flag":0,
        // "api_eSlot":[[501,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1]],
        // "api_eKyouka":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],"api_fParam":[[48,79,49,49],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],
        // "api_eParam":[[5,15,6,5],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],"api_search":[6,5],"api_formation":[1,1,2],"api_stage_flag":[1,0,0],
        // "api_kouku":{"api_plane_from":[[-1],[-1]],"api_stage1":{"api_f_count":0,"api_f_lostcount":0,"api_e_count":0,"api_e_lostcount":0,"api_disp_seiku":1,"api_touch_plane":[-1,-1]},
        // "api_stage2":null,"api_stage3":null},
        // "api_support_flag":0,"api_support_info":null,"api_opening_flag":0,"api_opening_atack":null,"api_hourai_flag":[1,0,0,0],
        // "api_hougeki1":{"api_at_list":[-1,1],"api_at_type":[-1,0],"api_df_list":[-1,[7]],"api_si_list":[-1,[2]],"api_cl_list":[-1,[1]],"api_damage":[-1,[39]]},
        // "api_hougeki2":null,"api_hougeki3":null,"api_raigeki":null}}
    }
}
