using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using KanColleLib.EventArgs;

namespace KanColleLib
{
    public class KanColleNotifier
    {
        public KanColleNotifier(bool IsAsync)
        {
            if (IsAsync)
                FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete_Async;
            else
                FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;
            FiddlerApplication.AfterSessionComplete += (oSession) => OnGetSessionData(new GetSessionDataEventArgs() { session = oSession });
            FiddlerApplication.Log.OnLogString += (sender, e) => OnGetFiddlerLogString(new GetFiddlerLogStringEventArgs() { logtext = e.LogString });
        }

        void FiddlerApplication_AfterSessionComplete(Session oSession)
        {
            string kcsapiurl = null;
            int kcsapiindex = oSession.fullUrl.IndexOf("/kcsapi/");
            if (kcsapiindex != -1)
                kcsapiurl = oSession.fullUrl.Substring(kcsapiindex + 8);

            if (kcsapiurl != null || oSession.oResponse.MIMEType == "text/plain")
            {
                string request = oSession.GetRequestBodyAsString();

                // not implemented
            }
        }

        async void FiddlerApplication_AfterSessionComplete_Async(Session oSession)
        {
            await Task.Run(() => FiddlerApplication_AfterSessionComplete(oSession));
        }

        #region イベント定義

        /// <summary>
        /// FiddlerがAfterSessionCompleteからデータを取り出した際に発生します。
        /// </summary>
        public event GetSessionDataEventHandler GetSessionData;
        public delegate void GetSessionDataEventHandler(object sender, GetSessionDataEventArgs e);
        protected virtual void OnGetSessionData(GetSessionDataEventArgs e) { if (GetSessionData != null) { GetSessionData(this, e); } }

        /// <summary>
        /// FiddlerからLogTextを受信した際に発生します。
        /// </summary>
        public event GetFiddlerLogStringEventHandler GetFiddlerLogString;
        public delegate void GetFiddlerLogStringEventHandler(object sender, GetFiddlerLogStringEventArgs e);
        protected virtual void OnGetFiddlerLogString(GetFiddlerLogStringEventArgs e) { if (GetFiddlerLogString != null) { GetFiddlerLogString(this, e); } }

        #endregion

    }
}
