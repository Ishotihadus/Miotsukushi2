using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_quest
{
    public class Clearitemget
    {
        /// <summary>
        /// 入手した燃料/弾薬/鋼材/ボーキ
        /// </summary>
        public int[] material;

        /// <summary>
        /// ボーナスの数
        /// </summary>
        public int bonus_count;

        /// <summary>
        /// ボーナス
        /// </summary>
        public List<BonusValue> bonus;

        public static Clearitemget fromDynamic(dynamic json)
        {
            Clearitemget ret = new Clearitemget();
            ret.material = json.api_material.Deserialize<int[]>();
            ret.bonus_count = (int)json.api_bounus_count;
            ret.bonus = new List<BonusValue>();
            foreach(var bonus in json.api_bounus)
            {
                ret.bonus.Add(BonusValue.fromDynamic(bonus));
            }
            return ret;
        }
    }
}
