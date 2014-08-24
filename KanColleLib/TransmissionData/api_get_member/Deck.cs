using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Deck
    {
        public List<DeckValue> decks;

        public static Deck fromDynamic(dynamic json)
        {
            Deck deck = new Deck();

            deck.decks = new List<DeckValue>();
            foreach (var data in json)
                deck.decks.Add(DeckValue.fromDynamic(data));

            return deck;
        }
    }
}
