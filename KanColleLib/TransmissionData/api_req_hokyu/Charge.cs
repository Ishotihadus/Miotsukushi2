using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_hokyu
{
    public class Charge
    {
        /// <summary>
        /// 補給後の艦娘の簡易データ
        /// </summary>
        public List<values.ShipValue> ship;

        /// <summary>
        /// 補給後の資源（燃料/弾薬/鋼材/ボーキ）
        /// </summary>
        public int[] material;

        /// <summary>
        /// 使用したボーキの量
        /// </summary>
        public int use_bou;

        public static Charge fromDynamic(dynamic json)
        {
            var charge = new Charge();

            charge.ship = new List<values.ShipValue>();
            foreach (var data in json.api_ship)
                charge.ship.Add(values.ShipValue.fromDynamic(data));

            charge.material = json.api_material.Deserialize<int[]>();

            charge.use_bou = (int)json.api_use_bou;

            return charge;
        }
    }
}
