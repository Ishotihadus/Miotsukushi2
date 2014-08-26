using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 海域マスターデータ
    /// </summary>
    public class MstMaparea
    {
        /// <summary>
        /// 海域ID
        /// </summary>
        public int id;

        /// <summary>
        /// 海域名
        /// </summary>
        public string name;

        /// <summary>
        /// 海域のタイプ（0が通常海域、1がイベント海域）
        /// </summary>
        public int type;

        public static MstMaparea fromDynamic(dynamic json)
        {
            MstMaparea maparea = new MstMaparea();

            maparea.id = (int)json.api_id;
            maparea.name = json.api_name as string;
            maparea.type = (int)json.api_type;

            return maparea;
        }

        // {"api_id":1,"api_name":"\u93ae\u5b88\u5e9c\u6d77\u57df","api_type":0}
    }
}
