using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class ShipDeck
    {
        public List<DeckValue> deck_data;
        public List<ShipValue> ship_data;

        public static ShipDeck fromDynamic(dynamic json)
        {
            ShipDeck ret = new ShipDeck();

            ret.deck_data = new List<DeckValue>();
            foreach (var data in json.api_deck_data)
                ret.deck_data.Add(DeckValue.fromDynamic(data));
            ret.ship_data = new List<ShipValue>();
            foreach (var data in json.api_ship_data)
                ret.ship_data.Add(ShipValue.fromDynamic(data));

            return ret;
        }

        
    }
}
