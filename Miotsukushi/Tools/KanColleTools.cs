using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Miotsukushi.Tools
{
    class KanColleTools
    {
        public static void ShipResource(int level, int resource_now, int resource_max, out int real_now, out int real_max)
        {
            if (level <= 99)
            {
                real_now = resource_now;
                real_max = resource_max;
            }
            else
            {
                real_max = (int)Math.Floor(resource_max * 0.85);

                if (resource_now == resource_max)
                    real_now = real_max;
                else
                {
                    int decrease = Math.Max((int)Math.Floor((resource_max - resource_now) * 0.85), 1);
                    real_now = real_max - decrease;
                }
            }
        }

        public static Color GetSlotItemEquipTypeColor(int equiptype)
        {
            switch (equiptype)
            {
                case 1: return Color.FromRgb(209, 91, 91); // 小口径主砲
                case 2: return Color.FromRgb(204, 61, 61); // 中口径主砲
                case 3: return Color.FromRgb(194, 34, 34); // 大口径主砲
                case 4: return Color.FromRgb(255, 234, 0); // 副砲
                case 5: return Color.FromRgb(88, 135, 171); // 魚雷
                case 6: return Color.FromRgb(102, 204, 119); // 艦上戦闘機
                case 7: return Color.FromRgb(243, 109, 109); // 艦上爆撃機
                case 8: return Color.FromRgb(101, 188, 255); // 艦上攻撃機
                case 9: return Color.FromRgb(255, 192, 0); // 艦上偵察機
                case 10: return Color.FromRgb(140, 200, 150); // 水上偵察機
                case 11: return Color.FromRgb(140, 200, 150); // 水上爆撃機
                case 12: return Color.FromRgb(229, 152, 53); // 小型電探
                case 13: return Color.FromRgb(229, 152, 53); // 大型電探
                case 14: return Color.FromRgb(90, 171, 184); // ソナー
                case 15: return Color.FromRgb(127, 204, 216); // 爆雷
                case 16: return Color.FromRgb(154, 127, 175); // 追加装甲
                case 17: return Color.FromRgb(255, 196, 77); // 機関部強化
                case 18: return Color.FromRgb(96, 190, 113); // 対空強化弾
                case 19: return Color.FromRgb(209, 91, 91); // 対艦強化弾
                case 20: return Color.FromRgb(218, 200, 176); // VT信管
                case 21: return Color.FromRgb(102, 203, 119); // 対空機銃
                case 22: return Color.FromRgb(88, 135, 171); // 特殊潜航艇（甲標的）
                case 23: return Color.FromRgb(255, 255, 255); // 応急修理要員
                case 24: return Color.FromRgb(154, 165, 93); // 上陸用舟艇
                case 25: return Color.FromRgb(102, 204, 119); // オートジャイロ
                case 26: return Color.FromRgb(127, 204, 216); // 対潜哨戒機
                case 27: return Color.FromRgb(154, 127, 175); // 追加装甲（中型）
                case 28: return Color.FromRgb(154, 127, 175); // 追加装甲（大型）
                case 29: return Color.FromRgb(231, 107, 25); // 探照灯
                case 30: return Color.FromRgb(163, 163, 163); // 簡易輸送部材
                case 31: return Color.FromRgb(176, 157, 127); // 艦艇修理施設
                case 32: return Color.FromRgb(88, 135, 171); // 潜水艦魚雷
                case 33: return Color.FromRgb(255, 155, 0); // 照明弾
                case 34: return Color.FromRgb(200, 170, 255); // 司令部施設
                case 35: return Color.FromRgb(205, 162, 105); // 航空要員
                case 36: return Color.FromRgb(137, 154, 77); // 高射装置
                case 37: return Color.FromRgb(255, 54, 54); // 対地装備
                case 38: return Color.FromRgb(194, 34, 34); // 大口径主砲(II)
                case 39: return Color.FromRgb(191, 235, 159); // 水上艦要員
                case 40: return Color.FromRgb(90, 171, 184); // 大型ソナー
                case 41: return Color.FromRgb(140, 205, 155); // 大型飛行艇
                default: return Colors.Transparent;
            }
        }

        public static int SlotAirMastery(Model.KanColle.SlotData slot, int onslot)
        {
            if (slot.iteminfo == null)
                return 0;

            switch (slot.iteminfo.type_equiptype)
            {
                case 6:
                case 7:
                case 8:
                case 11:
                    return (int)Math.Floor(slot.iteminfo.anti_air * Math.Sqrt(onslot));
                default:
                    return 0;
            }
        }

        public static int ShipAirMastery(Model.KanColle.ShipData ship)
        {
            if (Model.MainModel.Current == null || Model.MainModel.Current.kancolleModel == null || Model.MainModel.Current.kancolleModel.slotdata == null ||
                ship == null || ship.Slots == null || ship.OnSlotCount == null)
                return 0;

            int length = Math.Min(ship.Slots.Count, ship.OnSlotCount.Count);
            int ret = 0;

            for (int i = 0; i < length; i++)
            {
                var slotmodel = Model.MainModel.Current.kancolleModel.slotdata.FirstOrDefault(_ => _.id == ship.Slots[i]);
                if (slotmodel != null)
                    ret += SlotAirMastery(slotmodel, ship.OnSlotCount[i]);
            }

            return ret;
        }

        public static int ShipDrumCount(Model.KanColle.ShipData ship)
        {
            if (Model.MainModel.Current == null || Model.MainModel.Current.kancolleModel == null || Model.MainModel.Current.kancolleModel.slotdata == null ||
                ship == null || ship.Slots == null || ship.OnSlotCount == null)
                return 0;

            int ret = 0;

            for (int i = 0; i < ship.Slots.Count; i++)
            {
                var slotmodel = Model.MainModel.Current.kancolleModel.slotdata.FirstOrDefault(_ => _.id == ship.Slots[i]);
                if (slotmodel != null && slotmodel.itemid == 75)
                    ++ret;
            }

            return ret;
        }

        /// <summary>
        /// 2-5式（秋）の各艦パラメータ。艦隊のパラメータはこれの合計から司令部レベルの5の倍数切り上げの0.61倍を引いたものになる。
        /// 零式観戦21型（熟練）、三式指揮連絡機、熟練艦載機整備員、艦隊司令部施設などにかかる係数は不明なので0になっている。
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public static void ShipOkinoshimaSearchParameter(Model.KanColle.ShipData ship, out double parameter, out double error)
        {
            if (Model.MainModel.Current == null || Model.MainModel.Current.kancolleModel == null || Model.MainModel.Current.kancolleModel.slotdata == null ||
                ship == null || ship.Slots == null)
            {
                parameter = 0;
                error = 0;
                return;
            }

            /*

                索敵スコア[小数点第2位を四捨五入]
            = 艦上爆撃機 × (1.0376255 ± 0.09650285)
            + 艦上攻撃機 × (1.3677954 ± 0.10863618)
            + 艦上偵察機 × (1.6592780 ± 0.09760553)
            + 水上偵察機 × (2.0000000 ± 0.08662294)
            + 水上爆撃機 × (1.7787282 ± 0.09177225)
            + 小型電探 × (1.0045358 ± 0.04927736)
            + 大型電探 × (0.9906638 ± 0.04912215)
            + 探照灯 × (0.9067950 ± 0.06582838)
            + √(各艦毎の素索敵) × (1.6841056 ± 0.07815942)
            + (司令部レベルを5の倍数に切り上げ) × (-0.6142467 ± 0.03692224)

            http://ch.nicovideo.jp/biikame/blomaga/ar663428

                */

            double ret = 0;
            double err = 0;
            int sosakuteki = ship.reconnaissance;

            for (int i = 0; i < ship.Slots.Count; i++)
            {
                var slotmodel = Model.MainModel.Current.kancolleModel.slotdata.FirstOrDefault(_ => _.id == ship.Slots[i]);
                if (slotmodel != null && slotmodel.iteminfo != null)
                {
                    sosakuteki -= slotmodel.iteminfo.reconnaissance;
                    double coef = 0;
                    double errcoef = 0;
                    switch(slotmodel.iteminfo.type_equiptype)
                    {
                        case 7: // 艦上爆撃機 1.04
                            coef = 1.0376255;
                            errcoef = 0.09650285;
                            break;
                        case 8: // 艦上攻撃機 1.37
                            coef = 1.3677954;
                            errcoef = 0.10863618;
                            break;
                        case 9: // 艦上偵察機 1.66
                            coef = 1.6592780;
                            errcoef = 0.09760553;
                            break;
                        case 10: // 水上偵察機 2.00
                            coef = 2;
                            errcoef = 0.08662294;
                            break;
                        case 11: // 水上爆撃機 1.78
                            coef = 1.7787282;
                            errcoef = 0.09177225;
                            break;
                        case 12: // 小型電探 1
                            coef = 1.0045358;
                            errcoef = 0.04927736;
                            break;
                        case 13: // 大型電探 0.99
                            coef = 0.9906638;
                            errcoef = 0.04912215;
                            break;
                        case 29: // 探照灯 0.91
                            coef = 0.91;
                            errcoef = 0.06582838;
                            break;
                    }
                    ret += coef * slotmodel.iteminfo.reconnaissance;
                    err += errcoef * slotmodel.iteminfo.reconnaissance;
                }
            }
            ret += Math.Sqrt(sosakuteki) * 1.6841056;
            err += Math.Sqrt(sosakuteki) * 0.07815942;

            parameter = ret;
            error = err;
        }


        public static void ShipAbility(Model.KanColle.ShipData ship, out bool daycutin, out bool dayren, out bool nightcutin, out bool nightren)
        {
            daycutin = false;
            dayren = false;
            nightcutin = false;
            nightren = false;

            if (Model.MainModel.Current == null || Model.MainModel.Current.kancolleModel == null || Model.MainModel.Current.kancolleModel.slotdata == null ||
                ship == null || ship.Slots == null || ship.OnSlotCount == null)
                return;

            int planecount = 0; // 偵察機・爆撃機スロット数
            int mainguncount = 0; // 主砲数
            int subguncount = 0; // 副砲数
            int apacount = 0; // 徹甲弾数
            int radarcount = 0; // 電探数
            int torpedocount = 0; // 魚雷数


            int length = Math.Min(ship.Slots.Count, ship.OnSlotCount.Count);

            for (int i = 0; i < length; i++)
            {
                var slotmodel = Model.MainModel.Current.kancolleModel.slotdata.FirstOrDefault(_ => _.id == ship.Slots[i]);
                if (slotmodel != null && slotmodel.iteminfo != null)
                {
                    switch (slotmodel.iteminfo.type_equiptype)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 38:
                            // 主砲
                            ++mainguncount;
                            break;
                        case 4:
                            // 副砲
                            ++subguncount;
                            break;
                        case 5:
                        case 32:
                            // 魚雷
                            ++torpedocount;
                            break;
                        case 10:
                        case 11:
                            // 偵察機・爆撃機
                            planecount += ship.OnSlotCount[i];
                            break;
                        case 12:
                        case 13:
                            // 電探
                            ++radarcount;
                            break;
                        case 19:
                            // 対艦強化弾 = 徹甲弾
                            ++apacount;
                            break;
                    }
                }
            }

            // 弾着観測射撃
            // 最低条件: 水上偵察機・水上爆撃機が1スロット以上
            // 連撃: 主砲2副砲0徹甲弾1電探0/主砲1副砲1徹甲弾1電探0/主砲1副砲1徹甲弾0電探1/主砲1以上副砲1以上
            dayren = planecount > 0 && ((mainguncount >= 1 && subguncount >= 1) || (mainguncount == 2 && subguncount == 0 && apacount == 1 && radarcount == 0));
            // カットイン: 主砲2以上
            daycutin = planecount > 0 && mainguncount >= 2;

            // 夜戦
            // 連撃: 主砲2副砲0魚雷0/主砲1副砲1以上魚雷0/主砲0副砲2以上魚雷1以下
            nightren = ((mainguncount == 2 && subguncount == 0) || (mainguncount == 1 && subguncount >= 1) && torpedocount == 0) ||
                (mainguncount == 0 && subguncount >= 2 && torpedocount <= 1);
            // カットイン: 魚雷2以上/主砲3以上/主砲2副砲1以上/主砲2(副砲0)魚雷1/主砲1魚雷1
            nightcutin = torpedocount >= 2 || mainguncount >= 3 || (mainguncount == 2 && (subguncount >= 1 || torpedocount == 1)) ||
                (mainguncount == 1 && torpedocount == 1);
        }

        /// <summary>
        /// 対空カットイン
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public static bool ShipAntiAirCutinAbility(Model.KanColle.ShipData ship)
        {
            // パターンが多い
            // 2015年3月13日メンテ現在

            // 1. 高角砲 高射装置
            // 2. 25mm三連装機銃集中配備 対空機銃 対空電探
            // 3. 10cm高角砲+高射装置 対空電探
            // 4. 12.7cm高角砲+高射装置 対空電探
            // 5. 大口径主砲 高射装置 三式弾
            // 秋月（改）: 高角砲 （任意の）電探or高角砲
            // 摩耶改二: 高角砲 25mm三連装機銃集中配備

            // 高角砲: icontype==16
            // 高射装置: equiptype==36
            // 対空機銃: equiptype==21
            // 任意の電探: equiptype==12or13
            // 対空電探: equiptype==12or13 && nameに「対空」が含まれる
            // 大口径主砲: equiptype==3or38
            // 三式弾: equiptype==18

            // 25mm三連装機銃集中配備 131
            // 10cm高角砲+高射装置 122
            // 12.7cm高角砲+高射装置 130

            // 秋月 421
            // 秋月改 330
            // 摩耶改二 428

            if (Model.MainModel.Current == null || Model.MainModel.Current.kancolleModel == null || Model.MainModel.Current.kancolleModel.slotdata == null ||
                ship == null || ship.Slots == null)
                return false;

            int anti_air_gun = 0; // 高角砲
            int hacs = 0; // 高射装置
            int machinegun = 0; // 対空機銃
            int radar = 0;
            int anti_air_radar = 0;
            int big_main_gun = 0;
            int san_shiki = 0;
            int twenty_five_mm_triple_machinegun = 0;
            int ten_cm_anti_air_gun_with_hacs = 0;
            int twelve_point_seven_cm_air_gun_with_hacs = 0;

            for (int i = 0; i < ship.Slots.Count; i++)
            {
                var slotmodel = Model.MainModel.Current.kancolleModel.slotdata.FirstOrDefault(_ => _.id == ship.Slots[i]);
                var iteminfo = slotmodel.iteminfo;
                if (slotmodel != null && iteminfo != null)
                {
                    switch (iteminfo.type_equiptype)
                    {
                        case 36:
                            ++hacs;
                            break;
                        case 21:
                            ++anti_air_gun;
                            break;
                        case 12:
                        case 13:
                            ++radar;
                            if (iteminfo.name.IndexOf("対空") != -1)
                                ++anti_air_radar;
                            break;
                        case 3:
                        case 38:
                            ++big_main_gun;
                            break;
                        case 18:
                            ++san_shiki;
                            break;
                    }

                    if (iteminfo.type_icontype == 16)
                        ++anti_air_gun;

                    switch(slotmodel.itemid)
                    {
                        case 131:
                            ++twenty_five_mm_triple_machinegun;
                            break;
                        case 122:
                            ++ten_cm_anti_air_gun_with_hacs;
                            break;
                        case 130:
                            ++twelve_point_seven_cm_air_gun_with_hacs;
                            break;
                    }
                }
            }

            return
                (anti_air_gun >= 1 && hacs >= 1) ||
                (twenty_five_mm_triple_machinegun >= 1 && machinegun >= 2 && anti_air_radar >= 1) ||
                (ten_cm_anti_air_gun_with_hacs >= 1 && anti_air_radar >= 1) ||
                (twelve_point_seven_cm_air_gun_with_hacs >= 1 && anti_air_radar >= 1) ||
                (big_main_gun >= 1 && hacs >= 1 && san_shiki >= 1) ||
                ((ship.characterid == 421 || ship.characterid == 330) && anti_air_gun >= 1 && (radar >= 1 || anti_air_gun >= 2)) ||
                (ship.characterid == 428 && anti_air_gun >= 2 && twenty_five_mm_triple_machinegun >= 1);
        }

        /// <summary>
        /// 旗艦庇い発動かどうか
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public static bool IsFlagShipProtect(double damage)
        {
            return (damage - Math.Floor(damage)) > 0.09;
        }

    }
}
