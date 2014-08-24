using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib
{
    internal sealed class RequestDecorder
    {
        internal static Dictionary<string, object> DecordRequest(string str)
        {
            int lastindex = -1;
            while (true)
            {
                try
                {
                    int index = str.IndexOf('%', lastindex + 1);
                    if (index == -1)
                        break;
                    int charnum = Convert.ToInt32(str.Substring(index + 1, 2), 16);
                    str = str.Substring(0, index) + (char)charnum + str.Substring(index + 3);
                }
                catch (Exception) { break; }
            }

            var dic = new Dictionary<string, object>();

            string[] split = str.Split('&');
            foreach (string strval in split)
            {
                int ind = strval.IndexOf('=');
                if (ind == -1)
                    continue;
                string key = strval.Substring(0, ind);
                int ivalue;
                double dvalue;
                string value = strval.Substring(ind + 1);
                if (int.TryParse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out ivalue))
                {
                    dic[key] = ivalue;
                }
                else if (double.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out dvalue))
                {
                    dic[key] = dvalue;
                }
                else
                {
                    dic[key] = value;
                }

            }

            return dic;
        }
    }
}
