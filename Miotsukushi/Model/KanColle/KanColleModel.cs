using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;
using Miotsukushi.Model.Debugger;
using Miotsukushi.Tools;
using Miotsukushi.Model.KanColle.EventArgs;

namespace Miotsukushi.Model.KanColle
{
    class KanColleModel
    {
        KanColleNotifier _kclib;
        public ServerInfo Serverinfo = new ServerInfo();
        public Dictionary<int, CharacterData> Charamaster = new Dictionary<int,CharacterData>();
        public Dictionary<int, ShiptypeData> Shiptypemaster = new Dictionary<int,ShiptypeData>();
        public Dictionary<int, MissionData> Missionmaster = new Dictionary<int,MissionData>();
        public Dictionary<int, ItemData> Slotitemmaster = new Dictionary<int, ItemData>();
        public Dictionary<int, ItemEquipTypeData> SlotitemEquiptypemaster = new Dictionary<int, ItemEquipTypeData>();
        public Dictionary<int, MapAreaData> Mapareamaster = new Dictionary<int, MapAreaData>();
        public Dictionary<int, MapInfoData> Mapinfomaster = new Dictionary<int, MapInfoData>();
        public ObservableCollection<SlotData> Slotdata = new ObservableCollection<SlotData>();
        public ObservableCollection<ShipData> Shipdata = new ObservableCollection<ShipData>();
        public ExList<FleetData> Fleetdata = new ExList<FleetData>();
        public ExList<KDockData> Kdockdata = new ExList<KDockData>();
        public ExList<NDockData> Ndockdata = new ExList<NDockData>();
        public BasicData Basicdata = new BasicData();
        public QuestData Questdata = new QuestData();

        public BattleModels.BattleModel Battlemodel;
        public BattleModels.SortieModel Sortiemodel;
        public DebuggerModel Debuggermodel;
        public Plugins.StatisticsDbHelper Statsticshelper;
        public bool InitializeCompleted = false;
        public int CombinedFlag = 0;

        public KanColleModel()
        {
            var port = KanColleNotifier.FiddlerStartup();
            System.Diagnostics.Debug.WriteLine("Port:" + port);
            KanColleNotifier.FiddlerSetWinInetProxy();

            _kclib = new KanColleNotifier(true);
            new PacketSaver(_kclib);
            Statsticshelper = new Plugins.StatisticsDbHelper(_kclib);
            Battlemodel = new BattleModels.BattleModel(this, _kclib);
            Sortiemodel = new BattleModels.SortieModel(this, _kclib);
            Debuggermodel = new DebuggerModel(_kclib);

            _kclib.GameStart += Kclib_GameStart;
            _kclib.KcsAPIDataAnalyzeFailed += (_, e) => OnApiAnalyzeError(new ApiAnalyzeErrorEventArgs(e.kcsapiurl, e.request, e.response));
            _kclib.UnknownKcsAPIDataReceived += (_, e) => OnUnknownApiReceived(new ApiAnalyzeErrorEventArgs(e.kcsapiurl, e.request, e.response));
            _kclib.GetFiddlerLogString += (_, e) => OnGetFiddlerLog(new StringEventArgs(e.logtext));

            _kclib.GetGetmemberBasic += kclib_GetGetmemberBasic;
            _kclib.GetGetmemberDeck += kclib_GetGetmemberDeck;
            _kclib.GetGetmemberKdock += kclib_GetGetmemberKdock;
            _kclib.GetGetmemberMaterial += kclib_GetGetmemberMaterial;
            _kclib.GetGetmemberNdock += kclib_GetGetmemberNdock;
            _kclib.GetGetmemberQuestlist += Kclib_GetGetmemberQuestlist;
            _kclib.GetGetmemberShip2 += kclib_GetGetmemberShip2;
            _kclib.GetGetmemberShip3 += kclib_GetGetmemberShip3;
            _kclib.GetGetmemberShipDeck += Kclib_GetGetmemberShipDeck;
            _kclib.GetGetmemberSlotItem += kclib_GetGetmemberSlotItem;
            _kclib.GetPortPort += kclib_GetPortPort;
            _kclib.GetReqhenseiChange += kclib_GetReqhenseiChange;
            _kclib.GetReqhenseiCombined += Kclib_GetReqhenseiCombined;
            _kclib.GetReqhokyuCharge += Kclib_GetReqhokyuCharge;
            _kclib.GetReqkousyouCreateitem += kclib_GetReqkousyouCreateitem;
            _kclib.GetReqkousyouDestroyitem2 += Kclib_GetReqkousyouDestroyitem2;
            _kclib.GetReqkousyouDestroyship += kclib_GetReqkousyouDestroyship;
            _kclib.GetReqkousyouGetship += kclib_GetReqkousyouGetship;
            _kclib.GetReqmemberUpdatedeckname += Kclib_GetReqmemberUpdatedeckname;
            _kclib.GetReqmissionReturnInstruction += kclib_GetReqmissionReturnInstruction;
            _kclib.GetReqmissionStart += kclib_GetReqmissionStart;
            _kclib.GetReqnyukyoSpeedchange += Kclib_GetReqnyukyoSpeedchange;
            _kclib.GetReqnyukyoStart += Kclib_GetReqnyukyoStart;
            _kclib.GetStart2 += kclib_GetStart2;

            Shipdata.CollectionChanged += shipdata_CollectionChanged;
            Slotdata.CollectionChanged += slotdata_CollectionChanged;
        }

        private void Kclib_GetReqhenseiCombined(object sender, KanColleLib.TransmissionRequest.api_req_hensei.CombinedRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_hensei.Combined> response)
        {
            CombinedFlag = request.combined_type;
        }

        private void Kclib_GetReqnyukyoSpeedchange(object sender, KanColleLib.TransmissionRequest.api_req_nyukyo.SpeedchangeRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            var shipid = Ndockdata[request.ndock_id - 1].Shipid;
            Ndockdata[request.ndock_id - 1].Status = NDockStatus.Empty;

            var ship = Shipdata.FirstOrDefault(_ => _.Shipid == shipid);
            if (ship != null)
            {
                ship.HpNow = ship.HpMax;
                ship.NdockTime = TimeSpan.Zero;
                if (ship.Condition < 40)
                    ship.Condition = 40;
            }

            foreach (var fleet in Fleetdata)
                if(fleet.Ships.Contains(shipid))
                    fleet.ChangeNDockStatus();
        }

        private void Kclib_GetReqnyukyoStart(object sender, KanColleLib.TransmissionRequest.api_req_nyukyo.StartRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if(request.highspeed)
            {
                var ship = Shipdata.FirstOrDefault(_ => _.Shipid == request.ship_id);
                if(ship != null)
                {
                    ship.HpNow = ship.HpMax;
                    ship.NdockTime = TimeSpan.Zero;
                    if (ship.Condition < 40)
                        ship.Condition = 40;
                }

                foreach (var fleet in Fleetdata)
                    if (fleet.Ships.Contains(request.ship_id))
                        fleet.ChangeNDockStatus();
            }
        }

        private void Kclib_GetReqkousyouDestroyitem2(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.Destroyitem2Request request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Destroyitem2> response)
        {
            foreach (var i in request.slotitem_ids)
            {
                var s = Slotdata.FirstOrDefault(_ => _.Id == i);
                if (s != null)
                    Slotdata.Remove(s);
            }

            Basicdata.Fuel += response.data.get_material[0];
            Basicdata.Ammo += response.data.get_material[1];
            Basicdata.Steel += response.data.get_material[2];
            Basicdata.Bauxite += response.data.get_material[3];
        }

        private void Kclib_GetReqhokyuCharge(object sender, KanColleLib.TransmissionRequest.api_req_hokyu.ChargeRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_hokyu.Charge> response)
        {
            Basicdata.FromMaterialArray(response.data.material);
            foreach(var ship in response.data.ship)
            {
                var s = Shipdata.FirstOrDefault(_ => _.Shipid == ship.id);
                if (s != null)
                {
                    s.Fuel = ship.fuel;
                    s.Ammo = ship.bull;
                    for (var j = 0; j < ship.onslot.Length; j++)
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
                foreach(var fleet in Fleetdata)
                    if(fleet.Ships.Contains(response.data.ship[0].id))
                    {
                        fleet.ChangeSupplyStatus();
                        break;
                    }
        }

        private void Kclib_GetGetmemberShipDeck(object sender, KanColleLib.TransmissionRequest.api_get_member.ShipDeckRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.ShipDeck> response)
        {
            foreach(var deck in response.data.deck_data)
            {
                Fleetdata[deck.id - 1].FromDeckValue(deck);
            }

            AppendShipDataFromList(response.data.ship_data);
        }

        private void Kclib_GetReqmemberUpdatedeckname(object sender, KanColleLib.TransmissionRequest.api_req_member.UpdatedecknameRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if (request.deck_id <= Fleetdata.Count && request.deck_id > 1)
                Fleetdata[request.deck_id - 1].DeckName = request.name;
        }

        private void Kclib_GetGetmemberQuestlist(object sender, KanColleLib.TransmissionRequest.api_get_member.QuestlistRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Questlist> response)
        {
            Questdata.SetQuestList(response.data);
        }

        private void Kclib_GameStart(object sender, KanColleLib.EventArgs.GameStartEventArgs e)
        {
            Serverinfo.SetServerAddress(e.main2Dadress);
            System.Diagnostics.Debug.WriteLine("Server: " + Serverinfo.Address.ToString() + "（" + (Serverinfo.GetServerName() ?? "不明") + "）");
            OnGameStart(new System.EventArgs());
        }

        void slotdata_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Basicdata.NowEquipmentNumber = Slotdata.Count;
        }

        void shipdata_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Basicdata.NowShipNumber = Shipdata.Count;
        }

        void kclib_GetReqkousyouCreateitem(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.CreateitemRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Createitem> response)
        {
            if (response.data.slot_item != null)
                Slotdata.Add(new SlotData() { Id = response.data.slot_item.id, Itemid = response.data.slot_item.slotitem_id });

            var args = new CreateItemEventArgs() { Success = response.data.create_flag };
            if (response.data.create_flag)
            {
                if (response.data.slot_item != null)
                {
                    args.Id = response.data.slot_item.id;
                    args.ItemId = response.data.slot_item.slotitem_id;
                }
            }
            else
            {
                args.Id = 0;
                try
                {
                    args.ItemId = int.Parse(response.data.fdata.Split(',')[1]);
                }
                catch { }
            }
            if (Slotitemmaster.ContainsKey(args.ItemId))
            {
                args.Name = Slotitemmaster[args.ItemId].Name;
                args.TypeId = Slotitemmaster[args.ItemId].TypeEquiptype;
                if (SlotitemEquiptypemaster.ContainsKey(args.TypeId))
                    args.Type = SlotitemEquiptypemaster[args.TypeId].Name;
            }
            OnCreateItem(args);
        }

        async void kclib_GetGetmemberSlotItem(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.SlotItem> response)
        {
            await Task.Run(() =>
            {
                foreach (var item in response.data.slotitems)
                {
                    if (!Slotdata.Any(_ => _.Id == item.id))
                    {
                        Slotdata.Add(new SlotData() { Id = item.id, Itemid = item.slotitem_id, Alv = item.alv });
                    }
                    else
                    {
                        var s = Slotdata.First(_ => _.Id == item.id);
                        s.Alv = item.alv;
                    }
                }

                var deletelist = (from _ in Slotdata where !response.data.slotitems.Any(__ => __.id == _.Id) select _).ToList();

                foreach (var item in deletelist)
                {
                    Slotdata.Remove(item);
                }

                Basicdata.NowEquipmentNumber = Slotdata.Count;
            });
        }

        void kclib_GetReqhenseiChange(object sender, KanColleLib.TransmissionRequest.api_req_hensei.ChangeRequest request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if (request.id - 1 < Fleetdata.Count)
            {
                if (request.ship_id == -2)
                {
                    // 旗艦以外全部解除
                    var removecount = Fleetdata[request.id - 1].Ships.Count - 1;
                    for (var i = 0; i < removecount; i++)
                        Fleetdata[request.id - 1].Ships.RemoveAt(1);
                }
                else if (request.ship_id == -1)
                {
                    // その艦をはずす
                    Fleetdata[request.id - 1].Ships.RemoveAt(request.ship_idx);
                }
                else
                {
                    var replaceindex = Fleetdata[request.id - 1].Ships.IndexOf(request.ship_id);

                    if (replaceindex != -1)
                    {
                        // 艦隊の中に指定艦がいる場合
                        if (Fleetdata[request.id - 1].Ships.Count > request.ship_idx)
                        {
                            // 他の艦との交代である場合
                            var replaceshipid = Fleetdata[request.id - 1].Ships[request.ship_idx];
                            Fleetdata[request.id - 1].Ships[request.ship_idx] = request.ship_id;
                            Fleetdata[request.id - 1].Ships[replaceindex] = replaceshipid;
                        }
                        else
                        {
                            // なにもないところに宣言した場合
                            Fleetdata[request.id - 1].Ships.RemoveAt(replaceindex);
                            Fleetdata[request.id - 1].Ships.Add(request.ship_id);
                        }
                    }
                    else
                    {
                        int replaceshipid;

                        if (Fleetdata[request.id - 1].Ships.Count > request.ship_idx)
                        {
                            // 他の艦との交代である場合
                            // もとの艦のID
                            replaceshipid = Fleetdata[request.id - 1].Ships[request.ship_idx];
                            Fleetdata[request.id - 1].Ships[request.ship_idx] = request.ship_id;
                        }
                        else
                        {
                            // なにもないところに宣言した場合
                            replaceshipid = -1;
                            Fleetdata[request.id - 1].Ships.Add(request.ship_id);
                        }

                        for (var i = 0; i < Fleetdata.Count; i++)
                        {
                            if (i == request.id - 1)
                                continue;
                            var rplidx = Fleetdata[i].Ships.IndexOf(request.ship_id);
                            if (rplidx != -1)
                            {
                                if(replaceshipid == -1)
                                    Fleetdata[i].Ships.RemoveAt(rplidx);
                                else
                                    Fleetdata[i].Ships[rplidx] = replaceshipid;
                                Fleetdata[i].Recalc();
                                break;
                            }
                        }
                    }
                }
                Fleetdata[request.id - 1].Recalc();
            }
        }

        void kclib_GetGetmemberMaterial(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Material> response)
        {
            Basicdata.FromMaterial(response.data);
        }

        void kclib_GetGetmemberBasic(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Basic> response)
        {
            Basicdata.AdmiralName = response.data.nickname;
            Basicdata.AdmiralComment = response.data.comment;
            Basicdata.AppendRank(response.data.rank);
            Basicdata.AdmiralLevel = response.data.level;
            Basicdata.AdmiralExp = response.data.experience;
            Basicdata.FurnitureCoin = response.data.fcoin;
            Basicdata.MaxShip = response.data.max_chara;
            Basicdata.MaxEquipment = response.data.max_slotitem;
        }

        void kclib_GetReqkousyouDestroyship(object sender, KanColleLib.TransmissionRequest.api_req_kousyou.DestroyshipRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_kousyou.Destroyship> response)
        {
            DestroyShip(request.ship_id);

            // 艦隊にその艦娘がいたら削除
            foreach(var fleet in Fleetdata)
            {
                var rplidx = fleet.Ships.IndexOf(request.ship_id);
                if (rplidx != -1)
                    fleet.Ships.RemoveAt(rplidx);
            }

            Basicdata.FromMaterialArray(response.data.material);
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
                if (!Slotdata.Any(_ => _.Id == item.id))
                {
                    Slotdata.Add(new SlotData() { Id = item.id, Itemid = item.slotitem_id });
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
            if (Fleetdata.Count > request.deck_id - 1)
            {
                if (response.data.mission.Length >= 3)
                    Fleetdata[request.deck_id - 1].ChangeMissionStatus((int)response.data.mission[0], (int)response.data.mission[1], response.data.mission[2]);
                else
                    Fleetdata[request.deck_id - 1].ChangeMissionStatus(-1, -1, -1);
            }
        }

        void kclib_GetReqmissionStart(object sender, KanColleLib.TransmissionRequest.api_req_mission.StartRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_mission.Start> response)
        {
            if (Fleetdata.Count > request.deck_id - 1)
                Fleetdata[request.deck_id - 1].ChangeMissionStatus(1, request.mission_id, response.data.complatetime); // 遠征中の1は固定
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
            Basicdata.FromMaterial(response.data.material);
            AppendShipDataFromList(response.data.ship.ships);
            DeleteShipDataFromList(response.data.ship.ships);
            AppendDeckValue(response.data.deck_port);
            AppendNDockValue(response.data.ndock);
            CombinedFlag = response.data.combined_flag;
            InitializeConfirm();
        }

        void kclib_GetStart2(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_start2.Start2> response)
        {
            Charamaster = response.data.mst_ship.ToDictionary(_ => _.id, _ => CharacterData.FromKanColleLib(_));
            foreach (var s in response.data.mst_shipgraph)
                if(Charamaster.ContainsKey(s.id))
                    Charamaster[s.id].ResourceId = s.filename;

            Shiptypemaster = response.data.mst_stype.ToDictionary(_ => _.id, _ => new ShiptypeData() { Name = _.name });

            Missionmaster = response.data.mst_mission.ToDictionary(_ => _.id,
                _ => new MissionData()
                {
                    Name = _.name,
                    TimeMinute = _.time,
                    Details = _.details,
                    MapareaId = _.maparea_id
                });

            Slotitemmaster = response.data.mst_slotitem.ToDictionary(_ => _.id,
                _ => new ItemData()
                {
                    Name = _.name,
                    Type = _.type[0],
                    TypeCardtype = _.type[1],
                    TypeEquiptype = _.type[2],
                    TypeIcontype = _.type[3],
                    Firepower = _.houg,
                    Torpedo = _.raig,
                    Bombing = _.baku,
                    AntiAir = _.tyku,
                    AntiSubmarines = _.tais,
                    Reconnaissance = _.saku,
                    HitRate = _.houm,
                    Evasion = _.houk,
                    Armor = _.souk,
                    Range = _.leng
                });

            SlotitemEquiptypemaster = response.data.mst_slotitem_equiptype.ToDictionary(_ => _.id,
                _ => new ItemEquipTypeData()
                {
                    Name = _.name,
                    Typecolor = KanColleTools.GetSlotItemEquipTypeColor(_.id)
                });

            Mapareamaster = response.data.mst_maparea.ToDictionary(_ => _.id,
                _ => new MapAreaData()
                {
                    Name = _.name,
                    Type = _.type
                });

            Mapinfomaster = response.data.mst_mapinfo.ToDictionary(_ => _.id,
                _ => new MapInfoData()
                {
                    Name = _.name,
                    AreaId = _.maparea_id,
                    MapId = _.no,
                    MapLevel = _.level,
                    Opename = _.opetext,
                    OpeInfo = _.infotext,
                    DefeatType = _.required_defeat_count.HasValue ? MapInfoData.MapDefeatType.CountOfDefeat :
                                _.max_maphp.HasValue ? MapInfoData.MapDefeatType.MaxHp : MapInfoData.MapDefeatType.Normal,
                    DefeatCount = _.required_defeat_count.HasValue ? _.required_defeat_count.Value : 1
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
            foreach (var deck in Fleetdata)
                deck.Recalc();
        }

        void AppendShipData(KanColleLib.TransmissionData.api_get_member.values.ShipValue ship)
        {
            var isFound = false;
            for (var i = 0; i < Shipdata.Count; i++)
            {
                if (Shipdata[i].Shipid == ship.id)
                {
                    isFound = true;
                    Shipdata[i].FromKanColleLib(ship);
                }
            }
            if (!isFound)
            {
                var temp = new ShipData();
                temp.FromKanColleLib(ship);
                Shipdata.Add(temp);
            }
        }

        /// <summary>
        /// 艦娘一覧データから、ローカルデータに存在するが与えられたデータに存在しない艦をすべて削除する
        /// 与えられたリストの艦を削除するわけではない
        /// </summary>
        /// <param name="existshiplist"></param>
        void DeleteShipDataFromList(List<KanColleLib.TransmissionData.api_get_member.values.ShipValue> existshiplist)
        {
            var deletelist = (from _ in Shipdata where !existshiplist.Any(__ => __.id == _.Shipid) select _).ToList();
            foreach (var item in deletelist)
            {
                Shipdata.Remove(item);
            }
        }

        /// <summary>
        /// 指定されたIDの艦をリストから削除する
        /// </summary>
        /// <param name="ship"></param>
        void DestroyShip(int id)
        {
            var deletelist = (from _ in Shipdata where _.Shipid == id select _).ToList();
            foreach (var item in deletelist)
            {
                foreach(var slot in item.Slots)
                {
                    DestroyItem(slot);
                }
                Shipdata.Remove(item);
            }
        }

        /// <summary>
        /// 指定されたスロットIDの装備をリストから削除する
        /// 見つからなければ何もしない
        /// </summary>
        /// <param name="id"></param>
        void DestroyItem(int id)
        {
            var deletelist = (from _ in Slotdata where _.Id == id select _).ToList();
            foreach (var item in deletelist)
                Slotdata.Remove(item);
        }

        int _initializecount = 0;
        int _initializecountflag = 2;

        void InitializeConfirm()
        {
            ++_initializecount;
            if (_initializecountflag == _initializecount)
            {
                OnInitializeComplete(new System.EventArgs());
                InitializeCompleted = true;
            }
        }

        void AppendNDockValue(KanColleLib.TransmissionData.api_get_member.NDock data)
        {
            for (var i = 0; i < data.ndocks.Count; i++)
            {
                if (Ndockdata.Count <= i)
                    Ndockdata.Add(new NDockData());
                Ndockdata[i].FromNDockValue(data.ndocks[i]);
            }

            foreach (var fleet in Fleetdata)
                fleet.ChangeNDockStatus();
        }

        void AppendKDockValue(KanColleLib.TransmissionData.api_get_member.KDock data)
        {
            for (var i = 0; i < data.kdocks.Count; i++)
            {
                if (Kdockdata.Count <= i)
                    Kdockdata.Add(new KDockData());
                Kdockdata[i].FromKDockValue(data.kdocks[i]);
            }
        }

        void AppendDeckValue(KanColleLib.TransmissionData.api_get_member.Deck data)
        {
            if (data.decks.Count >= 1)
            {
                while (Fleetdata.Count < data.decks.Count)
                    Fleetdata.Add(new FleetData());
                while (Fleetdata.Count > data.decks.Count)
                    Fleetdata.RemoveAt(Fleetdata.Count - 1);
                for (var i = 0; i < Fleetdata.Count; i++)
                    Fleetdata[i].FromDeckValue(data.decks[i]);
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
        public event ApiAnalyzeErrorEventHandler ApiAnalyzeError;
        public delegate void ApiAnalyzeErrorEventHandler(object sender, ApiAnalyzeErrorEventArgs e);
        protected virtual void OnApiAnalyzeError(ApiAnalyzeErrorEventArgs e) { if (ApiAnalyzeError != null) { ApiAnalyzeError(this, e); } }

        public event UnknownApiReceivedEventHandler UnknownApiReceived;
        public delegate void UnknownApiReceivedEventHandler(object sender, ApiAnalyzeErrorEventArgs e);
        protected virtual void OnUnknownApiReceived(ApiAnalyzeErrorEventArgs e) { if (UnknownApiReceived != null) { UnknownApiReceived(this, e); } }

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
