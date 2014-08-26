using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 消費アイテムのマスターデータ
    /// 全然使ってないアイテムばっかりなので何に使うのか謎
    /// </summary>
    public class MstUseitem
    {
        /// <summary>
        /// 消費アイテムID
        /// </summary>
        public int id;

        /// <summary>
        /// ?
        /// </summary>
        public int usetype;

        /// <summary>
        /// カテゴリ?
        /// </summary>
        public int category;

        /// <summary>
        /// アイテム名
        /// </summary>
        public string name;

        /// <summary>
        /// アイテムの説明
        /// index0:説明記述
        /// index1:中に入っている個数（家具コインなど）
        /// </summary>
        public string[] description;

        /// <summary>
        /// 値段? おそらく共通通貨によって買える実装をしようとしていたころの名残
        /// </summary>
        public int price;

        public static MstUseitem fromDynamic(dynamic json)
        {
            MstUseitem useitem = new MstUseitem();

            useitem.id = (int)json.api_id;
            useitem.usetype = (int)json.api_usetype;
            useitem.category = (int)json.api_category;
            useitem.name = json.api_name as string;
            useitem.description = json.api_description.Deserialize<string[]>();
            useitem.price = (int)json.api_price;

            return useitem;
        }

        // {"api_id":1,"api_usetype":1,"api_category":1,"api_name":"\u9ad8\u901f\u4fee\u5fa9\u6750",
        // "api_description":["\u5165\u6e20\u6642\u9593\u3092\u77ed\u7e2e\u3067\u304d\u308b\u3002",""],"api_price":0}
    }
}
