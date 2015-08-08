using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class HappeningValue
    {
        /// <summary>
        /// 1
        /// </summary>
        public int type;

        /// <summary>
        /// 量
        /// </summary>
        public int count;

        /// <summary>
        /// 4
        /// </summary>
        public int usemst;

        /// <summary>
        /// 資源ID 1:燃料 2:弾薬 3:鋼材 4:ボーキサイト
        /// </summary>
        public int mst_id;

        /// <summary>
        /// 電探による回避があったか
        /// </summary>
        public bool dentan;

        public static HappeningValue fromDynamic(dynamic json)
        {
            return new HappeningValue()
            {
                type = (int)json.api_type,
                count = (int)json.api_count,
                usemst = (int)json.api_usemst,
                mst_id = (int)json.api_mst_id,
                dentan = (int)json.api_dentan == 1
            };
        }
    }
}
