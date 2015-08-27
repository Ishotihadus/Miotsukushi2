using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_member
{
    public class GetIncentive
    {
        /// <summary>
        /// もらえる褒章の個数
        /// </summary>
        public int count;

        // api_item があるという噂
        public List<values.ItemValue> item;

        public static GetIncentive fromDynamic(dynamic json)
        {
            var ret = new GetIncentive();
            ret.count = (int)json.api_count;
            ret.item = new List<values.ItemValue>();
            if (json.item())
                foreach (var item in json.item)
                    ret.item.Add(values.ItemValue.fromDynamic(item));
            return ret;
        }

        /*
https://twitter.com/andanteyk/status/636829185617477632

{
    "api_result": 1,
    "api_result_msg": "成功",
    "api_data": {
        "api_count": 2,
        "api_item": [
            {
                "api_mode": 3,
                "api_type": 2,
                "api_mst_id": 139,
                "api_getmes": "七月作戦 主力艦隊第三群(101～500位)<br>15.2cm連装砲改 を獲得しました！"
            },
            {
                "api_mode": 3,
                "api_type": 2,
                "api_mst_id": 148,
                "api_getmes": "七月作戦 主力艦隊第三群(101～500位)<br>試製南山 を獲得しました！"
            }
        ]
    }
}

        */
    }
}
