using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class HougekiValue
    {
        // "api_hougeki2":{"api_at_list":[-1,1,7,2,3,4],"api_at_type":[-1,2,0,2,0,0],"api_df_list":[-1,[8,8],[5],[11,11],[11],[7]],"api_si_list":[-1,[9,9],[-1],[9,9],[50],[6]],"api_cl_list":[-1,[2,1],[1],[1,1],[1],[1]],"api_damage":[-1,[129.1,94.1],[21],[3.1,2.1],[55],[9]]}
        // 変数の構造上、元データでは1からだが、0からとした。
        
        /// <summary>
        /// 攻撃を与えた艦（0から始まるインデックス、値は1から12）
        /// </summary>
        public int[] at_list;

        /// <summary>
        /// 攻撃の種類（0から始まるインデックス）
        /// 昼戦なら 0:通常砲撃 1:レーザー攻撃 2:AOS連撃 3～6:AOSカットイン
        /// 夜戦なら 0:通常砲撃 1:連撃 2～5:カットイン
        /// thanks to: http://www.ptt.cc/bbs/KanColle/M.1398354350.A.489.html
        /// </summary>
        public int[] at_type;

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
            int i = 0;
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
            int i = 0;
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
                at_type = DynamicParseArrayOnce(json.api_at_type),
                df_list = DynamicParseArrayTwice(json.api_df_list),
                si_list = DynamicParseArrayTwice(json.api_si_list),
                cl_list = DynamicParseArrayTwice(json.api_cl_list),
                damage = DynamicParseArrayTwice(json.api_damage)
            };
        }
    }
}
