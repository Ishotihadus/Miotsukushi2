using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class SupportHouraiValue
    {
        /*
        "api_support_hourai":{
        "api_deck_id":3,
        "api_ship_id":[3906,51,14896,14898,14895,0],
        "api_undressing_flag":[0,0,0,0,0,0],
        "api_cl_list":[-1,0,0,1,0,0,0],
        "api_damage":[-1,0,0,10,0,0,0]
        }
        */

        /// <summary>
        /// 艦隊ID
        /// </summary>
        public int deck_id;

        /// <summary>
        /// 艦娘ID（0から始まる、いなければ0）
        /// </summary>
        public int[] ship_id;

        /// <summary>
        /// ダメージを受けたかどうか（0から始まる）
        /// </summary>
        public bool[] undressing_flag;

        /// <summary>
        /// クリティカル等のフラグ（1から始まる）
        /// </summary>
        public int[] cl_list;

        /// <summary>
        /// 相手に与えたダメージ（1から始まる）
        /// </summary>
        public double[] damage;

        public static SupportHouraiValue fromDynamic(dynamic json)
        {
            return new SupportHouraiValue()
            {
                deck_id = (int)json.api_deck_id,
                ship_id = json.api_ship_id.Deserialize<int[]>(),
                undressing_flag = ((int[])(json.api_undressing_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                cl_list = json.api_cl_list.Deserialize<int[]>(),
                damage = json.api_damage.Deserialize<double[]>()
            };
        }
    }
}
