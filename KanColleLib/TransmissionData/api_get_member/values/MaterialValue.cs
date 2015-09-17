using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class MaterialValue
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int member_id;

        /// <summary>
        /// 資材ID
        /// 1:燃料 2:弾薬 3:鋼材 4:ボーキ 5:バーナー 6:バケツ 7:開発資材
        /// </summary>
        public int id;

        /// <summary>
        /// 値
        /// </summary>
        public int value;

        public static MaterialValue fromDynamic(dynamic json)
        {
            var materialvalue = new MaterialValue();
            
            materialvalue.member_id = (int)json.api_member_id;
            materialvalue.id = (int)json.api_id;
            materialvalue.value = (int)json.api_value;

            return materialvalue;
        }

    }
}
