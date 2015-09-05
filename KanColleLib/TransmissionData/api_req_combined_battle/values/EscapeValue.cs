using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_combined_battle.values
{
    public class EscapeValue
    {
        /// <summary>
        /// 大破艦の一覧
        /// </summary>
        public int[] escape_idx;

        /// <summary>
        /// 護衛に付き添える艦の一覧
        /// </summary>
        public int[] tow_idx;

        public static EscapeValue fromDynamic(dynamic json)
        {
            var ret = new EscapeValue();

            ret.escape_idx = json.api_escape_idx() ? json.api_escape_idx.Deserialize<int[]>() : null;
            ret.tow_idx = json.api_tow_idx() ? json.api_tow_idx.Deserialize<int[]>() : null;

            return ret;
        }
    }
}
