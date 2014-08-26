using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 装備アイテムの種類のマスターデータ
    /// </summary>
    public class MstSlotitemEquiptype
    {
        /// <summary>
        /// 装備アイテム種ID
        /// </summary>
        public int id;

        /// <summary>
        /// 装備アイテム種名
        /// </summary>
        public string name;

        /// <summary>
        /// 専用のアイコンがあるか
        /// </summary>
        public bool show_flg;

        public static MstSlotitemEquiptype fromDynamic(dynamic json)
        {
            MstSlotitemEquiptype slotitem_equiptype = new MstSlotitemEquiptype();

            slotitem_equiptype.id = (int)json.api_id;
            slotitem_equiptype.name = json.api_name as string;
            slotitem_equiptype.show_flg = (int)json.api_show_flg == 1;

            return slotitem_equiptype;
        }

        // {"api_id":1,"api_name":"\u5c0f\u53e3\u5f84\u4e3b\u7832","api_show_flg":1}
    }
}
