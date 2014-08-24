using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_hensei
{
    public class Lock
    {
        public bool locked;

        public static Lock fromDynamic(dynamic json)
        {
            Lock _lock = new Lock();
            _lock.locked = (int)json.api_locked == 1;
            return _lock;
        }
    }
}
