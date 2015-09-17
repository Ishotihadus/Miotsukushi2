using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// マップBGMのマスターデータ
    /// 昼戦と夜戦で同じ曲だと連続で再生されるようになっているらしい
    /// </summary>
    public class MstMapBGM
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
        /// 道中のBGM（昼戦/夜戦）
        /// </summary>
        public int[] map_bgm;

        /// <summary>
        /// ボス戦のBGM（昼戦/夜戦）
        /// </summary>
        public int[] boss_bgm;

        public static MstMapBGM fromDynamic(dynamic json)
        {
            var mapbgm = new MstMapBGM();

            mapbgm.id = (int)json.api_id;
            mapbgm.maparea_id = (int)json.api_maparea_id;
            mapbgm.no = (int)json.api_no;
            mapbgm.map_bgm = json.api_map_bgm.Deserialize<int[]>();
            mapbgm.boss_bgm = json.api_boss_bgm.Deserialize<int[]>();

            return mapbgm;
        }

        // {"api_id":11,"api_maparea_id":1,"api_no":1,"api_map_bgm":[1,2],"api_boss_bgm":[2,2]}
    }
}
