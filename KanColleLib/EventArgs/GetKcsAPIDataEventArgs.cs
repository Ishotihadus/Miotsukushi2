using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.EventArgs
{
    public class GetKcsAPIDataEventArgs : System.EventArgs
    {
        public string request;
        public string response;
        public string kcsapiurl;

        public GetKcsAPIDataEventArgs(string kcsapiurl, string request, string response)
        {
            this.kcsapiurl = kcsapiurl;
            this.request = request;
            this.response = response;
        }
    }
}
