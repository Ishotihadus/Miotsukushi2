using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionRequest
{
    public class RequestBase
    {
        Dictionary<string, string> _requestraw;

        public string token;
        public string verno;

        public RequestBase(string request)
        {
            _requestraw = RequestDecorder.DecordRequest(request);

            token = _Get_Request("api_token");
            verno = _Get_Request("api_verno");
        }

        protected string _Get_Request(string key)
        {
            if (_requestraw == null || !_requestraw.ContainsKey(key))
                return null;
            else
                return _requestraw[key];
        }

        protected int? _Get_Request_int(string key)
        {
            int r;
            var t = _Get_Request(key);
            if (t != null && int.TryParse(t, out r))
                return r;
            else
                return null;
        }

        /// <summary>
        /// カンマ区切りでうー
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected int?[] _Get_Request_int_array(string key)
        {
            var t = _Get_Request(key);
            if (t == null)
                return null;

            var ts = t.Split(',');
            var i_array = new int?[ts.Length];

            for (var i = 0; i < ts.Length; i++)
            {
                int c;
                if (int.TryParse(ts[i], out c))
                    i_array[i] = c;
                else
                    i_array[i] = null;
            }

            return i_array;
        }
    }
}
