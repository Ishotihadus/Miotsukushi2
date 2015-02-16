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
                    return (int)Math.Floor(slot.iteminfo.taiku * Math.Sqrt(onslot));
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
    }
}
