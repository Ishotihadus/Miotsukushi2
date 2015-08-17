using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_combined_battle.values
{
    public class CombinedKoukuValue : KanColleLib.TransmissionData.api_req_sortie.values.KoukuValue
    {
        /// <summary>
        /// ステージ3（雷撃・爆撃）の連合艦隊随伴艦状況
        /// </summary>
        public KoukuStage3Combined stage3_combined;

        new public static CombinedKoukuValue fromDynamic(dynamic json)
        {
            return new CombinedKoukuValue()
            {
                plane_from = json.api_plane_from.Deserialize<int[][]>(),
                stage1 = json.api_stage1() && json.api_stage1 != null ? KanColleLib.TransmissionData.api_req_sortie.values.KoukuStage1.fromDynamic(json.api_stage1) : null,
                stage2 = json.api_stage2() && json.api_stage2 != null ? KanColleLib.TransmissionData.api_req_sortie.values.KoukuStage2.fromDynamic(json.api_stage2) : null,
                stage3 = json.api_stage3() && json.api_stage3 != null ? KanColleLib.TransmissionData.api_req_sortie.values.KoukuStage3.fromDynamic(json.api_stage3) : null,
                stage3_combined = json.api_stage3_combined() && json.api_stage3_combined != null ? KoukuStage3Combined.fromDynamic(json.api_stage3_combined) : null
            };
        }
    }
}
