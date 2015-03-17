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

        public static Brush GetSlotItemEquipTypeBrush(int equiptype)
        {
            switch (equiptype)
            {
                case 1: return new SolidColorBrush(Color.FromRgb(209, 91, 91)); // 小口径主砲
                case 2: return new SolidColorBrush(Color.FromRgb(204, 61, 61)); // 中口径主砲
                case 3: return new SolidColorBrush(Color.FromRgb(194, 34, 34)); // 大口径主砲
                case 4: return new SolidColorBrush(Color.FromRgb(255, 234, 0)); // 副砲
                case 5: return new SolidColorBrush(Color.FromRgb(88, 135, 171)); // 魚雷
                case 6: return new SolidColorBrush(Color.FromRgb(102, 204, 119)); // 艦上戦闘機
                case 7: return new SolidColorBrush(Color.FromRgb(243, 109, 109)); // 艦上爆撃機
                case 8: return new SolidColorBrush(Color.FromRgb(101, 188, 255)); // 艦上攻撃機
                case 9: return new SolidColorBrush(Color.FromRgb(255, 192, 0)); // 艦上偵察機
                case 10: return new SolidColorBrush(Color.FromRgb(140, 200, 150)); // 水上偵察機
                case 11: return new SolidColorBrush(Color.FromRgb(140, 200, 150)); // 水上爆撃機
                case 12: return new SolidColorBrush(Color.FromRgb(229, 152, 53)); // 小型電探
                case 13: return new SolidColorBrush(Color.FromRgb(229, 152, 53)); // 大型電探
                case 14: return new SolidColorBrush(Color.FromRgb(90, 171, 184)); // ソナー
                case 15: return new SolidColorBrush(Color.FromRgb(127, 204, 216)); // 爆雷
                case 16: return new SolidColorBrush(Color.FromRgb(154, 127, 175)); // 追加装甲
                case 17: return new SolidColorBrush(Color.FromRgb(255, 196, 77)); // 機関部強化
                case 18: return new SolidColorBrush(Color.FromRgb(96, 190, 113)); // 対空強化弾
                case 19: return new SolidColorBrush(Color.FromRgb(209, 91, 91)); // 対艦強化弾
                case 20: return new SolidColorBrush(Color.FromRgb(218, 200, 176)); // VT信管
                case 21: return new SolidColorBrush(Color.FromRgb(102, 203, 119)); // 対空機銃
                case 22: return new SolidColorBrush(Color.FromRgb(88, 135, 171)); // 特殊潜航艇（甲標的）
                case 23: return new SolidColorBrush(Color.FromRgb(255, 255, 255)); // 応急修理要員
                case 24: return new SolidColorBrush(Color.FromRgb(154, 165, 93)); // 上陸用舟艇
                case 25: return new SolidColorBrush(Color.FromRgb(102, 204, 119)); // オートジャイロ
                case 26: return new SolidColorBrush(Color.FromRgb(127, 204, 216)); // 対潜哨戒機
                case 27: return new SolidColorBrush(Color.FromRgb(154, 127, 175)); // 追加装甲（中型）
                case 28: return new SolidColorBrush(Color.FromRgb(154, 127, 175)); // 追加装甲（大型）
                case 29: return new SolidColorBrush(Color.FromRgb(231, 107, 25)); // 探照灯
                case 30: return new SolidColorBrush(Color.FromRgb(163, 163, 163)); // 簡易輸送部材
                case 31: return new SolidColorBrush(Color.FromRgb(176, 157, 127)); // 艦艇修理施設
                case 32: return new SolidColorBrush(Color.FromRgb(88, 135, 171)); // 潜水艦魚雷
                case 33: return new SolidColorBrush(Color.FromRgb(255, 155, 0)); // 照明弾
                case 34: return new SolidColorBrush(Color.FromRgb(200, 170, 255)); // 司令部施設
                case 35: return new SolidColorBrush(Color.FromRgb(205, 162, 105)); // 航空要員
                case 36: return new SolidColorBrush(Color.FromRgb(137, 154, 77)); // 高射装置
                case 37: return new SolidColorBrush(Color.FromRgb(255, 54, 54)); // 対地装備
                case 38: return new SolidColorBrush(Color.FromRgb(194, 34, 34)); // 大口径主砲(II)
                case 39: return new SolidColorBrush(Color.FromRgb(191, 235, 159)); // 水上艦要員
                default: return Brushes.Transparent;
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
                ship == null || ship.Slots == null || ship.OnSlotCount == null)
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
    }
}
