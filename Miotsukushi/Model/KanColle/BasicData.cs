using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class BasicData : NotifyModelBase
    {
        private string _admiral_name;
        public string admiral_name
        {
            get
            {
                return _admiral_name;
            }

            set
            {
                if (_admiral_name != value)
                {
                    _admiral_name = value;
                    OnPropertyChanged(() => admiral_name);
                }
            }
        }

        private string _admiral_comment;
        public string admiral_comment
        {
            get
            {
                return _admiral_comment;
            }

            set
            {
                if (_admiral_comment != value)
                {
                    _admiral_comment = value;
                    OnPropertyChanged(() => admiral_comment);
                }
            }
        }

        private int _admiral_level;
        public int admiral_level
        {
            get
            {
                return _admiral_level;
            }

            set
            {
                if (_admiral_level != value)
                {
                    _admiral_level = value;
                    OnPropertyChanged(() => admiral_level);
                    OnPropertyChanged(() => resource_cap);
                }
            }
        }

        private int _admiral_exp;
        public int admiral_exp
        {
            get
            {
                return _admiral_exp;
            }

            set
            {
                if (_admiral_exp != value)
                {
                    _admiral_exp = value;
                    OnPropertyChanged(() => admiral_exp);
                }
            }
        }

        private AdmiralRank _admiral_rank;
        public AdmiralRank admiral_rank
        {
            get
            {
                return _admiral_rank;
            }

            set
            {
                if (_admiral_rank != value)
                {
                    _admiral_rank = value;
                    OnPropertyChanged(() => admiral_rank);
                }
            }
        }

        private int _max_ship;
        public int max_ship
        {
            get
            {
                return _max_ship;
            }

            set
            {
                if (_max_ship != value)
                {
                    _max_ship = value;
                    OnPropertyChanged(() => max_ship);
                    remaincount_append();
                }
            }
        }

        private int _max_equipment;
        public int max_equipment
        {
            get
            {
                return _max_equipment;
            }

            set
            {
                if (_max_equipment != value)
                {
                    _max_equipment = value;
                    OnPropertyChanged(() => max_equipment);
                    remaincount_append();
                }
            }
        }

        private int _now_ship_number;
        public int now_ship_number
        {
            get
            {
                return _now_ship_number;
            }

            set
            {
                if (_now_ship_number != value)
                {
                    _now_ship_number = value;
                    OnPropertyChanged(() => now_ship_number);
                    remaincount_append();
                }
            }
        }

        private int _now_equipment_number;
        public int now_equipment_number
        {
            get
            {
                return _now_equipment_number;
            }

            set
            {
                if (_now_equipment_number != value)
                {
                    _now_equipment_number = value;
                    OnPropertyChanged(() => now_equipment_number);
                    remaincount_append();
                }
            }
        }

        private int _remain_count;
        public int remain_count
        {
            get
            {
                return _remain_count;
            }

            set
            {
                if (_remain_count != value)
                {
                    _remain_count = value;
                    OnPropertyChanged(() => remain_count);
                }
            }
        }

        private int _fuel;
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

        private int _steel;
        public int steel
        {
            get
            {
                return _steel;
            }

            set
            {
                if (_steel != value)
                {
                    _steel = value;
                    OnPropertyChanged(() => steel);
                }
            }
        }

        private int _bauxite;
        public int bauxite
        {
            get
            {
                return _bauxite;
            }

            set
            {
                if (_bauxite != value)
                {
                    _bauxite = value;
                    OnPropertyChanged(() => bauxite);
                }
            }
        }


        /// <summary>
        /// 資源最大値
        /// </summary>
        public int resource_cap
        {
            get
            {
                return admiral_level * 250 + 750;
            }
        }

        private int _instant_repair;

        /// <summary>
        /// 高速修復材
        /// </summary>
        public int instant_repair
        {
            get
            {
                return _instant_repair;
            }

            set
            {
                if (_instant_repair != value)
                {
                    _instant_repair = value;
                    OnPropertyChanged(() => instant_repair);
                }
            }
        }

        private int _instant_construction;

        /// <summary>
        /// 高速建造材
        /// </summary>
        public int instant_construction
        {
            get
            {
                return _instant_construction;
            }

            set
            {
                if (_instant_construction != value)
                {
                    _instant_construction = value;
                    OnPropertyChanged(() => instant_construction);
                }
            }
        }

        private int _development_material;

        /// <summary>
        /// 開発資材
        /// </summary>
        public int development_material
        {
            get
            {
                return _development_material;
            }

            set
            {
                if (_development_material != value)
                {
                    _development_material = value;
                    OnPropertyChanged(() => development_material);
                }
            }
        }

        private int _furniture_coin;

        /// <summary>
        /// 家具コイン
        /// </summary>
        public int furniture_coin
        {
            get
            {
                return _furniture_coin;
            }

            set
            {
                if (_furniture_coin != value)
                {
                    _furniture_coin = value;
                    OnPropertyChanged(() => furniture_coin);
                }
            }
        }

        private int _modding_material;

        /// <summary>
        /// 改修資材
        /// </summary>
        public int modding_material
        {
            get
            {
                return _modding_material;
            }

            set
            {
                if(_modding_material != value)
                {
                    _modding_material = value;
                    OnPropertyChanged(() => modding_material);
                }
            }
        }


        public void AppendRank(int rank)
        {
            try
            {
                admiral_rank = (AdmiralRank)rank;
            }
            catch
            {
                admiral_rank = AdmiralRank.Unknown;
            }
        }

        public void FromMaterial(KanColleLib.TransmissionData.api_get_member.Material data)
        {
            foreach (var m in data.materials)
            {
                switch (m.id)
                {
                    case 1:
                        fuel = m.value;
                        break;
                    case 2:
                        ammo = m.value;
                        break;
                    case 3:
                        steel = m.value;
                        break;
                    case 4:
                        bauxite = m.value;
                        break;
                    case 5:
                        instant_construction = m.value;
                        break;
                    case 6:
                        instant_repair = m.value;
                        break;
                    case 7:
                        development_material = m.value;
                        break;
                    case 8:
                        modding_material = m.value;
                        break;
                }
            }
        }

        public void FromMaterialArray(int[] material)
        {
            for (var i = 0; i < material.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        fuel = material[i];
                        break;
                    case 1:
                        ammo = material[i];
                        break;
                    case 2:
                        steel = material[i];
                        break;
                    case 3:
                        bauxite = material[i];
                        break;
                    case 4:
                        instant_construction = material[i];
                        break;
                    case 5:
                        instant_repair = material[i];
                        break;
                    case 6:
                        development_material = material[i];
                        break;
                    case 7:
                        modding_material = material[i];
                        break;
                }
            }
        }

        private void remaincount_append()
        {
            var shipremain = max_ship - now_ship_number;
            var equipremain = (max_equipment + 3 - now_equipment_number) / 3;
            remain_count = shipremain < equipremain ? shipremain : equipremain;
        }
    }
}
