using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    class SortieModel
    {
        public Dictionary<int, MapModel> Mapmodels = new Dictionary<int, MapModel>();

        readonly KanColleModel _originalModel;
        KanColleNotifier _kclib;

        public MapModel NowMap;
        public CellModel NowCell;
        public int SortiingDeck;

        public bool OnSortiing = false;

        /// <summary>
        /// 退避済みの艦（0-11）
        /// </summary>
        public ExList<int> GobackShips = new ExList<int>();

        public SortieModel(KanColleModel originalModel, KanColleNotifier kclib)
        {
            this._originalModel = originalModel;
            this._kclib = kclib;

            kclib.GetGetmemberMapinfo += Kclib_GetGetmemberMapinfo;
            kclib.GetReqmapStart += Kclib_GetReqmapStart;
            kclib.GetReqmapNext += Kclib_GetReqmapNext;
            kclib.GetPortPort += Kclib_GetPortPort;
            kclib.GetReqcombinedbattleBattleresult += Kclib_GetReqcombinedbattleBattleresult;
            kclib.GetReqcombinedbattleGobackPort += Kclib_GetReqcombinedbattleGobackPort;
        }

        /// <summary>
        /// 前回の連合艦隊戦闘結果で送られてきたescape_idx（なければnull）
        /// </summary>
        public int[] EscapeIdx;

        /// <summary>
        /// 前回の連合艦隊戦闘結果で送られてきたtow_idx（なければnull）
        /// </summary>
        public int[] TowIdx;

        private void Kclib_GetReqcombinedbattleGobackPort(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<object> response)
        {
            if (EscapeIdx != null && EscapeIdx.Length > 0)
                GobackShips.Add(EscapeIdx[0] - 1);
            if (TowIdx != null && TowIdx.Length > 0)
                GobackShips.Add(TowIdx[0] - 1);
        }

        private void Kclib_GetReqcombinedbattleBattleresult(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_combined_battle.Battleresult> response)
        {
            if(response.data.escape == null)
            {
                EscapeIdx = null;
                TowIdx = null;
            }
            else
            {
                EscapeIdx = response.data.escape.escape_idx;
                TowIdx = response.data.escape.tow_idx;
            }
        }

        private void Kclib_GetPortPort(object sender, KanColleLib.TransmissionRequest.api_port.PortRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_port.Port> response)
        {
            if(OnSortiing)
            {
                OnGoBackPort(new System.EventArgs());
                OnSortiing = false;
            }
        }

        private void Kclib_GetReqmapNext(object sender, KanColleLib.TransmissionRequest.api_req_map.NextRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_map.Next> response)
        {
            AppendCellData(response.data.next_cell_data);
            OnCellAdvanced(new System.EventArgs());
        }

        private void Kclib_GetReqmapStart(object sender, KanColleLib.TransmissionRequest.api_req_map.StartRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_map.Start> response)
        {
            OnSortiing = true;
            SortiingDeck = request.deck_id;
            GobackShips.Clear();
            var map = from _ in Mapmodels where _.Value.MapareaId == request.maparea_id && _.Value.MapNo == request.mapinfo_no select _;
            if (map.Count() > 0)
                NowMap = map.First().Value;
            AppendCellData(response.data.next_cell_data);
            OnSortieStarted(new System.EventArgs());
        }
        
        private void AppendCellData(KanColleLib.TransmissionData.api_req_map.values.NextCellData cell)
        {
            NowCell = new CellModel();
            NowCell.CellNo = cell.no;
            NowCell.BossCellNo = cell.bosscell_no;
            NowCell.HasNoWay = cell.next <= 0;
            NowCell.IsBossBattle = cell.event_id == 5;
            switch(cell.event_id)
            {
                case 0: // 出発地
                    NowCell.CellType = CellType.Start;
                    break;
                case 2: // 補給
                    NowCell.CellType = CellType.Supply;
                    if(cell.itemget != null)
                    {
                        NowCell.CellEventContentId = cell.itemget.id;
                        NowCell.CellEventContentValue = cell.itemget.getcount;
                    }
                    break;
                case 3: // うずしお
                    NowCell.CellType = CellType.Happening;
                    if (cell.happening != null)
                    {
                        NowCell.CellEventContentId = cell.happening.mst_id;
                        NowCell.CellEventContentValue = cell.happening.count;
                    }
                    break;
                case 6: // 気のせいだった or 能動分岐
                    switch(cell.event_kind)
                    {
                        case 0:
                        case 1:
                            NowCell.CellType = CellType.NoBattle;
                            NowCell.CellEventContentId = cell.event_id;
                            break;
                        case 2: // 能動分岐
                            NowCell.CellType = CellType.RouteChoice;
                            NowCell.CellEventContentValues = cell.select_route.select_cells;
                            break;
                        default:
                            NowCell.CellType = CellType.Unknown;
                            break;
                    }
                    break;
                case 7: // 航空戦
                    switch(cell.event_kind)
                    {
                        case 0: // 航空偵察
                            NowCell.CellType = CellType.AirSearch;
                            NowCell.CellEventContentId = cell.airsearch?.result ?? 0;
                            break;
                        case 4: // 航空戦
                            NowCell.CellType = CellType.AirBattle;
                            break;
                        default:
                            NowCell.CellType = CellType.Unknown;
                            break;
                    }
                    break;
                case 8: // 船団護衛成功
                    NowCell.CellType = CellType.SuccessShipGuard;
                    break;
                case 10: // 空襲戦
                    NowCell.CellType = CellType.AirRaid;
                    break;
                case 4: // 通常戦闘
                case 5: // ボス戦闘
                default:
                    switch (cell.event_kind)
                    {
                        case 0: // 戦闘なし
                            NowCell.CellType = CellType.NoBattle;
                            break;
                        case 1: // 昼戦
                            NowCell.CellType = CellType.Battle;
                            break;
                        case 2: // 夜戦
                            NowCell.CellType = CellType.NightSpBattle;
                            break;
                        case 3: // 夜昼戦
                            NowCell.CellType = CellType.NightToDayBattle;
                            break;
                        case 4: // 航空戦
                            NowCell.CellType = CellType.AirBattle;
                            break;
                        case 6: // 空襲戦
                            NowCell.CellType = CellType.AirRaid;
                            break;
                        default:
                            NowCell.CellType = CellType.Unknown;
                            break;
                    }
                    break;
            }
        }

        private void Kclib_GetGetmemberMapinfo(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Mapinfo> response)
        {
            Mapmodels = new Dictionary<int, MapModel>();
            foreach(var map in response.data.mapinfos)
            {
                var m = new MapModel();
                if(_originalModel.Mapinfomaster.ContainsKey(map.id))
                {
                    var mst = _originalModel.Mapinfomaster[map.id];
                    m.MapareaId = mst.AreaId;
                    if (_originalModel.Mapareamaster.ContainsKey(mst.AreaId))
                    {
                        var areaMst = _originalModel.Mapareamaster[mst.AreaId];
                        m.MapareaName = areaMst.Name;
                    }
                    m.MapName = mst.Name;
                    m.MapNo = mst.MapId;
                    m.Level = mst.MapLevel;
                    m.OpeTitle = mst.Opename;
                    m.OpeInfo = mst.OpeInfo;
                    switch(mst.DefeatType)
                    {
                        case MapInfoData.MapDefeatType.CountOfDefeat:
                            m.RequiredDefeatCount = mst.DefeatCount;
                            m.NowDefeatCount = 0;
                            break;
                        case MapInfoData.MapDefeatType.MaxHp:
                            m.MaxHp = 0;
                            m.NowHp = 0;
                            break;
                    }
                }
                m.IsCleared = map.cleared;
                m.NowDefeatCount = map.defeat_count;
                if(map.eventmap != null)
                {
                    m.MaxHp = map.eventmap.max_maphp;
                    m.NowHp = map.eventmap.now_maphp;
                    m.SelectedRank = map.eventmap.selected_rank;
                }
                Mapmodels.Add(map.id, m);
            }

            OnGetMapInfo(new System.EventArgs());
        }

        /// <summary>
        /// マップのでぃくしょなりを更新したときに呼び出されます
        /// </summary>
        public event EventHandler GetMapInfo;
        protected virtual void OnGetMapInfo(System.EventArgs e) { if (GetMapInfo != null) { GetMapInfo(this, e); } }

        /// <summary>
        /// 出撃が開始された際に呼び出されます
        /// </summary>
        public event EventHandler SortieStarted;
        protected virtual void OnSortieStarted(System.EventArgs e) { if (SortieStarted != null) { SortieStarted(this, e); } }

        /// <summary>
        /// 進撃した際に呼び出されます
        /// </summary>
        public event EventHandler CellAdvanced;
        protected virtual void OnCellAdvanced(System.EventArgs e) { if (CellAdvanced != null) { CellAdvanced(this, e); } }
        
        /// <summary>
        /// 戦闘が終了し母港に帰投した際に呼び出されます
        /// </summary>
        public event EventHandler GoBackPort;
        protected virtual void OnGoBackPort(System.EventArgs e) { if (GoBackPort != null) { GoBackPort(this, e); } }
    }
}
