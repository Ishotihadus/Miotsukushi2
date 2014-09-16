using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
