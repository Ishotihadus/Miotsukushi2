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
        public static void ShipResource(int level, int resourceNow, int resourceMax, out int realNow, out int realMax)
        {
            if (level <= 99)
            {
                realNow = resourceNow;
                realMax = resourceMax;
            }
            else
            {
                realMax = (int)Math.Floor(resourceMax * 0.85);

                if (resourceNow == resourceMax)
                    realNow = realMax;
                else
                {
                    var decrease = Math.Max((int)Math.Floor((resourceMax - resourceNow) * 0.85), 1);
                    realNow = realMax - decrease;
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
                case 42: return Color.FromRgb(231, 107, 25); // 大型探照灯
                case 43: return Color.FromRgb(44, 58, 59); // 戦闘糧食
                case 44: return Color.FromRgb(96, 196, 157); // 補給物資
                case 93: return Color.FromRgb(229, 152, 53); // 大型電探(II)
                default: return Colors.Transparent;
            }
        }

        /// <summary>
        /// 艦載機熟練度と装備の種類から判明する制空値ボーナス
        /// </summary>
        /// <param name="alv">熟練度</param>
        /// <param name="equiptype">equiptype</param>
        /// <returns>スロット当たりの制空値ボーナス</returns>
        private static double BonusAirMasteryFromAlv(int alv, int equiptype)
        {
            int[] knsBonus = {0, 0, 2, 5, 9, 14, 14, 22};
            int[] sbkBonus = {0, 0, 1, 1, 1, 3, 3, 6};

            double bonus;

            if (alv <= 0)
                bonus = 0;
            else if (alv <= 6)
                bonus = Math.Sqrt((alv * 15 - 5) / 10.0);
            else
                bonus = Math.Sqrt(10);

            if (equiptype == 6)
            {
                if (alv <= 0)
                    bonus += knsBonus[0];
                else if (alv <= 6)
                    bonus += knsBonus[alv];
                else
                    bonus += knsBonus[7];
            }
            else if (equiptype == 11)
            {
                if (alv <= 0)
                    bonus += sbkBonus[0];
                else if (alv <= 6)
                    bonus += sbkBonus[alv];
                else
                    bonus += sbkBonus[7];
            }
            else if (equiptype == 7 || equiptype == 8)
            {

            }
            else
            {
                bonus = 0;
            }

            return bonus;
        }

        /// <summary>
        /// スロット当たりの制空値を計算する
        /// </summary>
        /// <param name="slot">スロット情報</param>
        /// <param name="onslot">搭載数</param>
        /// <returns>スロット当たりの制空値</returns>
        public static int SlotAirMastery(Model.KanColle.SlotData slot, int onslot)
        {
            if (slot.Iteminfo == null ||
                !(slot.Iteminfo.TypeEquiptype == 6 || slot.Iteminfo.TypeEquiptype == 7 ||
                  slot.Iteminfo.TypeEquiptype == 8 || slot.Iteminfo.TypeEquiptype == 11))
                return 0;

            var baseantiair = slot.Iteminfo.AntiAir * Math.Sqrt(onslot);
            var alvbonus = slot.Alv.HasValue ? BonusAirMasteryFromAlv(slot.Alv.Value, slot.Iteminfo.TypeEquiptype) : 0;
            
            return (int) Math.Floor(baseantiair + alvbonus);
        }

        public static int ShipAirMastery(Model.KanColle.ShipData ship)
        {
            if (Model.MainModel.Current == null || Model.MainModel.Current.KancolleModel == null || Model.MainModel.Current.KancolleModel.Slotdata == null || ship?.Slots == null || ship.OnSlotCount == null)
                return 0;

            var length = Math.Min(ship.Slots.Count, ship.OnSlotCount.Count);
            var ret = 0;

            for (var i = 0; i < length; i++)
            {
                var slotmodel = Model.MainModel.Current.KancolleModel.Slotdata.FirstOrDefault(_ => _.Id == ship.Slots[i]);
                if (slotmodel != null)
                    ret += SlotAirMastery(slotmodel, ship.OnSlotCount[i]);
            }

            return ret;
        }

        public static int ShipDrumCount(Model.KanColle.ShipData ship)
        {
            if (Model.MainModel.Current == null || Model.MainModel.Current.KancolleModel == null || Model.MainModel.Current.KancolleModel.Slotdata == null || ship?.Slots == null || ship.OnSlotCount == null)
                return 0;

            var ret = 0;

            for (var i = 0; i < ship.Slots.Count; i++)
            {
                var slotmodel = Model.MainModel.Current.KancolleModel.Slotdata.FirstOrDefault(_ => _.Id == ship.Slots[i]);
                if (slotmodel != null && slotmodel.Itemid == 75)
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
            if (Model.MainModel.Current == null || Model.MainModel.Current.KancolleModel == null || Model.MainModel.Current.KancolleModel.Slotdata == null ||
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
            var sosakuteki = ship.Reconnaissance;

            for (var i = 0; i < ship.Slots.Count; i++)
            {
                var slotmodel = Model.MainModel.Current.KancolleModel.Slotdata.FirstOrDefault(_ => _.Id == ship.Slots[i]);
                if (slotmodel != null && slotmodel.Iteminfo != null)
                {
                    sosakuteki -= slotmodel.Iteminfo.Reconnaissance;
                    double coef = 0;
                    double errcoef = 0;
                    switch(slotmodel.Iteminfo.TypeEquiptype)
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
                    ret += coef * slotmodel.Iteminfo.Reconnaissance;
                    err += errcoef * slotmodel.Iteminfo.Reconnaissance;
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

            if (Model.MainModel.Current == null || Model.MainModel.Current.KancolleModel == null || Model.MainModel.Current.KancolleModel.Slotdata == null ||
                ship == null || ship.Slots == null || ship.OnSlotCount == null)
                return;

            var planecount = 0; // 偵察機・爆撃機スロット数
            var mainguncount = 0; // 主砲数
            var subguncount = 0; // 副砲数
            var apacount = 0; // 徹甲弾数
            var radarcount = 0; // 電探数
            var torpedocount = 0; // 魚雷数


            var length = Math.Min(ship.Slots.Count, ship.OnSlotCount.Count);

            for (var i = 0; i < length; i++)
            {
                var slotmodel = Model.MainModel.Current.KancolleModel.Slotdata.FirstOrDefault(_ => _.Id == ship.Slots[i]);
                if (slotmodel != null && slotmodel.Iteminfo != null)
                {
                    switch (slotmodel.Iteminfo.TypeEquiptype)
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
            // 秋月（改）・照月（改）: 高角砲 （任意の）電探or高角砲
            // 摩耶改二: 高角砲 25mm三連装機銃集中配備
            // 五十鈴改二: 高角砲 対空機銃 対空電探

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
            // 照月 422
            // 秋月改 330
            // 照月改 346
            // 摩耶改二 428
            // 五十鈴改二 141

            if (Model.MainModel.Current == null || Model.MainModel.Current.KancolleModel == null || Model.MainModel.Current.KancolleModel.Slotdata == null ||
                ship == null || ship.Slots == null)
                return false;

            var antiAirGun = 0; // 高角砲
            var hacs = 0; // 高射装置
            var machinegun = 0; // 対空機銃
            var radar = 0; // 電探
            var antiAirRadar = 0; // 対空電探
            var bigMainGun = 0; // 大口径主砲
            var sanShiki = 0; // 三式弾
            var twentyFiveMmTripleMachinegun = 0; // 25mmうんちゃら
            var tenCmAntiAirGunWithHacs = 0; // 10cm高角砲＋高射装置
            var twelvePointSevenCmAirGunWithHacs = 0; // 12.7cm高角砲＋高射装置

            foreach (int slot in ship.Slots)
            {
                var slotmodel = Model.MainModel.Current.KancolleModel.Slotdata.FirstOrDefault(_ => _.Id == slot);
                if (slotmodel?.Iteminfo == null) continue;

                var iteminfo = slotmodel.Iteminfo;
                switch (iteminfo.TypeEquiptype)
                {
                    case 36:
                        ++hacs;
                        break;
                    case 21:
                        ++antiAirGun;
                        break;
                    case 12:
                    case 13:
                        ++radar;
                        if (iteminfo.Name.IndexOf("対空") != -1)
                            ++antiAirRadar;
                        break;
                    case 3:
                    case 38:
                        ++bigMainGun;
                        break;
                    case 18:
                        ++sanShiki;
                        break;
                }

                if (iteminfo.TypeIcontype == 16)
                    ++antiAirGun;

                switch(slotmodel.Itemid)
                {
                    case 131:
                        ++twentyFiveMmTripleMachinegun;
                        break;
                    case 122:
                        ++tenCmAntiAirGunWithHacs;
                        break;
                    case 130:
                        ++twelvePointSevenCmAirGunWithHacs;
                        break;
                }
            }

            return
                (antiAirGun >= 1 && hacs >= 1) ||
                (twentyFiveMmTripleMachinegun >= 1 && machinegun >= 2 && antiAirRadar >= 1) ||
                (tenCmAntiAirGunWithHacs >= 1 && antiAirRadar >= 1) ||
                (twelvePointSevenCmAirGunWithHacs >= 1 && antiAirRadar >= 1) ||
                (bigMainGun >= 1 && hacs >= 1 && sanShiki >= 1) ||
                ((ship.Characterid == 421 || ship.Characterid == 330 || ship.Characterid == 422 || ship.Characterid == 346) && antiAirGun >= 1 && (radar >= 1 || antiAirGun >= 2)) ||
                (ship.Characterid == 428 && antiAirGun >= 2 && twentyFiveMmTripleMachinegun >= 1) || 
                (ship.Characterid == 141 && antiAirGun >= 1 && machinegun >= 1 && antiAirRadar >= 1);
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
