using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using KanColleLib.EventArgs;
using KanColleLib.TransmissionData;
using KanColleLib.TransmissionRequest;

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
                kcsapiurl = oSession.fullUrl.Substring(kcsapiindex + 8); // 8は/kcsapi/の8文字のこと

            if (kcsapiurl != null || oSession.oResponse.MIMEType == "text/plain")
            {
                string request = oSession.GetRequestBodyAsString();
                string response = oSession.GetResponseBodyAsString();
                RaiseEventFromKcsAPISessions(kcsapiurl, request, response);
            }
        }

        void RaiseEventFromKcsAPISessions(string kcsapiurl, string request, string response)
        {
            dynamic json = null;

            try
            {
                json = Codeplex.Data.DynamicJson.Parse(response.Substring(("svdata").Length));
            }
            catch (Exception e)
            {
                throw new KanColleLibException("Response Json Parse Error", e);
            }

            SvdataHeader svdataheader = SvdataHeader.fromDynamic(json);

            switch (kcsapiurl)
            {
                case "api_get_member/basic":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_get_member.Basic.fromDynamic(json.api_data))
                        );
                    break;
                case "api_get_member/deck":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_get_member.Deck.fromDynamic(json.api_data))
                        );
                    break;
                case "api_get_member/furniture":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_get_member.Basic.fromDynamic(json.api_data))
                        );
                    break;
                case "api_get_member/kdock":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_get_member.KDock.fromDynamic(json.api_data))
                        );
                    break;
                case "api_get_member/mapcell":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new TransmissionRequest.api_get_member.MapcellRequest(request), svdataheader,
                        TransmissionData.api_get_member.Mapcell.fromDynamic(json.api_data))
                        );
                    break;
                case "api_get_member/mapinfo":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_get_member.Mapinfo.fromDynamic(json.api_data))
                        );
                    break;
                case "api_get_member/material":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_get_member.Material.fromDynamic(json.api_data))
                        );
                    break;


                case "api_start2":
                    OnGetKcsAPIData(new GetKcsAPIDataEventArgs(
                        kcsapiurl, new RequestBase(request), svdataheader,
                        TransmissionData.api_start2.Start2.fromDynamic(json.api_data))
                        );
                    break;
                default:
                    
                    System.Diagnostics.Debug.WriteLine("UNDEFINED KCSAPIURL: " + kcsapiurl);
                    System.Diagnostics.Debug.WriteLine("REQUEST BODY: " + request);
                    System.Diagnostics.Debug.WriteLine("RESPONSE BODY: " + response);
                    throw new NotImplementedException(kcsapiurl);
            }
        }

        async void FiddlerApplication_AfterSessionComplete_Async(Session oSession)
        {
            await Task.Run(() => FiddlerApplication_AfterSessionComplete(oSession));
        }

        #region 通常イベント定義

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

        /// <summary>
        /// 艦これのAPIからデータを受信した際に発生します。
        /// </summary>
        public event GetKcsAPIDataEventHandler GetKcsAPIData;
        public delegate void GetKcsAPIDataEventHandler(object sender, GetKcsAPIDataEventArgs e);
        protected virtual void OnGetKcsAPIData(GetKcsAPIDataEventArgs e) { if (GetKcsAPIData != null) { GetKcsAPIData(this, e); } }

        #endregion

    }
}
