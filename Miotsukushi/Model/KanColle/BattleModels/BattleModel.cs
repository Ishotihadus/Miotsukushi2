using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KanColleLib;
using Miotsukushi.Model.KanColle.BattleModels.EventArgs;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    class BattleModel
    {
        KanColleModel original_model;
        KanColleNotifier kclib;

        public int area_id;
        public int map_id;
        public string map_name;
        public int cell_id;

        public BattleModel(KanColleModel original_model, KanColleNotifier kclib)
        {
            this.original_model = original_model;
            this.kclib = kclib;

            kclib.GetReqcombinedbattleAirbattle += Kclib_GetReqcombinedbattleAirbattle;
            kclib.GetReqcombinedbattleBattle += Kclib_GetReqcombinedbattleBattle;
            kclib.GetReqcombinedbattleBattlewater += Kclib_GetReqcombinedbattleBattlewater;
            kclib.GetReqcombinedbattleMidnightbattle += Kclib_GetReqcombinedbattleMidnightbattle;
            kclib.GetReqcombinedbattleSpMidnight += Kclib_GetReqcombinedbattleSpMidnight;
            kclib.GetReqsortieAirbattle += Kclib_GetReqsortieAirbattle;
            kclib.GetReqsortieBattle += Kclib_GetReqsortieBattle;
            kclib.GetReqbattlemidnightBattle += Kclib_GetReqbattlemidnightBattle;
            kclib.GetReqbattlemidnightSpMidnight += Kclib_GetReqbattlemidnightSpMidnight;

            kclib.GetReqsortieBattleresult += Kclib_GetReqsortieBattleresult;
            kclib.GetReqcombinedbattleBattleresult += Kclib_GetReqcombinedbattleBattleresult;

            kclib.GetReqmapStart += Kclib_GetReqmapStart;
            kclib.GetReqmapNext += Kclib_GetReqmapNext;
        }


        void NextCellDataAppend(KanColleLib.TransmissionData.api_req_map.values.NextCellData next_cell_data)
        {
            area_id = next_cell_data.maparea_id;
            map_id = next_cell_data.mapinfo_no;
            map_name = original_model.mapinfomaster.ContainsKey(area_id * 10 + map_id) ? original_model.mapinfomaster[area_id * 10 + map_id].name : "不明";
            cell_id = next_cell_data.no;
        }

        private void Kclib_GetReqmapNext(object sender, KanColleLib.TransmissionRequest.api_req_map.NextRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_map.Next> response)
        {
            NextCellDataAppend(response.data.next_cell_data);
        }

        private void Kclib_GetReqmapStart(object sender, KanColleLib.TransmissionRequest.api_req_map.StartRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_map.Start> response)
        {
            NextCellDataAppend(response.data.next_cell_data);
        }

        private void Kclib_GetReqcombinedbattleMidnightbattle(object sender, KanColleLib.TransmissionRequest.api_req_combined_battle.MidnightBattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.MidnightBattle> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeCombinedNormalNightBattle(response.data));
        }

        private void Kclib_GetReqcombinedbattleBattlewater(object sender, KanColleLib.TransmissionRequest.api_req_combined_battle.BattleWaterRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.BattleWater> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeWaterCombinedNormalBattle(response.data));
        }

        private void Kclib_GetReqcombinedbattleBattle(object sender, KanColleLib.TransmissionRequest.api_req_combined_battle.BattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.Battle> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeAirCombinedNormalBattle(response.data));
        }

        private void Kclib_GetReqcombinedbattleSpMidnight(object sender, KanColleLib.TransmissionRequest.api_req_combined_battle.SpMidnightRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.SpMidnight> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeCombinedSpNightBattle(response.data));
        }

        private void Kclib_GetReqsortieAirbattle(object sender, KanColleLib.TransmissionRequest.api_req_sortie.AirbattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_sortie.Airbattle> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeNormalAirBattle(response.data));
        }

        private void Kclib_GetReqcombinedbattleAirbattle(object sender, KanColleLib.TransmissionRequest.api_req_combined_battle.AirbattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.Airbattle> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeAirCombinedAirBattle(response.data));
        }

        private void Kclib_GetReqbattlemidnightBattle(object sender, KanColleLib.TransmissionRequest.api_req_battle_midnight.BattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_battle_midnight.Battle> response)
        {
            var result = BattleAnalyzer.AnalyzeNormalNightBattle(response.data);
            OnBattleAnalyzed(result);
        }

        private void Kclib_GetReqbattlemidnightSpMidnight(object sender, KanColleLib.TransmissionRequest.api_req_battle_midnight.SpMidnightRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_battle_midnight.SpMidnight> response)
        {
            OnBattleAnalyzed(BattleAnalyzer.AnalyzeNormalSpNightBattle(response.data));
        }

        private void Kclib_GetReqsortieBattle(object sender, KanColleLib.TransmissionRequest.api_req_sortie.BattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_sortie.Battle> response)
        {
            var result = BattleAnalyzer.AnalyzeNormalBattle(response.data);
            OnBattleAnalyzed(result);
        }

        private void Kclib_GetReqsortieBattleresult(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_sortie.Battleresult> response)
        {
            OnGetBattleResult(new GetBattleResultEventArgs()
            {
                rank = response.data.win_rank,
                has_get_ship = response.data.get_flag[1],
                get_ship_name = response.data.get_ship != null ? response.data.get_ship.ship_name : ""
            });
        }

        private void Kclib_GetReqcombinedbattleBattleresult(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.Battleresult> response)
        {
            OnGetBattleResult(new GetBattleResultEventArgs()
            {
                rank = response.data.win_rank,
                has_get_ship = response.data.get_flag[1],
                get_ship_name = response.data.get_ship != null ? response.data.get_ship.ship_name : ""
            });
        }

        /// <summary>
        /// 戦闘が解析された際に呼び出されます
        /// </summary>
        public event BattleAnalyzedEventHandler BattleAnalyzed;
        public delegate void BattleAnalyzedEventHandler(object sender, BattleAnalyzedEventArgs e);
        protected virtual void OnBattleAnalyzed(BattleAnalyzedEventArgs e) { if (BattleAnalyzed != null) { BattleAnalyzed(this, e); } }

        /// <summary>
        /// 戦闘の結果を取得した際に呼び出されます
        /// </summary>
        public event GetBattleResultEventHandler GetBattleResult;
        public delegate void GetBattleResultEventHandler(object sender, GetBattleResultEventArgs e);
        protected virtual void OnGetBattleResult(GetBattleResultEventArgs e) { if (GetBattleResult != null) { GetBattleResult(this, e); } }
    }
}
