using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class EnemyInfoValue
    {
        /// <summary>
        /// ?
        /// </summary>
        public string level;

        /// <summary>
        /// ?
        /// </summary>
        public string rank;

        /// <summary>
        /// 艦隊名
        /// </summary>
        public string deck_name;

        public static EnemyInfoValue fromDynamic(dynamic json)
        {
            return new EnemyInfoValue()
            {
                level = json.api_level as string,
                rank = json.api_rank as string,
                deck_name = json.api_deck_name as string
            };
        }

        // "api_enemy_info":{"api_level":"","api_rank":"","api_deck_name":"\u6575\u4e3b\u529b\u8266\u968a"},
    }
}
