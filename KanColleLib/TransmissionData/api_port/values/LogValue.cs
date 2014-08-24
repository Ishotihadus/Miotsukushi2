using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_port.values
{
    public class LogValue
    {
        /// <summary>
        /// 表示順
        /// </summary>
        public int no;

        /// <summary>
        /// タイプ
        /// 1から順に、入渠/工廠/遠征/支給/演習/勲章/出撃/任務/申請/昇格/図鑑/達成/改造
        /// </summary>
        public string type;

        /// <summary>
        /// 状態? 0で固定
        /// </summary>
        public string state;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string message;

        public static LogValue fromDynamic(dynamic json)
        {
            LogValue log = new LogValue();

            log.no = (int)json.api_no;
            log.type = json.api_type as string;
            log.state = json.api_state as string;
            log.message = json.api_message as string;

            return log;
        }

    }
}
