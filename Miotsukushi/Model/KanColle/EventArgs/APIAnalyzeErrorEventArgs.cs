using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.EventArgs
{
    class APIAnalyzeErrorEventArgs : System.EventArgs
    {
        public string request;
        public string response;
        public string kcsapiurl;

        public APIAnalyzeErrorEventArgs(string kcsapiurl, string request, string response)
        {
            this.request = request;
            this.response = response;
            this.kcsapiurl = kcsapiurl;
        }
    }
}
