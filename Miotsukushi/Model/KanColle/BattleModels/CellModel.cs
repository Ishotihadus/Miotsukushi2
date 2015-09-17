using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    public enum CellType
    {
        Unknown, Start, Battle, NightSpBattle, NightToDayBattle, AirBattle, AirSearch, SuccessShipGuard, RouteChoice, NoBattle, Supply, Happening
    }

    class CellModel
    {

        public int CellNo;

        public int BossCellNo;

        /// <summary>
        /// 終点かどうか
        /// </summary>
        public bool HasNoWay;

        public bool IsBossBattle;

        public CellType CellType;

        /// <summary>
        /// air_search: 0失敗 1成功 2大成功
        /// no_battle: 0気のせいだった　1敵影を見ず
        /// supply: 手に入れたアイテムのID
        /// happening: 失ったアイテムのID
        /// </summary>
        public int CellEventContentId;

        /// <summary>
        /// supply: 手に入れたアイテムの量
        /// happening: 失ったアイテムの量
        /// </summary>
        public int CellEventContentValue;

        /// <summary>
        /// route_choice: 次に進めるマス
        /// </summary>
        public int[] CellEventContentValues;
    }
}
