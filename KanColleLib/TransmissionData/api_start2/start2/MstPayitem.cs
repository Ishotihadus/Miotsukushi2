using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 現実通貨で購入できるアイテムのマスターデータ
    /// </summary>
    public class MstPayitem
    {
        /// <summary>
        /// アイテムID
        /// </summary>
        public int id;

        /// <summary>
        /// アイテムの種類?
        /// </summary>
        public int type;

        /// <summary>
        /// アイテム名
        /// </summary>
        public string name;

        /// <summary>
        /// アイテムの説明
        /// </summary>
        public string description;

        /// <summary>
        /// アイテムによって得られるモノ
        /// 燃料/弾薬/鋼材/ボーキ/バーナー/バケツ/開発資材/ドックキー
        /// </summary>
        public int[] item;

        /// <summary>
        /// 金額（DMMポイント換算）
        /// </summary>
        public int price;

        public static MstPayitem fromDynamic(dynamic json)
        {
            MstPayitem payitem = new MstPayitem();

            payitem.id = (int)json.api_id;
            payitem.type = (int)json.api_type;
            payitem.name = json.api_name as string;
            payitem.description = json.api_description as string;
            payitem.item = json.api_item.Deserialize<int[]>();
            payitem.price = (int)json.api_price;

            return payitem;
        }

        // {"api_id":1,"api_type":0,"api_name":"\u71c3\u6599\u30d1\u30c3\u30af200",
        // "api_description":"\u88dc\u7d66\u306e\u304a\u4f9b\uff01\u71c3\u6599\u30d1\u30c3\u30af<br>(\u71c3\u6599\u3092200\u56de\u5fa9\u3055\u305b\u307e\u3059)",
        // "api_item":[200,0,0,0,0,0,0,0],"api_price":100}
    }
}
