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
                public class ShipViewModel
                {
                    public string Name { get; set; }

                    public int MaxHP { get; set; }

                    public int Speed { get; set; }

                    public bool IsFriend { get; set; }

                    public override string ToString()
                    {
                        return Name;
                    }
                }

                /// <summary>
                /// 攻撃する艦
                /// </summary>
                public ShipViewModel ShipFrom { get; set; }

                /// <summary>
                /// 攻撃される艦
                /// </summary>
                public ShipViewModel ShipTo { get; set; }

                /// <summary>
                /// ダメージの量
                /// </summary>
                public int Damage { get; set; }

                /// <summary>
                /// 攻撃された艦の後のHP
                /// </summary>
                public int ShipToAfterHP { get; set; }
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

        readonly static PhaseViewModel.AttackViewModel.ShipViewModel dameconvm = new PhaseViewModel.AttackViewModel.ShipViewModel()
        {
            Name = "ダメコン発動"
        };

        readonly static PhaseViewModel.AttackViewModel.ShipViewModel damecongoddessvm = new PhaseViewModel.AttackViewModel.ShipViewModel()
        {
            Name = "ダメコン女神発動"
        };

        private void Battlemodel_BattleAnalyzed(object sender, BattleAnalyzedEventArgs e)
        {
            var ships = new Dictionary<BattleAnalyzedEventArgs.Ship, int>();
            var shipsvm = new Dictionary<BattleAnalyzedEventArgs.Ship, PhaseViewModel.AttackViewModel.ShipViewModel>();
            foreach (var ship in e.friend)
            {
                ships.Add(ship, ship.before_hp);
                shipsvm.Add(ship, new PhaseViewModel.AttackViewModel.ShipViewModel()
                {
                    IsFriend = true,
                    Name = ship.name,
                    MaxHP = ship.max_hp,
                    Speed = ship.speed
                });
            }
            if(e.friend_combined != null)
                foreach (var ship in e.friend_combined)
                {
                    ships.Add(ship, ship.before_hp);
                    shipsvm.Add(ship, new PhaseViewModel.AttackViewModel.ShipViewModel()
                    {
                        IsFriend = true,
                        Name = ship.name,
                        MaxHP = ship.max_hp,
                        Speed = ship.speed
                    });
                }
            foreach (var ship in e.enemy)
            {
                ships.Add(ship, ship.before_hp);
                shipsvm.Add(ship, new PhaseViewModel.AttackViewModel.ShipViewModel()
                {
                    IsFriend = false,
                    Name = ship.name,
                    MaxHP = ship.max_hp,
                    Speed = ship.speed
                });
            }

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
                            ShipTo = shipsvm[attack.target_ship],
                            Damage = attack.damage,
                            ShipToAfterHP = ships[attack.target_ship]
                        });
                    }
                    foreach(var attack in phasem_r.damecon)
                    {
                        ships[attack.target_ship] -= attack.damage;
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipFrom = attack.damecon_type == 2 ? damecongoddessvm : dameconvm,
                            ShipTo = shipsvm[attack.target_ship],
                            Damage = attack.damage,
                            ShipToAfterHP = ships[attack.target_ship]
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
                            ShipFrom = attack.damecon_type == 0 ? shipsvm[attack.origin_ship] : 
                                attack.damecon_type == 2 ? damecongoddessvm : dameconvm,
                            ShipTo = shipsvm[attack.target_ship],
                            Damage = attack.damage,
                            ShipToAfterHP = ships[attack.target_ship]
                        });
                    }
                }
                phases.Add(phasevm);
            }
            Phases = phases;
        }
    }
}
