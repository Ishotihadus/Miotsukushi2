using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class MapinfoValue
    {
        /// <summary>
        /// マップID（海域ID×10＋海域中の番号）
        /// </summary>
        public int id;

        /// <summary>
        /// クリア済みか
        /// </summary>
        public bool cleared;

        /// <summary>
        /// ボス旗艦撃破回数によるクリア可能マップか（EO）
        /// </summary>
        public bool exboss_flag;

        /// <summary>
        /// exboss_flag=trueの場合、今までに撃破した回数
        /// </summary>
        public int? defeat_count;

        public class Eventmap
        {
            /// <summary>
            /// 現在のボス旗艦HP
            /// </summary>
            public int now_maphp;

            /// <summary>
            /// ボス旗艦HPの最大
            /// </summary>
            public int max_maphp;

            /// <summary>
            /// 状態（未解析、クリアすると2らしい?）
            /// </summary>
            public int state;

            public static Eventmap fromDynamic(dynamic json)
            {
                Eventmap eventmap = new Eventmap();
                eventmap.now_maphp = (int)json.api_now_maphp;
                eventmap.max_maphp = (int)json.api_max_maphp;
                eventmap.state = (int)json.api_state;
                return eventmap;
            }
        }

        /// <summary>
        /// イベントマップ（ボス旗艦を削ったHP量によるクリア可能マップ）の情報
        /// </summary>
        public Eventmap eventmap;

        public static MapinfoValue fromDynamic(dynamic json)
        {
            MapinfoValue mapinfo = new MapinfoValue();

            mapinfo.id = (int)json.api_id;
            mapinfo.cleared = (int)json.api_cleared == 1;
            mapinfo.exboss_flag = (int)json.api_exboss_flag == 1;
            
            if (json.api_defeat_count())
                mapinfo.defeat_count = (int)json.api_defeat_count;
            else
                mapinfo.defeat_count = null;

            if (json.api_eventmap())
                mapinfo.eventmap = Eventmap.fromDynamic(json.api_eventmap);
            else
                mapinfo.eventmap = null;

            return mapinfo;
        }

        // {"api_id":11,"api_cleared":1,"api_exboss_flag":0}
        // {"api_id":55,"api_cleared":0,"api_exboss_flag":1,"api_defeat_count":0}
        // {"api_id":271,"api_cleared":1,"api_eventmap":{"api_now_maphp":0,"api_max_maphp":528,"api_state":2},"api_exboss_flag":0}
    }
}
