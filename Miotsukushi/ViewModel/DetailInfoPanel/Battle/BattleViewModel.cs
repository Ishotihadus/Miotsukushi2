using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Battle
{
    class BattleViewModel : ViewModelBase
    {
        private string _FormationMe;
        public string FormationMe
        {
            get
            {
                return _FormationMe;
            }

            set
            {
                if (_FormationMe != value)
                {
                    _FormationMe = value;
                    OnPropertyChanged(() => FormationMe);
                }
            }
        }

        private string _FormationEnemy;
        public string FormationEnemy
        {
            get
            {
                return _FormationEnemy;
            }

            set
            {
                if (_FormationEnemy != value)
                {
                    _FormationEnemy = value;
                    OnPropertyChanged(() => FormationEnemy);
                }
            }
        }

        private string _SearchResultMe;
        public string SearchResultMe
        {
            get
            {
                return _SearchResultMe;
            }

            set
            {
                if (_SearchResultMe != value)
                {
                    _SearchResultMe = value;
                    OnPropertyChanged(() => SearchResultMe);
                }
            }
        }

        private string _SearchResultEnemy;
        public string SearchResultEnemy
        {
            get
            {
                return _SearchResultEnemy;
            }

            set
            {
                if (_SearchResultEnemy != value)
                {
                    _SearchResultEnemy = value;
                    OnPropertyChanged(() => SearchResultEnemy);
                }
            }
        }

        private string _AirMasteryStatus;
        public string AirMasteryStatus
        {
            get
            {
                return _AirMasteryStatus;
            }

            set
            {
                if (_AirMasteryStatus != value)
                {
                    _AirMasteryStatus = value;
                    OnPropertyChanged(() => AirMasteryStatus);
                }
            }
        }
        
        private string _CrossingStatus;
        public string CrossingStatus
        {
            get
            {
                return _CrossingStatus;
            }

            set
            {
                if (_CrossingStatus != value)
                {
                    _CrossingStatus = value;
                    OnPropertyChanged(() => CrossingStatus);
                }
            }
        }

        private double _GaugeMe;
        public double GaugeMe
        {
            get
            {
                return _GaugeMe;
            }

            set
            {
                if (_GaugeMe != value)
                {
                    _GaugeMe = value;
                    OnPropertyChanged(() => GaugeMe);
                }
            }
        }

        private double _GaugeEnemy;
        public double GaugeEnemy
        {
            get
            {
                return _GaugeEnemy;
            }

            set
            {
                if (_GaugeEnemy != value)
                {
                    _GaugeEnemy = value;
                    OnPropertyChanged(() => GaugeEnemy);
                }
            }
        }




        public BattleViewModel()
        {
            var kcmodel = Model.MainModel.Current.kancolleModel;
            kcmodel.battlemodel.BattleAnalyzed += Battlemodel_BattleAnalyzed;
        }

        private void Battlemodel_BattleAnalyzed(object sender, Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs e)
        {
            FormationMe = FormationString(e.friend_formation);
            FormationEnemy = FormationString(e.enemy_formation);
            AirMasteryStatus = AirMasteryString(e.air_mastery);
            CrossingStatus = CrossingString(e.crossing_type);
            GaugeMe = e.friend_gauge * 100;
            GaugeEnemy = e.enemy_gauge * 100;
        }

        private string FormationString(Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation formation)
        {
            switch(formation)
            {
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.tanju: return "単縦陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.fukuju: return "複従陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.rinkei: return "輪形陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.teikei: return "梯形陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.tanou: return "単横陣";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.daiichikeikai: return "第一警戒航行序列";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.dainikeikai: return "第二警戒航行序列";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.daisankeikai: return "第三警戒航行序列";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.Formation.daiyonkeikai: return "第四警戒航行序列";
                default: return "不明";
            }
        }

        private string CrossingString(Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType crossing_type)
        {
            switch(crossing_type)
            {
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.parallel: return "同航戦";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.anti_parallel: return "反航戦";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.cross_adv: return "T字有利";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.CrossingType.cross_disadv: return "T字不利";
                default: return "不明";
            }
        }

        private string AirMasteryString(Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus air_mastery)
        {
            switch(air_mastery)
            {
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.secure: return "制空権確保";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.superior: return "制空優勢";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.tie: return "制空均衡";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.inferior: return "制空劣勢";
                case Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs.AirMasteryStatus.lost: return "制空権喪失";
                default: return "不明";
            }
        }
    }
}
