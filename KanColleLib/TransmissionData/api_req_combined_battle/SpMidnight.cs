﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_battle_midnight.values;

namespace KanColleLib.TransmissionData.api_req_combined_battle
{
    public class SpMidnight
    {
        /// <summary>
        /// 艦隊番号（1から）
        /// </summary>
        public int deck_id;

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
        /// 現在のHP（連合艦隊、インデックスは1から6）
        /// </summary>
        public int[] nowhps_combined;

        /// <summary>
        /// 最大HP（連合艦隊、インデックスは1から6）
        /// </summary>
        public int[] maxhps_combined;

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
        /// 護衛艦隊パラメータ（火力/雷装/対空/装甲の順、インデックスは0から、なければ0）
        /// </summary>
        public int[][] fParam_combined;

        /// <summary>
        /// 退避艦一覧（主艦隊、1-6）　なければnull
        /// （システム的にはいらないので存在するか要確認）
        /// </summary>
        public int[] escape_idx;

        /// <summary>
        /// 退避艦一覧（随伴艦隊、1-6）　なければnull
        /// （システム的にはいらないので存在するか要確認）
        /// </summary>
        public int[] escape_idx_combined;

        /// <summary>
        /// 交戦情報（自艦隊/敵艦隊/交戦形態の順）
        /// 1:単縦 2:複縦 3:輪形 4:梯形 5:単横
        /// 1:同航戦 2:反航戦 3:T字有利 4:T字不利
        /// </summary>
        public int[] formation;

        /// <summary>
        /// 触接機のID
        /// </summary>
        public int[] touch_plane;

        /// <summary>
        /// 照明弾の発射艦（1-6）
        /// </summary>
        public int[] flare_pos;

        /// <summary>
        /// 砲雷撃戦の情報
        /// </summary>
        public HougekiValue hougeki;

        public static SpMidnight fromDynamic(dynamic json)
        {
            var deck_id = 0;
            int.TryParse(json.api_deck_id as string, out deck_id);

            return new SpMidnight()
            {
                deck_id = int.Parse(json.api_deck_id as string),
                ship_ke = json.api_ship_ke.Deserialize<int[]>(),
                ship_lv = json.api_ship_lv.Deserialize<int[]>(),
                nowhps = json.api_nowhps.Deserialize<int[]>(),
                maxhps = json.api_maxhps.Deserialize<int[]>(),
                nowhps_combined = json.api_nowhps_combined.Deserialize<int[]>(),
                maxhps_combined = json.api_maxhps_combined.Deserialize<int[]>(),
                eSlot = json.api_eSlot.Deserialize<int[][]>(),
                eKyouka = json.api_eKyouka.Deserialize<int[][]>(),
                fParam = json.api_fParam.Deserialize<int[][]>(),
                eParam = json.api_eParam.Deserialize<int[][]>(),
                fParam_combined = json.api_fParam_combined.Deserialize<int[][]>(),
                escape_idx = json.api_escape_idx() ? json.api_escape_idx.Deserialize<int[]>() : null,
                escape_idx_combined = json.api_escape_idx_combined() ? json.api_escape_idx_combined.Deserialize<int[]>() : null,
                formation = json.api_formation.Deserialize<int[]>(),
                touch_plane = json.api_touch_plane.Deserialize<int[]>(),
                flare_pos = json.api_flare_pos.Deserialize<int[]>(),
                hougeki = json.api_hougeki() && json.api_hougeki != null ? HougekiValue.fromDynamic(json.api_hougeki) : null
            };
        }
    }
}