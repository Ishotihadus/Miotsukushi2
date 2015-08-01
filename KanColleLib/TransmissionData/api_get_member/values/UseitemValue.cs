using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class UseitemValue
    {
        public int member_id;

        /// <summary>
        /// アイテムID
        /// 1:バケツ 2:開発資材 3:バーナー 4:ネジ 31:燃料 32:弾薬 33:鋼材 34:ボーキサイト 44:家具コイン 49:ドック開放キー 52:司令部要員 54:間宮 55:結婚指輪 57:勲章 58:改造設計図 59:伊良湖 60:プレゼント箱 61:甲勲章
        /// </summary>
        public int id;

        /// <summary>
        /// 使ってない（countと同じ値っぽい）
        /// </summary>
        public int value;

        /// <summary>
        /// 4だったらその場で解体できる　0はダメ
        /// </summary>
        public int usetype;

        /// <summary>
        /// 家具コインは6
        /// </summary>
        public int category;

        public string name;

        /// <summary>
        /// 枠は2つ　使ってないみたい?
        /// </summary>
        public string[] description;

        /// <summary>
        /// ?
        /// </summary>
        public int price;
        
        /// <summary>
        /// 所持数
        /// </summary>
        public int count;

        public static UseitemValue fromDynamic(dynamic json)
        {
            return new UseitemValue()
            {
                id = (int)json.api_id,
                value = (int)json.api_value,
                usetype = (int)json.api_usetype,
                category = (int)json.api_category,
                name = json.api_name as string,
                description = json.api_description.Deserialize<string[]>(),
                price = (int)json.api_price,
                count = (int)json.api_count
            };
        }

        // {"api_member_id":11073525,"api_id":11,"api_value":3,"api_usetype":4,"api_category":6,"api_name":"\u5bb6\u5177\u7bb1\uff08\u4e2d\uff09","api_description":["",""],"api_price":0,"api_count":3}
    }
}
