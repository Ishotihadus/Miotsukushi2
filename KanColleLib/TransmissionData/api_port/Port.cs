using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member;

namespace KanColleLib.TransmissionData.api_port
{
    public class Port
    {
        public Material material;
        public Deck deck_port;
        public NDock ndock;
        public Ship2 ship;
        public Basic basic;
        public Log log;

        /// <summary>
        /// 連合艦隊のフラグ
        /// 0:通常艦隊 1:連合艦隊機動部隊 2:連合艦隊水上部隊
        /// </summary>
        public int combined_flag;

        /// <summary>
        /// 母港BGMID
        /// </summary>
        public int p_bgm_id;

        /// <summary>
        /// 同時遂行可能任務数
        /// </summary>
        public int parallel_quest_count;

        public static Port fromDynamic(dynamic json)
        {
            Port port = new Port();

            port.material = Material.fromDynamic(json.api_material);
            port.deck_port = Deck.fromDynamic(json.api_deck_port);
            port.ndock = NDock.fromDynamic(json.api_ndock);
            port.ship = Ship2.fromDynamic(json.api_ship);
            port.basic = Basic.fromDynamic(json.api_basic);
            port.log = Log.fromDynamic(json.api_log);
            if (json.api_combined_flag())
                port.combined_flag = (int)json.api_combined_flag;
            else
                port.combined_flag = 0;
            port.p_bgm_id = (int)json.api_p_bgm_id;
            port.parallel_quest_count = (int)json.api_parallel_quest_count;

            return port;
        }
    }
}
