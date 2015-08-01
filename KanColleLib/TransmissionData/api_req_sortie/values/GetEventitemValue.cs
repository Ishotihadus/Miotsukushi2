using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_sortie.values
{
    public class GetEventitemValue
    {
        /// <summary>
        /// 獲得種
        /// 1:アイテム　2:艦娘　3:装備 
        /// </summary>
        public int type;
        
        public int id;
        public int? value;

        public static GetEventitemValue fromDynamic(dynamic json)
        {
            return new GetEventitemValue()
            {
                type = (int)json.api_type,
                id = (int)json.api_id,
                value = json.api_value() ? (int)json.api_value : (int?)null
            };
        }
    }
}
