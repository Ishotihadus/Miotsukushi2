using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.EventArgs
{
    public class GetSessionDataEventArgs : System.EventArgs
    {
        /// <summary>
        /// Fiddlerから受け取ったSession
        /// </summary>
        internal Fiddler.Session session;

        public string fullUrl;

        public string responseString;

        public string requestString;

        public string MIMEType;

        public GetSessionDataEventArgs(Fiddler.Session session)
        {
            this.session = session;
            fullUrl = session.fullUrl;
            responseString = session.GetResponseBodyAsString();
            requestString = session.GetRequestBodyAsString();
            MIMEType = session.oResponse.MIMEType;
        }
    }
}
