using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class KDockValue
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int member_id;

        /// <summary>
        /// 建造ドック番号（1番目が1）
        /// </summary>
        public int id;

        /// <summary>
        /// 現在の状態
        /// -1:ロックされている 0:空きドック 2:建造中 3:建造完了
        /// </summary>
        public int state;

        /// <summary>
        /// 建造されている（された）艦種のID
        /// </summary>
        public int created_ship_id;

        /// <summary>
        /// 建造完了時刻
        /// </summary>
        public long complete_time;

        /// <summary>
        /// 建造完了時刻
        /// </summary>
        public string complete_time_str;

        /// <summary>
        /// 燃料
        /// </summary>
        public int item1;

        /// <summary>
        /// 弾薬
        /// </summary>
        public int item2;

        /// <summary>
        /// 鋼材
        /// </summary>
        public int item3;

        /// <summary>
        /// ボーキ
        /// </summary>
        public int item4;

        /// <summary>
        /// 開発資材
        /// </summary>
        public int item5;

        public static KDockValue fromDynamic(dynamic json)
        {
            var kdock = new KDockValue();

            kdock.member_id = (int)json.api_member_id;
            kdock.id = (int)json.api_id;
            kdock.state = (int)json.api_state;
            kdock.created_ship_id = (int)json.api_created_ship_id;
            kdock.complete_time = (long)json.api_complete_time;
            kdock.complete_time_str = json.api_complete_time_str as string;
            kdock.item1 = (int)json.api_item1;
            kdock.item2 = (int)json.api_item2;
            kdock.item3 = (int)json.api_item3;
            kdock.item4 = (int)json.api_item4;
            kdock.item5 = (int)json.api_item5;

            return kdock;
        }

        // {"api_member_id":11073525,"api_id":1,"api_state":3,"api_created_ship_id":101,"api_complete_time":0,"api_complete_time_str":"0",
        // "api_item1":30,"api_item2":30,"api_item3":30,"api_item4":30,"api_item5":1}
    }
}
