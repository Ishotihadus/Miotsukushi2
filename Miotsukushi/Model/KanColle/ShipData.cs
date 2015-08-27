using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Miotsukushi.Model.KanColle
{
    class ShipData : NotifyModelBase
    {
        #region プロパティ定義

        private int _shipid;

        /// <summary>
        /// 艦娘ID（これが変わることはまずないはずだが……）
        /// </summary>
        public int shipid
        {
            get
            {
                return _shipid;
            }

            set
            {
                if (_shipid != value)
                {
                    _shipid = value;
                    OnPropertyChanged(() => shipid);
                }
            }
        }

        private int _characterid;

        /// <summary>
        /// 艦のID
        /// 改造時とかに変わるかも
        /// </summary>
        public int characterid
        {
            get
            {
                return _characterid;
            }

            set
            {
                if (_characterid != value)
                {
                    _characterid = value;
                    OnPropertyChanged(() => characterid);
                }
            }
        }

        /// <summary>
        /// 艦娘のキャラクター情報に直接アクセスする
        /// キャラクター情報がなければnullを返す
        /// </summary>
        public CharacterData characterinfo
        {
            get
            {
                if (Model.MainModel.Current != null && Model.MainModel.Current.kancolleModel != null && Model.MainModel.Current.kancolleModel.charamaster != null &&
                    Model.MainModel.Current.kancolleModel.charamaster.ContainsKey(characterid))
                    return Model.MainModel.Current.kancolleModel.charamaster[characterid];
                else
                    return null;
            }
        }

        private int _level;
        
        /// <summary>
        /// レベル
        /// </summary>
        public int level
        {
            get
            {
                return _level;
            }

            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged(() => level);
                }
            }
        }

        private int _exp_total;
        
        /// <summary>
        /// 累計経験値
        /// </summary>
        public int exp_total
        {
            get
            {
                return _exp_total;
            }

            set
            {
                if (_exp_total != value)
                {
                    _exp_total = value;
                    OnPropertyChanged(() => exp_total);
                }
            }
        }

        private int _exp_to_next_lv;

        /// <summary>
        /// 次のレベルまでの経験値
        /// </summary>
        public int exp_to_next_lv
        {
            get
            {
                return _exp_to_next_lv;
            }

            set
            {
                if (_exp_to_next_lv != value)
                {
                    _exp_to_next_lv = value;
                    OnPropertyChanged(() => exp_to_next_lv);
                }
            }
        }

        private int _exp_percent_in_this_lv;

        /// <summary>
        /// 今のレベルでの経験値パーセント
        /// </summary>
        public int exp_percent_in_this_lv
        {
            get
            {
                return _exp_percent_in_this_lv;
            }

            set
            {
                if (_exp_percent_in_this_lv != value)
                {
                    _exp_percent_in_this_lv = value;
                    OnPropertyChanged(() => exp_percent_in_this_lv);
                }
            }
        }

        private int _hp_now;

        /// <summary>
        /// 今のHP
        /// </summary>
        public int hp_now
        {
            get
            {
                return _hp_now;
            }

            set
            {
                if (_hp_now != value)
                {
                    _hp_now = value;
                    OnPropertyChanged(() => hp_now);
                }
            }
        }

        private int _hp_max;

        /// <summary>
        /// 最大HP
        /// </summary>
        public int hp_max
        {
            get
            {
                return _hp_max;
            }

            set
            {
                if (_hp_max != value)
                {
                    _hp_max = value;
                    OnPropertyChanged(() => hp_max);
                }
            }
        }

        private int _condition;

        /// <summary>
        /// コンディション
        /// </summary>
        public int condition
        {
            get
            {
                return _condition;
            }

            set
            {
                if (_condition != value)
                {
                    _condition = value;
                    OnPropertyChanged(() => condition);
                }
            }
        }

        private int _fuel;
        
        /// <summary>
        /// 現在の燃料
        /// </summary>
        public int fuel
        {
            get
            {
                return _fuel;
            }

            set
            {
                if (_fuel != value)
                {
                    _fuel = value;
                    OnPropertyChanged(() => fuel);
                }
            }
        }

        private int _ammo;

        /// <summary>
        /// 現在の弾薬
        /// </summary>
        public int ammo
        {
            get
            {
                return _ammo;
            }

            set
            {
                if (_ammo != value)
                {
                    _ammo = value;
                    OnPropertyChanged(() => ammo);
                }
            }
        }

        private TimeSpan _ndock_time;

        /// <summary>
        /// 入渠時間
        /// </summary>
        public TimeSpan ndock_time
        {
            get
            {
                return _ndock_time;
            }

            set
            {
                if (_ndock_time != value)
                {
                    _ndock_time = value;
                    OnPropertyChanged(() => ndock_time);
                }
            }
        }

        public ObservableCollection<int> Slots = new ObservableCollection<int>();
        public ObservableCollection<int> OnSlotCount = new ObservableCollection<int>();

        private int _air_mastery;

        /// <summary>
        /// 制空値
        /// </summary>
        public int air_mastery
        {
            get
            {
                return _air_mastery;
            }

            set
            {
                if (_air_mastery != value)
                {
                    _air_mastery = value;
                    OnPropertyChanged(() => air_mastery);
                }
            }
        }


        private int _fire_power;

        /// <summary>
        /// 火力
        /// </summary>
        public int fire_power
        {
            get
            {
                return _fire_power;
            }

            set
            {
                if (_fire_power != value)
                {
                    _fire_power = value;
                    OnPropertyChanged(() => fire_power);
                }
            }
        }


        private int _armor;

        /// <summary>
        /// 装甲
        /// </summary>
        public int armor
        {
            get
            {
                return _armor;
            }

            set
            {
                if (_armor != value)
                {
                    _armor = value;
                    OnPropertyChanged(() => armor);
                }
            }
        }


        private int _torpedo;

        /// <summary>
        /// 雷装
        /// </summary>
        public int torpedo
        {
            get
            {
                return _torpedo;
            }

            set
            {
                if (_torpedo != value)
                {
                    _torpedo = value;
                    OnPropertyChanged(() => torpedo);
                }
            }
        }


        private int _evasion;

        /// <summary>
        /// 回避
        /// </summary>
        public int evasion
        {
            get
            {
                return _evasion;
            }

            set
            {
                if (_evasion != value)
                {
                    _evasion = value;
                    OnPropertyChanged(() => evasion);
                }
            }
        }


        private int _anti_air;

        /// <summary>
        /// 対空
        /// </summary>
        public int anti_air
        {
            get
            {
                return _anti_air;
            }

            set
            {
                if (_anti_air != value)
                {
                    _anti_air = value;
                    OnPropertyChanged(() => anti_air);
                }
            }
        }


        private int _anti_submarine;

        /// <summary>
        /// 対潜
        /// </summary>
        public int anti_submarine
        {
            get
            {
                return _anti_submarine;
            }

            set
            {
                if (_anti_submarine != value)
                {
                    _anti_submarine = value;
                    OnPropertyChanged(() => anti_submarine);
                }
            }
        }


        private int _reconnaissance;

        /// <summary>
        /// 索敵
        /// </summary>
        public int reconnaissance
        {
            get
            {
                return _reconnaissance;
            }

            set
            {
                if (_reconnaissance != value)
                {
                    _reconnaissance = value;
                    OnPropertyChanged(() => reconnaissance);
                }
            }
        }


        private int _luck;

        /// <summary>
        /// 運
        /// </summary>
        public int luck
        {
            get
            {
                return _luck;
            }

            set
            {
                if (_luck != value)
                {
                    _luck = value;
                    OnPropertyChanged(() => luck);
                }
            }
        }

        #endregion

        public void FromKanColleLib(KanColleLib.TransmissionData.api_get_member.values.ShipValue data)
        {
            shipid = data.id;
            characterid = data.ship_id;
            level = data.lv;
            exp_total = data.exp[0];
            exp_to_next_lv = data.exp[1];
            exp_percent_in_this_lv = data.exp[2];
            hp_now = data.nowhp;
            hp_max = data.maxhp;
            condition = data.cond;
            fuel = data.fuel;
            ammo = data.bull;
            ndock_time = TimeSpan.FromMilliseconds(data.ndock_time);

            while(Slots.Count < data.slotnum)
                Slots.Add(-1);

            while(Slots.Count > data.slotnum)
                Slots.RemoveAt(Slots.Count - 1);

            while (OnSlotCount.Count < data.slotnum)
                OnSlotCount.Add(0);

            while (OnSlotCount.Count > data.slotnum)
                OnSlotCount.RemoveAt(OnSlotCount.Count - 1);

            for (int i = 0; i < data.slotnum; i++)
            {
                int slot = data.slot.Length > i ? data.slot[i] : -1;
                if (Slots[i] != slot)
                    Slots[i] = slot;

                int onslotcount = data.onslot.Length > i ? data.onslot[i] : 0;
                if (OnSlotCount[i] != onslotcount)
                    OnSlotCount[i] = onslotcount;
            }

            air_mastery = Tools.KanColleTools.ShipAirMastery(this);
            fire_power = data.karyoku[0];
            armor = data.soukou[0];
            torpedo = data.raisou[0];
            evasion = data.kaihi[0];
            anti_air = data.taiku[0];
            anti_submarine = data.taisen[0];
            reconnaissance = data.sakuteki[0];
            luck = data.lucky[0];
        }
    }
}
