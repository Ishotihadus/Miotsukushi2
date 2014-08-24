using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_port
{
    public class Log
    {
        public List<values.LogValue> logs;

        public static Log fromDynamic(dynamic json)
        {
            Log log = new Log();
            log.logs = new List<values.LogValue>();
            foreach (var data in json)
                log.logs.Add(values.LogValue.fromDynamic(data));
            return log;
        }
    }
}
