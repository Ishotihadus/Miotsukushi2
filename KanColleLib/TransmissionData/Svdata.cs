using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData
{
    public class Svdata<T>
    {
        public int result;
        public string result_msg;
        public T data;

        public static Svdata<T> fromDynamic(dynamic json, T data)
        {
            var header = new Svdata<T>();

            header.result = (int)json.api_result;
            header.result_msg = json.api_result_msg as string;
            header.data = data;

            return header;
        }
    }
}
