using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.start2
{
    /// <summary>
    /// 家具のリソースアドレスのマスターデータ
    /// れいによってmst_furnitureとデータがかぶっているので無駄が多い
    /// 何のために存在するのか謎
    /// </summary>
    public class MstFurnituregraph
    {
        /// <summary>
        /// 家具ID
        /// </summary>
        public int id;

        /// <summary>
        /// 家具の種類
        /// 0:床 1:壁紙 2:窓枠+カーテン 3:装飾 4:家具 5:椅子+机
        /// </summary>
        public int type;

        /// <summary>
        /// 家具の表示順番号?
        /// </summary>
        public int no;

        /// <summary>
        /// 家具のリソースアドレス
        /// </summary>
        public string filename;

        /// <summary>
        /// リソースバージョン
        /// </summary>
        public string version;

        public static MstFurnituregraph fromDynamic(dynamic json)
        {
            MstFurnituregraph furnituregraph = new MstFurnituregraph();

            furnituregraph.id = (int)json.api_id;
            furnituregraph.type = (int)json.api_type;
            furnituregraph.no = (int)json.api_no;
            furnituregraph.filename = json.api_filename as string;
            furnituregraph.version = json.api_version as string;

            return furnituregraph;
        }

        // {"api_id":6,"api_type":0,"api_no":5,"api_filename":"006","api_version":"1"}
    }
}
