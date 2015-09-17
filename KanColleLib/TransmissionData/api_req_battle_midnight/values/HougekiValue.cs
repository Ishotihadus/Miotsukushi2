using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_battle_midnight.values
{
    public class HougekiValue
    {   
        /// <summary>
        /// 攻撃を与えた艦（0から始まるインデックス、値は1から12）
        /// </summary>
        public int[] at_list;

        /// <summary>
        /// 攻撃の種類（0から始まるインデックス）
        /// 0:通常砲撃　1:連撃　2:カットイン(主砲/魚雷)　3:カットイン(魚雷/魚雷)　4:カットイン(主砲/主砲/副砲)　5:カットイン(主砲/主砲/主砲)　6:???　7:空撃　8:爆雷攻撃　9:雷撃　10:ロケット砲撃
        /// 7以上は通常はクライアントで判別
        /// </summary>
        public int[] sp_list;

        /// <summary>
        /// 攻撃を受けた艦（0から始まるインデックス、1から12）
        /// </summary>
        public int[][] df_list;

        /// <summary>
        /// 使用した装備（なければ-1、0から始まるインデックス）
        /// </summary>
        public int[][] si_list;

        /// <summary>
        /// クリティカル情報（0から始まるインデックス）
        /// </summary>
        public int[][] cl_list;

        /// <summary>
        /// ダメージ情報（0から始まるインデックス）
        /// </summary>
        public int[][] damage;

        static int[] DynamicParseArrayOnce(dynamic json)
        {
            var list = new List<int>();
            var i = 0;
            foreach (var data in json)
            {
                if (i != 0)
                    list.Add((int)data);
                ++i;
            }
            return list.ToArray();
        }

        static int[][] DynamicParseArrayTwice(dynamic json)
        {
            var list = new List<int[]>();
            var i = 0;
            foreach(var data in json)
            {
                if (i != 0)
                    list.Add(data.Deserialize<int[]>());
                ++i;
            }
            return list.ToArray();
        }

        public static HougekiValue fromDynamic(dynamic json)
        {
            return new HougekiValue()
            {
                at_list = DynamicParseArrayOnce(json.api_at_list),
                sp_list = DynamicParseArrayOnce(json.api_sp_list),
                df_list = DynamicParseArrayTwice(json.api_df_list),
                si_list = DynamicParseArrayTwice(json.api_si_list),
                cl_list = DynamicParseArrayTwice(json.api_cl_list),
                damage = DynamicParseArrayTwice(json.api_damage)
            };
        }
    }
}
