using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_quest
{
    public class BonusValue
    {
        /// <summary>
        /// ボーナスの種類
        /// 0:なし 1:資材 2:艦隊解放 3:家具箱 4:大型建造解放 11:艦娘 12:装備 13:その他アイテム 14:家具 15:機種転換
        /// </summary>
        public int type;

        /// <summary>
        /// 個数
        /// </summary>
        public int count;

        /// <summary>
        /// アイテム（typeによって中身が違うからそのまんま）
        /// </summary>
        public dynamic item;

        public static BonusValue fromDynamic(dynamic json)
        {
            return new BonusValue()
            {
                type = (int)json.api_type,
                count = (int)json.api_count,
                item = json.api_item
            };
        }
    }
}
