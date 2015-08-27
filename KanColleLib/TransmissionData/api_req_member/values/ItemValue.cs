using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_member.values
{
    public class ItemValue
    {
        /// <summary>
        /// 1=事前登録?, 2=招待?, 3=月次褒賞/プレゼント
        /// </summary>
        public int mode;

        /// <summary>
        /// 1=艦船?, 2=装備, 3=アイテム, 4=資源?, 5=家具
        /// </summary>
        public int type;

        /// <summary>
        /// ID
        /// </summary>
        public int mst_id;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string getmes;

        public static ItemValue fromDynamic(dynamic json)
        {
            return new ItemValue()
            {
                mode = (int)json.api_mode,
                type = (int)json.api_type,
                mst_id = (int)json.api_mst_id,
                getmes = json.api_getmes as string
            };
        }
    }
}
