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
        Material material;
        Deck deck_port;
        NDock ndock;
        Ship2 ship;
        Basic basic;
        Log log;
        bool combined_flag;

        public static Port fromDynamic(dynamic json)
        {
            Port port = new Port();

            port.material = Material.fromDynamic(json.api_material);
            port.deck_port = Deck.fromDynamic(json.api_deck_port);
            port.ndock = NDock.fromDynamic(json.api_ndock);
            port.ship = Ship2.fromDynamic(json.api_ship);
            port.basic = Basic.fromDynamic(json.api_basic);
            port.log = Log.fromDynamic(json.api_log);
            port.combined_flag = (int)json.api_combined_flag == 1;

            return port;
        }
    }
}
