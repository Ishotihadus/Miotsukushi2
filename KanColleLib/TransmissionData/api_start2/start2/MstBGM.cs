using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 母港BGMのマスターデータ
    /// </summary>
    public class MstBGM
    {
        /// <summary>
        /// 母港BGMID
        /// </summary>
        public int id;

        /// <summary>
        /// 題名
        /// </summary>
        public string name;

        public static MstBGM fromDynamic(dynamic json)
        {
            MstBGM bgm = new MstBGM();

            bgm.id = (int)json.api_id;
            bgm.name = json.api_name;

            return bgm;
        }
    }
}
