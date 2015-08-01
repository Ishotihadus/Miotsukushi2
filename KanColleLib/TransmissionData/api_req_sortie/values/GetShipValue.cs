using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class GetShipValue
    {
        /// <summary>
        /// 艦娘ID
        /// （種類IDであって配属後通しIDではない）
        /// </summary>
        public int ship_id;

        /// <summary>
        /// 艦種
        /// </summary>
        public string ship_type;

        /// <summary>
        /// 名前
        /// </summary>
        public string ship_name;

        /// <summary>
        /// 獲得時メッセージ
        /// </summary>
        public string ship_getmes;

        public static GetShipValue fromDynamic(dynamic json)
        {
            return new GetShipValue()
            {
                ship_id = (int)json.api_ship_id,
                ship_type = json.api_ship_type as string,
                ship_name = json.api_ship_name as string,
                ship_getmes = json.api_ship_getmes as string
            };
        }
        // api_get_ship":{"api_ship_id":29,"api_ship_type":"\u99c6\u9010\u8266","api_ship_name":"\u6587\u6708","api_ship_getmes":"\u3042\u305f\u3057\u3001\u6587\u6708\u3063\u3066\u3044\u3046\u306e\u3002<br>\u3088\u308d\u3057\u304f\u3045\uff5e\u3002"
    }
}
