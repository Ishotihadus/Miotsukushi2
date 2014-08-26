using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// マップ中のセルのマスターデータ
    /// どうしようもないが非常に効率が悪い気がする
    /// </summary>
    public class MstMapcell
    {
        /// <summary>
        /// マップID
        /// </summary>
        public int map_no;

        /// <summary>
        /// 海域ID（海域ID×10＋海域中の番号）
        /// </summary>
        public int maparea_id;

        /// <summary>
        /// 海域内のマップ番号
        /// </summary>
        public int mapinfo_no;

        /// <summary>
        /// セルの通し番号
        /// </summary>
        public int id;

        /// <summary>
        /// マップ内のセル番号
        /// </summary>
        public int no;

        /// <summary>
        /// セルの色番号
        /// 0:スタート 1:不明（未使用） 2:補給 3:うずしお 4:戦闘 5:ボス 6:夜戦?（現在未使用） 7:連合艦隊航空戦（矢印マス）
        /// </summary>
        public int color_no;

        // {"api_map_no":11,"api_maparea_id":1,"api_mapinfo_no":1,"api_id":1,"api_no":0,"api_color_no":0}
    }
}
