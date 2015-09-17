using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Basic
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int member_id;

        /// <summary>
        /// 提督名
        /// </summary>
        public string nickname;

        /// <summary>
        /// 提督名ID（未使用）
        /// </summary>
        public string nickname_id;

        /// <summary>
        /// 不明（未使用）
        /// </summary>
        public bool active_flag;

        /// <summary>
        /// ログイン時刻（未使用）
        /// </summary>
        public long starttime;

        /// <summary>
        /// 司令部レベル
        /// </summary>
        public int level;

        /// <summary>
        /// 称号 ["", "元帥", "大将", "中将", "少将", "大佐", "中佐", "新米中佐", "少佐", "中堅少佐", "新米少佐"]
        /// </summary>
        public int rank;

        /// <summary>
        /// 累計経験値
        /// </summary>
        public int experience;

        /// <summary>
        /// 艦隊名?（未使用）
        /// </summary>
        public string fleetname;

        /// <summary>
        /// コメント
        /// </summary>
        public string comment;

        /// <summary>
        /// コメントID（未使用）
        /// </summary>
        public string comment_id;

        /// <summary>
        /// 最大保有艦数
        /// </summary>
        public int max_chara;

        /// <summary>
        /// 最大保有装備数（-3）
        /// </summary>
        public int max_slotitem;

        /// <summary>
        /// 最大保有家具数（未使用）
        /// </summary>
        public int max_kagu;

        /// <summary>
        /// プレイ時間（未使用）
        /// </summary>
        public long playtime;

        /// <summary>
        /// 不明（未使用）
        /// </summary>
        public int tutorial;

        /// <summary>
        /// 現在使用している家具（床/壁紙/窓/装飾/家具/机）
        /// </summary>
        public int[] furniture;

        /// <summary>
        /// 開放艦隊数
        /// </summary>
        public int count_deck;

        /// <summary>
        /// 開放建造ドック数
        /// </summary>
        public int count_kdock;

        /// <summary>
        /// 開放入渠ドック数
        /// </summary>
        public int count_ndock;

        /// <summary>
        /// 家具コイン
        /// </summary>
        public int fcoin;

        /// <summary>
        /// 出撃の勝利数
        /// </summary>
        public int st_win;

        /// <summary>
        /// 出撃の敗北数
        /// </summary>
        public int st_lose;

        /// <summary>
        /// 遠征数
        /// </summary>
        public int ms_count;

        /// <summary>
        /// 遠征成功数
        /// </summary>
        public int ms_success;

        /// <summary>
        /// 演習勝利数
        /// </summary>
        public int pt_win;

        /// <summary>
        /// 演習敗北数
        /// </summary>
        public int pt_lose;

        /// <summary>
        /// 被演習数（未使用）
        /// </summary>
        public int pt_challenged;

        /// <summary>
        /// 被演習勝利数（未使用）
        /// </summary>
        public int pt_challenged_win;

        /// <summary>
        /// ？（ship3を取得した時に保有艦データがなくてもそれが空でよいことを保証するフラグ、しかし常に1なので意味が無い気がする）
        /// </summary>
        public bool firstflag;

        /// <summary>
        /// チュートリアルの進行度（単位は%、すべて終えたら100）
        /// </summary>
        public int tutorial_progress;

        /// <summary>
        /// 対人戦?（未使用）
        /// </summary>
        public int[] pvp;

        public static Basic fromDynamic(dynamic json)
        {
            var basic = new Basic();

            basic.member_id = int.Parse(json.api_member_id as string);
            basic.nickname = json.api_nickname as string;
            basic.nickname_id = json.api_nickname_id as string;
            basic.active_flag = (int)json.api_active_flag == 1;
            basic.starttime = (long)json.api_starttime;
            basic.level = (int)json.api_level;
            basic.rank = (int)json.api_rank;
            basic.experience = (int)json.api_experience;
            basic.fleetname = json.api_fleetname as string;
            basic.comment = json.api_comment as string;
            basic.comment_id = json.api_comment_id as string;
            basic.max_chara = (int)json.api_max_chara;
            basic.max_slotitem = (int)json.api_max_slotitem;
            basic.max_kagu = (int)json.api_max_kagu;
            basic.playtime = (long)json.api_playtime;
            basic.tutorial = (int)json.api_tutorial;
            basic.furniture = json.api_furniture.Deserialize<int[]>();
            basic.count_deck = (int)json.api_count_deck;
            basic.count_kdock = (int)json.api_count_kdock;
            basic.count_ndock = (int)json.api_count_ndock;
            basic.fcoin = (int)json.api_fcoin;
            basic.st_win = (int)json.api_st_win;
            basic.st_lose = (int)json.api_st_lose;
            basic.ms_count = (int)json.api_ms_count;
            basic.ms_success = (int)json.api_ms_success;
            basic.pt_win = (int)json.api_pt_win;
            basic.pt_lose = (int)json.api_pt_lose;
            basic.pt_challenged = (int)json.api_pt_challenged;
            basic.pt_challenged_win = (int)json.api_pt_challenged_win;
            basic.firstflag = (int)json.api_firstflag == 1;
            basic.tutorial_progress = (int)json.api_tutorial_progress;
            basic.pvp = json.api_pvp.Deserialize<int[]>();

            return basic;
        }

        // {"api_member_id":"11073525","api_nickname":"\u30a4\u30b7\u30e7\u30c6\u30a3\u30cf\u30c9\u30a5\u30b9","api_nickname_id":"117959389","api_active_flag":1,"api_starttime":1408453578725,
        // "api_level":98,"api_rank":3,"api_experience":970084,"api_fleetname":null,
        // "api_comment":"1\u65e51\u9320\u30ea\u30b9\u30da\u30ea\u30c9\u30f3\u3002","api_comment_id":"126704184","api_max_chara":120,"api_max_slotitem":577,"api_max_kagu":0,"api_playtime":0,
        // "api_tutorial":0,"api_furniture":[22,58,210,203,140,181],"api_count_deck":4,"api_count_kdock":2,"api_count_ndock":4,"api_fcoin":14500,"api_st_win":3887,"api_st_lose":115,
        // "api_ms_count":1810,"api_ms_success":1783,"api_pt_win":251,"api_pt_lose":15,"api_pt_challenged":0,"api_pt_challenged_win":0,"api_firstflag":1,"api_tutorial_progress":100,"api_pvp":[0,0]}
    }
}
