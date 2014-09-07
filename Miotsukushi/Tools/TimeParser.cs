using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Tools
{
    class TimeParser
    {
        public static DateTime ParseTimeFromLong(long ticks)
        {
            return new DateTime(1970, 1, 1, 9, 0, 0) + TimeSpan.FromMilliseconds(ticks);
        }
    }
}
