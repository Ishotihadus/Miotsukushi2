using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.start2
{
    /// <summary>
    /// 艦の船種のマスターデータ
    /// </summary>
    public class MstStype
    {
        /// <summary>
        /// 船の艦種ID
        /// </summary>
        public int id;

        /// <summary>
        /// 表示順
        /// </summary>
        public int sortno;

        /// <summary>
        /// 艦種名
        /// </summary>
        public string name;

        /// <summary>
        /// スロット数らしい?
        /// </summary>
        public int scnt;

        /// <summary>
        /// buildPhaseCount?
        /// </summary>
        public int kcnt;

        public static MstStype fromDynamic(dynamic json)
        {
            MstStype stype = new MstStype();

            stype.id = (int)json.api_id;
            stype.sortno = (int)json.api_sortno;
            stype.name = json.api_name as string;
            stype.scnt = (int)json.api_scnt;
            stype.kcnt = (int)json.api_kcnt;

            return stype;
        }

        // {"api_id":1,"api_sortno":1,"api_name":"\u6d77\u9632\u8266","api_scnt":1,"api_kcnt":2,
        // "api_equip_type":{"1":0,"2":0,"3":0,"4":0,"5":0,"6":0,"7":0,"8":0,"9":0,"10":0,"11":0,"12":0,"13":0,"14":0,"15":0,"16":0,"17":0,"18":0,"19":0,"20":0,"21":0,"22":0,"23":0,"24":0,"25":0,"26":0,"27":0,"28":0,"29":0,"30":0,"31":0,"32":0,"33":0,"34":0,"35":0}}
    }
}
