using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member.values
{
    public class QuestlistValue
    {
        /// <summary>
        /// 通し番号
        /// </summary>
        public int no;

        /// <summary>
        /// カテゴリ
        /// 1:編成 2:出撃 3:演習 4:遠征 5:補給/入渠 6:工廠 7:改装 8:その他
        /// </summary>
        public int category;

        /// <summary>
        /// タイプ?
        /// 1:無期限 2,4,5:デイリー 3:ウィークリーとささやかれている
        /// </summary>
        public int type;

        /// <summary>
        /// 任務の状況
        /// 1:未遂行 2:遂行中 3:完了
        /// </summary>
        public int state;

        /// <summary>
        /// 任務名
        /// </summary>
        public string title;

        /// <summary>
        /// 任務の詳細
        /// </summary>
        public string detail;

        /// <summary>
        /// 得られる資源（燃料/弾薬/鋼材/ボーキ）
        /// </summary>
        public int[] get_material;

        /// <summary>
        /// ボーナスがあるか
        /// 0:なし 1以上:あり 特に2:艦娘を得られる
        /// </summary>
        public int bonus_flag;

        /// <summary>
        /// 任務の進行状況
        /// 0:ダメ 1:50% 2:80%
        /// </summary>
        public int progress_flag;

        public static QuestlistValue fromDynamic(dynamic json)
        {
            return new QuestlistValue()
            {
                no = (int)json.api_no,
                category = (int)json.api_category,
                type = (int)json.api_type,
                state = (int)json.api_state,
                title = json.api_title as string,
                detail = json.api_detail as string,
                get_material = json.api_get_material.Deserialize<int[]>(),
                bonus_flag = (int)json.api_bonus_flag,
                progress_flag = (int)json.api_progress_flag
            };
        }
    }
}
