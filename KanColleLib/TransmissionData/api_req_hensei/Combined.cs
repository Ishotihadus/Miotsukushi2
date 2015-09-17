using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_req_hensei
{
    public class Combined
    {
        public bool combined;

        public static Combined fromDynamic(dynamic json)
        {
            var combined = new Combined();
            combined.combined = (int)json.api_combined == 1;
            return combined;
        }
    }
}
