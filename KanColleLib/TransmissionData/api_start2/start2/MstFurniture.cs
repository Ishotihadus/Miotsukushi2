using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 家具のマスターデータ
    /// </summary>
    public class MstFurniture
    {
        /// <summary>
        /// 家具ID
        /// </summary>
        public int id;

        /// <summary>
        /// 家具の種類
        /// 0:床 1:壁紙 2:窓枠+カーテン 3:装飾 4:家具 5:椅子+机
        /// </summary>
        public int type;

        /// <summary>
        /// 家具の表示順番号?
        /// </summary>
        public int no;

        /// <summary>
        /// 家具名
        /// </summary>
        public string title;

        /// <summary>
        /// 家具の説明
        /// </summary>
        public string description;

        /// <summary>
        /// レアリティ
        /// </summary>
        public int rarity;

        /// <summary>
        /// 値段
        /// </summary>
        public int price;

        /// <summary>
        /// 現在売っているかどうか
        /// </summary>
        public bool saleflg;

        public static MstFurniture fromDynamic(dynamic json)
        {
            MstFurniture furniture = new MstFurniture();

            furniture.id = (int)json.api_id;
            furniture.type = (int)json.api_type;
            furniture.no = (int)json.api_no;
            furniture.title = json.api_title as string;
            furniture.description = json.api_description as string;
            furniture.rarity = (int)json.api_rarity;
            furniture.price = (int)json.api_price;
            furniture.saleflg = (int)json.api_saleflg == 1;

            return furniture;
        }

        // {"api_id":1,"api_type":0,"api_no":0,"api_title":"\u93ae\u5b88\u5e9c\u306e\u5e8a",
        // "api_description":"\u93ae\u5b88\u5e9c\u306e\u8266\u968a\u53f8\u4ee4\u5b98\u5ba4\u3002\u305d\u306e\u901a\u5e38\u4ed5\u69d8\u306e\u5e8a\u677f\u3067\u3059\u3002",
        // "api_rarity":0,"api_price":0,"api_saleflg":0}
    }
}
