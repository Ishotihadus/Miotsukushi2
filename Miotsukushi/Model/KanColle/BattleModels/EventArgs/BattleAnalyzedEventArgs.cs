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
            Normal,

            /// <summary>
            /// 昼戦（航空戦）
            /// </summary>
            NormalAir,

            /// <summary>
            /// 昼戦（水上戦/連合艦隊のみ）
            /// </summary>
            NormalWater,

            /// <summary>
            /// 昼戦の夜戦
            /// </summary>
            NormalMidnight,

            /// <summary>
            /// 夜戦オンリー
            /// </summary>
            SpMidnight
        }

        public enum Formation
        {
            Unknown,
            Tanju = 1,
            Fukuju = 2,
            Rinkei = 3,
            Teikei = 4,
            Tanou = 5,
            Daiichikeikai = 11,
            Dainikeikai = 12,
            Daisankeikai = 13,
            Daiyonkeikai = 14
        }

        public enum CrossingType
        {
            /// <summary>
            /// 不明
            /// </summary>
            Unknown,

            /// <summary>
            /// 同航戦
            /// </summary>
            Parallel = 1,

            /// <summary>
            /// 反航戦
            /// </summary>
            AntiParallel = 2,

            /// <summary>
            /// T字有利
            /// </summary>
            CrossAdv = 3,

            /// <summary>
            /// T字不利
            /// </summary>
            CrossDisadv = 4
        }

        public enum AirMasteryStatus
        {
            /// <summary>
            /// 不明
            /// </summary>
            Unknown = -1,

            /// <summary>
            /// 制空均衡
            /// </summary>
            Tie = 0,

            /// <summary>
            /// 制空権確保
            /// </summary>
            Secure = 1,

            /// <summary>
            /// 制空優勢
            /// </summary>
            Superior = 2,

            /// <summary>
            /// 制空劣勢
            /// </summary>
            Inferior = 3,

            /// <summary>
            /// 制空権喪失
            /// </summary>
            Lost = 4
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

            public int CharacterId;

            /// <summary>
            /// 艦娘のID（敵艦には存在しない）
            /// </summary>
            public int OriginalId;

            public string Name;
            public int Level;
            public int MaxHp;
            public int BeforeHp;
            public int AfterHp;
            
            /// <summary>
            /// 退避フラグ
            /// </summary>
            public bool Escaped = false;

            /// <summary>
            /// ダメコンの有り無し
            /// </summary>
            public DameConType Damecontype;

            /// <summary>
            /// ダメコンを使ったか
            /// </summary>
            public bool UseDamecon;

            public int FirePower;
            public int Torpedo;
            public int AntiAir;
            public int Armor;

            /// <summary>
            /// 速度パラメータ
            /// </summary>
            public int Speed;

            /// <summary>
            /// 与えたダメージの合計
            /// </summary>
            public int SumAttack;

            /// <summary>
            /// スロット情報
            /// -1は未装備
            /// </summary>
            public int[] Slot;
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
            public string PhaseName;

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
                public Ship OriginShip;

                /// <summary>
                /// 攻撃先の艦
                /// ダメコンの情報の時はこれにダメコン発動艦が入る
                /// </summary>
                public Ship TargetShip;
                
                /// <summary>
                /// ダメコン発動時は負の値とする
                /// </summary>
                public int Damage;
                public int Type;
                public bool FlagShipProtect;

                /// <summary>
                /// ダメコンであれば1、女神であれば2
                /// </summary>
                public int DameconType = 0;
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
            public List<Attack> Attackee = new List<Attack>();

            /// <summary>
            /// ダメコン発動情報
            /// </summary>
            public List<Attack> Damecon = new List<Attack>();

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
            public List<Attack> Attacks = new List<Attack>();

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
        public bool IsCombinedBattle;

        /// <summary>
        /// 自艦隊
        /// </summary>
        public List<Ship> Friend;

        /// <summary>
        /// 自艦隊（護衛艦隊）
        /// </summary>
        public List<Ship> FriendCombined;

        /// <summary>
        /// 敵艦隊
        /// </summary>
        public List<Ship> Enemy;

        /// <summary>
        /// フェイズ
        /// </summary>
        public List<Phase> Phases;

        /// <summary>
        /// 自艦隊ゲージ（1がMax）
        /// </summary>
        public double FriendGauge;

        /// <summary>
        /// 敵艦隊ゲージ（1がMax）
        /// </summary>
        public double EnemyGauge;

        /// <summary>
        /// 自艦隊陣形
        /// </summary>
        public Formation FriendFormation;

        /// <summary>
        /// 敵艦隊陣形
        /// </summary>
        public Formation EnemyFormation;

        /// <summary>
        /// 交戦形態
        /// </summary>
        public CrossingType crossing_type;

        /// <summary>
        /// 制空状態
        /// </summary>
        public AirMasteryStatus AirMastery;
    }
}
