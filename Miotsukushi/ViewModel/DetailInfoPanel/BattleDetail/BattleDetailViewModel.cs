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
            kcmodel = Model.MainModel.Current.KancolleModel;
            kcmodel.Battlemodel.BattleAnalyzed += Battlemodel_BattleAnalyzed;
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
            foreach (var ship in e.Friend)
            {
                ships.Add(ship, ship.BeforeHp);
                shipsvm.Add(ship, new PhaseViewModel.AttackViewModel.ShipViewModel()
                {
                    IsFriend = true,
                    Name = ship.Name,
                    MaxHP = ship.MaxHp,
                    Speed = ship.Speed
                });
            }
            if(e.FriendCombined != null)
                foreach (var ship in e.FriendCombined)
                {
                    ships.Add(ship, ship.BeforeHp);
                    shipsvm.Add(ship, new PhaseViewModel.AttackViewModel.ShipViewModel()
                    {
                        IsFriend = true,
                        Name = ship.Name,
                        MaxHP = ship.MaxHp,
                        Speed = ship.Speed
                    });
                }
            foreach (var ship in e.Enemy)
            {
                ships.Add(ship, ship.BeforeHp);
                shipsvm.Add(ship, new PhaseViewModel.AttackViewModel.ShipViewModel()
                {
                    IsFriend = false,
                    Name = ship.Name,
                    MaxHP = ship.MaxHp,
                    Speed = ship.Speed
                });
            }

            var phases = new List<PhaseViewModel>();
            foreach(var phasem in e.Phases)
            {
                var phasevm = new PhaseViewModel();
                phasevm.PhaseName = phasem.PhaseName;
                phasevm.Attacks = new List<PhaseViewModel.AttackViewModel>();
                if(phasem is BattleAnalyzedEventArgs.AllOverPhase)
                {
                    var phasem_r = phasem as BattleAnalyzedEventArgs.AllOverPhase;
                    foreach(var attack in phasem_r.Attackee)
                    {
                        ships[attack.TargetShip] -= attack.Damage;
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipTo = shipsvm[attack.TargetShip],
                            Damage = attack.Damage,
                            ShipToAfterHP = ships[attack.TargetShip]
                        });
                    }
                    foreach(var attack in phasem_r.Damecon)
                    {
                        ships[attack.TargetShip] -= attack.Damage;
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipFrom = attack.DameconType == 2 ? damecongoddessvm : dameconvm,
                            ShipTo = shipsvm[attack.TargetShip],
                            Damage = attack.Damage,
                            ShipToAfterHP = ships[attack.TargetShip]
                        });
                    }
                }
                else if(phasem is BattleAnalyzedEventArgs.InOrderPhase)
                {
                    var phasem_r = phasem as BattleAnalyzedEventArgs.InOrderPhase;
                    foreach (var attack in phasem_r.Attacks)
                    {
                        ships[attack.TargetShip] -= attack.Damage;
                        
                        phasevm.Attacks.Add(new PhaseViewModel.AttackViewModel()
                        {
                            ShipFrom = attack.DameconType == 0 ? shipsvm[attack.OriginShip] : 
                                attack.DameconType == 2 ? damecongoddessvm : dameconvm,
                            ShipTo = shipsvm[attack.TargetShip],
                            Damage = attack.Damage,
                            ShipToAfterHP = ships[attack.TargetShip]
                        });
                    }
                }
                phases.Add(phasevm);
            }
            Phases = phases;
        }
    }
}
