using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class BasicData : NotifyModelBase
    {
        private string _admiralName;
        public string AdmiralName
        {
            get
            {
                return _admiralName;
            }

            set
            {
                if (_admiralName != value)
                {
                    _admiralName = value;
                    OnPropertyChanged(() => AdmiralName);
                }
            }
        }

        private string _admiralComment;
        public string AdmiralComment
        {
            get
            {
                return _admiralComment;
            }

            set
            {
                if (_admiralComment != value)
                {
                    _admiralComment = value;
                    OnPropertyChanged(() => AdmiralComment);
                }
            }
        }

        private int _admiralLevel;
        public int AdmiralLevel
        {
            get
            {
                return _admiralLevel;
            }

            set
            {
                if (_admiralLevel != value)
                {
                    _admiralLevel = value;
                    OnPropertyChanged(() => AdmiralLevel);
                    OnPropertyChanged(() => ResourceCap);
                }
            }
        }

        private int _admiralExp;
        public int AdmiralExp
        {
            get
            {
                return _admiralExp;
            }

            set
            {
                if (_admiralExp != value)
                {
                    _admiralExp = value;
                    OnPropertyChanged(() => AdmiralExp);
                }
            }
        }

        private AdmiralRank _admiralRank;
        public AdmiralRank AdmiralRank
        {
            get
            {
                return _admiralRank;
            }

            set
            {
                if (_admiralRank != value)
                {
                    _admiralRank = value;
                    OnPropertyChanged(() => AdmiralRank);
                }
            }
        }

        private int _maxShip;
        public int MaxShip
        {
            get
            {
                return _maxShip;
            }

            set
            {
                if (_maxShip != value)
                {
                    _maxShip = value;
                    OnPropertyChanged(() => MaxShip);
                    remaincount_append();
                }
            }
        }

        private int _maxEquipment;
        public int MaxEquipment
        {
            get
            {
                return _maxEquipment;
            }

            set
            {
                if (_maxEquipment != value)
                {
                    _maxEquipment = value;
                    OnPropertyChanged(() => MaxEquipment);
                    remaincount_append();
                }
            }
        }

        private int _nowShipNumber;
        public int NowShipNumber
        {
            get
            {
                return _nowShipNumber;
            }

            set
            {
                if (_nowShipNumber != value)
                {
                    _nowShipNumber = value;
                    OnPropertyChanged(() => NowShipNumber);
                    remaincount_append();
                }
            }
        }

        private int _nowEquipmentNumber;
        public int NowEquipmentNumber
        {
            get
            {
                return _nowEquipmentNumber;
            }

            set
            {
                if (_nowEquipmentNumber != value)
                {
                    _nowEquipmentNumber = value;
                    OnPropertyChanged(() => NowEquipmentNumber);
                    remaincount_append();
                }
            }
        }

        private int _remainCount;
        public int RemainCount
        {
            get
            {
                return _remainCount;
            }

            set
            {
                if (_remainCount != value)
                {
                    _remainCount = value;
                    OnPropertyChanged(() => RemainCount);
                }
            }
        }

        private int _fuel;
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

        private int _steel;
        public int Steel
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
                    OnPropertyChanged(() => Steel);
                }
            }
        }

        private int _bauxite;
        public int Bauxite
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
                    OnPropertyChanged(() => Bauxite);
                }
            }
        }


        /// <summary>
        /// 資源最大値
        /// </summary>
        public int ResourceCap
        {
            get
            {
                return AdmiralLevel * 250 + 750;
            }
        }

        private int _instantRepair;

        /// <summary>
        /// 高速修復材
        /// </summary>
        public int InstantRepair
        {
            get
            {
                return _instantRepair;
            }

            set
            {
                if (_instantRepair != value)
                {
                    _instantRepair = value;
                    OnPropertyChanged(() => InstantRepair);
                }
            }
        }

        private int _instantConstruction;

        /// <summary>
        /// 高速建造材
        /// </summary>
        public int InstantConstruction
        {
            get
            {
                return _instantConstruction;
            }

            set
            {
                if (_instantConstruction != value)
                {
                    _instantConstruction = value;
                    OnPropertyChanged(() => InstantConstruction);
                }
            }
        }

        private int _developmentMaterial;

        /// <summary>
        /// 開発資材
        /// </summary>
        public int DevelopmentMaterial
        {
            get
            {
                return _developmentMaterial;
            }

            set
            {
                if (_developmentMaterial != value)
                {
                    _developmentMaterial = value;
                    OnPropertyChanged(() => DevelopmentMaterial);
                }
            }
        }

        private int _furnitureCoin;

        /// <summary>
        /// 家具コイン
        /// </summary>
        public int FurnitureCoin
        {
            get
            {
                return _furnitureCoin;
            }

            set
            {
                if (_furnitureCoin != value)
                {
                    _furnitureCoin = value;
                    OnPropertyChanged(() => FurnitureCoin);
                }
            }
        }

        private int _moddingMaterial;

        /// <summary>
        /// 改修資材
        /// </summary>
        public int ModdingMaterial
        {
            get
            {
                return _moddingMaterial;
            }

            set
            {
                if(_moddingMaterial != value)
                {
                    _moddingMaterial = value;
                    OnPropertyChanged(() => ModdingMaterial);
                }
            }
        }


        public void AppendRank(int rank)
        {
            try
            {
                AdmiralRank = (AdmiralRank)rank;
            }
            catch
            {
                AdmiralRank = AdmiralRank.Unknown;
            }
        }

        public void FromMaterial(KanColleLib.TransmissionData.api_get_member.Material data)
        {
            foreach (var m in data.materials)
            {
                switch (m.id)
                {
                    case 1:
                        Fuel = m.value;
                        break;
                    case 2:
                        Ammo = m.value;
                        break;
                    case 3:
                        Steel = m.value;
                        break;
                    case 4:
                        Bauxite = m.value;
                        break;
                    case 5:
                        InstantConstruction = m.value;
                        break;
                    case 6:
                        InstantRepair = m.value;
                        break;
                    case 7:
                        DevelopmentMaterial = m.value;
                        break;
                    case 8:
                        ModdingMaterial = m.value;
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
                        Fuel = material[i];
                        break;
                    case 1:
                        Ammo = material[i];
                        break;
                    case 2:
                        Steel = material[i];
                        break;
                    case 3:
                        Bauxite = material[i];
                        break;
                    case 4:
                        InstantConstruction = material[i];
                        break;
                    case 5:
                        InstantRepair = material[i];
                        break;
                    case 6:
                        DevelopmentMaterial = material[i];
                        break;
                    case 7:
                        ModdingMaterial = material[i];
                        break;
                }
            }
        }

        private void remaincount_append()
        {
            var shipremain = MaxShip - NowShipNumber;
            var equipremain = (MaxEquipment + 3 - NowEquipmentNumber) / 3;
            RemainCount = shipremain < equipremain ? shipremain : equipremain;
        }
    }
}
