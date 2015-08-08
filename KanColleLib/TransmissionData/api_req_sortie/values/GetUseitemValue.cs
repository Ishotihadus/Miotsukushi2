using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class GetUseitemValue
    {
        /// <summary>
        /// アイテムのID
        /// </summary>
        public int useitem_id;

        /// <summary>
        /// 空欄らしい
        /// </summary>
        public string useitem_name;

        public static GetUseitemValue fromDynamic(dynamic json)
        {
            return new GetUseitemValue()
            {
                useitem_id = (int)json.api_useitem_id,
                useitem_name = json.api_useitem_name as string
            };
        }
    }
}
