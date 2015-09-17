using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.BattleModels.EventArgs
{
    class GetBattleResultEventArgs : System.EventArgs
    {
        /// <summary>
        /// 勝利ランク
        /// </summary>
        public string Rank;

        /// <summary>
        /// 艦娘を手に入れたか
        /// </summary>
        public bool HasGetShip;

        /// <summary>
        /// 手に入れた艦娘の名前
        /// </summary>
        public string GetShipName;
    }
}
