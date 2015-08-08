using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_map.values
{
    public class ItemgetValue
    {
        /// <summary>
        /// 4:資源　11:家具箱中　12:家具箱大
        /// </summary>
        public int usemst;

        /// <summary>
        /// アイテムID
        /// </summary>
        public int id;

        /// <summary>
        /// 資源量
        /// </summary>
        public int getcount;

        /// <summary>
        /// 名前（資源の時は空）
        /// </summary>
        public string name;

        /// <summary>
        /// アイテムIDとほぼ同様
        /// </summary>
        public int icon_id;

        public static ItemgetValue fromDynamic(dynamic json)
        {
            return new ItemgetValue()
            {
                usemst = (int)json.api_usemst,
                id = (int)json.api_id,
                getcount = (int)json.api_getcount,
                name = json.api_name as string,
                icon_id = (int)json.api_icon_id
            };
        }
    }
}
