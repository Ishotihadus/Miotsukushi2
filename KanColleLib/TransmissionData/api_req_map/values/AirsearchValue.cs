using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class AirsearchValue
    {
        /// <summary>
        /// 発艦した索敵機の種別
        /// 0:なし　1:大型飛行艇　2:水上偵察機
        /// </summary>
        public int plane_type;

        /// <summary>
        /// 航空偵察の結果　0:失敗　1:成功　2:大成功
        /// </summary>
        public int result;

        public static AirsearchValue fromDynamic(dynamic json)
        {
            return new AirsearchValue()
            {
                plane_type = (int)json.api_plane_type,
                result = (int)json.api_result
            };
        }
    }
}
