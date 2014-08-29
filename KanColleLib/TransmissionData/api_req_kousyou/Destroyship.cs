using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_kousyou
{
    public class Destroyship
    {
        /// <summary>
        /// 解体後の資材情報（燃料/弾薬/鋼材/ボーキ）
        /// </summary>
        public int[] material;

        public Destroyship fromDynamic(dynamic json)
        {
            Destroyship destroyship = new Destroyship();
            destroyship.material = json.api_material.Deserialize<int[]>();
            return destroyship;
        }
    }
}
