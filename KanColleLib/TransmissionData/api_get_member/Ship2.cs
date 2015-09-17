using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Ship2
    {
        public List<ShipValue> ships;

        public static Ship2 fromDynamic(dynamic json)
        {
            var ship2 = new Ship2();

            ship2.ships = new List<ShipValue>();

            foreach (var data in json)
                ship2.ships.Add(ShipValue.fromDynamic(data));

            return ship2;
        }
    }
}
