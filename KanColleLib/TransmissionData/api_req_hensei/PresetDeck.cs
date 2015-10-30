using System;
using System.Collections.Generic;
using KanColleLib.TransmissionData.api_req_hensei.values;

namespace KanColleLib.TransmissionData.api_req_hensei
{
    public class PresetDeck
    {
        public int max_num;

        /// <summary>
        /// 本来はデータ構造が入るが、扱いにくすぎるのでListに整形してある
        /// ぐちゃぐちゃAPIを書くな。
        /// </summary>
        public List<PresetDeckValue> deck;
        
        public static PresetDeck fromDynamic(dynamic json)
        {
            var deck = new PresetDeck();
            deck.max_num = (int) json.api_max_num;
            deck.deck = new List<PresetDeckValue>();

            foreach (KeyValuePair<string, dynamic> data in json.api_deck)
            {
                deck.deck.Add(PresetDeckValue.fromDynamic(data.Value));
            }

            return deck;
        }

        // svdata=
        // {"api_result":1,"api_result_msg":"\u6210\u529f","api_data":
        //   {"api_max_num":3,"api_deck":
        //     {"1":
        //       {"api_preset_no":1,
        //        "api_name":"\u96fb\u6c17\u96fb\u5b50\u60c5\u5831\u5b9f\u9a13\u6f14\u7fd2\u7b2c\u4e8c",
        //        "api_name_id":"140749500",
        //        "api_ship":[1490,-1,-1,-1,-1,-1]
        //       }
        //     }
        //   }
        // }
    }
}
