using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class ShipData
    {
        /// <summary>
        /// 艦娘ID
        /// </summary>
        public int shipid;

        /// <summary>
        /// 艦のID
        /// </summary>
        public int characterid;

        /// <summary>
        /// レベル
        /// </summary>
        public int level;

        public override bool Equals(object obj)
        {
            if (!(obj is ShipData) || obj == null)
                return false;

            var s = obj as ShipData;

            return shipid == s.shipid && characterid == s.characterid && level == s.level;
        }

        public static ShipData FromKanColleLib(KanColleLib.TransmissionData.api_get_member.values.ShipValue data)
        {
            return new ShipData()
            {
                shipid = data.id,
                characterid = data.ship_id,
                level = data.lv
            };
        }
    }
}
