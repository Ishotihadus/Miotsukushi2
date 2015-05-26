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
        List<double> processingcode = new List<double>();
        Random random = new Random();
        OutProxySettings proxy = new OutProxySettings();

        public KanColleNotifier(bool isAsync)
        {
            if (isAsync)
                FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete_Async;
            else
                FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;
            FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
            FiddlerApplication.AfterSessionComplete += (oSession) => OnGetSessionData(new GetSessionDataEventArgs() { session = oSession });
            FiddlerApplication.Log.OnLogString += (sender, e) => OnGetFiddlerLogString(new GetFiddlerLogStringEventArgs() { logtext = e.LogString });
        }

        private void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (proxy.enabled)
                oSession["X-OverrideGateway"] = proxy.GetGatewayString();
        }

        public static int FiddlerStartup(int iListenPort=0)
        {
            Fiddler.FiddlerApplication.Startup(iListenPort, Fiddler.FiddlerCoreStartupFlags.ChainToUpstreamGateway);
            int port = Fiddler.FiddlerApplication.oProxy.ListenPort;
            return port;
        }

        public static void FiddlerSetWinInetProxy()
        {
            Fiddler.URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", Fiddler.FiddlerApplication.oProxy.ListenPort), "<local>");
        }

        public void SetUpstreamProxy(ProxyType type, string server, int port)
        {
            proxy.type = type;
            proxy.servername = server;
            proxy.port = port;
            proxy.enabled = true;
        }

        public void DisableUpstreamProxy()
        {
            proxy.enabled = false;
        }

        async void FiddlerApplication_AfterSessionComplete_Async(Session oSession)
        {
            await Task.Run(() => FiddlerApplication_AfterSessionComplete(oSession));
        }

        void FiddlerApplication_AfterSessionComplete(Session oSession)
        {

            string kcsapiurl = null;

            if (oSession.fullUrl.IndexOf("kcs/mainD2.swf") != -1)
                OnGameStart(new GameStartEventArgs() { main2Dadress = oSession.fullUrl });

            int kcsapiindex = oSession.fullUrl.IndexOf("/kcsapi/");
            if (kcsapiindex != -1)
                kcsapiurl = oSession.fullUrl.Substring(kcsapiindex + 8); // "/kcsapi/".Length

            if (kcsapiurl != null && oSession.oResponse.MIMEType == "text/plain")
            {
                // 処理の順番をなるべく保証する
                var thisprocessingcode = random.NextDouble();
                processingcode.Add(thisprocessingcode);
                while (processingcode[0] != thisprocessingcode)
                {
                    System.Threading.Thread.Sleep(1);
                }

                string request = oSession.GetRequestBodyAsString();
                string response = oSession.GetResponseBodyAsString();

                try
                {
                    RaiseEventFromKcsAPISessions(kcsapiurl, request, response);
                }
                catch (Exception e)
                {
                    // throw new KanColleLibException(string.Format("Session Response Analyze Error: {0}", kcsapiurl), e);
                    OnKcsAPIDataAnalyzeFailed(new KcsAPIDataAnalyzeFailedEventArgs(kcsapiurl, request, response, e));
                    System.Diagnostics.Debug.WriteLine("----------------------");
                    System.Diagnostics.Debug.WriteLine("KanColleLibException: Session Response Analyze Error: " + kcsapiurl);
                    System.Diagnostics.Debug.WriteLine(e);
                    System.Diagnostics.Debug.WriteLine("Response: " + response);
                }
                OnGetKcsAPIData(new GetKcsAPIDataEventArgs(kcsapiurl, request, response));

                processingcode.RemoveAt(0);
            }
        }

        void RaiseEventFromKcsAPISessions(string kcsapiurl, string request, string response)
        {
            dynamic json = null;

            try
            {
                json = Codeplex.Data.DynamicJson.Parse(response.Substring(7)); // "svdata=".Length
            }
            catch (Exception e)
            {
                throw new KanColleLibException("Response Json Parse Error", e);
            }
            System.Diagnostics.Debug.WriteLine("KCSAPIURL: " + kcsapiurl);

            switch (kcsapiurl)
            {
                case "api_get_member/basic":
                    if (json.api_data())
                        OnGetGetmemberBasic(new RequestBase(request), Svdata<TransmissionData.api_get_member.Basic>.fromDynamic(json, TransmissionData.api_get_member.Basic.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/deck":
                    if (json.api_data())
                        OnGetGetmemberDeck(new RequestBase(request), Svdata<TransmissionData.api_get_member.Deck>.fromDynamic(json, TransmissionData.api_get_member.Deck.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/furniture":
                    if (json.api_data())
                        OnGetGetmemberFurniture(new RequestBase(request), Svdata<TransmissionData.api_get_member.Furniture>.fromDynamic(json, TransmissionData.api_get_member.Furniture.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/kdock":
                    if (json.api_data())
                        OnGetGetmemberKdock(new RequestBase(request), Svdata<TransmissionData.api_get_member.KDock>.fromDynamic(json, TransmissionData.api_get_member.KDock.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/mapcell":
                    if (json.api_data())
                        OnGetGetmemberMapcell(new TransmissionRequest.api_get_member.MapcellRequest(request), Svdata<TransmissionData.api_get_member.Mapcell>.fromDynamic(json, TransmissionData.api_get_member.Mapcell.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/mapinfo":
                    if (json.api_data())
                        OnGetGetmemberMapinfo(new RequestBase(request), Svdata<TransmissionData.api_get_member.Mapinfo>.fromDynamic(json, TransmissionData.api_get_member.Mapinfo.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/material":
                    if (json.api_data())
                        OnGetGetmemberMaterial(new RequestBase(request), Svdata<TransmissionData.api_get_member.Material>.fromDynamic(json, TransmissionData.api_get_member.Material.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/mission":
                    if (json.api_data())
                        OnGetGetmemberMission(new RequestBase(request), Svdata<TransmissionData.api_get_member.Mission>.fromDynamic(json, TransmissionData.api_get_member.Mission.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/ndock":
                    if (json.api_data())
                        OnGetGetmemberNdock(new RequestBase(request), Svdata<TransmissionData.api_get_member.NDock>.fromDynamic(json, TransmissionData.api_get_member.NDock.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/questlist":
                    if (json.api_data())
                        OnGetGetmemberQuestlist(new TransmissionRequest.api_get_member.QuestlistRequest(request), Svdata<TransmissionData.api_get_member.Questlist>.fromDynamic(json, TransmissionData.api_get_member.Questlist.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/ship2":
                    if (json.api_data())
                        OnGetGetmemberShip2(new TransmissionRequest.api_get_member.Ship2Request(request), Svdata<TransmissionData.api_get_member.Ship2>.fromDynamic(json, TransmissionData.api_get_member.Ship2.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/ship3":
                    if (json.api_data())
                        OnGetGetmemberShip3(new TransmissionRequest.api_get_member.Ship3Request(request), Svdata<TransmissionData.api_get_member.Ship3>.fromDynamic(json, TransmissionData.api_get_member.Ship3.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/ship_deck":
                    if (json.api_data())
                        OnGetGetmemberShipDeck(new TransmissionRequest.api_get_member.ShipDeckRequest(request), Svdata<TransmissionData.api_get_member.ShipDeck>.fromDynamic(json, TransmissionData.api_get_member.ShipDeck.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/slot_item":
                    if (json.api_data())
                        OnGetGetmemberSlotItem(new RequestBase(request), Svdata<TransmissionData.api_get_member.SlotItem>.fromDynamic(json, TransmissionData.api_get_member.SlotItem.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_get_member/unsetslot":
                    if (json.api_data())
                        OnGetGetmemberUnsetslot(new RequestBase(request), Svdata<TransmissionData.api_get_member.Unsetslot>.fromDynamic(json, TransmissionData.api_get_member.Unsetslot.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_port/port":
                    if (json.api_data())
                        OnGetPortPort(new TransmissionRequest.api_port.PortRequest(request), Svdata<TransmissionData.api_port.Port>.fromDynamic(json, TransmissionData.api_port.Port.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_hensei/change":
                    OnGetReqhenseiChange(new TransmissionRequest.api_req_hensei.ChangeRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_hensei/combined":
                    if (json.api_data())
                        OnGetReqhenseiCombined(new RequestBase(request), Svdata<TransmissionData.api_req_hensei.Combined>.fromDynamic(json, TransmissionData.api_req_hensei.Combined.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_hensei/lock":
                    if (json.api_data())
                        OnGetReqhenseiLock(new TransmissionRequest.api_req_hensei.LockRequest(request), Svdata<TransmissionData.api_req_hensei.Lock>.fromDynamic(json, TransmissionData.api_req_hensei.Lock.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_hokyu/charge":
                    if (json.api_data())
                        OnGetReqhokyuCharge(new TransmissionRequest.api_req_hokyu.ChargeRequest(request), Svdata<TransmissionData.api_req_hokyu.Charge>.fromDynamic(json, TransmissionData.api_req_hokyu.Charge.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_kousyou/createitem":
                    if (json.api_data())
                        OnGetReqkousyouCreateitem(new TransmissionRequest.api_req_kousyou.CreateitemRequest(request), Svdata<TransmissionData.api_req_kousyou.Createitem>.fromDynamic(json, TransmissionData.api_req_kousyou.Createitem.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_kousyou/createship":
                    OnGetReqkousyouCreateship(new TransmissionRequest.api_req_kousyou.CreateshipRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_kousyou/createship_speedchange":
                    OnGetReqkousyouCreateshipSpeedchange(new TransmissionRequest.api_req_kousyou.CreateshipSpeedchangeRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_kousyou/destroyship":
                    OnGetReqkousyouDestroyship(new TransmissionRequest.api_req_kousyou.DestroyshipRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_kousyou/getship":
                    if (json.api_data())
                        OnGetReqkousyouGetship(new TransmissionRequest.api_req_kousyou.GetshipRequest(request), Svdata<TransmissionData.api_req_kousyou.Getship>.fromDynamic(json, TransmissionData.api_req_kousyou.Getship.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_member/get_incentive":
                    if (json.api_data())
                        OnGetReqmemberGetIncentive(new RequestBase(request), Svdata<TransmissionData.api_req_member.GetIncentive>.fromDynamic(json, TransmissionData.api_req_member.GetIncentive.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_member/updatecomment":
                    OnGetReqmemberUpdatecomment(new TransmissionRequest.api_req_member.UpdatecommentRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_member/updatedeckname":
                    OnGetReqmemberUpdatedeckname(new TransmissionRequest.api_req_member.UpdatedecknameRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_mission/result":
                    if (json.api_data())
                        OnGetReqmissionResult(new TransmissionRequest.api_req_mission.ResultRequest(request), Svdata<TransmissionData.api_req_mission.Result>.fromDynamic(json, TransmissionData.api_req_mission.Result.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_mission/return_instruction":
                    if (json.api_data())
                        OnGetReqmissionReturnInstruction(new TransmissionRequest.api_req_mission.ReturnInstructionRequest(request), Svdata<TransmissionData.api_req_mission.ReturnInstruction>.fromDynamic(json, TransmissionData.api_req_mission.ReturnInstruction.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_mission/start":
                    if (json.api_data())
                        OnGetReqmissionStart(new TransmissionRequest.api_req_mission.StartRequest(request), Svdata<TransmissionData.api_req_mission.Start>.fromDynamic(json, TransmissionData.api_req_mission.Start.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_nyukyo/speedchange":
                    OnGetReqnyukyoSpeedchange(new TransmissionRequest.api_req_nyukyo.SpeedchangeRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_nyukyo/start":
                    OnGetReqnyukyoStart(new TransmissionRequest.api_req_nyukyo.StartRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_quest/clearitemget":
                    if (json.api_data())
                        OnGetReqquestClearitemget(new TransmissionRequest.api_req_quest.ClearitemgetRequest(request), Svdata<TransmissionData.api_req_quest.Clearitemget>.fromDynamic(json, TransmissionData.api_req_quest.Clearitemget.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;
                case "api_req_quest/start":
                    OnGetReqquestStart(new TransmissionRequest.api_req_quest.StartRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_req_quest/stop":
                    OnGetReqquestStop(new TransmissionRequest.api_req_quest.StopRequest(request), Svdata<object>.fromDynamic(json, null));
                    break;
                case "api_start2":
                    if (json.api_data())
                        OnGetStart2(new RequestBase(request), Svdata<TransmissionData.api_start2.Start2>.fromDynamic(json, TransmissionData.api_start2.Start2.fromDynamic(json.api_data)));
                    else
                        throw new KanColleLibException(string.Format("No api_data: {0}", kcsapiurl));
                    break;


                default:
                    System.Diagnostics.Debug.WriteLine("REQUEST BODY: " + request);
                    System.Diagnostics.Debug.WriteLine("RESPONSE BODY: " + response);
                    OnUnknownKcsAPIDataReceived(new KcsAPIDataAnalyzeFailedEventArgs(kcsapiurl, request, response, null));
                    break;
                    // throw new NotImplementedException(kcsapiurl);
            }
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
        /// ゲームが開始された際に発生します。
        /// </summary>
        public event GameStartEventHandler GameStart;
        public delegate void GameStartEventHandler(object sender, GameStartEventArgs e);
        protected virtual void OnGameStart(GameStartEventArgs e) { if (GameStart != null) { GameStart(this, e); } }

        /// <summary>
        /// 艦これのAPIからデータを受信した際に発生します。
        /// </summary>
        public event GetKcsAPIDataEventHandler GetKcsAPIData;
        public delegate void GetKcsAPIDataEventHandler(object sender, GetKcsAPIDataEventArgs e);
        protected virtual void OnGetKcsAPIData(GetKcsAPIDataEventArgs e) { if (GetKcsAPIData != null) { GetKcsAPIData(this, e); } }

        /// <summary>
        /// 艦これのAPIデータの解析に失敗した際に発生します。
        /// </summary>
        public event KcsAPIDataAnalyzeFailedEventHandler KcsAPIDataAnalyzeFailed;
        public delegate void KcsAPIDataAnalyzeFailedEventHandler(object sender, KcsAPIDataAnalyzeFailedEventArgs e);
        protected virtual void OnKcsAPIDataAnalyzeFailed(KcsAPIDataAnalyzeFailedEventArgs e) { if (KcsAPIDataAnalyzeFailed != null) { KcsAPIDataAnalyzeFailed(this, e); } }

        /// <summary>
        /// 未知の艦これのAPIデータを受信した際に発生します。
        /// </summary>
        public event UnknownKcsAPIDataReceivedEventHandler UnknownKcsAPIDataReceived;
        public delegate void UnknownKcsAPIDataReceivedEventHandler(object sender, KcsAPIDataAnalyzeFailedEventArgs e);
        protected virtual void OnUnknownKcsAPIDataReceived(KcsAPIDataAnalyzeFailedEventArgs e) { if (UnknownKcsAPIDataReceived != null) { UnknownKcsAPIDataReceived(this, e); } }

        #endregion

        #region 受信URLごとのイベント定義（自動処理）

        /// <summary>
        /// api_get_member/basic を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberBasicEventHandler GetGetmemberBasic;
        public delegate void GetGetmemberBasicEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Basic> response);
        protected virtual void OnGetGetmemberBasic(RequestBase request, Svdata<TransmissionData.api_get_member.Basic> response) { if (GetGetmemberBasic != null) GetGetmemberBasic(this, request, response); }

        /// <summary>
        /// api_get_member/deck を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberDeckEventHandler GetGetmemberDeck;
        public delegate void GetGetmemberDeckEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Deck> response);
        protected virtual void OnGetGetmemberDeck(RequestBase request, Svdata<TransmissionData.api_get_member.Deck> response) { if (GetGetmemberDeck != null) GetGetmemberDeck(this, request, response); }

        /// <summary>
        /// api_get_member/furniture を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberFurnitureEventHandler GetGetmemberFurniture;
        public delegate void GetGetmemberFurnitureEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Furniture> response);
        protected virtual void OnGetGetmemberFurniture(RequestBase request, Svdata<TransmissionData.api_get_member.Furniture> response) { if (GetGetmemberFurniture != null) GetGetmemberFurniture(this, request, response); }

        /// <summary>
        /// api_get_member/kdock を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberKdockEventHandler GetGetmemberKdock;
        public delegate void GetGetmemberKdockEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.KDock> response);
        protected virtual void OnGetGetmemberKdock(RequestBase request, Svdata<TransmissionData.api_get_member.KDock> response) { if (GetGetmemberKdock != null) GetGetmemberKdock(this, request, response); }

        /// <summary>
        /// api_get_member/mapcell を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberMapcellEventHandler GetGetmemberMapcell;
        public delegate void GetGetmemberMapcellEventHandler(object sender, TransmissionRequest.api_get_member.MapcellRequest request, Svdata<TransmissionData.api_get_member.Mapcell> response);
        protected virtual void OnGetGetmemberMapcell(TransmissionRequest.api_get_member.MapcellRequest request, Svdata<TransmissionData.api_get_member.Mapcell> response) { if (GetGetmemberMapcell != null) GetGetmemberMapcell(this, request, response); }

        /// <summary>
        /// api_get_member/mapinfo を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberMapinfoEventHandler GetGetmemberMapinfo;
        public delegate void GetGetmemberMapinfoEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Mapinfo> response);
        protected virtual void OnGetGetmemberMapinfo(RequestBase request, Svdata<TransmissionData.api_get_member.Mapinfo> response) { if (GetGetmemberMapinfo != null) GetGetmemberMapinfo(this, request, response); }

        /// <summary>
        /// api_get_member/material を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberMaterialEventHandler GetGetmemberMaterial;
        public delegate void GetGetmemberMaterialEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Material> response);
        protected virtual void OnGetGetmemberMaterial(RequestBase request, Svdata<TransmissionData.api_get_member.Material> response) { if (GetGetmemberMaterial != null) GetGetmemberMaterial(this, request, response); }

        /// <summary>
        /// api_get_member/mission を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberMissionEventHandler GetGetmemberMission;
        public delegate void GetGetmemberMissionEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Mission> response);
        protected virtual void OnGetGetmemberMission(RequestBase request, Svdata<TransmissionData.api_get_member.Mission> response) { if (GetGetmemberMission != null) GetGetmemberMission(this, request, response); }

        /// <summary>
        /// api_get_member/ndock を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberNdockEventHandler GetGetmemberNdock;
        public delegate void GetGetmemberNdockEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.NDock> response);
        protected virtual void OnGetGetmemberNdock(RequestBase request, Svdata<TransmissionData.api_get_member.NDock> response) { if (GetGetmemberNdock != null) GetGetmemberNdock(this, request, response); }

        /// <summary>
        /// api_get_member/questlist を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberQuestlistEventHandler GetGetmemberQuestlist;
        public delegate void GetGetmemberQuestlistEventHandler(object sender, TransmissionRequest.api_get_member.QuestlistRequest request, Svdata<TransmissionData.api_get_member.Questlist> response);
        protected virtual void OnGetGetmemberQuestlist(TransmissionRequest.api_get_member.QuestlistRequest request, Svdata<TransmissionData.api_get_member.Questlist> response) { if (GetGetmemberQuestlist != null) GetGetmemberQuestlist(this, request, response); }

        /// <summary>
        /// api_get_member/ship2 を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberShip2EventHandler GetGetmemberShip2;
        public delegate void GetGetmemberShip2EventHandler(object sender, TransmissionRequest.api_get_member.Ship2Request request, Svdata<TransmissionData.api_get_member.Ship2> response);
        protected virtual void OnGetGetmemberShip2(TransmissionRequest.api_get_member.Ship2Request request, Svdata<TransmissionData.api_get_member.Ship2> response) { if (GetGetmemberShip2 != null) GetGetmemberShip2(this, request, response); }

        /// <summary>
        /// api_get_member/ship3 を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberShip3EventHandler GetGetmemberShip3;
        public delegate void GetGetmemberShip3EventHandler(object sender, TransmissionRequest.api_get_member.Ship3Request request, Svdata<TransmissionData.api_get_member.Ship3> response);
        protected virtual void OnGetGetmemberShip3(TransmissionRequest.api_get_member.Ship3Request request, Svdata<TransmissionData.api_get_member.Ship3> response) { if (GetGetmemberShip3 != null) GetGetmemberShip3(this, request, response); }

        /// <summary>
        /// api_get_member/ship_deck を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberShipDeckEventHandler GetGetmemberShipDeck;
        public delegate void GetGetmemberShipDeckEventHandler(object sender, TransmissionRequest.api_get_member.ShipDeckRequest request, Svdata<TransmissionData.api_get_member.ShipDeck> response);
        protected virtual void OnGetGetmemberShipDeck(TransmissionRequest.api_get_member.ShipDeckRequest request, Svdata<TransmissionData.api_get_member.ShipDeck> response) { if (GetGetmemberShipDeck != null) GetGetmemberShipDeck(this, request, response); }

        /// <summary>
        /// api_get_member/slot_item を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberSlotItemEventHandler GetGetmemberSlotItem;
        public delegate void GetGetmemberSlotItemEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.SlotItem> response);
        protected virtual void OnGetGetmemberSlotItem(RequestBase request, Svdata<TransmissionData.api_get_member.SlotItem> response) { if (GetGetmemberSlotItem != null) GetGetmemberSlotItem(this, request, response); }

        /// <summary>
        /// api_get_member/unsetslot を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetGetmemberUnsetslotEventHandler GetGetmemberUnsetslot;
        public delegate void GetGetmemberUnsetslotEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_get_member.Unsetslot> response);
        protected virtual void OnGetGetmemberUnsetslot(RequestBase request, Svdata<TransmissionData.api_get_member.Unsetslot> response) { if (GetGetmemberUnsetslot != null) GetGetmemberUnsetslot(this, request, response); }

        /// <summary>
        /// api_port/port を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetPortPortEventHandler GetPortPort;
        public delegate void GetPortPortEventHandler(object sender, TransmissionRequest.api_port.PortRequest request, Svdata<TransmissionData.api_port.Port> response);
        protected virtual void OnGetPortPort(TransmissionRequest.api_port.PortRequest request, Svdata<TransmissionData.api_port.Port> response) { if (GetPortPort != null) GetPortPort(this, request, response); }

        /// <summary>
        /// api_req_hensei/change を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqhenseiChangeEventHandler GetReqhenseiChange;
        public delegate void GetReqhenseiChangeEventHandler(object sender, TransmissionRequest.api_req_hensei.ChangeRequest request, Svdata<object> response);
        protected virtual void OnGetReqhenseiChange(TransmissionRequest.api_req_hensei.ChangeRequest request, Svdata<object> response) { if (GetReqhenseiChange != null) GetReqhenseiChange(this, request, response); }

        /// <summary>
        /// api_req_hensei/combined を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqhenseiCombinedEventHandler GetReqhenseiCombined;
        public delegate void GetReqhenseiCombinedEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_req_hensei.Combined> response);
        protected virtual void OnGetReqhenseiCombined(RequestBase request, Svdata<TransmissionData.api_req_hensei.Combined> response) { if (GetReqhenseiCombined != null) GetReqhenseiCombined(this, request, response); }

        /// <summary>
        /// api_req_hensei/lock を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqhenseiLockEventHandler GetReqhenseiLock;
        public delegate void GetReqhenseiLockEventHandler(object sender, TransmissionRequest.api_req_hensei.LockRequest request, Svdata<TransmissionData.api_req_hensei.Lock> response);
        protected virtual void OnGetReqhenseiLock(TransmissionRequest.api_req_hensei.LockRequest request, Svdata<TransmissionData.api_req_hensei.Lock> response) { if (GetReqhenseiLock != null) GetReqhenseiLock(this, request, response); }

        /// <summary>
        /// api_req_hokyu/charge を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqhokyuChargeEventHandler GetReqhokyuCharge;
        public delegate void GetReqhokyuChargeEventHandler(object sender, TransmissionRequest.api_req_hokyu.ChargeRequest request, Svdata<TransmissionData.api_req_hokyu.Charge> response);
        protected virtual void OnGetReqhokyuCharge(TransmissionRequest.api_req_hokyu.ChargeRequest request, Svdata<TransmissionData.api_req_hokyu.Charge> response) { if (GetReqhokyuCharge != null) GetReqhokyuCharge(this, request, response); }

        /// <summary>
        /// api_req_kousyou/createitem を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqkousyouCreateitemEventHandler GetReqkousyouCreateitem;
        public delegate void GetReqkousyouCreateitemEventHandler(object sender, TransmissionRequest.api_req_kousyou.CreateitemRequest request, Svdata<TransmissionData.api_req_kousyou.Createitem> response);
        protected virtual void OnGetReqkousyouCreateitem(TransmissionRequest.api_req_kousyou.CreateitemRequest request, Svdata<TransmissionData.api_req_kousyou.Createitem> response) { if (GetReqkousyouCreateitem != null) GetReqkousyouCreateitem(this, request, response); }

        /// <summary>
        /// api_req_kousyou/createship を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqkousyouCreateshipEventHandler GetReqkousyouCreateship;
        public delegate void GetReqkousyouCreateshipEventHandler(object sender, TransmissionRequest.api_req_kousyou.CreateshipRequest request, Svdata<object> response);
        protected virtual void OnGetReqkousyouCreateship(TransmissionRequest.api_req_kousyou.CreateshipRequest request, Svdata<object> response) { if (GetReqkousyouCreateship != null) GetReqkousyouCreateship(this, request, response); }

        /// <summary>
        /// api_req_kousyou/createship_speedchange を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqkousyouCreateshipSpeedchangeEventHandler GetReqkousyouCreateshipSpeedchange;
        public delegate void GetReqkousyouCreateshipSpeedchangeEventHandler(object sender, TransmissionRequest.api_req_kousyou.CreateshipSpeedchangeRequest request, Svdata<object> response);
        protected virtual void OnGetReqkousyouCreateshipSpeedchange(TransmissionRequest.api_req_kousyou.CreateshipSpeedchangeRequest request, Svdata<object> response) { if (GetReqkousyouCreateshipSpeedchange != null) GetReqkousyouCreateshipSpeedchange(this, request, response); }

        /// <summary>
        /// api_req_kousyou/destroyship を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqkousyouDestroyshipEventHandler GetReqkousyouDestroyship;
        public delegate void GetReqkousyouDestroyshipEventHandler(object sender, TransmissionRequest.api_req_kousyou.DestroyshipRequest request, Svdata<object> response);
        protected virtual void OnGetReqkousyouDestroyship(TransmissionRequest.api_req_kousyou.DestroyshipRequest request, Svdata<object> response) { if (GetReqkousyouDestroyship != null) GetReqkousyouDestroyship(this, request, response); }

        /// <summary>
        /// api_req_kousyou/getship を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqkousyouGetshipEventHandler GetReqkousyouGetship;
        public delegate void GetReqkousyouGetshipEventHandler(object sender, TransmissionRequest.api_req_kousyou.GetshipRequest request, Svdata<TransmissionData.api_req_kousyou.Getship> response);
        protected virtual void OnGetReqkousyouGetship(TransmissionRequest.api_req_kousyou.GetshipRequest request, Svdata<TransmissionData.api_req_kousyou.Getship> response) { if (GetReqkousyouGetship != null) GetReqkousyouGetship(this, request, response); }

        /// <summary>
        /// api_req_member/get_incentive を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqmemberGetIncentiveEventHandler GetReqmemberGetIncentive;
        public delegate void GetReqmemberGetIncentiveEventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_req_member.GetIncentive> response);
        protected virtual void OnGetReqmemberGetIncentive(RequestBase request, Svdata<TransmissionData.api_req_member.GetIncentive> response) { if (GetReqmemberGetIncentive != null) GetReqmemberGetIncentive(this, request, response); }

        /// <summary>
        /// api_req_member/updatecomment を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqmemberUpdatecommentEventHandler GetReqmemberUpdatecomment;
        public delegate void GetReqmemberUpdatecommentEventHandler(object sender, TransmissionRequest.api_req_member.UpdatecommentRequest request, Svdata<object> response);
        protected virtual void OnGetReqmemberUpdatecomment(TransmissionRequest.api_req_member.UpdatecommentRequest request, Svdata<object> response) { if (GetReqmemberUpdatecomment != null) GetReqmemberUpdatecomment(this, request, response); }

        /// <summary>
        /// api_req_member/updatedeckname を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqmemberUpdatedecknameEventHandler GetReqmemberUpdatedeckname;
        public delegate void GetReqmemberUpdatedecknameEventHandler(object sender, TransmissionRequest.api_req_member.UpdatedecknameRequest request, Svdata<object> response);
        protected virtual void OnGetReqmemberUpdatedeckname(TransmissionRequest.api_req_member.UpdatedecknameRequest request, Svdata<object> response) { if (GetReqmemberUpdatedeckname != null) GetReqmemberUpdatedeckname(this, request, response); }

        /// <summary>
        /// api_req_mission/result を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqmissionResultEventHandler GetReqmissionResult;
        public delegate void GetReqmissionResultEventHandler(object sender, TransmissionRequest.api_req_mission.ResultRequest request, Svdata<TransmissionData.api_req_mission.Result> response);
        protected virtual void OnGetReqmissionResult(TransmissionRequest.api_req_mission.ResultRequest request, Svdata<TransmissionData.api_req_mission.Result> response) { if (GetReqmissionResult != null) GetReqmissionResult(this, request, response); }

        /// <summary>
        /// api_req_mission/return_instruction を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqmissionReturnInstructionEventHandler GetReqmissionReturnInstruction;
        public delegate void GetReqmissionReturnInstructionEventHandler(object sender, TransmissionRequest.api_req_mission.ReturnInstructionRequest request, Svdata<TransmissionData.api_req_mission.ReturnInstruction> response);
        protected virtual void OnGetReqmissionReturnInstruction(TransmissionRequest.api_req_mission.ReturnInstructionRequest request, Svdata<TransmissionData.api_req_mission.ReturnInstruction> response) { if (GetReqmissionReturnInstruction != null) GetReqmissionReturnInstruction(this, request, response); }

        /// <summary>
        /// api_req_mission/start を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqmissionStartEventHandler GetReqmissionStart;
        public delegate void GetReqmissionStartEventHandler(object sender, TransmissionRequest.api_req_mission.StartRequest request, Svdata<TransmissionData.api_req_mission.Start> response);
        protected virtual void OnGetReqmissionStart(TransmissionRequest.api_req_mission.StartRequest request, Svdata<TransmissionData.api_req_mission.Start> response) { if (GetReqmissionStart != null) GetReqmissionStart(this, request, response); }

        /// <summary>
        /// api_req_nyukyo/speedchange を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqnyukyoSpeedchangeEventHandler GetReqnyukyoSpeedchange;
        public delegate void GetReqnyukyoSpeedchangeEventHandler(object sender, TransmissionRequest.api_req_nyukyo.SpeedchangeRequest request, Svdata<object> response);
        protected virtual void OnGetReqnyukyoSpeedchange(TransmissionRequest.api_req_nyukyo.SpeedchangeRequest request, Svdata<object> response) { if (GetReqnyukyoSpeedchange != null) GetReqnyukyoSpeedchange(this, request, response); }

        /// <summary>
        /// api_req_nyukyo/start を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqnyukyoStartEventHandler GetReqnyukyoStart;
        public delegate void GetReqnyukyoStartEventHandler(object sender, TransmissionRequest.api_req_nyukyo.StartRequest request, Svdata<object> response);
        protected virtual void OnGetReqnyukyoStart(TransmissionRequest.api_req_nyukyo.StartRequest request, Svdata<object> response) { if (GetReqnyukyoStart != null) GetReqnyukyoStart(this, request, response); }

        /// <summary>
        /// api_req_quest/clearitemget を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqquestClearitemgetEventHandler GetReqquestClearitemget;
        public delegate void GetReqquestClearitemgetEventHandler(object sender, TransmissionRequest.api_req_quest.ClearitemgetRequest request, Svdata<TransmissionData.api_req_quest.Clearitemget> response);
        protected virtual void OnGetReqquestClearitemget(TransmissionRequest.api_req_quest.ClearitemgetRequest request, Svdata<TransmissionData.api_req_quest.Clearitemget> response) { if (GetReqquestClearitemget != null) GetReqquestClearitemget(this, request, response); }

        /// <summary>
        /// api_req_quest/start を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqquestStartEventHandler GetReqquestStart;
        public delegate void GetReqquestStartEventHandler(object sender, TransmissionRequest.api_req_quest.StartRequest request, Svdata<object> response);
        protected virtual void OnGetReqquestStart(TransmissionRequest.api_req_quest.StartRequest request, Svdata<object> response) { if (GetReqquestStart != null) GetReqquestStart(this, request, response); }

        /// <summary>
        /// api_req_quest/stop を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetReqquestStopEventHandler GetReqquestStop;
        public delegate void GetReqquestStopEventHandler(object sender, TransmissionRequest.api_req_quest.StopRequest request, Svdata<object> response);
        protected virtual void OnGetReqquestStop(TransmissionRequest.api_req_quest.StopRequest request, Svdata<object> response) { if (GetReqquestStop != null) GetReqquestStop(this, request, response); }

        /// <summary>
        /// api_start2 を受信して解析に成功した際に呼び出されます
        /// </summary>
        public event GetStart2EventHandler GetStart2;
        public delegate void GetStart2EventHandler(object sender, RequestBase request, Svdata<TransmissionData.api_start2.Start2> response);
        protected virtual void OnGetStart2(RequestBase request, Svdata<TransmissionData.api_start2.Start2> response) { if (GetStart2 != null) GetStart2(this, request, response); }


        #endregion

    }
}
