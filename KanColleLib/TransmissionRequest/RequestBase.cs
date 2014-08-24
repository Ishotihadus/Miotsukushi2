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

            token = _Get_Reqest("api_token");
            verno = _Get_Reqest("api_verno");
        }

        protected string _Get_Reqest(string key)
        {
            if (_requestraw == null || !_requestraw.ContainsKey(key))
                return null;
            else
                return _requestraw[key];
        }

        protected int? _Get_Reqest_int(string key)
        {
            int r;
            string t = _Get_Reqest(key);
            if (t != null && int.TryParse(t, out r))
                return r;
            else
                return null;
        }
    }
}
