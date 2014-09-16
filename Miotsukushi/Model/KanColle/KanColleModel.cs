using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;
using Fiddler;
using Miotsukushi.Tools;

namespace Miotsukushi.Model.KanColle
{
    class KanColleModel
    {
        KanColleNotifier kclib;
        public Dictionary<int, CharacterData> charamaster = new Dictionary<int,CharacterData>();
        public Dictionary<int, ShiptypeData> shiptypemaster = new Dictionary<int,ShiptypeData>();
        public Dictionary<int, MissionData> missionmaster = new Dictionary<int,MissionData>();
        public ObservableCollection<ShipData> shipdata = new ObservableCollection<ShipData>();
        public ExList<FleetData> fleetdata = new ExList<FleetData>();
        public ExList<KDockData> kdockdata = new ExList<KDockData>();
        public ExList<NDockData> ndockdata = new ExList<NDockData>();
        public BasicData basicdata = new BasicData();

        public KanColleModel()
        {
            FiddlerApplication.Startup(0, FiddlerCoreStartupFlags.ChainToUpstreamGateway);
            URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", FiddlerApplication.oProxy.ListenPort), "<local>");

            kclib = new KanColleNotifier(true);

            kclib.GameStart += (_, __) => OnGameStart(new EventArgs());

            kclib.GetStart2 += kclib_GetStart2;
            kclib.GetPortPort += kclib_GetPortPort;
            kclib.GetGetmemberBasic += kclib_GetGetmemberBasic;
            kclib.GetGetmemberShip2 += kclib_GetGetmemberShip2;
            kclib.GetGetmemberShip3 += kclib_GetGetmemberShip3;
            kclib.GetGetmemberDeck += kclib_GetGetmemberDeck;
            kclib.GetReqmissionStart += kclib_GetReqmissionStart;
            kclib.GetReqmissionReturnInstruction += kclib_GetReqmissionReturnInstruction;
            kclib.GetGetmemberKdock += kclib_GetGetmemberKdock;
            kclib.GetReqkousyouGetship += kclib_GetReqkousyouGetship;
            kclib.GetGetmemberNdock += kclib_GetGetmemberNdock;
            kclib.GetReqkousyouDestroyship += kclib_GetReqkousyouDestroyship;
            kclib.GetGetmemberMaterial += kclib_GetGetmemberMaterial;
            kclib.GetReqhenseiChange += kclib_GetReqhenseiChange;
        }

        void kclib_GetReqhenseiChange(object sender, KanColleLib.TransmissionRequest.api_req_hensei.ChangeRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if (request.id - 1 < fleetdata.Count)
            {
                if (request.ship_id == -2)
                {
                    // 旗艦以外全部解除
                    int removecount = fleetdata[request.id - 1].ships.Count - 1;
                    for (int i = 0; i < removecount; i++)
                        fleetdata[request.id - 1].ships.RemoveAt(1);
                }
                else if (request.ship_id == -1)
                {
                    // その艦をはずす
                    fleetdata[request.id - 1].ships.RemoveAt(request.ship_idx);
                }
                else
                {
                    int replaceindex = fleetdata[request.id - 1].ships.IndexOf(request.ship_id);

                    if (replaceindex != -1)
                    {
                        // 艦隊の中に指定艦がいる場合
                        if (fleetdata[request.id - 1].ships.Count > request.ship_idx)
                        {
                            // 他の艦との交代である場合
                            int replaceshipid = fleetdata[request.id - 1].ships[request.ship_idx];
                            fleetdata[request.id - 1].ships[request.ship_idx] = request.ship_id;
                            fleetdata[request.id - 1].ships[replaceindex] = replaceshipid;
                        }
                        else
                        {
                            // なにもないところに宣言した場合
                            fleetdata[request.id - 1].ships.RemoveAt(request.ship_idx);
                            fleetdata[request.id - 1].ships.Add(request.ship_id);
                        }
                    }
                    else
                    {
                        if (fleetdata[request.id - 1].ships.Count > request.ship_idx)
                        {
                            // 他の艦との交代である場合
                            int replaceshipid = fleetdata[request.id - 1].ships[request.ship_idx];
                            fleetdata[request.id - 1].ships[request.ship_idx] = request.ship_id;

                            // 他の艦隊に同艦がいないかどうか確かめる
                            for (int i = 0; i < fleetdata.Count; i++)
                            {
                                if (i == request.id - 1)
                                    continue;
                                for (int j = 0; j < fleetdata[i].ships.Count; j++)
                                {
                                    if (fleetdata[i].ships[j] == request.ship_id)
                                    {
                                        // もしあったら
                                        fleetdata[i].ships[j] = replaceshipid;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // なにもないところに宣言した場合
                            fleetdata[request.id - 1].ships.Add(request.ship_id);
                        }
                    }
                }
            }
        }

        void kclib_GetGetmemberMaterial(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Material> response)
        {
            basicdata.FromMaterial(response.data);
        }

        void kclib_GetGetmemberBasic(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Basic> response)
        {
            basicdata.admiral_name = response.data.nickname;
            basicdata.admiral_comment = response.data.comment;
            basicdata.AppendRank(response.data.rank);
            basicdata.admiral_level = response.data.level;
            basicdata.admiral_exp = response.data.experience;
            basicdata.furniture_coin = response.data.fcoin;
            basicdata.max_ship = response.data.max_chara;
            basicdata.max_equipment = response.data.max_slotitem;
        }

        void kclib_GetReqkousyouDestroyship(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.DestroyshipRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            DestroyShip(request.ship_id);
        }

        void kclib_GetGetmemberNdock(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.NDock> response)
        {
            AppendNDockValue(response.data);
        }

        void kclib_GetReqkousyouGetship(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.GetshipRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Getship> response)
        {
            AppendKDockValue(response.data.kdock);
            AppendShipData(response.data.ship);
        }

        void kclib_GetGetmemberKdock(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.KDock> response)
        {
            AppendKDockValue(response.data);
        }

        void kclib_GetReqmissionReturnInstruction(object sender, KanColleLib.TransmissionRequest.api_req_mission.ReturnInstructionRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_mission.ReturnInstruction> response)
        {
            if (fleetdata.Count > request.deck_id - 1)
            {
                if (response.data.mission.Length >= 3)
                    fleetdata[request.deck_id - 1].ChangeMissionStatus((int)response.data.mission[0], (int)response.data.mission[1], response.data.mission[2]);
                else
                    fleetdata[request.deck_id - 1].ChangeMissionStatus(-1, -1, -1);
            }
        }

        void kclib_GetReqmissionStart(object sender, KanColleLib.TransmissionRequest.api_req_mission.StartRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_mission.Start> response)
        {
            if (fleetdata.Count > request.deck_id - 1)
                fleetdata[request.deck_id - 1].ChangeMissionStatus(1, request.mission_id, response.data.complatetime); // 遠征中の1は固定
        }

        void kclib_GetGetmemberDeck(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Deck> response)
        {
            AppendDeckValue(response.data);
        }

        async void kclib_GetGetmemberShip3(object sender, KanColleLib.TransmissionRequest.api_get_member.Ship3Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Ship3> response)
        {
            await AppendShipDataFromList(response.data.ship_data.ships);
        }

        async void kclib_GetGetmemberShip2(object sender, KanColleLib.TransmissionRequest.api_get_member.Ship2Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Ship2> response)
        {
            basicdata.now_ship_number = response.data.ships.Count;
            await AppendShipDataFromList(response.data.ships);
        }

        async void kclib_GetPortPort(object sender, KanColleLib.TransmissionRequest.api_port.PortRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_port.Port> response)
        {
            basicdata.now_ship_number = response.data.ship.ships.Count;
            basicdata.FromMaterial(response.data.material);
            await AppendShipDataFromList(response.data.ship.ships);
            await DeleteShipDataFromList(response.data.ship.ships);
            AppendDeckValue(response.data.deck_port);
            AppendNDockValue(response.data.ndock);
            InitializeConfirm();
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
        async Task AppendShipDataFromList(List<KanColleLib.TransmissionData.api_get_member.values.ShipValue> shiplist)
        {
            await Task.Run(() =>
            {
                foreach (var ship in shiplist)
                {
                    AppendShipData(ship);
                }
            });
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
        /// 艦娘一覧データから、ローカルデータに存在するが与えられたデータに存在しない艦をすべて削除する
        /// 与えられたリストの艦を削除するわけではない
        /// </summary>
        /// <param name="existshiplist"></param>
        async Task DeleteShipDataFromList(List<KanColleLib.TransmissionData.api_get_member.values.ShipValue> existshiplist)
        {
            await Task.Run(() =>
            {
                var deletelist = (from _ in shipdata where !existshiplist.Any(__ => __.id == _.shipid) select _).ToList();
                foreach (var item in deletelist)
                {
                    shipdata.Remove(item);
                }
            });
        }

        /// <summary>
        /// 指定されたIDの艦をリストから削除します。（同時に装備も削除したいけどとりあえず保留）
        /// </summary>
        /// <param name="ship"></param>
        void DestroyShip(int id)
        {
            var deletelist = (from _ in shipdata where _.shipid == id select _).ToList();
            foreach (var item in deletelist)
            {
                shipdata.Remove(item);
            }
        }

        int initializecount = 0;
        int initializecountflag = 4;

        void InitializeConfirm()
        {
            ++initializecount;
            if (initializecountflag == initializecount)
                OnInitializeComplete(new EventArgs());
        }

        void AppendNDockValue(KanColleLib.TransmissionData.api_get_member.NDock data)
        {
            for (int i = 0; i < data.ndocks.Count; i++)
            {
                if (ndockdata.Count <= i)
                    ndockdata.Add(new NDockData());
                ndockdata[i].FromNDockValue(data.ndocks[i]);
            }
        }

        void AppendKDockValue(KanColleLib.TransmissionData.api_get_member.KDock data)
        {
            for (int i = 0; i < data.kdocks.Count; i++)
            {
                if (kdockdata.Count <= i)
                    kdockdata.Add(new KDockData());
                kdockdata[i].FromKDockValue(data.kdocks[i]);
            }
        }

        void AppendDeckValue(KanColleLib.TransmissionData.api_get_member.Deck data)
        {
            if (data.decks.Count >= 1)
            {
                while (fleetdata.Count < data.decks.Count)
                    fleetdata.Add(new FleetData());
                while (fleetdata.Count > data.decks.Count)
                    fleetdata.RemoveAt(fleetdata.Count - 1);
                for (int i = 0; i < fleetdata.Count; i++)
                    fleetdata[i].FromDeckValue(data.decks[i]);
            }
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
