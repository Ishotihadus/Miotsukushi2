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

        public BattleModel(KanColleModel original_model, KanColleNotifier kclib)
        {
            this.original_model = original_model;
            this.kclib = kclib;

            kclib.GetReqsortieBattleresult += Kclib_GetReqsortieBattleresult;
            kclib.GetReqsortieBattle += Kclib_GetReqsortieBattle;
            kclib.GetReqbattlemidnightBattle += Kclib_GetReqbattlemidnightBattle;
            kclib.GetReqcombinedbattleBattleresult += Kclib_GetReqcombinedbattleBattleresult;
        }

        private void Kclib_GetReqbattlemidnightBattle(object sender, KanColleLib.TransmissionRequest.api_req_battle_midnight.BattleRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_battle_midnight.Battle> response)
        {
            var result = BattleAnalyzer.AnalyzeNormalNightBattle(response.data);
            OnBattleAnalyzed(result);
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
