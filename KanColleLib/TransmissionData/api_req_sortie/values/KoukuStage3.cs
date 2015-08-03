using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class KoukuStage3
    {
        // "api_stage3":{"api_frai_flag":[-1,0,0,0,0,0,0],"api_erai_flag":[-1,0,1,0,1,0,0],"api_fbak_flag":[-1,0,0,0,0,0,0],"api_ebak_flag":[-1,0,0,0,0,0,0],"api_fcl_flag":[-1,0,0,0,0,0,0],"api_ecl_flag":[-1,0,0,0,0,0,0],"api_fdam":[-1,0,0,0,0,0,0],"api_edam":[-1,0,0,0,57,0,0]}

        /// <summary>
        /// 自艦隊の被雷撃状況（1から始まる）
        /// </summary>
        public bool[] frai_flag;

        /// <summary>
        /// 敵艦隊の被雷撃状況（1から始まる）
        /// </summary>
        public bool[] erai_flag;

        /// <summary>
        /// 自艦隊の被爆撃状況（1から始まる）
        /// </summary>
        public bool[] fbak_flag;

        /// <summary>
        /// 敵艦隊の被爆撃状況（1から始まる）
        /// </summary>
        public bool[] ebak_flag;

        /// <summary>
        /// 自艦隊のクリティカル等情報（1から始まる）
        /// </summary>
        public int[] fcl_flag;

        /// <summary>
        /// 敵艦隊のクリティカル等情報（1から始まる）
        /// </summary>
        public int[] ecl_flag;

        /// <summary>
        /// 自艦隊の受けたダメージ（1から始まる）
        /// </summary>
        public double[] fdam;

        /// <summary>
        /// 敵艦隊の受けたダメージ（1から始まる）
        /// </summary>
        public double[] edam;

        public static KoukuStage3 fromDynamic(dynamic json)
        {
            return new KoukuStage3()
            {
                frai_flag = ((int[])(json.api_frai_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                erai_flag = ((int[])(json.api_erai_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                fbak_flag = ((int[])(json.api_fbak_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                ebak_flag = ((int[])(json.api_ebak_flag.Deserialize<int[]>())).Select(_ => _ == 1).ToArray(),
                fcl_flag = json.api_fcl_flag.Deserialize<int[]>(),
                ecl_flag = json.api_ecl_flag.Deserialize<int[]>(),
                fdam = json.api_fdam.Deserialize<double[]>(),
                edam = json.api_edam.Deserialize<double[]>(),
            };
        }
    }
}
