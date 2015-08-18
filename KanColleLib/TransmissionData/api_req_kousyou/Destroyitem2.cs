using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_kousyou
{
    public class Destroyitem2
    {
        /// <summary>
        /// 得られる資材（燃料/弾薬/鋼材/ボーキ）
        /// </summary>
        public int[] get_material;

        public static Destroyitem2 fromDynamic(dynamic json)
        {
            return new Destroyitem2()
            {
                get_material = json.api_get_material.Deserialize<int[]>()
            };
        }
    }
}
