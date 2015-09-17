using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 艦種のリソースアドレスおよび描画位置情報
    /// http://125.6.189.247/kcs/resources/swf/ships/snohitatusbk.swf
    /// http://125.6.189.247/kcs/sound/kcsnohitatusbk/1.mp3
    /// </summary>
    public class MstShipgraph
    {
        /// <summary>
        /// 艦種ID
        /// </summary>
        public int id;

        /// <summary>
        /// 艦種図鑑番号
        /// </summary>
        public int sortno;

        /// <summary>
        /// リソースキー
        /// </summary>
        public string filename;

        /// <summary>
        /// バージョン
        /// </summary>
        public string version;

        public static MstShipgraph fromDynamic(dynamic json)
        {
            var shipgraph = new MstShipgraph();

            shipgraph.id = (int)json.api_id;
            shipgraph.sortno = (int)json.api_sortno;
            shipgraph.filename = json.api_filename as string;
            shipgraph.version = json.api_version as string;

            return shipgraph;
        }

        // {"api_id":1,"api_sortno":31,"api_filename":"snohitatusbk","api_version":"1","api_boko_n":[125,24],"api_boko_d":[83,25],"api_kaisyu_n":[28,7],"api_kaisyu_d":[28,7],
        // "api_kaizo_n":[70,-31],"api_kaizo_d":[29,-32],"api_map_n":[29,24],"api_map_d":[-13,15],"api_ensyuf_n":[129,18],"api_ensyuf_d":[-3,-32],"api_ensyue_n":[129,0],
        // "api_battle_n":[73,27],"api_battle_d":[29,21],"api_weda":[112,108],"api_wedb":[145,153]}
    }
}
