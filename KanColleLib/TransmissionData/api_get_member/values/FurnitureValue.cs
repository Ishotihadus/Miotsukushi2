using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class FurnitureValue
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int member_id;

        /// <summary>
        /// 家具ID（furniture_idと同じ）
        /// </summary>
        public int id;

        /// <summary>
        /// 家具の種類
        /// 0:床 1:壁紙 2:窓枠+カーテン 3:装飾 4:家具 5:椅子+机
        /// </summary>
        public int furniture_type;

        /// <summary>
        /// 同一種類内での番号
        /// </summary>
        public int furniture_no;

        /// <summary>
        /// 家具ID（idと同じ）
        /// </summary>
        public int furniture_id;

        public static FurnitureValue fromDynamic(dynamic json)
        {
            FurnitureValue furniture = new FurnitureValue();

            furniture.member_id = (int)json.api_member_id;
            furniture.id = (int)json.api_id;
            furniture.furniture_type = (int)json.api_furniture_type;
            furniture.furniture_no = (int)json.api_furniture_no;
            furniture.furniture_id = (int)json.api_furniture_id;

            return furniture;
        }

        // {"api_member_id":11073525,"api_id":1,"api_furniture_type":0,"api_furniture_no":0,"api_furniture_id":1}
    }
}
