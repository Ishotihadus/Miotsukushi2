using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_combined_battle.values
{
    public class KoukuStage3Combined
    {
        /// <summary>
        /// 随伴艦隊の被雷撃状況（1から始まる）
        /// </summary>
        public bool[] frai_flag;

        /// <summary>
        /// 随伴艦隊の被爆撃状況（1から始まる）
        /// </summary>
        public bool[] fbak_flag;

        /// <summary>
        /// 随伴艦隊のクリティカル等情報（1から始まる）
        /// </summary>
        public int[] fcl_flag;

        /// <summary>
        /// 随伴艦隊の受けたダメージ（1から始まる）
        /// </summary>
        public double[] fdam;

        public static KoukuStage3Combined fromDynamic(dynamic json)
        {
            return new KoukuStage3Combined()
            {
                frai_flag = ((int[])(json.api_frai_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                fbak_flag = ((int[])(json.api_fbak_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                fcl_flag = json.api_fcl_flag.Deserialize<int[]>(),
                fdam = json.api_fdam.Deserialize<double[]>(),
            };
        }
    }
}
