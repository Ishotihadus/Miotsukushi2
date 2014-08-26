using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    public class MstMapinfo
    {
        /// <summary>
        /// マップID（海域ID×10＋海域中の番号）
        /// </summary>
        public int id;

        /// <summary>
        /// 海域ID
        /// </summary>
        public int maparea_id;

        /// <summary>
        /// 海域内のマップ番号
        /// </summary>
        public int no;

        /// <summary>
        /// マップ名
        /// </summary>
        public string name;

        /// <summary>
        /// レベル
        /// </summary>
        public int level;

        /// <summary>
        /// 作戦名
        /// </summary>
        public string opetext;

        /// <summary>
        /// 作戦概要説明
        /// </summary>
        public string infotext;

        /// <summary>
        /// 取得可能アイテム
        /// 要素にもらえるアイテムIDが格納されており、IDはmst_useitemのIDに対応
        /// 0は空欄
        /// </summary>
        public int[] item;

        /// <summary>
        /// マップのクリアに必要な破壊ゲージ量（null許容）
        /// 実際には使われていないっぽい?（じゃあ送るなって話で）
        /// </summary>
        public int? max_maphp;

        /// <summary>
        /// マップのクリアに必要な敵ボス旗艦撃沈回数（null許容）
        /// </summary>
        public int? required_defeat_count;

        /// <summary>
        /// 単独艦隊、連合艦隊でそれぞれ出撃可能か
        /// </summary>
        public bool[] sally_flag;

        public static MstMapinfo fromDynamic(dynamic json)
        {
            MstMapinfo mapinfo = new MstMapinfo();

            mapinfo.id = (int)json.api_id;
            mapinfo.maparea_id = (int)json.api_maparea_id;
            mapinfo.no = (int)json.api_no;
            mapinfo.name = json.api_name as string;
            mapinfo.level = (int)json.api_level;
            mapinfo.opetext = json.api_opetext as string;
            mapinfo.infotext = json.api_infotext as string;
            mapinfo.item = json.api_item.Deserialize<int[]>();
            mapinfo.max_maphp = json.api_max_maphp == null ? (int?)null : (int)json.api_max_maphp;
            mapinfo.required_defeat_count = json.api_required_defeat_count == null ? (int?)null : (int)json.api_required_defeat_count;
            int[] sally_flag = json.api_sally_flag.Deserialize<int[]>();
            mapinfo.sally_flag = new bool[sally_flag.Length];
            for (int i = 0; i < sally_flag.Length; i++)
                mapinfo.sally_flag[i] = sally_flag[i] == 1;

            return mapinfo;
        }

        // {"api_id":11,"api_maparea_id":1,"api_no":1,"api_name":"\u93ae\u5b88\u5e9c\u6b63\u9762\u6d77\u57df","api_level":1,
        // "api_opetext":"\u8fd1\u6d77\u8b66\u5099","api_infotext":"\u93ae\u5b88\u5e9c\u6b63\u9762\u8fd1\u6d77\u306e<br>\u8b66\u5099\u306b\u51fa\u52d5\u305b\u3088\uff01",
        // "api_item":[0,0,0,0],"api_max_maphp":null,"api_required_defeat_count":null,"api_sally_flag":[1,0]}
    }
}
