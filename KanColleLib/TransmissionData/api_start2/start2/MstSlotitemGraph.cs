using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 装備アイテムのリソースアドレス
    /// 実際にはこのデータは使用されていない（マジ）
    /// 今後使うのかな……?
    /// 現状では非常に無駄なデータとなっている
    /// </summary>
    public class MstSlotitemgraph
    {
        /// <summary>
        /// 装備アイテムID
        /// </summary>
        public int id;

        /// <summary>
        /// 装備アイテムの図鑑番号
        /// </summary>
        public int sortno;

        /// <summary>
        /// 装備アイテムリソースアドレス
        /// </summary>
        public string filename;

        /// <summary>
        /// バージョン
        /// </summary>
        public string version;

        public static MstSlotitemgraph fromDynamic(dynamic json)
        {
            MstSlotitemgraph slotitemgraph = new MstSlotitemgraph();

            slotitemgraph.id = (int)json.api_id;
            slotitemgraph.sortno = (int)json.api_sortno;
            slotitemgraph.filename = json.api_filename as string;
            slotitemgraph.version = json.api_version as string;

            return slotitemgraph;
        }

        // {"api_id":1,"api_sortno":1,"api_filename":"1","api_version":"1"}
    }
}
