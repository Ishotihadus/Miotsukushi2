using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BattleModels = Miotsukushi.Model.KanColle.BattleModels;
using BattleAnalyzedEventArgs = Miotsukushi.Model.KanColle.BattleModels.EventArgs.BattleAnalyzedEventArgs;

namespace Miotsukushi.ViewModel.DetailInfoPanel.BattleDetail
{
    class BattleDetailViewModel : ViewModelBase
    {
        public class PhaseViewModel : ViewModelBase
        {
            public class AttackViewModel
            {
                /// <summary>
                /// 攻撃する艦
                /// </summary>
                public string ShipFrom { get; set; }

                /// <summary>
                /// 攻撃される艦
                /// </summary>
                public string ShipTo { get; set; }

                /// <summary>
                /// ダメージの量
                /// </summary>
                public int Damage { get; set; }

                /// <summary>
                /// 攻撃された艦の後のHP
                /// </summary>
                public int ShipToAfterHP { get; set; }

                /// <summary>
                /// 攻撃された艦のMaxHP
                /// </summary>
                public int ShipToMaxHP { get; set; }
            }

            private string _PhaseName;
            public string PhaseName
            {
                get
                {
                    return _PhaseName;
                }

                set
                {
                    if (_PhaseName != value)
                    {
                        _PhaseName = value;
                        OnPropertyChanged(() => PhaseName);
                    }
                }
            }

            private List<AttackViewModel> _Attacks;
            public List<AttackViewModel> Attacks
            {
                get
                {
                    return _Attacks;
                }

                set
                {
                    if (_Attacks != value)
                    {
                        _Attacks = value;
                        OnPropertyChanged(() => Attacks);
                    }
                }
            }

        }

        private List<PhaseViewModel> _Phases;
        public List<PhaseViewModel> Phases
        {
            get
            {
                return _Phases;
            }

            set
            {
                if (_Phases != value)
                {
                    _Phases = value;
                    OnPropertyChanged(() => Phases);
                }
            }
        }

        Model.KanColle.KanColleModel kcmodel;

        public BattleDetailViewModel()
        {
            kcmodel = Model.MainModel.Current.kancolleModel;
            kcmodel.battlemodel.BattleAnalyzed += Battlemodel_BattleAnalyzed;
        }

        private void Battlemodel_BattleAnalyzed(object sender, BattleAnalyzedEventArgs e)
        {
            var ships = new Dictionary<BattleAnalyzedEventArgs.Ship, int>();
            foreach (var ship in e.friend)
                ships.Add(ship, ship.before_hp);
            if(e.friend_combined != null)
                foreach (var ship in e.friend_combined)
                    ships.Add(ship, ship.before_hp);
            foreach (var ship in e.enemy)
                ships.Add(ship, ship.before_hp);

            var phases = new List<PhaseViewModel>();
            foreach(var phasem in e.phases)
            {
                var phasevm = new PhaseViewModel();
                phasevm.PhaseName = phasem.phase_name;
                phasevm.Attacks = new List<PhaseViewModel.AttackViewModel>();
                if(phasem is BattleAnalyzedEventArgs.AllOverPhase)
                {
                    var phasem_r = phasem as BattleAnalyzedEventArgs.AllOverPhase;
                    foreach(var attack in phasem_r.attackee)
                    {
                        ships[attack.target_ship] -= attack.damage;
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipTo = attack.target_ship.name,
                            Damage = attack.damage,
                            ShipToAfterHP = ships[attack.target_ship],
                            ShipToMaxHP = attack.target_ship.max_hp
                        });
                    }
                    foreach(var attack in phasem_r.damecon)
                    {
                        ships[attack.target_ship] -= attack.damage;
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipFrom = attack.damecon_type == 2 ? "ダメコン女神発動" : "ダメコン発動",
                            ShipTo = attack.target_ship.name,
                            Damage = attack.damage,
                            ShipToAfterHP = ships[attack.target_ship],
                            ShipToMaxHP = attack.target_ship.max_hp
                        });
                    }
                }
                else if(phasem is BattleAnalyzedEventArgs.InOrderPhase)
                {
                    var phasem_r = phasem as BattleAnalyzedEventArgs.InOrderPhase;
                    foreach (var attack in phasem_r.attacks)
                    {
                        ships[attack.target_ship] -= attack.damage;
                        
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipFrom = attack.damecon_type == 0 ? attack.origin_ship.name : 
                                attack.damecon_type == 2 ? "ダメコン女神発動" : "ダメコン発動",
                            ShipTo = attack.target_ship.name,
                            Damage = attack.damage,
                            ShipToAfterHP = ships[attack.target_ship],
                            ShipToMaxHP = attack.target_ship.max_hp
                        });
                    }
                }
                phases.Add(phasevm);
            }
            Phases = phases;
        }
    }
}
