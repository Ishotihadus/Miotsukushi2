using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;
using Miotsukushi.Tools;
using Miotsukushi.Model.KanColle.EventArgs;

namespace Miotsukushi.Model.KanColle
{
    class KanColleModel
    {
        KanColleNotifier kclib;
        public ServerInfo serverinfo = new ServerInfo();
        public Dictionary<int, CharacterData> charamaster = new Dictionary<int,CharacterData>();
        public Dictionary<int, ShiptypeData> shiptypemaster = new Dictionary<int,ShiptypeData>();
        public Dictionary<int, MissionData> missionmaster = new Dictionary<int,MissionData>();
        public Dictionary<int, ItemData> slotitemmaster = new Dictionary<int, ItemData>();
        public Dictionary<int, ItemEquipTypeData> slotitem_equiptypemaster = new Dictionary<int, ItemEquipTypeData>();
        public Dictionary<int, MapAreaData> mapareamaster = new Dictionary<int, MapAreaData>();
        public Dictionary<int, MapInfoData> mapinfomaster = new Dictionary<int, MapInfoData>();
        public ObservableCollection<SlotData> slotdata = new ObservableCollection<SlotData>();
        public ObservableCollection<ShipData> shipdata = new ObservableCollection<ShipData>();
        public ExList<FleetData> fleetdata = new ExList<FleetData>();
        public ExList<KDockData> kdockdata = new ExList<KDockData>();
        public ExList<NDockData> ndockdata = new ExList<NDockData>();
        public BasicData basicdata = new BasicData();
        public QuestData questdata = new QuestData();

        public BattleModels.BattleModel battlemodel;
        public BattleModels.SortieModel sortiemodel;
        public DebuggerModel debuggermodel;
        public bool initializeCompleted = false;

        public KanColleModel()
        {
            int port = KanColleNotifier.FiddlerStartup();
            System.Diagnostics.Debug.WriteLine("Port:" + port);
            KanColleNotifier.FiddlerSetWinInetProxy();

            kclib = new KanColleNotifier(true);
            new PacketSaver(kclib);
            battlemodel = new BattleModels.BattleModel(this, kclib);
            sortiemodel = new BattleModels.SortieModel(this, kclib);
            debuggermodel = new DebuggerModel(kclib);

            kclib.GameStart += Kclib_GameStart;
            kclib.KcsAPIDataAnalyzeFailed += (_, e) => OnAPIAnalyzeError(new APIAnalyzeErrorEventArgs(e.kcsapiurl, e.request, e.response));
            kclib.UnknownKcsAPIDataReceived += (_, e) => OnUnknownAPIReceived(new APIAnalyzeErrorEventArgs(e.kcsapiurl, e.request, e.response));
            kclib.GetFiddlerLogString += (_, e) => OnGetFiddlerLog(new StringEventArgs(e.logtext));

            kclib.GetGetmemberBasic += kclib_GetGetmemberBasic;
            kclib.GetGetmemberDeck += kclib_GetGetmemberDeck;
            kclib.GetGetmemberKdock += kclib_GetGetmemberKdock;
            kclib.GetGetmemberMaterial += kclib_GetGetmemberMaterial;
            kclib.GetGetmemberNdock += kclib_GetGetmemberNdock;
            kclib.GetGetmemberQuestlist += Kclib_GetGetmemberQuestlist;
            kclib.GetGetmemberShip2 += kclib_GetGetmemberShip2;
            kclib.GetGetmemberShip3 += kclib_GetGetmemberShip3;
            kclib.GetGetmemberShipDeck += Kclib_GetGetmemberShipDeck;
            kclib.GetGetmemberSlotItem += kclib_GetGetmemberSlotItem;
            kclib.GetPortPort += kclib_GetPortPort;
            kclib.GetReqhenseiChange += kclib_GetReqhenseiChange;
            kclib.GetReqhokyuCharge += Kclib_GetReqhokyuCharge;
            kclib.GetReqkousyouCreateitem += kclib_GetReqkousyouCreateitem;
            kclib.GetReqkousyouDestroyitem2 += Kclib_GetReqkousyouDestroyitem2;
            kclib.GetReqkousyouDestroyship += kclib_GetReqkousyouDestroyship;
            kclib.GetReqkousyouGetship += kclib_GetReqkousyouGetship;
            kclib.GetReqmemberUpdatedeckname += Kclib_GetReqmemberUpdatedeckname;
            kclib.GetReqmissionReturnInstruction += kclib_GetReqmissionReturnInstruction;
            kclib.GetReqmissionStart += kclib_GetReqmissionStart;
            kclib.GetReqnyukyoSpeedchange += Kclib_GetReqnyukyoSpeedchange;
            kclib.GetReqnyukyoStart += Kclib_GetReqnyukyoStart;
            kclib.GetStart2 += kclib_GetStart2;

            shipdata.CollectionChanged += shipdata_CollectionChanged;
            slotdata.CollectionChanged += slotdata_CollectionChanged;
        }
        
        private void Kclib_GetReqnyukyoSpeedchange(object sender, KanColleLib.TransmissionRequest.api_req_nyukyo.SpeedchangeRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            var shipid = ndockdata[request.ndock_id - 1].shipid;
            ndockdata[request.ndock_id - 1].status = NDockStatus.Empty;

            var ship = shipdata.FirstOrDefault(_ => _.shipid == shipid);
            if (ship != null)
            {
                ship.hp_now = ship.hp_max;
                ship.ndock_time = TimeSpan.Zero;
                if (ship.condition < 40)
                    ship.condition = 40;
            }
        }

        private void Kclib_GetReqnyukyoStart(object sender, KanColleLib.TransmissionRequest.api_req_nyukyo.StartRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if(request.highspeed)
            {
                var ship = shipdata.FirstOrDefault(_ => _.shipid == request.ship_id);
                if(ship != null)
                {
                    ship.hp_now = ship.hp_max;
                    ship.ndock_time = TimeSpan.Zero;
                    if (ship.condition < 40)
                        ship.condition = 40;
                }
            }
        }

        private void Kclib_GetReqkousyouDestroyitem2(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.Destroyitem2Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Destroyitem2> response)
        {
            foreach (var i in request.slotitem_ids)
            {
                var s = slotdata.FirstOrDefault(_ => _.id == i);
                if (s != null)
                    slotdata.Remove(s);
            }

            basicdata.fuel += response.data.get_material[0];
            basicdata.ammo += response.data.get_material[1];
            basicdata.steel += response.data.get_material[2];
            basicdata.bauxite += response.data.get_material[3];
        }

        private void Kclib_GetReqhokyuCharge(object sender, KanColleLib.TransmissionRequest.api_req_hokyu.ChargeRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_hokyu.Charge> response)
        {
            basicdata.FromMaterialArray(response.data.material);
            foreach(var ship in response.data.ship)
            {
                var s = shipdata.FirstOrDefault(_ => _.shipid == ship.id);
                if (s != null)
                {
                    s.fuel = ship.fuel;
                    s.ammo = ship.bull;
                    for (int j = 0; j < ship.onslot.Length; j++)
                    {
                        if (s.OnSlotCount.Count > j)
                            s.OnSlotCount[j] = ship.onslot[j];
                        else
                            s.OnSlotCount.Add(ship.onslot[j]);
                    }
                }
            }

            // 1艦隊の中でしか一気に補給できないことを利用する
            if(response.data.ship.Count > 0)
                foreach(var fleet in fleetdata)
                    if(fleet.ships.Contains(response.data.ship[0].id))
                    {
                        fleet.ChangeSupplyStatus();
                        break;
                    }
        }

        private void Kclib_GetGetmemberShipDeck(object sender, KanColleLib.TransmissionRequest.api_get_member.ShipDeckRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.ShipDeck> response)
        {
            foreach(var deck in response.data.deck_data)
            {
                fleetdata[deck.id - 1].FromDeckValue(deck);
            }

            AppendShipDataFromList(response.data.ship_data);
        }

        private void Kclib_GetReqmemberUpdatedeckname(object sender, KanColleLib.TransmissionRequest.api_req_member.UpdatedecknameRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if (request.deck_id <= fleetdata.Count && request.deck_id > 1)
                fleetdata[request.deck_id - 1].DeckName = request.name;
        }

        private void Kclib_GetGetmemberQuestlist(object sender, KanColleLib.TransmissionRequest.api_get_member.QuestlistRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Questlist> response)
        {
            questdata.SetQuestList(response.data);
        }

        private void Kclib_GameStart(object sender, KanColleLib.EventArgs.GameStartEventArgs e)
        {
            serverinfo.SetServerAddress(e.main2Dadress);
            System.Diagnostics.Debug.WriteLine("Server: " + serverinfo.address.ToString() + "（" + (serverinfo.GetServerName() ?? "不明") + "）");
            OnGameStart(new System.EventArgs());
        }

        void slotdata_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            basicdata.now_equipment_number = slotdata.Count;
        }

        void shipdata_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            basicdata.now_ship_number = shipdata.Count;
        }

        void kclib_GetReqkousyouCreateitem(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.CreateitemRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Createitem> response)
        {
            if (response.data.slot_item != null)
                slotdata.Add(new SlotData() { id = response.data.slot_item.id, itemid = response.data.slot_item.slotitem_id });

            var args = new CreateItemEventArgs() { success = response.data.create_flag };
            if (response.data.create_flag)
            {
                if (response.data.slot_item != null)
                {
                    args.id = response.data.slot_item.id;
                    args.item_id = response.data.slot_item.slotitem_id;
                }
            }
            else
            {
                args.id = 0;
                try
                {
                    args.item_id = int.Parse(response.data.fdata.Split(',')[1]);
                }
                catch { }
            }
            if (slotitemmaster.ContainsKey(args.item_id))
            {
                args.name = slotitemmaster[args.item_id].name;
                args.type_id = slotitemmaster[args.item_id].type_equiptype;
                if (slotitem_equiptypemaster.ContainsKey(args.type_id))
                    args.type = slotitem_equiptypemaster[args.type_id].name;
            }
            OnCreateItem(args);
        }

        async void kclib_GetGetmemberSlotItem(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.SlotItem> response)
        {
            await Task.Run(() =>
            {
                foreach (var item in response.data.slotitems)
                {
                    if (!slotdata.Any(_ => _.id == item.id))
                    {
                        slotdata.Add(new SlotData() { id = item.id, itemid = item.slotitem_id, alv = item.alv });
                    }
                    else
                    {
                        var s = slotdata.First(_ => _.id == item.id);
                        s.alv = item.alv;
                    }
                }

                var deletelist = (from _ in slotdata where !response.data.slotitems.Any(__ => __.id == _.id) select _).ToList();

                foreach (var item in deletelist)
                {
                    slotdata.Remove(item);
                }

                basicdata.now_equipment_number = slotdata.Count;
            });
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
                        int replaceshipid;

                        if (fleetdata[request.id - 1].ships.Count > request.ship_idx)
                        {
                            // 他の艦との交代である場合
                            // もとの艦のID
                            replaceshipid = fleetdata[request.id - 1].ships[request.ship_idx];
                            fleetdata[request.id - 1].ships[request.ship_idx] = request.ship_id;
                        }
                        else
                        {
                            // なにもないところに宣言した場合
                            replaceshipid = -1;
                            fleetdata[request.id - 1].ships.Add(request.ship_id);
                        }

                        for (int i = 0; i < fleetdata.Count; i++)
                        {
                            if (i == request.id - 1)
                                continue;
                            int rplidx = fleetdata[i].ships.IndexOf(request.ship_id);
                            if (rplidx != -1)
                            {
                                if(replaceshipid == -1)
                                    fleetdata[i].ships.RemoveAt(rplidx);
                                else
                                    fleetdata[i].ships[rplidx] = replaceshipid;
                                fleetdata[i].Recalc();
                                break;
                            }
                        }
                    }
                }
                fleetdata[request.id - 1].Recalc();
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

        void kclib_GetReqkousyouDestroyship(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.DestroyshipRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Destroyship> response)
        {
            DestroyShip(request.ship_id);

            // 艦隊にその艦娘がいたら削除
            foreach(var fleet in fleetdata)
            {
                int rplidx = fleet.ships.IndexOf(request.ship_id);
                if (rplidx != -1)
                    fleet.ships.RemoveAt(rplidx);
            }

            basicdata.FromMaterialArray(response.data.material);
        }

        void kclib_GetGetmemberNdock(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.NDock> response)
        {
            AppendNDockValue(response.data);
        }

        void kclib_GetReqkousyouGetship(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.GetshipRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Getship> response)
        {
            AppendKDockValue(response.data.kdock);
            foreach(var item in response.data.slotitem)
            {
                if (!slotdata.Any(_ => _.id == item.id))
                {
                    slotdata.Add(new SlotData() { id = item.id, itemid = item.slotitem_id });
                }
            }
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

        void kclib_GetGetmemberShip3(object sender, KanColleLib.TransmissionRequest.api_get_member.Ship3Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Ship3> response)
        {
            AppendShipDataFromList(response.data.ship_data.ships);
        }

        void kclib_GetGetmemberShip2(object sender, KanColleLib.TransmissionRequest.api_get_member.Ship2Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Ship2> response)
        {
            AppendShipDataFromList(response.data.ships);
        }

        void kclib_GetPortPort(object sender, KanColleLib.TransmissionRequest.api_port.PortRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_port.Port> response)
        {
            basicdata.FromMaterial(response.data.material);
            AppendShipDataFromList(response.data.ship.ships);
            DeleteShipDataFromList(response.data.ship.ships);
            AppendDeckValue(response.data.deck_port);
            AppendNDockValue(response.data.ndock);
            InitializeConfirm();
        }

        void kclib_GetStart2(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_start2.Start2> response)
        {
            charamaster = response.data.mst_ship.ToDictionary(_ => _.id, _ => CharacterData.fromKanColleLib(_));
            foreach (var s in response.data.mst_shipgraph)
                if(charamaster.ContainsKey(s.id))
                    charamaster[s.id].resource_id = s.filename;

            shiptypemaster = response.data.mst_stype.ToDictionary(_ => _.id, _ => new ShiptypeData() { name = _.name });

            missionmaster = response.data.mst_mission.ToDictionary(_ => _.id,
                _ => new MissionData()
                {
                    name = _.name,
                    time_minute = _.time,
                    details = _.details,
                    maparea_id = _.maparea_id
                });

            slotitemmaster = response.data.mst_slotitem.ToDictionary(_ => _.id,
                _ => new ItemData()
                {
                    name = _.name,
                    type = _.type[0],
                    type_cardtype = _.type[1],
                    type_equiptype = _.type[2],
                    type_icontype = _.type[3],
                    firepower = _.houg,
                    torpedo = _.raig,
                    bombing = _.baku,
                    anti_air = _.tyku,
                    anti_submarines = _.tais,
                    reconnaissance = _.saku,
                    hit_rate = _.houm,
                    evasion = _.houk,
                    armor = _.souk,
                    range = _.leng
                });

            slotitem_equiptypemaster = response.data.mst_slotitem_equiptype.ToDictionary(_ => _.id,
                _ => new ItemEquipTypeData()
                {
                    name = _.name,
                    typecolor = KanColleTools.GetSlotItemEquipTypeColor(_.id)
                });

            mapareamaster = response.data.mst_maparea.ToDictionary(_ => _.id,
                _ => new MapAreaData()
                {
                    name = _.name,
                    type = _.type
                });

            mapinfomaster = response.data.mst_mapinfo.ToDictionary(_ => _.id,
                _ => new MapInfoData()
                {
                    name = _.name,
                    area_id = _.maparea_id,
                    map_id = _.no,
                    map_level = _.level,
                    opename = _.opetext,
                    ope_info = _.infotext,
                    defeat_type = _.required_defeat_count.HasValue ? MapInfoData.MapDefeatType.count_of_defeat :
                                _.max_maphp.HasValue ? MapInfoData.MapDefeatType.max_hp : MapInfoData.MapDefeatType.normal,
                    defeat_count = _.required_defeat_count.HasValue ? _.required_defeat_count.Value : 1
                });
            
            InitializeConfirm();
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
            foreach (var deck in fleetdata)
                deck.Recalc();
        }

        void AppendShipData(KanColleLib.TransmissionData.api_get_member.values.ShipValue ship)
        {
            bool is_found = false;
            for (int i = 0; i < shipdata.Count; i++)
            {
                if (shipdata[i].shipid == ship.id)
                {
                    is_found = true;
                    shipdata[i].FromKanColleLib(ship);
                }
            }
            if (!is_found)
            {
                var temp = new ShipData();
                temp.FromKanColleLib(ship);
                shipdata.Add(temp);
            }
        }

        /// <summary>
        /// 艦娘一覧データから、ローカルデータに存在するが与えられたデータに存在しない艦をすべて削除する
        /// 与えられたリストの艦を削除するわけではない
        /// </summary>
        /// <param name="existshiplist"></param>
        void DeleteShipDataFromList(List<KanColleLib.TransmissionData.api_get_member.values.ShipValue> existshiplist)
        {
            var deletelist = (from _ in shipdata where !existshiplist.Any(__ => __.id == _.shipid) select _).ToList();
            foreach (var item in deletelist)
            {
                shipdata.Remove(item);
            }
        }

        /// <summary>
        /// 指定されたIDの艦をリストから削除する
        /// </summary>
        /// <param name="ship"></param>
        void DestroyShip(int id)
        {
            var deletelist = (from _ in shipdata where _.shipid == id select _).ToList();
            foreach (var item in deletelist)
            {
                foreach(var slot in item.Slots)
                {
                    DestroyItem(slot);
                }
                shipdata.Remove(item);
            }
        }

        /// <summary>
        /// 指定されたスロットIDの装備をリストから削除する
        /// 見つからなければ何もしない
        /// </summary>
        /// <param name="id"></param>
        void DestroyItem(int id)
        {
            var deletelist = (from _ in slotdata where _.id == id select _).ToList();
            foreach (var item in deletelist)
                slotdata.Remove(item);
        }

        int initializecount = 0;
        int initializecountflag = 2;

        void InitializeConfirm()
        {
            ++initializecount;
            if (initializecountflag == initializecount)
            {
                OnInitializeComplete(new System.EventArgs());
                initializeCompleted = true;
            }
        }

        void AppendNDockValue(KanColleLib.TransmissionData.api_get_member.NDock data)
        {
            for (int i = 0; i < data.ndocks.Count; i++)
            {
                if (ndockdata.Count <= i)
                    ndockdata.Add(new NDockData());
                ndockdata[i].FromNDockValue(data.ndocks[i]);
            }

            foreach (var fleet in fleetdata)
                fleet.ChangeNDockStatus();
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
        protected virtual void OnInitializeComplete(System.EventArgs e) { if (InitializeComplete != null) { InitializeComplete(this, e); } }

        /// <summary>
        /// ゲームが開始された（フラッシュがロードされた）際に呼び出されます
        /// </summary>
        public event EventHandler GameStart;
        protected virtual void OnGameStart(System.EventArgs e) { if (GameStart != null) { GameStart(this, e); } }

        /// <summary>
        /// APIの解析エラーが起きた際に呼び出されます
        /// </summary>
        public event APIAnalyzeErrorEventHandler APIAnalyzeError;
        public delegate void APIAnalyzeErrorEventHandler(object sender, APIAnalyzeErrorEventArgs e);
        protected virtual void OnAPIAnalyzeError(APIAnalyzeErrorEventArgs e) { if (APIAnalyzeError != null) { APIAnalyzeError(this, e); } }

        public event UnknownAPIReceivedEventHandler UnknownAPIReceived;
        public delegate void UnknownAPIReceivedEventHandler(object sender, APIAnalyzeErrorEventArgs e);
        protected virtual void OnUnknownAPIReceived(APIAnalyzeErrorEventArgs e) { if (UnknownAPIReceived != null) { UnknownAPIReceived(this, e); } }

        /// <summary>
        /// FiddlerのLogを取得した際に呼び出されます
        /// </summary>
        public event GetFiddlerLogEventHandler GetFiddlerLog;
        public delegate void GetFiddlerLogEventHandler(object sender, StringEventArgs e);
        protected virtual void OnGetFiddlerLog(StringEventArgs e) { if (GetFiddlerLog != null) { GetFiddlerLog(this, e); } }

        /// <summary>
        /// 装備を開発したときに呼び出されます
        /// </summary>
        public event CreateItemEventHandler CreateItem;
        public delegate void CreateItemEventHandler(object sender, CreateItemEventArgs e);
        protected virtual void OnCreateItem(CreateItemEventArgs e) { if (CreateItem != null) { CreateItem(this, e); } }
    }
}
