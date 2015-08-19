using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static BattleAnalyzedEventArgs day_battle;

        /// <summary>
        /// ダメコンを使用しなければいけないかどうか判断する
        /// ダメコンを使用した場合はafter_hpは自動で更新される
        /// </summary>
        /// <param name="ship">被攻撃を考慮した後のafter_hpを持つship</param>
        /// <returns>使用すれば使用情報、使用しなければnull</returns>
        private static BattleAnalyzedEventArgs.Phase.Attack DameConCheck(BattleAnalyzedEventArgs.Ship ship)
        {
            if (ship.after_hp <= 0 && ship.damecontype != BattleAnalyzedEventArgs.Ship.DameConType.None)
            {
                ship.use_damecon = true;
                int recoverhp = 0;
                if (ship.damecontype == BattleAnalyzedEventArgs.Ship.DameConType.Goddess)
                {
                    recoverhp = ship.max_hp - ship.after_hp;
                }
                else
                {
                    recoverhp = (int)(ship.max_hp * 0.2) - ship.after_hp;
                }

                ship.after_hp += recoverhp;
                return new BattleAnalyzedEventArgs.Phase.Attack()
                {
                    damage = 0 - recoverhp,
                    target_ship = ship,
                    damecon_type = (ship.damecontype == BattleAnalyzedEventArgs.Ship.DameConType.Goddess) ? 2 : 1
                };
            }
            else
                return null;   
        }

        /// <summary>
        /// 自艦隊の情報を入手する
        /// </summary>
        /// <param name="dock_id">艦隊番号</param>
        /// <param name="maxhps">svdataのmaxhpsそのまま</param>
        /// <param name="nowhps">svdataのnowhpsそのまま</param>
        /// <returns></returns>
        private static List<BattleAnalyzedEventArgs.Ship> GetFriendshipList(int dock_id, int[] maxhps, int[] nowhps, int[][] fParam)
        {
            var kcmodel = MainModel.Current.kancolleModel;

            var friendship = new List<BattleAnalyzedEventArgs.Ship>();
            foreach (var ship in kcmodel.fleetdata[dock_id - 1].ships)
                friendship.Add(new BattleAnalyzedEventArgs.Ship() { original_id = ship });
            for (int i = 0; i < friendship.Count; i++)
            {
                var ship = friendship[i];

                ship.fire_power = fParam[i][0];
                ship.torpedo = fParam[i][1];
                ship.anti_air = fParam[i][2];
                ship.armor = fParam[i][3];

                var shipdata = kcmodel.shipdata.FirstOrDefault(_ => _.shipid == ship.original_id);
                if (shipdata != null && shipdata.characterinfo != null)
                {
                    ship.name = shipdata.characterinfo.name;
                    ship.level = shipdata.level;
                    ship.escaped = false;
                    ship.character_id = shipdata.characterid;
                    ship.max_hp = maxhps[i + 1];
                    ship.before_hp = nowhps[i + 1];
                    ship.after_hp = ship.before_hp;
                    ship.damecontype = BattleAnalyzedEventArgs.Ship.DameConType.None;
                    ship.slot = new int[shipdata.characterinfo.slot_count];
                    for (int j = 0; j < ship.slot.Length; j++)
                    {
                        ship.slot[j] = -1;
                    }
                    for (int j = 0; j < shipdata.Slots.Count; j++)
                    {
                        var slotdata = kcmodel.slotdata.FirstOrDefault(_ => _.id == shipdata.Slots[j]);
                        if (slotdata != null)
                        {
                            ship.slot[j] = slotdata.itemid;
                            if (slotdata.itemid == 42)
                            {
                                ship.damecontype = BattleAnalyzedEventArgs.Ship.DameConType.Normal;
                                break;
                            }
                            else if (slotdata.itemid == 43)
                            {
                                ship.damecontype = BattleAnalyzedEventArgs.Ship.DameConType.Goddess;
                                break;
                            }
                        }
                    }
                }
            }
            return friendship;
        }

        private static List<BattleAnalyzedEventArgs.Ship> GetEnemyshipList(int[] ship_ke, int[] ship_lv, int[] maxhps, int[] nowhps, int[][] eParam, int[][] eSlot)
        {
            var kcmodel = MainModel.Current.kancolleModel;

            var enemyship = new List<BattleAnalyzedEventArgs.Ship>();
            foreach (var id in ship_ke)
                if (id != -1)
                    enemyship.Add(new BattleAnalyzedEventArgs.Ship() { character_id = id });
            for (int i = 0; i < enemyship.Count; i++)
            {
                var ship = enemyship[i];
                ship.fire_power = eParam[i][0];
                ship.torpedo = eParam[i][1];
                ship.anti_air = eParam[i][2];
                ship.armor = eParam[i][3];
                

                if (kcmodel.charamaster.ContainsKey(ship.character_id))
                {
                    var charadata = kcmodel.charamaster[ship.character_id];
                    ship.name = charadata.name;
                    if (charadata.name_yomi != "" && charadata.name_yomi != "-")
                        ship.name += " " + charadata.name_yomi;
                    ship.escaped = false;
                    ship.level = ship_lv[i + 1];
                    ship.max_hp = maxhps[i + 7];
                    ship.before_hp = nowhps[i + 7];
                    ship.after_hp = ship.before_hp;
                    ship.slot = new int[charadata.slot_count];
                    for (int j = 0; j < ship.slot.Length; j++)
                    {
                        ship.slot[j] = -1;
                    }
                    for (int j = 0; j < eSlot[i].Length && j < ship.slot.Length ; j++)
                    {
                        ship.slot[j] = eSlot[i][j];
                    }
                }
            }
            return enemyship;
        }

        /// <summary>
        /// 航空戦ステージ1
        /// </summary>
        /// <param name="stage1"></param>
        /// <param name="air_mastery"></param>
        private static BattleAnalyzedEventArgs.Phase GetAirBattleStage1Phase(api_req_sortie.values.KoukuStage1 stage1, out BattleAnalyzedEventArgs.AirMasteryStatus air_mastery)
        {
            switch (stage1.disp_seiku)
            {
                case 0: air_mastery = BattleAnalyzedEventArgs.AirMasteryStatus.tie; break;
                case 1: air_mastery = BattleAnalyzedEventArgs.AirMasteryStatus.secure; break;
                case 2: air_mastery = BattleAnalyzedEventArgs.AirMasteryStatus.superior; break;
                case 3: air_mastery = BattleAnalyzedEventArgs.AirMasteryStatus.inferior; break;
                case 4: air_mastery = BattleAnalyzedEventArgs.AirMasteryStatus.lost; break;
                default: air_mastery = BattleAnalyzedEventArgs.AirMasteryStatus.unknown; break;
            }
            return null;
        }

        /// <summary>
        /// 航空戦ステージ3の解析（非連合艦隊戦）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ret"></param>
        /// <param name="friendship"></param>
        /// <param name="enemyship"></param>
        private static BattleAnalyzedEventArgs.Phase GetAirBattleStage3Phase
            (api_req_sortie.values.KoukuStage3 stage3, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.AllOverPhase() { phase_name = "航空戦" };
            
            for (int i = 1; i < stage3.edam.Length; i++)
            {
                if (stage3.erai_flag[i] || stage3.ebak_flag[i])
                {
                    phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        target_ship = enemyship[i - 1],
                        damage = (int)stage3.edam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage3.edam[i]),
                        type = stage3.ecl_flag[i]
                    });
                    enemyship[i - 1].after_hp -= (int)stage3.edam[i];
                }
            }

            for (int i = 1; i < stage3.fdam.Length; i++)
            {
                if (stage3.frai_flag[i] || stage3.fbak_flag[i])
                {
                    phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        target_ship = friendship[i - 1],
                        damage = (int)stage3.fdam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage3.fdam[i]),
                        type = stage3.fcl_flag[i]
                    });
                    friendship[i - 1].after_hp -= (int)stage3.fdam[i];
                }
            }

            foreach(var ship in friendship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.damecon.Add(damecondata);
            }

            if (phase.attackee.Count > 0)
                return phase;
            else
                return null;
        }

        /// <summary>
        /// 支援艦隊戦
        /// </summary>
        /// <param name="support_flag"></param>
        /// <param name="support_info"></param>
        /// <param name="ret"></param>
        /// <param name="friendship"></param>
        /// <param name="enemyship"></param>
        private static BattleAnalyzedEventArgs.Phase GetSupportPhase
            (int support_flag, api_req_sortie.values.SupportInfoValue support_info, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            if (support_flag > 0)
            {
                BattleAnalyzedEventArgs.AllOverPhase phase = null;

                if (support_info.support_airatack != null && support_info.support_airatack.stage_flag[2])
                {
                    var stage = support_info.support_airatack.stage3;
                    phase = new BattleAnalyzedEventArgs.AllOverPhase();
                    for (int i = 1; i < stage.edam.Length; i++)
                    {
                        if (stage.ebak_flag[i] || stage.erai_flag[i])
                        {
                            phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                            {
                                target_ship = enemyship[i - 1],
                                damage = (int)stage.edam[i],
                                flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage.edam[i]),
                                type = stage.ecl_flag[i]
                            });
                            enemyship[i - 1].after_hp -= (int)stage.edam[i];
                        }
                    }
                }
                else if (support_info.support_hourai != null)
                {
                    var stage = support_info.support_hourai;
                    phase = new BattleAnalyzedEventArgs.AllOverPhase();
                    for (int i = 1; i < stage.damage.Length; i++)
                    {
                        if (stage.damage[i] >= 0)
                        {
                            phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                            {
                                target_ship = enemyship[i - 1],
                                damage = (int)stage.damage[i],
                                flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage.damage[i]),
                                type = stage.cl_list[i]
                            });
                            enemyship[i - 1].after_hp -= (int)stage.damage[i];
                        }
                    }
                }

                if (phase != null)
                {
                    switch (support_flag)
                    {
                        case 1: phase.phase_name = "支援艦隊（航空支援）"; break;
                        case 2: phase.phase_name = "支援艦隊（砲撃支援）"; break;
                        case 3: phase.phase_name = "支援艦隊（雷撃支援）"; break;
                        default: phase.phase_name = "支援艦隊（種別不明）"; break;
                    }
                    return phase;
                }
            }
            return null;
        }

        private static BattleAnalyzedEventArgs.Phase GetRaigekiPhase
            (api_req_sortie.values.RaigekiValue raigeki, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.InOrderPhase();

            for (int i = 1; i < raigeki.frai.Length; i++)
            {
                if(raigeki.frai[i] >= 1 && raigeki.frai[i] <= 6)
                {
                    var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        origin_ship = friendship[i - 1],
                        target_ship = enemyship[raigeki.frai[i] - 1],
                        damage = raigeki.fydam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(raigeki.edam[raigeki.frai[i] - 1])
                    };
                    phase.attacks.Add(attack);
                    attack.target_ship.after_hp -= attack.damage;
                    attack.origin_ship.sum_attack += attack.damage;
                }
            }

            for (int i = 1; i < raigeki.erai.Length; i++)
            {
                if (raigeki.erai[i] >= 1 && raigeki.erai[i] <= 6)
                {
                    var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        origin_ship = enemyship[i - 1],
                        target_ship = friendship[raigeki.erai[i] - 1],
                        damage = raigeki.eydam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(raigeki.fdam[raigeki.erai[i] - 1])
                    };
                    phase.attacks.Add(attack);
                    attack.target_ship.after_hp -= attack.damage;
                    attack.origin_ship.sum_attack += attack.damage;
                }
            }

            foreach (var ship in friendship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.attacks.Add(damecondata);
            }

            if (phase.attacks.Count > 0)
                return phase;
            else
                return null;
        }

        private static BattleAnalyzedEventArgs.Phase GetHougekiPhase
            (api_req_sortie.values.HougekiValue hougeki, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.InOrderPhase();

            for (int i = 0; i < hougeki.at_list.Length; i++)
            {
                int from = hougeki.at_list[i];
                int type = hougeki.at_type[i];
                for (int j = 0; j < hougeki.df_list[i].Length; j++)
                {
                    int to = hougeki.df_list[i][j];
                    double damage = hougeki.damage[i][j];
                    if(damage >= 0)
                    {
                        var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                        {
                            origin_ship = (from <= 6) ? friendship[from - 1] : enemyship[from - 7],
                            target_ship = (to <= 6) ? friendship[to - 1] : enemyship[to - 7],
                            damage = (int)damage,
                            flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(damage),
                            type = type
                        };
                        phase.attacks.Add(attack);
                        attack.target_ship.after_hp -= attack.damage;
                        attack.origin_ship.sum_attack += attack.damage;

                        if (to <= 6)
                        {
                            var damecondata = DameConCheck(friendship[to - 1]);
                            if (damecondata != null)
                                phase.attacks.Add(damecondata);
                        }
                    }
                }
            }

            if (phase.attacks.Count > 0)
                return phase;
            else
                return null;
        }

        private static void CalcGauge(List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship, out double friendgauge, out double enemygauge)
        {
            friendgauge = 1 - (double)enemyship.Sum(_ => Math.Max(_.after_hp, 0)) / enemyship.Sum(_ => _.before_hp);
            enemygauge = 1 - (double)friendship.Sum(_ => Math.Max(_.after_hp, 0)) / friendship.Sum(_ => _.before_hp);

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

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.normal;
            ret.is_combined_battle = false;
            ret.phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊の情報
            var friendship = GetFriendshipList(data.dock_id, data.maxhps, data.nowhps, data.fParam);
            ret.friend = friendship;

            // 敵艦隊の情報
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);
            ret.enemy = enemyship;

            ret.friend_formation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.enemy_formation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.air_mastery);

            if (data.stage_flag[2])
            {
                var kouku_stage2_phase = GetAirBattleStage3Phase(data.kouku.stage3, friendship, enemyship);
                if(kouku_stage2_phase != null)
                    ret.phases.Add(kouku_stage2_phase);
            }

            // 支援艦隊フェイズ
            var support_phase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (support_phase != null)
                ret.phases.Add(support_phase);

            // 開幕雷撃フェイズ
            if(data.opening_flag)
            {
                var opening_phase = GetRaigekiPhase(data.opening_atack, friendship, enemyship);
                if(opening_phase != null)
                {
                    opening_phase.phase_name = "開幕雷撃";
                    ret.phases.Add(opening_phase);
                }
            }

            // 砲撃戦フェイズ
            if(data.hourai_flag[0])
            {
                var hougeki_phase = GetHougekiPhase(data.hougeki1, friendship, enemyship);
                if(hougeki_phase != null)
                {
                    hougeki_phase.phase_name = "砲撃戦（1巡目）";
                    ret.phases.Add(hougeki_phase);
                }
            }

            if (data.hourai_flag[1])
            {
                var hougeki_phase = GetHougekiPhase(data.hougeki2, friendship, enemyship);
                if (hougeki_phase != null)
                {
                    hougeki_phase.phase_name = "砲撃戦（2巡目）";
                    ret.phases.Add(hougeki_phase);
                }
            }

            if (data.hourai_flag[2])
            {
                var hougeki_phase = GetHougekiPhase(data.hougeki3, friendship, enemyship);
                if (hougeki_phase != null)
                {
                    hougeki_phase.phase_name = "砲撃戦（3巡目）";
                    ret.phases.Add(hougeki_phase);
                }
            }

            // 雷撃戦フェイズ
            if (data.hourai_flag[3])
            {
                var raigeki_phase = GetRaigekiPhase(data.raigeki, friendship, enemyship);
                if(raigeki_phase != null)
                {
                    raigeki_phase.phase_name = "雷撃戦";
                    ret.phases.Add(raigeki_phase);
                }
            }

            CalcGauge(friendship, enemyship, out ret.friend_gauge, out ret.enemy_gauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                day_battle = ret;

            return ret;
        }
        
        private static BattleAnalyzedEventArgs.Phase GetHougekiPhase
            (api_req_battle_midnight.values.HougekiValue hougeki, List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.InOrderPhase();

            for (int i = 0; i < hougeki.at_list.Length; i++)
            {
                int from = hougeki.at_list[i];
                int type = hougeki.sp_list[i];
                for (int j = 0; j < hougeki.df_list[i].Length; j++)
                {
                    int to = hougeki.df_list[i][j];
                    double damage = hougeki.damage[i][j];
                    if (damage >= 0)
                    {
                        var attack = new BattleAnalyzedEventArgs.Phase.Attack()
                        {
                            origin_ship = (from <= 6) ? friendship[from - 1] : enemyship[from - 7],
                            target_ship = (to <= 6) ? friendship[to - 1] : enemyship[to - 7],
                            damage = (int)damage,
                            flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(damage),
                            type = type
                        };
                        phase.attacks.Add(attack);
                        attack.target_ship.after_hp -= attack.damage;
                        attack.origin_ship.sum_attack += attack.damage;

                        if (to <= 6)
                        {
                            var damecondata = DameConCheck(friendship[to - 1]);
                            if (damecondata != null)
                                phase.attacks.Add(damecondata);
                        }
                    }
                }
            }

            if (phase.attacks.Count > 0)
                return phase;
            else
                return null;
        }

        public static BattleAnalyzedEventArgs AnalyzeNormalNightBattle(api_req_battle_midnight.Battle data)
        {
            var ret = day_battle;

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.normal_midnight;
            var hougekiphase = GetHougekiPhase(data.hougeki, ret.friend, ret.enemy);
            if (hougekiphase != null)
            {
                hougekiphase.phase_name = "夜戦";
                ret.phases.Add(hougekiphase);
            }
            CalcGauge(ret.friend, ret.enemy, out ret.friend_gauge, out ret.enemy_gauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif

            return ret;
        }

        
        private static BattleAnalyzedEventArgs.Phase GetCombinedAirBattleStage3Phase
            (api_req_sortie.values.KoukuStage3 stage3, api_req_combined_battle.values.KoukuStage3Combined stage3_combined,
            List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> combinedship, List<BattleAnalyzedEventArgs.Ship> enemyship)
        {
            var phase = new BattleAnalyzedEventArgs.AllOverPhase() { phase_name = "航空戦" };

            for (int i = 1; i < stage3.edam.Length; i++)
            {
                if (stage3.erai_flag[i] || stage3.ebak_flag[i])
                {
                    phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        target_ship = enemyship[i - 1],
                        damage = (int)stage3.edam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage3.edam[i]),
                        type = stage3.ecl_flag[i]
                    });
                    enemyship[i - 1].after_hp -= (int)stage3.edam[i];
                }
            }

            for (int i = 1; i < stage3.fdam.Length; i++)
            {
                if (stage3.frai_flag[i] || stage3.fbak_flag[i])
                {
                    phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        target_ship = friendship[i - 1],
                        damage = (int)stage3.fdam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage3.fdam[i]),
                        type = stage3.fcl_flag[i]
                    });
                    friendship[i - 1].after_hp -= (int)stage3.fdam[i];
                }
            }

            for (int i = 0; i < stage3_combined.fdam.Length; i++)
            {
                if (stage3_combined.frai_flag[i] || stage3_combined.fbak_flag[i])
                {
                    phase.attackee.Add(new BattleAnalyzedEventArgs.Phase.Attack()
                    {
                        target_ship = combinedship[i - 1],
                        damage = (int)stage3_combined.fdam[i],
                        flag_ship_protect = Tools.KanColleTools.IsFlagShipProtect(stage3_combined.fdam[i]),
                        type = stage3_combined.fcl_flag[i]
                    });
                    combinedship[i - 1].after_hp -= (int)stage3_combined.fdam[i];
                }
            }

            foreach (var ship in friendship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.damecon.Add(damecondata);
            }

            foreach (var ship in combinedship)
            {
                var damecondata = DameConCheck(ship);
                if (damecondata != null)
                    phase.damecon.Add(damecondata);
            }

            if (phase.attackee.Count > 0)
                return phase;
            else
                return null;
        }
        private static void CalcGaugeCombined(List<BattleAnalyzedEventArgs.Ship> friendship, List<BattleAnalyzedEventArgs.Ship> combinedship, List<BattleAnalyzedEventArgs.Ship> enemyship, out double friendgauge, out double enemygauge)
        {
            friendgauge = 1 - (double)enemyship.Sum(_ => Math.Max(_.after_hp, 0)) / enemyship.Sum(_ => _.before_hp);
            enemygauge = 1 - (double)(friendship.Sum(_ => Math.Max(_.after_hp, 0)) + combinedship.Sum(_ => Math.Max(_.after_hp, 0))) / (friendship.Sum(_ => _.before_hp) + combinedship.Sum(_ => _.before_hp));

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

            ret.battle_type = BattleAnalyzedEventArgs.BattleType.normal;
            ret.is_combined_battle = true;
            ret.phases = new List<BattleAnalyzedEventArgs.Phase>();

            // 自艦隊
            var friendship = GetFriendshipList(1, data.maxhps, data.nowhps, data.fParam);
            ret.friend = friendship;

            // 随伴艦隊
            var combinedship = GetFriendshipList(2, data.maxhps_combined, data.nowhps_combined, data.fParam_combined);
            ret.friend_combined = combinedship;

            // 敵艦隊
            var enemyship = GetEnemyshipList(data.ship_ke, data.ship_lv, data.maxhps, data.nowhps, data.eParam, data.eSlot);

            ret.friend_formation = (BattleAnalyzedEventArgs.Formation)data.formation[0];
            ret.enemy_formation = (BattleAnalyzedEventArgs.Formation)data.formation[1];
            ret.crossing_type = (BattleAnalyzedEventArgs.CrossingType)data.formation[2];

            // 航空戦フェイズ
            if (data.stage_flag[0])
                GetAirBattleStage1Phase(data.kouku.stage1, out ret.air_mastery);

            if (data.stage_flag[2])
            {
                var kouku_stage2_phase = GetCombinedAirBattleStage3Phase(data.kouku.stage3, data.kouku.stage3_combined, friendship, combinedship, enemyship);
                if (kouku_stage2_phase != null)
                    ret.phases.Add(kouku_stage2_phase);
            }
            
            // 支援艦隊フェイズ
            var support_phase = GetSupportPhase(data.support_flag, data.support_info, enemyship);
            if (support_phase != null)
                ret.phases.Add(support_phase);

            // 開幕雷撃フェイズ
            if (data.opening_flag)
            {
                var opening_phase = GetRaigekiPhase(data.opening_atack, combinedship, enemyship);
                if (opening_phase != null)
                {
                    opening_phase.phase_name = "開幕雷撃";
                    ret.phases.Add(opening_phase);
                }
            }

            // 第2艦隊砲撃戦
            if (data.hourai_flag[0])
            {
                var hougeki_phase = GetHougekiPhase(data.hougeki1, combinedship, enemyship);
                if (hougeki_phase != null)
                {
                    hougeki_phase.phase_name = "砲撃戦（随伴艦隊）";
                    ret.phases.Add(hougeki_phase);
                }
            }

            // 雷撃戦
            if (data.hourai_flag[1])
            {
                var raigeki_phase = GetRaigekiPhase(data.raigeki, combinedship, enemyship);
                if (raigeki_phase != null)
                {
                    raigeki_phase.phase_name = "雷撃戦";
                    ret.phases.Add(raigeki_phase);
                }
            }

            // 第1艦隊砲撃戦（1巡目）
            if (data.hourai_flag[2])
            {
                var hougeki_phase = GetHougekiPhase(data.hougeki2, friendship, enemyship);
                if (hougeki_phase != null)
                {
                    hougeki_phase.phase_name = "砲撃戦（本隊1巡目）";
                    ret.phases.Add(hougeki_phase);
                }
            }

            // 第1艦隊砲撃戦（2巡目）
            if (data.hourai_flag[3])
            {
                var hougeki_phase = GetHougekiPhase(data.hougeki3, friendship, enemyship);
                if (hougeki_phase != null)
                {
                    hougeki_phase.phase_name = "砲撃戦（本隊2巡目）";
                    ret.phases.Add(hougeki_phase);
                }
            }

            CalcGaugeCombined(friendship, combinedship, enemyship, out ret.friend_gauge, out ret.enemy_gauge);

#if DEBUG
            EventArgsDebugOutput(ret);
#endif
            if (data.midnight_flag)
                day_battle = ret;

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
