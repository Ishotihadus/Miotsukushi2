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
        public int Shipid
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
                    OnPropertyChanged(() => Shipid);
                }
            }
        }

        private int _characterid;

        /// <summary>
        /// 艦のID
        /// 改造時とかに変わるかも
        /// </summary>
        public int Characterid
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
                    OnPropertyChanged(() => Characterid);
                }
            }
        }

        /// <summary>
        /// 艦娘のキャラクター情報に直接アクセスする
        /// キャラクター情報がなければnullを返す
        /// </summary>
        public CharacterData Characterinfo
        {
            get
            {
                if (Model.MainModel.Current != null && Model.MainModel.Current.KancolleModel != null && Model.MainModel.Current.KancolleModel.Charamaster != null &&
                    Model.MainModel.Current.KancolleModel.Charamaster.ContainsKey(Characterid))
                    return Model.MainModel.Current.KancolleModel.Charamaster[Characterid];
                else
                    return null;
            }
        }

        private int _level;
        
        /// <summary>
        /// レベル
        /// </summary>
        public int Level
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
                    OnPropertyChanged(() => Level);
                }
            }
        }

        private int _expTotal;
        
        /// <summary>
        /// 累計経験値
        /// </summary>
        public int ExpTotal
        {
            get
            {
                return _expTotal;
            }

            set
            {
                if (_expTotal != value)
                {
                    _expTotal = value;
                    OnPropertyChanged(() => ExpTotal);
                }
            }
        }

        private int _expToNextLv;

        /// <summary>
        /// 次のレベルまでの経験値
        /// </summary>
        public int ExpToNextLv
        {
            get
            {
                return _expToNextLv;
            }

            set
            {
                if (_expToNextLv != value)
                {
                    _expToNextLv = value;
                    OnPropertyChanged(() => ExpToNextLv);
                }
            }
        }

        private int _expPercentInThisLv;

        /// <summary>
        /// 今のレベルでの経験値パーセント
        /// </summary>
        public int ExpPercentInThisLv
        {
            get
            {
                return _expPercentInThisLv;
            }

            set
            {
                if (_expPercentInThisLv != value)
                {
                    _expPercentInThisLv = value;
                    OnPropertyChanged(() => ExpPercentInThisLv);
                }
            }
        }

        private int _hpNow;

        /// <summary>
        /// 今のHP
        /// </summary>
        public int HpNow
        {
            get
            {
                return _hpNow;
            }

            set
            {
                if (_hpNow != value)
                {
                    _hpNow = value;
                    OnPropertyChanged(() => HpNow);
                }
            }
        }

        private int _hpMax;

        /// <summary>
        /// 最大HP
        /// </summary>
        public int HpMax
        {
            get
            {
                return _hpMax;
            }

            set
            {
                if (_hpMax != value)
                {
                    _hpMax = value;
                    OnPropertyChanged(() => HpMax);
                }
            }
        }

        private int _condition;

        /// <summary>
        /// コンディション
        /// </summary>
        public int Condition
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
                    OnPropertyChanged(() => Condition);
                }
            }
        }

        private int _fuel;
        
        /// <summary>
        /// 現在の燃料
        /// </summary>
        public int Fuel
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
                    OnPropertyChanged(() => Fuel);
                }
            }
        }

        private int _ammo;

        /// <summary>
        /// 現在の弾薬
        /// </summary>
        public int Ammo
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
                    OnPropertyChanged(() => Ammo);
                }
            }
        }

        private TimeSpan _ndockTime;

        /// <summary>
        /// 入渠時間
        /// </summary>
        public TimeSpan NdockTime
        {
            get
            {
                return _ndockTime;
            }

            set
            {
                if (_ndockTime != value)
                {
                    _ndockTime = value;
                    OnPropertyChanged(() => NdockTime);
                }
            }
        }

        public ObservableCollection<int> Slots = new ObservableCollection<int>();
        public ObservableCollection<int> OnSlotCount = new ObservableCollection<int>();

        private int _airMastery;

        /// <summary>
        /// 制空値
        /// </summary>
        public int AirMastery
        {
            get
            {
                return _airMastery;
            }

            set
            {
                if (_airMastery != value)
                {
                    _airMastery = value;
                    OnPropertyChanged(() => AirMastery);
                }
            }
        }


        private int _firePower;

        /// <summary>
        /// 火力
        /// </summary>
        public int FirePower
        {
            get
            {
                return _firePower;
            }

            set
            {
                if (_firePower != value)
                {
                    _firePower = value;
                    OnPropertyChanged(() => FirePower);
                }
            }
        }


        private int _armor;

        /// <summary>
        /// 装甲
        /// </summary>
        public int Armor
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
                    OnPropertyChanged(() => Armor);
                }
            }
        }


        private int _torpedo;

        /// <summary>
        /// 雷装
        /// </summary>
        public int Torpedo
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
                    OnPropertyChanged(() => Torpedo);
                }
            }
        }


        private int _evasion;

        /// <summary>
        /// 回避
        /// </summary>
        public int Evasion
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
                    OnPropertyChanged(() => Evasion);
                }
            }
        }


        private int _antiAir;

        /// <summary>
        /// 対空
        /// </summary>
        public int AntiAir
        {
            get
            {
                return _antiAir;
            }

            set
            {
                if (_antiAir != value)
                {
                    _antiAir = value;
                    OnPropertyChanged(() => AntiAir);
                }
            }
        }


        private int _antiSubmarine;

        /// <summary>
        /// 対潜
        /// </summary>
        public int AntiSubmarine
        {
            get
            {
                return _antiSubmarine;
            }

            set
            {
                if (_antiSubmarine != value)
                {
                    _antiSubmarine = value;
                    OnPropertyChanged(() => AntiSubmarine);
                }
            }
        }


        private int _reconnaissance;

        /// <summary>
        /// 索敵
        /// </summary>
        public int Reconnaissance
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
                    OnPropertyChanged(() => Reconnaissance);
                }
            }
        }


        private int _luck;

        /// <summary>
        /// 運
        /// </summary>
        public int Luck
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
                    OnPropertyChanged(() => Luck);
                }
            }
        }


        private bool _exSlotOpened;
        /// <summary>
        /// 穴が開いているか
        /// </summary>
        public bool ExSlotOpened
        {
            get
            {
                return _exSlotOpened;
            }

            set
            {
                if (_exSlotOpened != value)
                {
                    _exSlotOpened = value;
                    OnPropertyChanged(() => ExSlotOpened);
                }
            }
        }
        
        private int _exSlot;
        /// <summary>
        /// 穴に入ってる装備
        /// </summary>
        public int ExSlot
        {
            get
            {
                return _exSlot;
            }

            set
            {
                if (_exSlot != value)
                {
                    _exSlot = value;
                    OnPropertyChanged(() => ExSlot);
                }
            }
        }



        #endregion

        public void FromKanColleLib(KanColleLib.TransmissionData.api_get_member.values.ShipValue data)
        {
            Shipid = data.id;
            Characterid = data.ship_id;
            Level = data.lv;
            ExpTotal = data.exp[0];
            ExpToNextLv = data.exp[1];
            ExpPercentInThisLv = data.exp[2];
            HpNow = data.nowhp;
            HpMax = data.maxhp;
            Condition = data.cond;
            Fuel = data.fuel;
            Ammo = data.bull;
            NdockTime = TimeSpan.FromMilliseconds(data.ndock_time);

            while(Slots.Count < data.slotnum)
                Slots.Add(-1);

            while(Slots.Count > data.slotnum)
                Slots.RemoveAt(Slots.Count - 1);

            while (OnSlotCount.Count < data.slotnum)
                OnSlotCount.Add(0);

            while (OnSlotCount.Count > data.slotnum)
                OnSlotCount.RemoveAt(OnSlotCount.Count - 1);

            for (var i = 0; i < data.slotnum; i++)
            {
                var slot = data.slot.Length > i ? data.slot[i] : -1;
                if (Slots[i] != slot)
                    Slots[i] = slot;

                var onslotcount = data.onslot.Length > i ? data.onslot[i] : 0;
                if (OnSlotCount[i] != onslotcount)
                    OnSlotCount[i] = onslotcount;
            }

            AirMastery = Tools.KanColleTools.ShipAirMastery(this);
            FirePower = data.karyoku[0];
            Armor = data.soukou[0];
            Torpedo = data.raisou[0];
            Evasion = data.kaihi[0];
            AntiAir = data.taiku[0];
            AntiSubmarine = data.taisen[0];
            Reconnaissance = data.sakuteki[0];
            Luck = data.lucky[0];

            ExSlotOpened = data.slot_ex != 0;
            ExSlot = data.slot_ex;
        }
    }
}
