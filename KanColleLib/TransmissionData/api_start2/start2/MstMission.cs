using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_start2.start2
{
    /// <summary>
    /// 遠征情報のマスターデータ
    /// </summary>
    public class MstMission
    {
        /// <summary>
        /// 遠征のID
        /// </summary>
        public int id;

        /// <summary>
        /// 海域ID
        /// </summary>
        public int maparea_id;

        /// <summary>
        /// 遠征名
        /// </summary>
        public string name;

        /// <summary>
        /// 遠征の説明
        /// </summary>
        public string details;

        /// <summary>
        /// 時間（分）
        /// </summary>
        public int time;

        /// <summary>
        /// 難易度
        /// </summary>
        public int difficulty;

        /// <summary>
        /// 使う燃料
        /// </summary>
        public double use_fuel;

        /// <summary>
        /// 使う弾薬
        /// </summary>
        public double use_bull;

        /// <summary>
        /// 獲得アイテム1
        /// </summary>
        public int[] win_item1;

        /// <summary>
        /// 獲得アイテム2
        /// </summary>
        public int[] win_item2;

        /// <summary>
        /// 帰投時に母港で帰投表示するか（支援遠征などはfalseになる）
        /// </summary>
        public bool return_flag;

        public static MstMission fromDynamic(dynamic json)
        {
            var mission = new MstMission();

            mission.id = (int)json.api_id;
            mission.maparea_id = (int)json.api_maparea_id;
            mission.name = json.api_name as string;
            mission.details = json.api_details as string;
            mission.time = (int)json.api_time;
            mission.difficulty = (int)json.api_difficulty;
            mission.use_fuel = (double)json.api_use_fuel;
            mission.use_bull = (double)json.api_use_bull;
            mission.win_item1 = json.api_win_item1.Deserialize<int[]>();
            mission.win_item2 = json.api_win_item2.Deserialize<int[]>();
            mission.return_flag = (int)json.api_return_flag == 1;

            return mission;
        }

        // {"api_id":1,"api_maparea_id":1,"api_name":"\u7df4\u7fd2\u822a\u6d77","api_details":"\u93ae\u5b88\u5e9c\u8fd1\u6d77\u3092\u822a\u6d77\u3057\u3001\u8266\u968a\u306e\u7df4\u5ea6\u3092\u9ad8\u3081\u3088\u3046\uff01",
        // "api_time":15,"api_difficulty":1,"api_use_fuel":0.3,"api_use_bull":0,"api_win_item1":[0,0],"api_win_item2":[0,0],"api_return_flag":1}
    }
}
