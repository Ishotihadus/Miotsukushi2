using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.BattleModels.EventArgs
{
    class BattleAnalyzedEventArgs : System.EventArgs
    {
        public enum BattleType
        {
            /// <summary>
            /// 昼戦
            /// </summary>
            normal,

            /// <summary>
            /// 昼戦（航空戦）
            /// </summary>
            normal_air,

            /// <summary>
            /// 昼戦（水上戦/連合艦隊のみ）
            /// </summary>
            normal_water,

            /// <summary>
            /// 昼戦の夜戦
            /// </summary>
            normal_midnight,

            /// <summary>
            /// 夜戦オンリー
            /// </summary>
            sp_midnight
        }

        public enum Formation
        {
            unknown,
            tanju = 1,
            fukuju = 2,
            rinkei = 3,
            teikei = 4,
            tanou = 5,
            daiichikeikai = 11,
            dainikeikai = 12,
            daisankeikai = 13,
            daiyonkeikai = 14
        }

        public enum CrossingType
        {
            /// <summary>
            /// 不明
            /// </summary>
            unknown,

            /// <summary>
            /// 同航戦
            /// </summary>
            parallel = 1,

            /// <summary>
            /// 反航戦
            /// </summary>
            anti_parallel = 2,

            /// <summary>
            /// T字有利
            /// </summary>
            cross_adv = 3,

            /// <summary>
            /// T字不利
            /// </summary>
            cross_disadv = 4
        }

        public enum AirMasteryStatus
        {
            /// <summary>
            /// 不明
            /// </summary>
            unknown = -1,

            /// <summary>
            /// 制空均衡
            /// </summary>
            tie = 0,

            /// <summary>
            /// 制空権確保
            /// </summary>
            secure = 1,

            /// <summary>
            /// 制空優勢
            /// </summary>
            superior = 2,

            /// <summary>
            /// 制空劣勢
            /// </summary>
            inferior = 3,

            /// <summary>
            /// 制空権喪失
            /// </summary>
            lost = 4
        }

        /// <summary>
        /// 艦娘情報
        /// </summary>
        public class Ship
        {
            public enum DameConType
            {
                None, Normal, Goddess
            }

            public int character_id;

            /// <summary>
            /// 艦娘のID（敵艦には存在しない）
            /// </summary>
            public int original_id;

            public string name;
            public int level;
            public int max_hp;
            public int before_hp;
            public int after_hp;
            
            /// <summary>
            /// 退避フラグ
            /// </summary>
            public bool escaped = false;

            /// <summary>
            /// ダメコンの有り無し
            /// </summary>
            public DameConType damecontype;

            /// <summary>
            /// ダメコンを使ったか
            /// </summary>
            public bool use_damecon;

            public int fire_power;
            public int torpedo;
            public int anti_air;
            public int armor;

            /// <summary>
            /// 与えたダメージの合計
            /// </summary>
            public int sum_attack;

            /// <summary>
            /// スロット情報
            /// -1は未装備
            /// </summary>
            public int[] slot;
        }

        /// <summary>
        /// 戦闘フェイズ
        /// </summary>
        public class Phase
        {
            public enum PhaseType
            {
                AllOverPhase, InOrderPhase
            }

            /// <summary>
            /// フェイズの名前
            /// </summary>
            public string phase_name;

            /// <summary>
            /// フェイズの種類
            /// </summary>
            public PhaseType phase_type;

            /// <summary>
            /// 攻撃情報
            /// </summary>
            public class Attack
            {
                /// <summary>
                /// 攻撃元の艦
                /// ダメコンの情報の時などはnull
                /// </summary>
                public Ship origin_ship;

                /// <summary>
                /// 攻撃先の艦
                /// ダメコンの情報の時はこれにダメコン発動艦が入る
                /// </summary>
                public Ship target_ship;
                
                /// <summary>
                /// ダメコン発動時は負の値とする
                /// </summary>
                public int damage;
                public int type;
                public bool flag_ship_protect;

                /// <summary>
                /// ダメコンであれば1、女神であれば2
                /// </summary>
                public int damecon_type = 0;
            }
        }

        /// <summary>
        /// みんなでやりあうフェイズ
        /// </summary>
        public class AllOverPhase : Phase
        {
            /// <summary>
            /// 攻撃された情報
            /// </summary>
            public List<Attack> attackee = new List<Attack>();

            /// <summary>
            /// ダメコン発動情報
            /// </summary>
            public List<Attack> damecon = new List<Attack>();

            public AllOverPhase() : base()
            {
                phase_type = PhaseType.AllOverPhase;
            }
        }

        /// <summary>
        /// 順繰りに進んでいくフェイズ
        /// </summary>
        public class InOrderPhase : Phase
        {
            public List<Attack> attacks = new List<Attack>();

            public InOrderPhase() : base()
            {
                phase_type = PhaseType.InOrderPhase;
            }
        }

        /// <summary>
        /// 戦闘の種類
        /// </summary>
        public BattleType battle_type;

        /// <summary>
        /// 連合艦隊戦かどうか
        /// </summary>
        public bool is_combined_battle;

        /// <summary>
        /// 自艦隊
        /// </summary>
        public List<Ship> friend;

        /// <summary>
        /// 自艦隊（護衛艦隊）
        /// </summary>
        public List<Ship> friend_combined;

        /// <summary>
        /// 敵艦隊
        /// </summary>
        public List<Ship> enemy;

        /// <summary>
        /// フェイズ
        /// </summary>
        public List<Phase> phases;

        /// <summary>
        /// 自艦隊ゲージ（1がMax）
        /// </summary>
        public double friend_gauge;

        /// <summary>
        /// 敵艦隊ゲージ（1がMax）
        /// </summary>
        public double enemy_gauge;

        /// <summary>
        /// 自艦隊陣形
        /// </summary>
        public Formation friend_formation;

        /// <summary>
        /// 敵艦隊陣形
        /// </summary>
        public Formation enemy_formation;

        /// <summary>
        /// 交戦形態
        /// </summary>
        public CrossingType crossing_type;

        /// <summary>
        /// 制空状態
        /// </summary>
        public AirMasteryStatus air_mastery;
    }
}
