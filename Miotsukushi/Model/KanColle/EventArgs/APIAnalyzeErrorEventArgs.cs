using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.EventArgs
{
    class ApiAnalyzeErrorEventArgs : System.EventArgs
    {
        public string Request;
        public string Response;
        public string Kcsapiurl;

        public ApiAnalyzeErrorEventArgs(string kcsapiurl, string request, string response)
        {
            this.Request = request;
            this.Response = response;
            this.Kcsapiurl = kcsapiurl;
        }
    }
}
