using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_mission
{
    public class Start
    {
        /// <summary>
        /// 遠征完了時刻
        /// </summary>
        public long complatetime;

        /// <summary>
        /// 遠征完了時刻
        /// </summary>
        public string complatetime_str;

        public static Start fromDynamic(dynamic json)
        {
            var start = new Start();
            start.complatetime = (long)json.api_complatetime;
            start.complatetime_str = json.api_complatetime_str as string;

            return start;
        }

    }
}
