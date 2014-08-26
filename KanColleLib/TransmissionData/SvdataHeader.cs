using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData
{
    public class SvdataHeader
    {
        public int result;
        public string result_msg;

        public static SvdataHeader fromDynamic(dynamic json)
        {
            SvdataHeader header = new SvdataHeader();

            header.result = (int)json.api_result;
            header.result_msg = json.api_result_msg as string;

            return header;
        }
    }
}
