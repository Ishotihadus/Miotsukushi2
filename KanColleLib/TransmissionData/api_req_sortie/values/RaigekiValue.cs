using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class RaigekiValue
    {
        // "api_raigeki":{"api_frai":[-1,0,0,0,0,0,0],"api_erai":[-1,0,0,0,0,0,6],"api_fdam":[-1,0,0,0,0,0,12],"api_edam":[-1,0,0,0,0,0,0],"api_fydam":[-1,0,0,0,0,0,0],"api_eydam":[-1,0,0,0,0,0,12],"api_fcl":[-1,0,0,0,0,0,0],"api_ecl":[-1,0,0,0,0,0,1]}

        /// <summary>
        /// 自艦隊が雷撃するターゲット（1から6、インデックスは1から）
        /// </summary>
        public int[] frai;

        /// <summary>
        /// 敵艦隊が雷撃するターゲット（1から6、インデックスは1から）
        /// </summary>
        public int[] erai;

        /// <summary>
        /// 自艦隊が受けるダメージ（インデックスは1から）
        /// </summary>
        public double[] fdam;

        /// <summary>
        /// 敵艦隊が受けるダメージ（インデックスは1から）
        /// </summary>
        public double[] edam;

        /// <summary>
        /// 自艦隊が与えるダメージ（インデックスは1から）
        /// </summary>
        public int[] fydam;

        /// <summary>
        /// 敵艦隊が与えるダメージ（インデックスは1から）
        /// </summary>
        public int[] eydam;

        /// <summary>
        /// 自艦隊が与えるダメージのクリティカルフラグ（インデックスは1から）
        /// </summary>
        public int[] fcl;

        /// <summary>
        /// 敵艦隊が与えるダメージのクリティカルフラグ（インデックスは1から）
        /// </summary>
        public int[] ecl;

        public static RaigekiValue fromDynamic(dynamic json)
        {
            return new RaigekiValue()
            {
                frai = json.api_frai.Deserialize<int[]>(),
                erai = json.api_erai.Deserialize<int[]>(),
                fdam = json.api_fdam.Deserialize<double[]>(),
                edam = json.api_edam.Deserialize<double[]>(),
                fydam = json.api_fydam.Deserialize<int[]>(),
                eydam = json.api_eydam.Deserialize<int[]>(),
                fcl = json.api_fcl.Deserialize<int[]>(),
                ecl = json.api_ecl.Deserialize<int[]>()
            };
        }
    }
}
