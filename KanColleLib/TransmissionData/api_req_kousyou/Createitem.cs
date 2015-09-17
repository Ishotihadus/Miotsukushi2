using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_req_kousyou.values;

namespace KanColleLib.TransmissionData.api_req_kousyou
{
    public class Createitem
    {
        /// <summary>
        /// 開発に成功したか
        /// </summary>
        public bool create_flag;

        /// <summary>
        /// 開発資材を消費したか
        /// </summary>
        public bool shizai_flag;

        /// <summary>
        /// 資材情報（燃料/弾薬/鋼材/ボーキ/バーナー/バケツ/開発資材）
        /// </summary>
        public int[] material;

        /// <summary>
        /// unsetslotの種類番号（開発成功時のみ）
        /// </summary>
        public int? type3;

        /// <summary>
        /// unsetslot情報（typeはtype3の値）（開発成功時のみ）
        /// </summary>
        public int[] unsetslot;

        /// <summary>
        /// 開発失敗時の情報 "m,n"
        /// mは失敗の種類?（不明、常に2）、nは開発に失敗した装備
        /// </summary>
        public string fdata;

        /// <summary>
        /// 開発成功時のアイテム情報
        /// </summary>
        public CreateitemValue slot_item;

        public static Createitem fromDynamic(dynamic json)
        {
            var createitem = new Createitem();

            createitem.create_flag = (int)json.api_create_flag == 1;
            createitem.shizai_flag = (int)json.api_shizai_flag == 1;
            createitem.slot_item = json.api_slot_item() ? CreateitemValue.fromDynamic(json.api_slot_item) : null;
            createitem.material = json.api_material.Deserialize<int[]>();
            createitem.type3 = json.api_type3() ? (int)json.api_type3 : (int?)null;
            createitem.unsetslot = json.api_unsetslot() ? json.api_unsetslot.Deserialize<int[]>() : null;
            createitem.fdata = json.api_fdata() ? json.api_fdata as string : null;

            return createitem;
        }
    }
}
