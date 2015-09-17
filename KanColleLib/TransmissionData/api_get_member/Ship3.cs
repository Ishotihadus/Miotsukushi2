using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Ship3
    {
        public Ship2 ship_data;
        public Deck deck_data;
        public Unsetslot slot_data;

        public static Ship3 fromDynamic(dynamic json)
        {
            var ship3 = new Ship3();

            ship3.ship_data = Ship2.fromDynamic(json.api_ship_data);
            ship3.deck_data = Deck.fromDynamic(json.api_deck_data);
            ship3.slot_data = Unsetslot.fromDynamic(json.api_slot_data);

            return ship3;
        }
    }
}
