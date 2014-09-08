using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;
using Fiddler;

namespace Miotsukushi.Model.KanColle
{
    class KanColleModel
    {
        KanColleNotifier kclib;
        public Dictionary<int, CharacterData> charamaster = new Dictionary<int,CharacterData>();
        public Dictionary<int, ShiptypeData> shiptypemaster = new Dictionary<int,ShiptypeData>();
        public Dictionary<int, MissionData> missionmaster = new Dictionary<int,MissionData>();
        public ObservableCollection<ShipData> shipdata = new ObservableCollection<ShipData>();
        public List<FleetData> fleetdata = new List<FleetData>();
        public List<KDockData> kdockdata = new List<KDockData>();

        public KanColleModel()
        {
            FiddlerApplication.Startup(0, FiddlerCoreStartupFlags.ChainToUpstreamGateway);
            URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", FiddlerApplication.oProxy.ListenPort), "<local>");

            kclib = new KanColleNotifier();

            kclib.GameStart += (_, __) => OnGameStart(new EventArgs());

            kclib.GetStart2 += kclib_GetStart2;
            kclib.GetPortPort += kclib_GetPortPort;
            kclib.GetGetmemberShip2 += kclib_GetGetmemberShip2;
            kclib.GetGetmemberShip3 += kclib_GetGetmemberShip3;
            kclib.GetGetmemberDeck += kclib_GetGetmemberDeck;
            kclib.GetReqmissionStart += kclib_GetReqmissionStart;
            kclib.GetReqmissionReturnInstruction += kclib_GetReqmissionReturnInstruction;
            kclib.GetGetmemberKdock += kclib_GetGetmemberKdock;
            kclib.GetReqkousyouGetship += kclib_GetReqkousyouGetship;
        }

        async void kclib_GetReqkousyouGetship(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.GetshipRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Getship> response)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < response.data.kdock.kdocks.Count; i++)
                {
                    if (kdockdata.Count <= i)
                        kdockdata.Add(new KDockData());
                    kdockdata[i].FromKDockValue(response.data.kdock.kdocks[i]);
                }
                AppendShipData(response.data.ship);
            });
        }

        async void kclib_GetGetmemberKdock(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.KDock> response)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < response.data.kdocks.Count; i++)
                {
                    if (kdockdata.Count <= i)
                        kdockdata.Add(new KDockData());
                    kdockdata[i].FromKDockValue(response.data.kdocks[i]);
                }
            });
        }

        async void kclib_GetReqmissionReturnInstruction(object sender, KanColleLib.TransmissionRequest.api_req_mission.ReturnInstructionRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_mission.ReturnInstruction> response)
        {
            await Task.Run(() =>
            {
                if (fleetdata.Count > request.deck_id - 1)
                {
                    if(response.data.mission.Length >= 3)
                        fleetdata[request.deck_id - 1].ChangeMissionStatus((int)response.data.mission[0], (int)response.data.mission[1], response.data.mission[2]);
                    else
                        fleetdata[request.deck_id - 1].ChangeMissionStatus(-1, -1, -1);
                }
            });
        }

        async void kclib_GetReqmissionStart(object sender, KanColleLib.TransmissionRequest.api_req_mission.StartRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_mission.Start> response)
        {
            await Task.Run(() =>
            {
                if(fleetdata.Count > request.deck_id - 1)
                    fleetdata[request.deck_id - 1].ChangeMissionStatus(1, request.mission_id, response.data.complatetime); // 遠征中の1は固定
            });
        }

        async void kclib_GetGetmemberDeck(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Deck> response)
        {
            await Task.Run(() =>
            {
                if (response.data.decks.Count >= 1)
                {
                    while (fleetdata.Count < response.data.decks.Count)
                        fleetdata.Add(new FleetData());
                    while (fleetdata.Count > response.data.decks.Count)
                        fleetdata.RemoveAt(fleetdata.Count - 1);
                    for (int i = 0; i < fleetdata.Count; i++)
                        fleetdata[i].FromDeckValue(response.data.decks[i]);
                }
            });
        }

        async void kclib_GetGetmemberShip3(object sender, KanColleLib.TransmissionRequest.api_get_member.Ship3Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Ship3> response)
        {
            await Task.Run(() => AppendShipDataFromList(response.data.ship_data.ships));
        }

        async void kclib_GetGetmemberShip2(object sender, KanColleLib.TransmissionRequest.api_get_member.Ship2Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Ship2> response)
        {
            await Task.Run(() => AppendShipDataFromList(response.data.ships));
        }

        async void kclib_GetPortPort(object sender, KanColleLib.TransmissionRequest.api_port.PortRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_port.Port> response)
        {
            await Task.Run(() =>
            {
                AppendShipDataFromList(response.data.ship.ships);
                DeleteShipDataFromList(response.data.ship.ships);
                InitializeConfirm();
            });
            await Task.Run(() =>
            {
                if (response.data.deck_port.decks.Count >= 1)
                {
                    while (fleetdata.Count < response.data.deck_port.decks.Count)
                        fleetdata.Add(new FleetData());
                    while (fleetdata.Count > response.data.deck_port.decks.Count)
                        fleetdata.RemoveAt(fleetdata.Count - 1);
                    for (int i = 0; i < fleetdata.Count; i++)
                        fleetdata[i].FromDeckValue(response.data.deck_port.decks[i]);
                }
                InitializeConfirm();
            });
        }

        async void kclib_GetStart2(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_start2.Start2> response)
        {
            await Task.Run(() =>
            {
                charamaster = new Dictionary<int, CharacterData>();
                foreach (var chara in response.data.mst_ship)
                {
                    charamaster.Add(chara.id, CharacterData.fromKanColleLib(chara));
                }
                System.Diagnostics.Debug.WriteLine("Get: api_start2/mst_ship");
                InitializeConfirm();
            });

            await Task.Run(() =>
            {
                shiptypemaster = new Dictionary<int, ShiptypeData>();
                foreach (var stype in response.data.mst_stype)
                {
                    shiptypemaster.Add(stype.id, new ShiptypeData() { name = stype.name });
                }
                System.Diagnostics.Debug.WriteLine("Get: api_start2/mst_stype");
                InitializeConfirm();
            });

            await Task.Run(() =>
            {
                missionmaster = new Dictionary<int, MissionData>();
                foreach (var mission in response.data.mst_mission)
                {
                    missionmaster.Add(mission.id, new MissionData()
                    {
                        name = mission.name,
                        time_minute = mission.time
                    });
                }
                System.Diagnostics.Debug.WriteLine("Get: api_start2/mst_mission");
                InitializeConfirm();
            });
        }

        /// <summary>
        /// 艦娘一覧データを統合する。見つからなかった艦を削除することはしない
        /// </summary>
        /// <param name="shiplist"></param>
        /// <returns></returns>
        void AppendShipDataFromList(List<KanColleLib.TransmissionData.api_get_member.values.ShipValue> shiplist)
        {
            foreach (var ship in shiplist)
            {
                AppendShipData(ship);
            }
        }

        void AppendShipData(KanColleLib.TransmissionData.api_get_member.values.ShipValue ship)
        {
            var temp = ShipData.FromKanColleLib(ship);

            bool is_found = false;
            for (int i = 0; i < shipdata.Count; i++)
            {
                if (shipdata[i].shipid == ship.id)
                {
                    is_found = true;
                    if (!shipdata[i].Equals(temp))
                        shipdata[i] = temp;
                }
            }
            if (!is_found)
                shipdata.Add(temp);
        }

        /// <summary>
        /// 艦娘一覧データからそこに存在しない艦を削除する
        /// </summary>
        /// <param name="shiplist"></param>
        void DeleteShipDataFromList(List<KanColleLib.TransmissionData.api_get_member.values.ShipValue> shiplist)
        {
            for (int i = 0; i < shipdata.Count; i++)
            {
                var query = from _ in shiplist where _.id == shipdata[i].shipid select true;

                if (query.Count() <= 0)
                    shipdata.RemoveAt(i);
            }
        }

        int initializecount = 0;
        int initializecountflag = 5;

        void InitializeConfirm()
        {
            ++initializecount;
            if (initializecountflag == initializecount)
                OnInitializeComplete(new EventArgs());
        }

        /// <summary>
        /// 初期データをすべて取得した際に呼び出されます
        /// </summary>
        public event EventHandler InitializeComplete;
        protected virtual void OnInitializeComplete(EventArgs e) { if (InitializeComplete != null) { InitializeComplete(this, e); } }

        /// <summary>
        /// ゲームが開始された（フラッシュがロードされた）際に呼び出されます
        /// </summary>
        public event EventHandler GameStart;
        protected virtual void OnGameStart(EventArgs e) { if (GameStart != null) { GameStart(this, e); } }
    }
}
