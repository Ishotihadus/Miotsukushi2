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
        /// 自艦隊が雷撃するターゲット
        /// </summary>
        public int[] frai;

        /// <summary>
        /// 敵艦隊が雷撃するターゲット
        /// </summary>
        public int[] erai;

        /// <summary>
        /// 自艦隊が受けるダメージ
        /// </summary>
        public double[] fdam;

        /// <summary>
        /// 敵艦隊が受けるダメージ
        /// </summary>
        public double[] edam;

        /// <summary>
        /// 自艦隊が与えるダメージ
        /// </summary>
        public int[] fydam;

        /// <summary>
        /// 敵艦隊が与えるダメージ
        /// </summary>
        public int[] eydam;

        /// <summary>
        /// 自艦隊が与えるダメージのクリティカルフラグ
        /// </summary>
        public int[] fcl;

        /// <summary>
        /// 敵艦隊が与えるダメージのクリティカルフラグ
        /// </summary>
        public int[] ecl;
    }
}
