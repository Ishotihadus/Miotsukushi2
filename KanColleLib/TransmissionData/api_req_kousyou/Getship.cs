using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member;
using KanColleLib.TransmissionData.api_get_member.values;
using KanColleLib.TransmissionData.api_req_kousyou.values;

namespace KanColleLib.TransmissionData.api_req_kousyou
{
    public class Getship
    {
        /// <summary>
        /// 手に入れた艦娘の割り当てID
        /// </summary>
        public int id;

        /// <summary>
        /// 艦種ID
        /// </summary>
        public int ship_id;

        /// <summary>
        /// 手に入れたあとの建造ドックの状況
        /// </summary>
        public KDock kdock;

        /// <summary>
        /// 手に入れた艦娘の情報
        /// </summary>
        public ShipValue ship;

        /// <summary>
        /// 手に入れた艦娘に付属している装備アイテムの情報
        /// </summary>
        public List<GetshipSlotitemValue> slotitem;

        public static Getship fromDynamic(dynamic json)
        {
            Getship getship = new Getship();

            getship.id = (int)json.api_id;
            getship.ship_id = (int)json.api_ship_id;
            getship.kdock = KDock.fromDynamic(json.api_kdock);
            getship.ship = ShipValue.fromDynamic(json.api_ship);

            getship.slotitem = new List<GetshipSlotitemValue>();
            if(json.api_slotitem != null)
                foreach (var data in json.api_slotitem)
                    getship.slotitem.Add(GetshipSlotitemValue.fromDynamic(data));

            return getship;
        }
    }
}
