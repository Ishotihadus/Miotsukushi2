using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest
{
    internal sealed class RequestDecorder
    {
        public static Dictionary<string, string> DecordRequest(string str)
        {
            var dic = new Dictionary<string, string>();

            var split = str.Split('&');
            foreach (var strval in split)
            {
                var ind = strval.IndexOf('=');
                if (ind == -1)
                    continue;
                var key = DecodeString(strval.Substring(0, ind));
                var value = DecodeString(strval.Substring(ind + 1));


                dic[key] = value;

            }

            return dic;
        }

        private static string DecodeString(string str)
        {
            return System.Web.HttpUtility.UrlDecode(str, Encoding.UTF8);

            //string returnstring = str;

            //int lastindex = -1;
            //while (true)
            //{
            //    try
            //    {
            //        int index = returnstring.IndexOf('%', lastindex + 1);
            //        if (index == -1)
            //            break;
            //        int charnum = Convert.ToInt32(returnstring.Substring(index + 1, 2), 16);
            //        returnstring = returnstring.Substring(0, index) + (char)charnum + returnstring.Substring(index + 3);
            //        lastindex = index;
            //    }
            //    catch { ++lastindex; }
            //}

            //return returnstring;
        }
    }
}
