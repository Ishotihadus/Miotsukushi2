using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Questlist
    {
        /// <summary>
        /// 任務の数
        /// </summary>
        public int count;

        /// <summary>
        /// ページ数
        /// </summary>
        public int page_count;

        /// <summary>
        /// 表示しているページ番号
        /// </summary>
        public int disp_page;

        /// <summary>
        /// 任務のリスト
        /// </summary>
        public List<values.QuestlistValue> list;

        /// <summary>
        /// 遂行中の任務数
        /// </summary>
        public int exec_count;

        /// <summary>
        /// 不明
        /// </summary>
        public long exec_type;

        public static Questlist fromDynamic(dynamic json)
        {
            Questlist questlist = new Questlist()
            {
                count = (int)json.api_count,
                page_count = (int)json.api_page_count,
                disp_page = (int)json.api_disp_page,
                exec_count = (int)json.api_exec_count,
                exec_type = (int)json.api_exec_type
            };

            questlist.list = new List<values.QuestlistValue>();
            foreach (var data in json.api_list)
            {
                try
                {
                    questlist.list.Add(values.QuestlistValue.fromDynamic(data));
                }
                catch { }
            }

            return questlist;
        }
    }
}
