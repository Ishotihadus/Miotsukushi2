using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class DeckValue
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int member_id;

        /// <summary>
        /// デッキID（艦隊番号、第1艦隊なら1）
        /// </summary>
        public int id;

        /// <summary>
        /// 艦隊名
        /// </summary>
        public string name;

        /// <summary>
        /// 艦隊名ID（なぜstringなのか?）
        /// </summary>
        public string name_id;

        /// <summary>
        /// 遠征情報（ステータス/遠征ID/帰投時刻/未使用）
        /// ステータスは0:母港にいる 1:遠征中 2:遠征完了 3:遠征強制帰投中
        /// </summary>
        public long[] mission;
        
        /// <summary>
        /// 不明（未使用）
        /// </summary>
        public string flagship;

        /// <summary>
        /// 所属艦（空きの場合は-1）
        /// </summary>
        public int[] ship;

        public static DeckValue fromDynamic(dynamic json)
        {
            DeckValue deck = new DeckValue();

            deck.member_id = (int)json.api_member_id;
            deck.id = (int)json.api_id;
            deck.name = json.api_name as string;
            deck.name_id = json.api_name_id as string;
            deck.mission = json.api_mission.Deserialize<long[]>();
            deck.flagship = json.api_flagship as string;
            deck.ship = json.api_ship.Deserialize<int[]>();

            return deck;
        }

    }
}
