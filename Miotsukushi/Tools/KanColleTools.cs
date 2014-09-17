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
                case 1: return Brushes.Salmon;
                case 2: return Brushes.Tomato;
                case 3: return Brushes.Crimson;
                case 4: return Brushes.Yellow;
                case 5: return Brushes.SteelBlue;
                case 6: return Brushes.ForestGreen;
                case 7: return Brushes.IndianRed;
                case 8: return Brushes.CornflowerBlue;
                case 9: return Brushes.Gold;
                case 10: return Brushes.Honeydew;
                case 11: return Brushes.MintCream;
                case 12: return Brushes.Goldenrod;
                case 13: return Brushes.Goldenrod;
                case 14: return Brushes.SkyBlue;
                case 15: return Brushes.PowderBlue;
                case 16: return Brushes.LightGreen;
                case 17: return Brushes.Gold;
                case 18: return Brushes.MediumSeaGreen;
                case 19: return Brushes.Tomato;
                case 20: return Brushes.White;
                case 21: return Brushes.PaleGreen;
                case 22: return Brushes.SteelBlue;
                case 23: return Brushes.Gainsboro;
                case 24: return Brushes.OliveDrab;
                case 25: return Brushes.LimeGreen;
                case 26: return Brushes.LightSkyBlue;
                case 27: return Brushes.MediumPurple;
                case 28: return Brushes.MediumPurple;
                case 29: return Brushes.DarkOrange;
                case 30: return Brushes.DarkGray;
                case 31: return Brushes.HotPink;
                case 32: return Brushes.SteelBlue;
                case 33: return Brushes.Orange;
                default: return Brushes.White;
            }
        }
    }
}
