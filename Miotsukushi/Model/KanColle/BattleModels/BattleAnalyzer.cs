using System;
using System.Collections.Generic;
using System.Linq;
using KanColleLib.TransmissionData.api_req_sortie.values;
using Miotsukushi.Model.KanColle.BattleModels.EventArgs;
using api_req_sortie = KanColleLib.TransmissionData.api_req_sortie;
using api_req_battle_midnight = KanColleLib.TransmissionData.api_req_battle_midnight;
using api_req_combined_battle = KanColleLib.TransmissionData.api_req_combined_battle;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    class BattleAnalyzer
    {
        /// <summary>
        /// 夜戦突入用昼戦データ
        /// </summary>
        private static BattleAnalyzedEventArgs _dayBattle;

        /// <summary>
        /// ダメコンを使用しなければいけないかどうか判断する
        /// ダメコンを使用した場合はafter_hpは自動で更新される
        /// </summary>
        /// <param name="ship">被攻撃を考慮した後のafter_hpを持つship</param>
        /// <returns>使用すれば使用情報、使用しなければnull</returns>
        private static BattleAnalyzedEventArgs.Phase.Attack DameConCheck(BattleAnalyzedEventArgs.Ship ship)
        {
            if (ship.AfterHp <= 0 && ship.Damecontype != BattleAnalyzedEventArgs.Ship.DameConType.None)
            {
                ship.UseDamecon = true;
                int recoverhp;
                if (ship.Damecontype == BattleAnalyzedEventArgs.Ship.DameConType.Goddess)
                {
                    recoverhp = ship.MaxHp - ship.AfterHp;
                }
                else
                {
                    recoverhp = (int)(ship.MaxHp * 0.2) - ship.AfterHp;
                }

                ship.AfterHp += recoverhp;
                return new BattleAnalyzedEventArgs.Phase.Attack()
                {
                    Damage = 0 - recoverhp,
                    TargetShip = ship,
                    DameconType = (ship.Damecontype == BattleAnalyzedEventArgs.Ship.DameConType.Goddess) ? 2 : 1
                };
            }
            else
                return null;   
        }

        /// <summary>
        /// 自艦隊の情報を入手する
        /// </summary>
        /// <param name="dockId">艦隊番号</param>
        /// <param name="maxhps">svdataのmaxhpsそのまま</param>
        /// <param name="nowhps">svdataのnowhpsそのまま</param>
        /// <param name="fParam"></param>
        /// <returns></returns>
        private static List<BattleAnalyzedEventArgs.Ship> GetFriendshipList(int dockId, int[] maxhps, int[] nowhps, int[][] fParam)
        {
            var kcmodel = MainModel.Current.KancolleModel;

            var friendship = kcmodel.Fleetdata[dockId - 1].Ships.Select(ship => new BattleAnalyzedEventArgs.Ship() {OriginalId = ship}).ToList();
            for (var i = 0; i < friendship.Count; i++)
            {
                var ship = friendship[i];

                ship.FirePower = fParam[i][0];
                ship.Torpedo = fParam[i][1];
                ship.AntiAir = fParam[i][2];
                ship.Armor = fParam[i][3];

                var shipdata = kcmodel.Shipdata.FirstOrDefault(_ => _.Shipid == ship.OriginalId);
                if (shipdata != null && shipdata.Characterinfo != null)
                {
                    ship.Name = shipdata.Characterinfo.Name;
                    ship.Level = shipdata.Level;
                    ship.Escaped = false;
                    ship.CharacterId = shipdata.Characterid;
                    ship.MaxHp = maxhps[i + 1];
                    ship.BeforeHp = nowhps[i + 1];
                    ship.AfterHp = ship.BeforeHp;
                    ship.Speed = shipdata.Characterinfo.Speed;
                    ship.Slot = new int[shipdata.Characterinfo.SlotCount];
                    for (var j = 0; j < ship.Slot.Length; j++)
                    {
                        ship.Slot[j] = -1;
                    }
                    for (var j = 0; j < shipdata.Slots.Count; j++)
                    {
                        var slotdata = kcmodel.Slotdata.FirstOrDefault(_ => _.Id == shipdata.Slots[j]);
                        if (slotdata != null)
                            ship.Slot[j] = slotdata.Itemid;
                    }

                    ship.Damecontype = BattleAnalyzedEventArgs.Ship.DameConType.None;
                    if(shipdata.ExSlotOpened)
                    {
                        var slotdata = kcmodel.Slotdata.FirstOrDefault(_ => _.Id == shipdata.ExSlot);
                        if (slotdata != null)
                            if (slotdata.Itemid == 42)
                                ship.Damecontype = BattleAnalyzedEventArgs.Ship.DameConType.Normal;
                            else if (slotdata.Itemid == 43)
                                ship.Damecontype = BattleAnalyzedEventArgs.Ship.DameConType.Goddess;
                    }

                    if(ship.Damecontype == BattleAnalyzedEventArgs.Ship.DameConType.None)
                    {
                        foreach (var slot in ship.Slot)
                        {
                            if (slot == 42)
                            {
                                ship.Damecontype = BattleAnalyzedEventArgs.Ship.DameConType.Normal;
                                break;
                            }
                            else if (slot == 43)
                            {
                                ship.Damecontype = BattleAnalyzedEventArgs.Ship.DameConType.Goddess;
                                break;
                            }
                        }
                    }
                }
            }
            return friendship;
        }

        private static List<BattleAnalyzedEventArgs.Ship> GetEnemyshipList(int[] shipKe, int[] shipLv, int[] maxhps, int[] nowhps, int[][] eParam, int[][] eSlot)
        {
            var kcmodel = MainModel.Current.KancolleModel;

            var enemyship = (from id in shipKe where id != -1 select new BattleAnalyzedEventArgs.Ship() {CharacterId = id}).ToList();
            for (var i = 0; i < enemyship.Count; i++)
            {
                var ship = enemyship[i];
                ship.FirePower = eParam[i][0];
                ship.Torpedo = eParam[i][1];
                ship.AntiAir = eParam[i][2];
                ship.Armor = eParam[i][3];
                

                if (kcmodel.Charamaster.ContainsKey(ship.CharacterId))
                {
                    var charadata = kcmodel.Charamaster[ship.CharacterId];
                    ship.Name = charadata.Name;
                    if (charadata.NameYomi != "" && charadata.NameYomi != "-")
                        ship.Name += " " + charadata.NameYomi;
                    ship.Escaped = false;
                    ship.Level = shipLv[i + 1];
                    ship.MaxHp = maxhps[i + 7];
                    ship.BeforeHp = nowhps[i + 7];
                    ship.AfterHp = ship.BeforeHp;
                    ship.Speed = charadata.Speed;

                    ship.Slot = new int[charadata.SlotCount];
                    
                    for (var j = 0; j < ship.Slot.Length; j++)
                    {
                        ship.Slot[j] = -1;
                    }
                    for (var j = 0; j < eSlot[i].Length && j < ship.Slot.Length ; j++)
                    {
                        ship.Slot[j] = eSlot[i][j];
                    }
                }
            }
            return enemyship;
        }

        /// <summary>
        /// 航空戦ステージ1
        /// </summary>
        /// <param name="stage1"></param>
        /// <param name="airMastery"></param>
        private static void GetAirBattleStage1Phase(KoukuStage1 stage1, out BattleAnalyzedEventArgs.AirMasteryStatus airMastery)
        {
            switch (stage1.disp_seiku)
            {
                case 0: airMastery = BattleAnalyzedEventArgs.AirMasteryStatus.Tie; break;
                case 1: airMastery = BattleAnalyzedEventArgs.AirMasteryStatus.Secure; break;
                case 2: airMastery = BattleAnalyzedEventArgs.AirMasteryStatus.Superior; break;
                case 3: airMastery = BattleAnalyzedEventArgs.AirMasteryStatus.Inferior; break;
                case 4: airMastery = BattleAnalyzedEventArgs.AirMasteryStatus.Lost; break;
                default: airMastery = BattleAnalyzedEventArgs.AirMasteryStatus.Unknown; break;
            }
        }

        /// <summary>
        /// 航空戦ステージ3の解析（非連合艦隊戦）
        /// </summary>
        /// <param name="stage3"></param>
        /// <param name="friendship"></param>
        /// <param name="enemyship"></param>
        private static BattleAnalyzedEventArgs.Phase GetAirBattleStage3Phase
            (KoukuStage3 stage3, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.AllOverPhase() { PhaseName = "航空戦" };
            
            for (var i = 1; i < stage3.edam.Length; i++)
            {
                if (stage3.erai_flag[i] || stage3.ebak_flag[i])
                {
                    phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        TargetShip = enemyship[i - 1],
                        Damage = (int)stage3.edam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage3.edam[i]),
                        Type = stage3.ecl_flag[i]
                    });
                    enemyship[i - 1].AfterHp -= (int)stage3.edam[i];
                }
            }

            for (var i = 1; i < stage3.fdam.Length; i++)
            {
                if (stage3.frai_flag[i] || stage3.fbak_flag[i])
                {
                    phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        TargetShip = friendship[i - 1],
                        Damage = (int)stage3.fdam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage3.fdam[i]),
                        Type = stage3.fcl_flag[i]
                    });
                    friendship[i - 1].AfterHp -= (int)stage3.fdam[i];
                }
            }

            foreach(var ship in friendship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.Damecon.Add(damecondata);
            }

            if (phase.Attackee.Count > 0)
                return phase;
            else
                return null;
        }

        /// <summary>
        /// 支援艦隊戦
        /// </summary>
        /// <param name="supportInfo"></param>
        /// <param name="enemyship"></param>
        /// <param name="supportFlag"></param>
        private static BattleAnalyzedEventArgs.Phase GetSupportPhase
            (int supportFlag, SupportInfoValue supportInfo, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            if (supportFlag > 0)
            {
                BattleAnalyzedEventArgs.AllOverPhase phase = null;

                if (supportInfo.support_airatack != null && supportInfo.support_airatack.stage_flag[2])
                {
                    var stage = supportInfo.support_airatack.stage3;
                    phase = new BattleAnalyzedEventArgs.AllOverPhase();
                    for (var i = 1; i < stage.edam.Length; i++)
                    {
                        if (stage.ebak_flag[i] || stage.erai_flag[i])
                        {
                            phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                            {
                                TargetShip = enemyship[i - 1],
                                Damage = (int)stage.edam[i],
                                FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage.edam[i]),
                                Type = stage.ecl_flag[i]
                            });
                            enemyship[i - 1].AfterHp -= (int)stage.edam[i];
                        }
                    }
                }
                else if (supportInfo.support_hourai != null)
                {
                    var stage = supportInfo.support_hourai;
                    phase = new BattleAnalyzedEventArgs.AllOverPhase();
                    for (var i = 1; i < stage.damage.Length; i++)
                    {
                        if (i > enemyship.Count)
                            break;
                        if (stage.damage[i] >= 0 && enemyship[i - 1].AfterHp > 0)
                        {
                            phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                            {
                                TargetShip = enemyship[i - 1],
                                Damage = (int)stage.damage[i],
                                FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage.damage[i]),
                                Type = stage.cl_list[i]
                            });
                            enemyship[i - 1].AfterHp -= (int)stage.damage[i];
                        }
                    }
                }

                if (phase != null)
                {
                    switch (supportFlag)
                    {
                        case 1: phase.PhaseName = "支援艦隊（航空支援）"; break;
                        case 2: phase.PhaseName = "支援艦隊（砲撃支援）"; break;
                        case 3: phase.PhaseName = "支援艦隊（雷撃支援）"; break;
                        default: phase.PhaseName = "支援艦隊（種別不明）"; break;
                    }
                    return phase;
                }
            }
            return null;
        }

        private static BattleAnalyzedEventArgs.Phase GetRaigekiPhase
            (RaigekiValue raigeki, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.InOrderPhase();

            for (var i = 1; i < raigeki.frai.Length; i++)
            {
                if(raigeki.frai[i] >= 1 && raigeki.frai[i] <= 6)
                {
                    var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        OriginShip = friendship[i - 1],
                        TargetShip = enemyship[raigeki.frai[i] - 1],
                        Damage = raigeki.fydam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(raigeki.edam[raigeki.frai[i] - 1])
                    };
                    phase.Attacks.Add(attack);
                    attack.TargetShip.AfterHp -= attack.Damage;
                    attack.OriginShip.SumAttack += attack.Damage;
                }
            }

            for (var i = 1; i < raigeki.erai.Length; i++)
            {
                if (raigeki.erai[i] >= 1 && raigeki.erai[i] <= 6)
                {
                    var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        OriginShip = enemyship[i - 1],
                        TargetShip = friendship[raigeki.erai[i] - 1],
                        Damage = raigeki.eydam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(raigeki.fdam[raigeki.erai[i] - 1])
                    };
                    phase.Attacks.Add(attack);
                    attack.TargetShip.AfterHp -= attack.Damage;
                    attack.OriginShip.SumAttack += attack.Damage;
                }
            }

            foreach (var ship in friendship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.Attacks.Add(damecondata);
            }

            if (phase.Attacks.Count > 0)
                return phase;
            else
                return null;
        }

        private static BattleAnalyzedEventArgs.Phase GetHougekiPhase
            (HougekiValue hougeki, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.InOrderPhase();

            for (var i = 0; i < hougeki.at_list.Length; i++)
            {
                var from = hougeki.at_list[i];
                var type = hougeki.at_type[i];
                for (var j = 0; j < hougeki.df_list[i].Length; j++)
                {
                    var to = hougeki.df_list[i][j];
                    double damage = hougeki.damage[i][j];
                    if(damage >= 0)
                    {
                        var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                        {
                            OriginShip = (from <= 6) ? friendship[from - 1] : enemyship[from - 7],
                            TargetShip = (to <= 6) ? friendship[to - 1] : enemyship[to - 7],
                            Damage = (int)damage,
                            FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(damage),
                            Type = type
                        };
                        phase.Attacks.Add(attack);
                        attack.TargetShip.AfterHp -= attack.Damage;
                        attack.OriginShip.SumAttack += attack.Damage;

                        if (to <= 6)
                        {
                            var damecondata = DameConCheck(friendship[to - 1]);
                            if (damecondata != null)
                                phase.Attacks.Add(damecondata);
                        }
                    }
                }
            }

            if (phase.Attacks.Count > 0)
                return phase;
            else
                return null;
        }

        private static void CalcGauge(List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship, out double friendgauge, out double enemygauge)
        {
            friendgauge = 1 - (double)enemyship.Sum(_ => Math.Max(_.AfterHp, 0)) / enemyship.Sum(_ => _.BeforeHp);
            enemygauge = 1 - (double)friendship.Sum(_ => Math.Max(_.AfterHp, 0)) / friendship.Sum(_ => _.BeforeHp);

            // 0を切ってしまうかもしれない
            friendgauge = Math.Max(friendgauge, 0);
            enemygauge = Math.Max(enemygauge, 0);

            // 1を超えてしまうかもしれない
            friendgauge = Math.Min(friendgauge, 1);
            enemygauge = Math.Min(enemygauge, 1);
        }

        public static BattleAnalyzedEventArgs AnalyzeNormalBattle(api_req_sortie.Battle data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.Normal;
            ret.IsCombinedBattle = false;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊の情報
            var friendship = GetFriendshipList(data.dock_id, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 敵艦隊の情報
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.AirMastery);

            if (data.stage_flag[2])
            {
                var koukuStage2Phase = GetAirBattleStage3Phase(data.kouku.stage3, friendship, enemyship);
                if(koukuStage2Phase != null)
                    ret.Phases.Add(koukuStage2Phase);
            }

            // 支援艦隊フェイズ
            var supportPhase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (supportPhase != null)
                ret.Phases.Add(supportPhase);

            // 開幕雷撃フェイズ
            if(data.opening_flag)
            {
                var openingPhase = GetRaigekiPhase(data.opening_atack, friendship, enemyship);
                if(openingPhase != null)
                {
                    openingPhase.PhaseName = "開幕雷撃";
                    ret.Phases.Add(openingPhase);
                }
            }

            // 砲撃戦フェイズ
            if(data.hourai_flag[0])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki1, friendship, enemyship);
                if(hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（1巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            if (data.hourai_flag[1])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki2, friendship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（2巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            if (data.hourai_flag[2])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki3, friendship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（3巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            // 雷撃戦フェイズ
            if (data.hourai_flag[3])
            {
                var raigekiPhase = GetRaigekiPhase(data.raigeki, friendship, enemyship);
                if(raigekiPhase != null)
                {
                    raigekiPhase.PhaseName = "雷撃戦";
                    ret.Phases.Add(raigekiPhase);
                }
            }

            CalcGauge(friendship, enemyship, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                _dayBattle = ret;

            return ret;
        }

        /// <summary>
        /// 通常艦隊航空戦
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BattleAnalyzedEventArgs AnalyzeNormalAirBattle(api_req_sortie.Airbattle data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.Normal;
            ret.IsCombinedBattle = false;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊の情報
            var friendship = GetFriendshipList(data.dock_id, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 敵艦隊の情報
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.AirMastery);

            if (data.stage_flag[2])
            {
                var koukuStage2Phase = GetAirBattleStage3Phase(data.kouku.stage3, friendship, enemyship);
                if (koukuStage2Phase != null)
                    ret.Phases.Add(koukuStage2Phase);
            }

            // 支援艦隊フェイズ
            var supportPhase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (supportPhase != null)
                ret.Phases.Add(supportPhase);

            // 第2航空戦フェイズ
            if (data.stage_flag2[2])
            {
                var koukuStage2Phase2 = GetAirBattleStage3Phase(data.kouku2.stage3, friendship, enemyship);
                if (koukuStage2Phase2 != null)
                {
                    koukuStage2Phase2.PhaseName = "航空戦（2巡目）";
                    ret.Phases.Add(koukuStage2Phase2);
                }
            }


            CalcGauge(friendship, enemyship, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                _dayBattle = ret;

            return ret;
        }

        private static BattleAnalyzedEventArgs.Phase GetHougekiPhase
            (api_req_battle_midnight.values.HougekiValue hougeki, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.InOrderPhase();

            for (var i = 0; i < hougeki.at_list.Length; i++)
            {
                var from = hougeki.at_list[i];
                var type = hougeki.sp_list[i];
                for (var j = 0; j < hougeki.df_list[i].Length; j++)
                {
                    var to = hougeki.df_list[i][j];
                    double damage = hougeki.damage[i][j];
                    if (damage >= 0)
                    {
                        var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                        {
                            OriginShip = (from <= 6) ? friendship[from - 1] : enemyship[from - 7],
                            TargetShip = (to <= 6) ? friendship[to - 1] : enemyship[to - 7],
                            Damage = (int)damage,
                            FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(damage),
                            Type = type
                        };
                        phase.Attacks.Add(attack);
                        attack.TargetShip.AfterHp -= attack.Damage;
                        attack.OriginShip.SumAttack += attack.Damage;

                        if (to <= 6)
                        {
                            var damecondata = DameConCheck(friendship[to - 1]);
                            if (damecondata != null)
                                phase.Attacks.Add(damecondata);
                        }
                    }
                }
            }

            if (phase.Attacks.Count > 0)
                return phase;
            else
                return null;
        }

        public static BattleAnalyzedEventArgs AnalyzeNormalNightBattle(api_req_battle_midnight.Battle data)
        {
            var ret = _dayBattle;

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.NormalMidnight;
            var hougekiphase = GetHougekiPhase(data.hougeki, ret.Friend, ret.Enemy);
            if (hougekiphase != null)
            {
                hougekiphase.PhaseName = "夜戦";
                ret.Phases.Add(hougekiphase);
            }
            CalcGauge(ret.Friend, ret.Enemy, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif

            return ret;
        }

        public static BattleAnalyzedEventArgs AnalyzeNormalSpNightBattle(api_req_battle_midnight.SpMidnight data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.SpMidnight;
            ret.IsCombinedBattle = false;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊の情報
            var friendship = GetFriendshipList(data.deck_id, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 敵艦隊の情報
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.NormalMidnight;
            var hougekiphase = GetHougekiPhase(data.hougeki, ret.Friend, ret.Enemy);
            if (hougekiphase != null)
            {
                hougekiphase.PhaseName = "夜戦";
                ret.Phases.Add(hougekiphase);
            }
            CalcGauge(ret.Friend, ret.Enemy, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif

            return ret;
        }

        /// <summary>
        /// 退避の是非をアップデート
        /// </summary>
        /// <param name="friendship"></param>
        /// <param name="escapeIdx"></param>
        private static void AppendEscapes(List<BattleAnalyzedEventArgs.Ship> friendship, int[] escapeIdx)
        {
            if (escapeIdx == null)
                foreach (var s in friendship)
                    s.Escaped = false;
            else
                for (var i = 0; i < friendship.Count; i++)
                    if (escapeIdx.Contains(i + 1))
                        friendship[i].Escaped = true;
                    else
                        friendship[i].Escaped = false;
        }

        private static BattleAnalyzedEventArgs.Phase GetCombinedAirBattleStage3Phase
            (KoukuStage3 stage3, api_req_combined_battle.values.KoukuStage3Combined stage3Combined,
            List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> combinedship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.AllOverPhase() { PhaseName = "航空戦" };

            for (var i = 1; i < stage3.edam.Length; i++)
            {
                if (stage3.erai_flag[i] || stage3.ebak_flag[i])
                {
                    phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        TargetShip = enemyship[i - 1],
                        Damage = (int)stage3.edam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage3.edam[i]),
                        Type = stage3.ecl_flag[i]
                    });
                    enemyship[i - 1].AfterHp -= (int)stage3.edam[i];
                }
            }

            for (var i = 1; i < stage3.fdam.Length; i++)
            {
                if (stage3.frai_flag[i] || stage3.fbak_flag[i])
                {
                    phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        TargetShip = friendship[i - 1],
                        Damage = (int)stage3.fdam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage3.fdam[i]),
                        Type = stage3.fcl_flag[i]
                    });
                    friendship[i - 1].AfterHp -= (int)stage3.fdam[i];
                }
            }

            for (var i = 0; i < stage3Combined.fdam.Length; i++)
            {
                if (stage3Combined.frai_flag[i] || stage3Combined.fbak_flag[i])
                {
                    phase.Attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        TargetShip = combinedship[i - 1],
                        Damage = (int)stage3Combined.fdam[i],
                        FlagShipProtect = Tools.KanColleTools.IsFlagShipProtect(stage3Combined.fdam[i]),
                        Type = stage3Combined.fcl_flag[i]
                    });
                    combinedship[i - 1].AfterHp -= (int)stage3Combined.fdam[i];
                }
            }

            foreach (var ship in friendship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.Damecon.Add(damecondata);
            }

            foreach (var ship in combinedship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.Damecon.Add(damecondata);
            }

            if (phase.Attackee.Count > 0)
                return phase;
            else
                return null;
        }
        private static void CalcGaugeCombined(List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> combinedship, List<BattleAnalyzedEventArgs.Ship> enemyship, out double friendgauge, out double enemygauge)
        {
            friendgauge = 1 - (double)enemyship.Sum(_ => Math.Max(_.AfterHp, 0)) / enemyship.Sum(_ => _.BeforeHp);
            enemygauge = 1 - (double)(friendship.Sum(_ => _.Escaped ? 0 : Math.Max(_.AfterHp, 0)) + combinedship.Sum(_ => _.Escaped ? 0 : Math.Max(_.AfterHp, 0))) / (friendship.Sum(_ => _.Escaped ? 0 : _.BeforeHp) + combinedship.Sum(_ => _.Escaped ? 0 : _.BeforeHp));

            // 0を切ってしまうかもしれない
            friendgauge = Math.Max(friendgauge, 0);
            enemygauge = Math.Max(enemygauge, 0);

            // 1を超えてしまうかもしれない
            friendgauge = Math.Min(friendgauge, 1);
            enemygauge = Math.Min(enemygauge, 1);
        }

        /// <summary>
        /// 連合艦隊機動部隊通常戦
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BattleAnalyzedEventArgs AnalyzeAirCombinedNormalBattle(api_req_combined_battle.Battle data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.Normal;
            ret.IsCombinedBattle = true;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊
            var friendship = GetFriendshipList(1, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 随伴艦隊
            var combinedship = GetFriendshipList(2, data.maxhps_combined, data.nowhps_combined, data.fParam_combined);
            ret.FriendCombined = combinedship;

            // 敵艦隊
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            // 護衛退避
            AppendEscapes(friendship, data.escape_idx);
            AppendEscapes(combinedship, data.escape_idx_combined);

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.AirMastery);

            if (data.stage_flag[2])
            {
                var koukuStage2Phase = GetCombinedAirBattleStage3Phase(data.kouku.stage3, data.kouku.stage3_combined, friendship, combinedship, enemyship);
                if (koukuStage2Phase != null)
                    ret.Phases.Add(koukuStage2Phase);
            }
            
            // 支援艦隊フェイズ
            var supportPhase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (supportPhase != null)
                ret.Phases.Add(supportPhase);

            // 開幕雷撃フェイズ
            if (data.opening_flag)
            {
                var openingPhase = GetRaigekiPhase(data.opening_atack, combinedship, enemyship);
                if (openingPhase != null)
                {
                    openingPhase.PhaseName = "開幕雷撃";
                    ret.Phases.Add(openingPhase);
                }
            }

            // 第2艦隊砲撃戦
            if (data.hourai_flag[0])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki1, combinedship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（随伴艦隊）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            // 雷撃戦
            if (data.hourai_flag[1])
            {
                var raigekiPhase = GetRaigekiPhase(data.raigeki, combinedship, enemyship);
                if (raigekiPhase != null)
                {
                    raigekiPhase.PhaseName = "雷撃戦";
                    ret.Phases.Add(raigekiPhase);
                }
            }

            // 第1艦隊砲撃戦（1巡目）
            if (data.hourai_flag[2])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki2, friendship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（本隊1巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            // 第1艦隊砲撃戦（2巡目）
            if (data.hourai_flag[3])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki3, friendship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（本隊2巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            CalcGaugeCombined(friendship, combinedship, enemyship, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                _dayBattle = ret;

            return ret;
        }

        /// <summary>
        /// 連合艦隊水上部隊通常戦
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BattleAnalyzedEventArgs AnalyzeWaterCombinedNormalBattle(api_req_combined_battle.BattleWater data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.NormalWater;
            ret.IsCombinedBattle = true;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊
            var friendship = GetFriendshipList(1, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 随伴艦隊
            var combinedship = GetFriendshipList(2, data.maxhps_combined, data.nowhps_combined, data.fParam_combined);
            ret.FriendCombined = combinedship;

            // 敵艦隊
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            // 護衛退避
            AppendEscapes(friendship, data.escape_idx);
            AppendEscapes(combinedship, data.escape_idx_combined);

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.AirMastery);

            if (data.stage_flag[2])
            {
                var koukuStage2Phase = GetCombinedAirBattleStage3Phase(data.kouku.stage3, data.kouku.stage3_combined, friendship, combinedship, enemyship);
                if (koukuStage2Phase != null)
                    ret.Phases.Add(koukuStage2Phase);
            }

            // 支援艦隊フェイズ
            var supportPhase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (supportPhase != null)
                ret.Phases.Add(supportPhase);

            // 開幕雷撃フェイズ
            if (data.opening_flag)
            {
                var openingPhase = GetRaigekiPhase(data.opening_atack, combinedship, enemyship);
                if (openingPhase != null)
                {
                    openingPhase.PhaseName = "開幕雷撃";
                    ret.Phases.Add(openingPhase);
                }
            }

            // 第1艦隊砲撃戦（1巡目）
            if (data.hourai_flag[0])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki1, friendship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（本隊1巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            // 第1艦隊砲撃戦（2巡目）
            if (data.hourai_flag[1])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki2, friendship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（本隊2巡目）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            // 第2艦隊砲撃戦
            if (data.hourai_flag[2])
            {
                var hougekiPhase = GetHougekiPhase(data.hougeki3, combinedship, enemyship);
                if (hougekiPhase != null)
                {
                    hougekiPhase.PhaseName = "砲撃戦（随伴艦隊）";
                    ret.Phases.Add(hougekiPhase);
                }
            }

            // 雷撃戦
            if (data.hourai_flag[3])
            {
                var raigekiPhase = GetRaigekiPhase(data.raigeki, combinedship, enemyship);
                if (raigekiPhase != null)
                {
                    raigekiPhase.PhaseName = "雷撃戦";
                    ret.Phases.Add(raigekiPhase);
                }
            }

            CalcGaugeCombined(friendship, combinedship, enemyship, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                _dayBattle = ret;

            return ret;
        }

        /// <summary>
        /// 連合艦隊機動部隊航空戦
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BattleAnalyzedEventArgs AnalyzeAirCombinedAirBattle(api_req_combined_battle.Airbattle data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.NormalAir;
            ret.IsCombinedBattle = true;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊
            var friendship = GetFriendshipList(1, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 随伴艦隊
            var combinedship = GetFriendshipList(2, data.maxhps_combined, data.nowhps_combined, data.fParam_combined);
            ret.FriendCombined = combinedship;

            // 敵艦隊
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            // 護衛退避
            AppendEscapes(friendship, data.escape_idx);
            AppendEscapes(combinedship, data.escape_idx_combined);

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.AirMastery);

            if (data.stage_flag[2])
            {
                var koukuStage2Phase = GetCombinedAirBattleStage3Phase(data.kouku.stage3, data.kouku.stage3_combined, friendship, combinedship, enemyship);
                if (koukuStage2Phase != null)
                    ret.Phases.Add(koukuStage2Phase);
            }

            // 支援艦隊フェイズ
            var supportPhase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (supportPhase != null)
                ret.Phases.Add(supportPhase);

            // 第2航空戦フェイズ
            if (data.stage_flag2[2])
            {
                var koukuStage2Phase2 = GetCombinedAirBattleStage3Phase(data.kouku2.stage3, data.kouku2.stage3_combined, friendship, combinedship, enemyship);
                if (koukuStage2Phase2 != null)
                {
                    koukuStage2Phase2.PhaseName = "航空戦（2巡目）";
                    ret.Phases.Add(koukuStage2Phase2);
                }
            }

            CalcGaugeCombined(friendship, combinedship, enemyship, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                _dayBattle = ret;

            return ret;
        }

        /// <summary>
        /// 連合艦隊通常夜戦
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BattleAnalyzedEventArgs AnalyzeCombinedNormalNightBattle(api_req_combined_battle.MidnightBattle data)
        {
            var ret = _dayBattle;

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.NormalMidnight;
            var hougekiphase = GetHougekiPhase(data.hougeki, ret.FriendCombined, ret.Enemy);
            if (hougekiphase != null)
            {
                hougekiphase.PhaseName = "夜戦（随伴艦隊）";
                ret.Phases.Add(hougekiphase);
            }
            CalcGaugeCombined(ret.Friend, ret.FriendCombined, ret.Enemy, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif

            return ret;
        }

        /// <summary>
        /// 連合艦隊開幕夜戦
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BattleAnalyzedEventArgs AnalyzeCombinedSpNightBattle(api_req_combined_battle.SpMidnight data)
        {
            var ret = new BattleAnalyzedEventArgs();

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.SpMidnight;
            ret.IsCombinedBattle = true;
            ret.Phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊
            var friendship = GetFriendshipList(1, data.maxhps, data.nowhps, data.fParam);
            ret.Friend = friendship;

            // 随伴艦隊
            var combinedship = GetFriendshipList(2, data.maxhps_combined, data.nowhps_combined, data.fParam_combined);
            ret.FriendCombined = combinedship;

            // 敵艦隊
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.Enemy = enemyship;

            // 護衛退避
            AppendEscapes(friendship, data.escape_idx);
            AppendEscapes(combinedship, data.escape_idx_combined);

            ret.FriendFormation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.EnemyFormation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.NormalMidnight;
            var hougekiphase = GetHougekiPhase(data.hougeki, ret.FriendCombined, ret.Enemy);
            if (hougekiphase != null)
            {
                hougekiphase.PhaseName = "夜戦（随伴艦隊）";
                ret.Phases.Add(hougekiphase);
            }

            CalcGaugeCombined(ret.Friend, ret.FriendCombined, ret.Enemy, out ret.FriendGauge, out ret.EnemyGauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif

            return ret;
        }


#if DEBUG

        private static void EventArgsDebugOutput(BattleAnalyzedEventArgs eventargs)
        {
            var formationstring = new Dictionary<BattleAnalyzedEventArgs.Formation, string>()
            {
                { BattleAnalyzedEventArgs.Formation.unknown, "不明" },
                { BattleAnalyzedEventArgs.Formation.tanju, "単縦陣" },
                { BattleAnalyzedEventArgs.Formation.fukuju, "複縦陣" },
                { BattleAnalyzedEventArgs.Formation.rinkei, "輪形陣" },
                { BattleAnalyzedEventArgs.Formation.teikei, "梯形陣" },
                { BattleAnalyzedEventArgs.Formation.tanou, "単横陣" },
                { BattleAnalyzedEventArgs.Formation.daiichikeikai, "第一警戒航行序列" },
                { BattleAnalyzedEventArgs.Formation.dainikeikai, "第二警戒航行序列" },
                { BattleAnalyzedEventArgs.Formation.daisankeikai, "第三警戒航行序列" },
                { BattleAnalyzedEventArgs.Formation.daiyonkeikai, "第四警戒航行序列" }
            };

            var crossingstring = new Dictionary<BattleAnalyzedEventArgs.CrossingType, string>()
            {
                { BattleAnalyzedEventArgs.CrossingType.unknown, "不明" },
                { BattleAnalyzedEventArgs.CrossingType.parallel, "同航戦" },
                { BattleAnalyzedEventArgs.CrossingType.anti_parallel, "反航戦" },
                { BattleAnalyzedEventArgs.CrossingType.cross_adv, "T字有利" },
                { BattleAnalyzedEventArgs.CrossingType.cross_disadv, "T字不利" }
            };

            var airmasterystring = new Dictionary<BattleAnalyzedEventArgs.AirMasteryStatus, string>()
            {
                { BattleAnalyzedEventArgs.AirMasteryStatus.unknown, "不明" },
                { BattleAnalyzedEventArgs.AirMasteryStatus.inferior, "制空劣勢" },
                { BattleAnalyzedEventArgs.AirMasteryStatus.lost, "制空権喪失" },
                { BattleAnalyzedEventArgs.AirMasteryStatus.secure, "制空権確保" },
                { BattleAnalyzedEventArgs.AirMasteryStatus.superior, "制空優勢" },
                { BattleAnalyzedEventArgs.AirMasteryStatus.tie, "制空均衡" },

            };

            System.Diagnostics.Debug.WriteLine("自艦隊: " + formationstring[eventargs.friend_formation] + "   敵艦隊: " + formationstring[eventargs.enemy_formation]);
            System.Diagnostics.Debug.WriteLine("交戦形態: " + crossingstring[eventargs.crossing_type]);
            System.Diagnostics.Debug.WriteLine("制空状態: " + airmasterystring[eventargs.air_mastery]);
            
            foreach(var phase in eventargs.phases)
            {
                System.Diagnostics.Debug.WriteLine("==============  " + phase.phase_name + "  ==============");

                if(phase.phase_type == BattleAnalyzedEventArgs.Phase.PhaseType.AllOverPhase)
                {
                    var aphase = (BattleAnalyzedEventArgs.AllOverPhase)phase;
                    foreach(var attack in aphase.attackee)
                    {
                        System.Diagnostics.Debug.WriteLine(attack.target_ship.name + " - 被ダメージ " + attack.damage);
                    }
                    foreach(var attack in aphase.damecon)
                    {
                        System.Diagnostics.Debug.WriteLine(attack.target_ship.name + " - ダメコン発動  回復 " + -attack.damage);
                    }
                }
                else
                {
                    var aphase = (BattleAnalyzedEventArgs.InOrderPhase)phase;
                    foreach(var attack in aphase.attacks)
                    {
                        if(attack.damecon_type == 0)
                        {
                            System.Diagnostics.Debug.WriteLine(attack.origin_ship.name + " -> " + attack.target_ship.name + " - " + attack.damage);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(attack.target_ship.name + " - ダメコン発動  回復" + -attack.damage);
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("[ 自艦隊 ]");
            foreach (var ship in eventargs.friend)
            {
                System.Diagnostics.Debug.WriteLine(ship.name + " Lv." + ship.level + " - " + ship.before_hp + " -> " + Math.Max(ship.after_hp, 0) + " / " + ship.max_hp);
            }

            if(eventargs.is_combined_battle)
            {
                System.Diagnostics.Debug.WriteLine("[ 随伴艦隊 ]");
                foreach (var ship in eventargs.friend_combined)
                {
                    System.Diagnostics.Debug.WriteLine(ship.name + " Lv." + ship.level + " - " + ship.before_hp + " -> " + Math.Max(ship.after_hp, 0) + " / " + ship.max_hp);
                }
            }

            System.Diagnostics.Debug.WriteLine("[ 敵艦隊 ]");
            foreach (var ship in eventargs.enemy)
            {
                System.Diagnostics.Debug.WriteLine(ship.name + " Lv." + ship.level + " - " + ship.before_hp + " -> " + Math.Max(ship.after_hp, 0) + " / " + ship.max_hp);
            }

            System.Diagnostics.Debug.WriteLine("ゲージ (%): " + (int)(eventargs.friend_gauge * 100) + " VS " + (int)(eventargs.enemy_gauge * 100));
        }

#endif
    }
}
