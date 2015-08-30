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
        public Dictionary<int, MapModel> mapmodels = new Dictionary<int, MapModel>();
        
        KanColleModel original_model;
        KanColleNotifier kclib;

        public MapModel now_map;
        public CellModel now_cell;
        public int sortiing_deck;

        public bool on_sortiing = false;

        public SortieModel(KanColleModel original_model, KanColleNotifier kclib)
        {
            this.original_model = original_model;
            this.kclib = kclib;

            kclib.GetGetmemberMapinfo += Kclib_GetGetmemberMapinfo;
            kclib.GetReqmapStart += Kclib_GetReqmapStart;
            kclib.GetReqmapNext += Kclib_GetReqmapNext;
            kclib.GetPortPort += Kclib_GetPortPort;
        }

        private void Kclib_GetPortPort(object sender, KanColleLib.TransmissionRequest.api_port.PortRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_port.Port> response)
        {
            if(on_sortiing)
            {
                OnGoBackPort(new System.EventArgs());
                on_sortiing = false;
            }
        }

        private void Kclib_GetReqmapNext(object sender, KanColleLib.TransmissionRequest.api_req_map.NextRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_map.Next> response)
        {
            AppendCellData(response.data.next_cell_data);
            OnCellAdvanced(new System.EventArgs());
        }

        private void Kclib_GetReqmapStart(object sender, KanColleLib.TransmissionRequest.api_req_map.StartRequest request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_req_map.Start> response)
        {
            on_sortiing = true;
            sortiing_deck = request.deck_id;
            var map = from _ in mapmodels where _.Value.maparea_id == request.maparea_id && _.Value.map_no == request.mapinfo_no select _;
            if (map.Count() > 0)
                now_map = map.First().Value;
            AppendCellData(response.data.next_cell_data);
            OnSortieStarted(new System.EventArgs());
        }
        
        private void AppendCellData(KanColleLib.TransmissionData.api_req_map.values.NextCellData cell)
        {
            now_cell = new CellModel();
            now_cell.cell_no = cell.no;
            now_cell.boss_cell_no = cell.bosscell_no;
            now_cell.has_no_way = cell.next <= 0;
            now_cell.is_boss_battle = cell.event_id == 5;
            switch(cell.event_id)
            {
                case 0: // 出発地
                    now_cell.cell_type = CellModel.CellType.start;
                    break;
                case 2: // 補給
                    now_cell.cell_type = CellModel.CellType.supply;
                    if(cell.itemget != null)
                    {
                        now_cell.cell_event_content_id = cell.itemget.id;
                        now_cell.cell_event_content_value = cell.itemget.getcount;
                    }
                    break;
                case 3: // うずしお
                    now_cell.cell_type = CellModel.CellType.happening;
                    if (cell.happening != null)
                    {
                        now_cell.cell_event_content_id = cell.happening.mst_id;
                        now_cell.cell_event_content_value = cell.happening.count;
                    }
                    break;
                case 6: // 気のせいだった or 能動分岐
                    switch(cell.event_kind)
                    {
                        case 0:
                        case 1:
                            now_cell.cell_type = CellModel.CellType.no_battle;
                            now_cell.cell_event_content_id = cell.event_id;
                            break;
                        case 2: // 能動分岐
                            now_cell.cell_type = CellModel.CellType.route_choice;
                            now_cell.cell_event_content_values = cell.select_route.select_cells;
                            break;
                        default:
                            now_cell.cell_type = CellModel.CellType.unknown;
                            break;
                    }
                    break;
                case 7: // 航空戦
                    switch(cell.event_kind)
                    {
                        case 0: // 航空偵察
                            now_cell.cell_type = CellModel.CellType.air_search;
                            now_cell.cell_event_content_id = cell.airsearch != null ? cell.airsearch.result : 0;
                            break;
                        case 4: // 航空戦
                            now_cell.cell_type = CellModel.CellType.air_battle;
                            break;
                        default:
                            now_cell.cell_type = CellModel.CellType.unknown;
                            break;
                    }
                    break;
                case 8: // 船団護衛成功
                    now_cell.cell_type = CellModel.CellType.success_ship_guard;
                    break;
                case 4: // 通常戦闘
                case 5: // ボス戦闘
                default:
                    switch (cell.event_kind)
                    {
                        case 0: // 戦闘なし
                            now_cell.cell_type = CellModel.CellType.no_battle;
                            break;
                        case 1: // 昼戦
                            now_cell.cell_type = CellModel.CellType.battle;
                            break;
                        case 2: // 夜戦
                            now_cell.cell_type = CellModel.CellType.night_sp_battle;
                            break;
                        case 3: // 夜昼戦
                            now_cell.cell_type = CellModel.CellType.night_to_day_battle;
                            break;
                        case 4: // 航空戦
                            now_cell.cell_type = CellModel.CellType.air_battle;
                            break;
                        case 6: // 能動分岐
                            now_cell.cell_type = CellModel.CellType.route_choice;
                            now_cell.cell_event_content_values = cell.select_route.select_cells;
                            break;
                        default:
                            now_cell.cell_type = CellModel.CellType.unknown;
                            break;
                    }
                    break;
            }
        }

        private void Kclib_GetGetmemberMapinfo(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_get_member.Mapinfo> response)
        {
            mapmodels = new Dictionary<int, MapModel>();
            foreach(var map in response.data.mapinfos)
            {
                var m = new MapModel();
                if(original_model.mapinfomaster.ContainsKey(map.id))
                {
                    var mst = original_model.mapinfomaster[map.id];
                    m.maparea_id = mst.area_id;
                    if (original_model.mapareamaster.ContainsKey(mst.area_id))
                    {
                        var area_mst = original_model.mapareamaster[mst.area_id];
                        m.maparea_name = area_mst.name;
                    }
                    m.map_name = mst.name;
                    m.map_no = mst.map_id;
                    m.level = mst.map_level;
                    m.ope_title = mst.opename;
                    m.ope_info = mst.ope_info;
                    switch(mst.defeat_type)
                    {
                        case MapInfoData.MapDefeatType.count_of_defeat:
                            m.required_defeat_count = mst.defeat_count;
                            m.now_defeat_count = 0;
                            break;
                        case MapInfoData.MapDefeatType.max_hp:
                            m.max_hp = 0;
                            m.now_hp = 0;
                            break;
                    }
                }
                m.is_cleared = map.cleared;
                m.now_defeat_count = map.defeat_count;
                if(map.eventmap != null)
                {
                    m.max_hp = map.eventmap.max_maphp;
                    m.now_hp = map.eventmap.now_maphp;
                    m.selected_rank = map.eventmap.selected_rank;
                }
                mapmodels.Add(map.id, m);
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
