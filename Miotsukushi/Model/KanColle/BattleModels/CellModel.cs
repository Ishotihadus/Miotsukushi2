using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    class CellModel
    {
        public enum CellType
        {
            unknown, start, battle, night_sp_battle, night_to_day_battle, air_battle, air_search, success_ship_guard, route_choice, no_battle, supply, happening
        }

        public int cell_no;

        public int boss_cell_no;

        /// <summary>
        /// 終点かどうか
        /// </summary>
        public bool has_no_way;

        public bool is_boss_battle;

        public CellType cell_type;

        /// <summary>
        /// air_search: 0失敗 1成功 2大成功
        /// no_battle: 0気のせいだった　1敵影を見ず
        /// supply: 手に入れたアイテムのID
        /// happening: 失ったアイテムのID
        /// </summary>
        public int cell_event_content_id;

        /// <summary>
        /// supply: 手に入れたアイテムの量
        /// happening: 失ったアイテムの量
        /// </summary>
        public int cell_event_content_value;

        /// <summary>
        /// route_choice: 次に進めるマス
        /// </summary>
        public int[] cell_event_content_values;
    }
}
