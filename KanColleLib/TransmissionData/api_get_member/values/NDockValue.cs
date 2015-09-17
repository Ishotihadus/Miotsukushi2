using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class NDockValue
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int member_id;

        /// <summary>
        /// 入渠ドック番号（1番目が1）
        /// </summary>
        public int id;

        /// <summary>
        /// 状態
        /// -1:ロックされている 0:空き 1:入渠中
        /// </summary>
        public int state;

        /// <summary>
        /// 入渠している艦
        /// </summary>
        public int ship_id;

        /// <summary>
        /// 入渠完了時刻
        /// </summary>
        public long complete_time;

        /// <summary>
        /// 入渠完了時刻
        /// </summary>
        public string complete_time_str;

        /// <summary>
        /// 入渠に使用した燃料
        /// </summary>
        public int item1;

        /// <summary>
        /// 入渠に使用した弾薬
        /// </summary>
        public int item2;

        /// <summary>
        /// 入渠に使用した鋼材
        /// </summary>
        public int item3;

        /// <summary>
        /// 入渠に使用したボーキ
        /// </summary>
        public int item4;

        public static NDockValue fromDynamic(dynamic json)
        {
            var ndock = new NDockValue();

            ndock.member_id = (int)json.api_member_id;
            ndock.id = (int)json.api_id;
            ndock.state = (int)json.api_state;
            ndock.ship_id = (int)json.api_ship_id;
            ndock.complete_time = (long)json.api_complete_time;
            ndock.complete_time_str = json.api_complete_time_str as string;
            ndock.item1 = (int)json.api_item1;
            ndock.item2 = (int)json.api_item2;
            ndock.item3 = (int)json.api_item3;
            ndock.item4 = (int)json.api_item4;

            return ndock;
        }

        // {"api_member_id":230597,"api_id":1,"api_state":0,"api_ship_id":0,"api_complete_time":0,"api_complete_time_str":"0","api_item1":0,"api_item2":0,"api_item3":0,"api_item4":0}
    }
}
