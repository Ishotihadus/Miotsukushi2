using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_hokyu.values
{
    public class ShipValue
    {
        /// <summary>
        /// 艦娘ID
        /// </summary>
        public int id;

        /// <summary>
        /// 現在の燃料
        /// </summary>
        public int fuel;

        /// <summary>
        /// 現在の弾薬
        /// </summary>
        public int bull;

        /// <summary>
        /// 搭載数
        /// </summary>
        public int[] onslot;

        public static ShipValue fromDynamic(dynamic json)
        {
            ShipValue ship = new ShipValue();

            ship.id = (int)json.api_id;
            ship.onslot = json.api_onslot.Deserialize<int[]>();
            ship.fuel = (int)json.api_fuel;
            ship.bull = (int)json.api_bull;

            return ship;
        }
    }
}
