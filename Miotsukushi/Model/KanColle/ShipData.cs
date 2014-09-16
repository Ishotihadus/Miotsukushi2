using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
