using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.EventArgs
{
    class KcsAPIDataAnalyzeFailedEventArgs : System.EventArgs
    {
        public string request;
        public string response;
        public string kcsapiurl;
        public Exception originalexception;

        public KcsAPIDataAnalyzeFailedEventArgs(string kcsapiurl, string request, string response, Exception originalexception)
        {
            this.request = request;
            this.response = response;
            this.kcsapiurl = kcsapiurl;
            this.originalexception = originalexception;
        }
    }
}
