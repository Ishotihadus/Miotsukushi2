using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeplex.Data;

namespace KanColleLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // test_get_member();
            // test_request();
            // test_req_map();
            // test_start2();
            // test_req_sortie();
            // test_battle();
            test_combined_battle();
        }
        
        static void test_request()
        {
            string api_mapcell_request = "api%5Fmapinfo%5Fno=5&api%5Fmaparea%5Fid=27&api%5Fverno=1&api%5Ftoken=xxx";
            var mapcell_request = new KanColleLib.TransmissionRequest.api_get_member.MapcellRequest(api_mapcell_request);

            string api_ship3_request = "api%5Fverno=1&spi%5Fsort%5Forder=2&api%5Fsort%5Fkey=5&api%5Ftoken=xxx";
            var ship3_request = new KanColleLib.TransmissionRequest.api_get_member.Ship3Request(api_ship3_request);

            string api_ship3_request_2 = "api%5Fverno=1&spi%5Fsort%5Forder=1&api%5Fshipid=14315&api%5Fsort%5Fkey=1&api%5Ftoken=xxx";
            var ship3_request_2 = new KanColleLib.TransmissionRequest.api_get_member.Ship3Request(api_ship3_request_2);

            string api_change = "api%5Fid=1&api%5Fship%5Fid=%2D2&api%5Fverno=1&api%5Ftoken=xxx&api%5Fship%5Fidx=%2D1";
            var change = new KanColleLib.TransmissionRequest.api_req_hensei.ChangeRequest(api_change);

            string api_port = "api%5Fverno=1&api%5Fport=8525211803957362787025&api%5Ftoken=xxx&spi%5Fsort%5Forder=2&api%5Fsort%5Fkey=5";
            var port = new KanColleLib.TransmissionRequest.api_port.PortRequest(api_port);

            string api_charge = "api%5Fverno=1&api%5Fonslot=1&api%5Fid%5Fitems=379%2C858%2C2061%2C857%2C125%2C1366&api%5Ftoken=xxx&api%5Fkind=3";
            var charge = new KanColleLib.TransmissionRequest.api_req_hokyu.ChargeRequest(api_charge);

            string api_nyukyostart = "api%5Fverno=1&api%5Fship%5Fid=14315&api%5Fndock%5Fid=2&api%5Ftoken=xxx&api%5Fhighspeed=0";
            var nyukyostart = new KanColleLib.TransmissionRequest.api_req_nyukyo.StartRequest(api_nyukyostart);

            string api_missionstart = "api%5Fmission=55&api%5Fmission%5Fid=21&api%5Fdeck%5Fid=2&api%5Fverno=1&api%5Ftoken=xxx";
            var missionstart = new KanColleLib.TransmissionRequest.api_req_mission.StartRequest(api_missionstart);

            string api_changecomment = "api%5Fverno=1&api%5Fcmt%5Fid=127435711&api%5Ftoken=xxx&api%5Fcmt=1%E6%97%A51%E9%8C%A0%E3%83%AA%E3%82%B9%E3%83%9A%E3%83%AA%E3%83%89%E3%83%B3%E3%80%82";
            var changecomment = new KanColleLib.TransmissionRequest.api_req_member.UpdatecommentRequest(api_changecomment);

            string api_changedeckname = "api%5Fname=%E9%9B%BB%E7%A3%81%E6%B0%97%E5%AD%A6&api%5Fdeck%5Fid=3&api%5Fverno=1&api%5Fname%5Fid=127435660&api%5Ftoken=xxx";
            var changedeckname = new KanColleLib.TransmissionRequest.api_req_member.UpdatedecknameRequest(api_changedeckname);

            string api_ship_deck = "api%5Fdeck%5Frid=1&api%5Fverno=1&api%5Ftoken=166f039095f84ef4c969a09210ae46b346ca790f";
            var shipdeck = new KanColleLib.TransmissionRequest.api_get_member.ShipDeckRequest(api_ship_deck);

            string api_ship_deck2 = "api%5Fdeck%5Frid=1,2&api%5Fverno=1&api%5Ftoken=166f039095f84ef4c969a09210ae46b346ca790f";
            var shipdeck2 = new KanColleLib.TransmissionRequest.api_get_member.ShipDeckRequest(api_ship_deck2);
        }

        static void test_start2()
        {
            string api_start2 = System.IO.File.ReadAllText("api_start2.txt");
            var before = DateTime.Now;
            var start2 = KanColleLib.TransmissionData.api_start2.Start2.fromDynamic(DynamicJson.Parse(api_start2).api_data);
            var after = DateTime.Now;
            System.Diagnostics.Debug.WriteLine((after - before).TotalMilliseconds);
        }

        static void test_get_member()
        {
            // string api_material = System.IO.File.ReadAllText("api_get_member/material.txt");
            //var material = KanColleLib.TransmissionData.api_get_member.Material.fromDynamic(DynamicJson.Parse(api_material).api_data);

            //string api_deck = System.IO.File.ReadAllText("api_get_member/deck.txt");
            //var deck = KanColleLib.TransmissionData.api_get_member.Deck.fromDynamic(DynamicJson.Parse(api_deck).api_data);

            string api_kdock = System.IO.File.ReadAllText("api_get_member/kdock.txt");
            var kdock = KanColleLib.TransmissionData.api_get_member.KDock.fromDynamic(DynamicJson.Parse(api_kdock).api_data);

            string api_basic = System.IO.File.ReadAllText("api_get_member/basic.txt");
            var basic = KanColleLib.TransmissionData.api_get_member.Basic.fromDynamic(DynamicJson.Parse(api_basic).api_data);

            string api_furniture = System.IO.File.ReadAllText("api_get_member/furniture.txt");
            var furniture = KanColleLib.TransmissionData.api_get_member.Furniture.fromDynamic(DynamicJson.Parse(api_furniture).api_data);

            //string api_mapcell = System.IO.File.ReadAllText("api_get_member/mapcell.txt");
            //var mapcell = KanColleLib.TransmissionData.api_get_member.Mapcell.fromDynamic(DynamicJson.Parse(api_mapcell).api_data);

            //string api_mapinfo = System.IO.File.ReadAllText("api_get_member/mapinfo.txt");
            //var mapinfo = KanColleLib.TransmissionData.api_get_member.Mapinfo.fromDynamic(DynamicJson.Parse(api_mapinfo).api_data);

            //string api_mission = System.IO.File.ReadAllText("api_get_member/mission.txt");
            //var mission = KanColleLib.TransmissionData.api_get_member.Mission.fromDynamic(DynamicJson.Parse(api_mission).api_data);

            //string api_ndock = System.IO.File.ReadAllText("api_get_member/ndock.txt");
            //var ndock = KanColleLib.TransmissionData.api_get_member.NDock.fromDynamic(DynamicJson.Parse(api_ndock).api_data);

            //string api_ship2 = System.IO.File.ReadAllText("api_get_member/ship2.txt");
            //var ship2 = KanColleLib.TransmissionData.api_get_member.Ship2.fromDynamic(DynamicJson.Parse(api_ship2).api_data);

            string api_unsetslot = System.IO.File.ReadAllText("api_get_member/unsetslot.txt");
            var unsetslot = KanColleLib.TransmissionData.api_get_member.Unsetslot.fromDynamic(DynamicJson.Parse(api_unsetslot).api_data);

            //string api_ship3 = System.IO.File.ReadAllText("api_get_member/ship3.txt");
            //var ship3 = KanColleLib.TransmissionData.api_get_member.Ship3.fromDynamic(DynamicJson.Parse(api_ship3).api_data);

            //string api_ship3_2 = System.IO.File.ReadAllText("api_get_member/ship3_2.txt");
            //var ship3_2 = KanColleLib.TransmissionData.api_get_member.Ship3.fromDynamic(DynamicJson.Parse(api_ship3_2).api_data);

            string api_slot_item = System.IO.File.ReadAllText("api_get_member/slot_item.txt");
            var slot_item = KanColleLib.TransmissionData.api_get_member.SlotItem.fromDynamic(DynamicJson.Parse(api_slot_item).api_data);

            string api_questlist = System.IO.File.ReadAllText("api_get_member/questlist.txt");
            var questlist = KanColleLib.TransmissionData.api_get_member.Questlist.fromDynamic(DynamicJson.Parse(api_questlist).api_data);
            
            string api_ship_deck = System.IO.File.ReadAllText("api_get_member/ship_deck.txt");
            var ship_deck = KanColleLib.TransmissionData.api_get_member.ShipDeck.fromDynamic(DynamicJson.Parse(api_ship_deck).api_data);
            
            string api_useitem = System.IO.File.ReadAllText("api_get_member/useitem.txt");
            var useitem = KanColleLib.TransmissionData.api_get_member.Useitem.fromDynamic(DynamicJson.Parse(api_useitem).api_data);
        }

        static void test_port()
        {
            string api_port = System.IO.File.ReadAllText("api_port/port.txt");
            var port = KanColleLib.TransmissionData.api_port.Port.fromDynamic(DynamicJson.Parse(api_port).api_data);
        }

        static void test_req_hensei()
        {
            string api_combined = System.IO.File.ReadAllText("api_req_hensei/combined.txt");
            var combined = KanColleLib.TransmissionData.api_req_hensei.Combined.fromDynamic(DynamicJson.Parse(api_combined).api_data);

            string api_lock = System.IO.File.ReadAllText("api_req_hensei/lock.txt");
            var _lock = KanColleLib.TransmissionData.api_req_hensei.Lock.fromDynamic(DynamicJson.Parse(api_lock).api_data);
        }

        static void test_req_hokyu()
        {
            string api_charge = System.IO.File.ReadAllText("api_req_hokyu/charge.txt");
            var charge = KanColleLib.TransmissionData.api_req_hokyu.Charge.fromDynamic(DynamicJson.Parse(api_charge).api_data);

        }

        static void test_req_mission()
        {
            string api_start = System.IO.File.ReadAllText("api_req_mission/start.txt");
            var start = KanColleLib.TransmissionData.api_req_mission.Start.fromDynamic(DynamicJson.Parse(api_start).api_data);

            string api_result = System.IO.File.ReadAllText("api_req_mission/result.txt");
            var result = KanColleLib.TransmissionData.api_req_mission.Result.fromDynamic(DynamicJson.Parse(api_result).api_data);

            string api_result_2 = System.IO.File.ReadAllText("api_req_mission/result_2.txt");
            var result_2 = KanColleLib.TransmissionData.api_req_mission.Result.fromDynamic(DynamicJson.Parse(api_result_2).api_data);

            string api_result_3 = System.IO.File.ReadAllText("api_req_mission/result_3.txt");
            var result_3 = KanColleLib.TransmissionData.api_req_mission.Result.fromDynamic(DynamicJson.Parse(api_result_3).api_data);

            string api_return_instruction = System.IO.File.ReadAllText("api_req_mission/return_instruction.txt");
            var return_instruction = KanColleLib.TransmissionData.api_req_mission.ReturnInstruction.fromDynamic(DynamicJson.Parse(api_return_instruction).api_data);
        }

        static void test_req_kousyou()
        {
            string api_createitem = System.IO.File.ReadAllText("api_req_kousyou/createitem.txt");
            var createitem = KanColleLib.TransmissionData.api_req_kousyou.Createitem.fromDynamic(DynamicJson.Parse(api_createitem).api_data);

            string api_createitem_2 = System.IO.File.ReadAllText("api_req_kousyou/createitem_2.txt");
            var createitem_2 = KanColleLib.TransmissionData.api_req_kousyou.Createitem.fromDynamic(DynamicJson.Parse(api_createitem_2).api_data);
        }

        static void test_req_quest()
        {
            string api_clearitemget = System.IO.File.ReadAllText("api_req_quest/clearitemget.txt");
            var clearitemget = KanColleLib.TransmissionData.api_req_quest.Clearitemget.fromDynamic(DynamicJson.Parse(api_clearitemget).api_data);
        }

        static void test_req_map()
        {
            string api_start = System.IO.File.ReadAllText("api_req_map/start.txt");
            var start = KanColleLib.TransmissionData.api_req_map.Start.fromDynamic(DynamicJson.Parse(api_start).api_data);

            string api_next = System.IO.File.ReadAllText("api_req_map/next.txt");
            var next = KanColleLib.TransmissionData.api_req_map.Start.fromDynamic(DynamicJson.Parse(api_next).api_data);
        }

        static void test_req_sortie()
        {
            string api_battleresult = System.IO.File.ReadAllText("api_req_sortie/battleresult.txt");
            var battleresult = KanColleLib.TransmissionData.api_req_sortie.Battleresult.fromDynamic(DynamicJson.Parse(api_battleresult).api_data);


            string api_battleresult2 = System.IO.File.ReadAllText("api_req_sortie/battleresult2.txt");
            var battleresult2 = KanColleLib.TransmissionData.api_req_sortie.Battleresult.fromDynamic(DynamicJson.Parse(api_battleresult2).api_data);
        }

        static void test_battle()
        {
            var hougeki = KanColleLib.TransmissionData.api_req_sortie.values.HougekiValue.fromDynamic(DynamicJson.Parse("{\"api_at_list\":[-1,2,9,1,12,4,11,3,8,7,5],\"api_at_type\":[-1,0,0,2,0,0,0,2,0,0,0],\"api_df_list\":[-1,[11],[4],[9,9],[3],[8],[5],[12,12],[6],[1],[8]],\"api_si_list\":[-1,[9],[509],[9,9],[502],[6],[502],[50,6],[-1],[-1],[-1]],\"api_cl_list\":[-1,[0],[2],[2,2],[1],[1],[0],[1,1],[1],[0],[0]],\"api_damage\":[-1,[0],[33],[139,143],[3.1],[9],[0.1],[59,72],[2],[0],[0]]}"));

            var koukustage1 = KanColleLib.TransmissionData.api_req_sortie.values.KoukuStage1.fromDynamic(DynamicJson.Parse("{\"api_f_count\":95,\"api_f_lostcount\":0,\"api_e_count\":176,\"api_e_lostcount\":49,\"api_disp_seiku\":1,\"api_touch_plane\":[93,-1]}"));
            var koukustage2 = KanColleLib.TransmissionData.api_req_sortie.values.KoukuStage2.fromDynamic(DynamicJson.Parse("{\"api_f_count\":66,\"api_f_lostcount\":1,\"api_e_count\":20,\"api_e_lostcount\":13,\"api_air_fire\":{\"api_idx\":3,\"api_kind\":5,\"api_use_items\":[122,122,32]}}"));
            var koukustage3 = KanColleLib.TransmissionData.api_req_sortie.values.KoukuStage3.fromDynamic(DynamicJson.Parse("{\"api_frai_flag\":[-1,0,1,0,0,1,0],\"api_erai_flag\":[-1,0,0,0,1,1,0],\"api_fbak_flag\":[-1,1,0,0,1,1,0],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,5,0],\"api_edam\":[-1,0,0,0,73,0,0]}"));
            var koukuvalue = KanColleLib.TransmissionData.api_req_sortie.values.KoukuValue.fromDynamic(DynamicJson.Parse("{\"api_plane_from\":[[5,6],[7,8]],\"api_stage1\":{\"api_f_count\":95,\"api_f_lostcount\":0,\"api_e_count\":176,\"api_e_lostcount\":49,\"api_disp_seiku\":1,\"api_touch_plane\":[93,-1]},\"api_stage2\":{\"api_f_count\":29,\"api_f_lostcount\":2,\"api_e_count\":93,\"api_e_lostcount\":35},\"api_stage3\":{\"api_frai_flag\":[-1,0,1,0,0,1,0],\"api_erai_flag\":[-1,0,0,0,1,1,0],\"api_fbak_flag\":[-1,1,0,0,1,1,0],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,5,0],\"api_edam\":[-1,0,0,0,73,0,0]}}"));

            var raigeki = KanColleLib.TransmissionData.api_req_sortie.values.RaigekiValue.fromDynamic(DynamicJson.Parse("{\"api_frai\":[-1,0,0,1,0,0,0],\"api_erai\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0],\"api_edam\":[-1,8,0,0,0,0,0],\"api_fydam\":[-1,0,0,8,0,0,0],\"api_eydam\":[-1,0,0,0,0,0,0],\"api_fcl\":[-1,0,0,1,0,0,0],\"api_ecl\":[-1,0,0,0,0,0,0]}"));

            var supportairatack = KanColleLib.TransmissionData.api_req_sortie.values.SupportAiratackValue.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":4,\"api_ship_id\":[134,7122,19742,18959,7028,0],\"api_undressing_flag\":[0,0,0,0,0,0],\"api_stage_flag\":[1,1,1],\"api_plane_from\":[[-1]],\"api_stage1\":{\"api_f_count\":58,\"api_f_lostcount\":0,\"api_e_count\":0,\"api_e_lostcount\":0},\"api_stage2\":{\"api_f_count\":58,\"api_f_lostcount\":3},\"api_stage3\":{\"api_erai_flag\":[-1,0,1,0,0,0,1],\"api_ebak_flag\":[-1,0,1,1,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_edam\":[-1,0,6,0,0,0,0]}}"));
            var supporthouraiatack = KanColleLib.TransmissionData.api_req_sortie.values.SupportHouraiValue.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":3,\"api_ship_id\":[3906,51,14896,14898,14895,0],\"api_undressing_flag\":[0,0,0,0,0,0],\"api_cl_list\":[-1,0,0,1,0,0,0],\"api_damage\":[-1,0,0,10,0,0,0]}"));

            var test1 = KanColleLib.TransmissionData.api_req_sortie.Battle.fromDynamic(DynamicJson.Parse("{\"api_dock_id\":1,\"api_ship_ke\":[-1,560,560,529,555,576,576],\"api_ship_lv\":[-1,1,1,1,1,1,1],\"api_nowhps\":[-1,68,64,41,44,45,18,84,84,98,57,37,37],\"api_maxhps\":[-1,75,75,50,50,45,50,84,84,98,57,37,37],\"api_midnight_flag\":1,\"api_eSlot\":[[520,524,524,517,-1],[520,524,524,517,-1],[509,509,525,528,-1],[506,525,542,543,-1],[502,545,542,-1,-1],[502,545,542,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[94,0,55,89],[94,0,50,89],[75,69,59,71],[75,69,59,71],[29,0,39,59],[29,0,69,59]],\"api_eParam\":[[18,0,36,70],[18,0,36,70],[90,0,80,99],[48,80,30,39],[38,66,32,26],[38,66,32,26]],\"api_search\":[1,1],\"api_formation\":[1,3,2],\"api_stage_flag\":[1,1,1],\"api_kouku\":{\"api_plane_from\":[[5,6],[7,8]],\"api_stage1\":{\"api_f_count\":95,\"api_f_lostcount\":0,\"api_e_count\":176,\"api_e_lostcount\":49,\"api_disp_seiku\":1,\"api_touch_plane\":[93,-1]},\"api_stage2\":{\"api_f_count\":29,\"api_f_lostcount\":2,\"api_e_count\":93,\"api_e_lostcount\":35},\"api_stage3\":{\"api_frai_flag\":[-1,0,1,0,0,1,0],\"api_erai_flag\":[-1,0,0,0,1,1,0],\"api_fbak_flag\":[-1,1,0,0,1,1,0],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,5,0],\"api_edam\":[-1,0,0,0,73,0,0]}},\"api_support_flag\":0,\"api_support_info\":null,\"api_opening_flag\":0,\"api_opening_atack\":null,\"api_hourai_flag\":[1,1,0,1],\"api_hougeki1\":{\"api_at_list\":[-1,2,9,1,12,4,11,3,8,7,5],\"api_at_type\":[-1,0,0,2,0,0,0,2,0,0,0],\"api_df_list\":[-1,[11],[4],[9,9],[3],[8],[5],[12,12],[6],[1],[8]],\"api_si_list\":[-1,[9],[509],[9,9],[502],[6],[502],[50,6],[-1],[-1],[-1]],\"api_cl_list\":[-1,[0],[2],[2,2],[1],[1],[0],[1,1],[1],[0],[0]],\"api_damage\":[-1,[0],[33],[139,143],[3.1],[9],[0.1],[59,72],[2],[0],[0]]},\"api_hougeki2\":{\"api_at_list\":[-1,1,7,2,3,4],\"api_at_type\":[-1,2,0,2,0,0],\"api_df_list\":[-1,[8,8],[5],[11,11],[11],[7]],\"api_si_list\":[-1,[9,9],[-1],[9,9],[50],[6]],\"api_cl_list\":[-1,[2,1],[1],[1,1],[1],[1]],\"api_damage\":[-1,[129.1,94.1],[21],[3.1,2.1],[55],[9]]},\"api_hougeki3\":null,\"api_raigeki\":{\"api_frai\":[-1,0,0,1,0,0,0],\"api_erai\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0],\"api_edam\":[-1,8,0,0,0,0,0],\"api_fydam\":[-1,0,0,8,0,0,0],\"api_eydam\":[-1,0,0,0,0,0,0],\"api_fcl\":[-1,0,0,1,0,0,0],\"api_ecl\":[-1,0,0,0,0,0,0]}}"));
            var test2 = KanColleLib.TransmissionData.api_req_sortie.Battle.fromDynamic(DynamicJson.Parse("{\"api_dock_id\":1,\"api_ship_ke\":[-1,502,501,-1,-1,-1,-1],\"api_ship_lv\":[-1,1,1,-1,-1,-1,-1],\"api_nowhps\":[-1,24,-1,-1,-1,-1,-1,22,20,-1,-1,-1,-1],\"api_maxhps\":[-1,24,-1,-1,-1,-1,-1,22,20,-1,-1,-1,-1],\"api_midnight_flag\":0,\"api_eSlot\":[[502,-1,-1,-1,-1],[501,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1],[-1,-1,-1,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[27,69,24,39],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_eParam\":[[7,16,7,6],[5,15,6,5],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_search\":[6,5],\"api_formation\":[1,1,1],\"api_stage_flag\":[1,0,0],\"api_kouku\":{\"api_plane_from\":[[-1],[-1]],\"api_stage1\":{\"api_f_count\":0,\"api_f_lostcount\":0,\"api_e_count\":0,\"api_e_lostcount\":0,\"api_disp_seiku\":1,\"api_touch_plane\":[-1,-1]},\"api_stage2\":null,\"api_stage3\":null},\"api_support_flag\":0,\"api_support_info\":null,\"api_opening_flag\":0,\"api_opening_atack\":null,\"api_hourai_flag\":[1,0,0,1],\"api_hougeki1\":{\"api_at_list\":[-1,1,7],\"api_at_type\":[-1,0,0],\"api_df_list\":[-1,[8],[1]],\"api_si_list\":[-1,[2],[502]],\"api_cl_list\":[-1,[1],[0]],\"api_damage\":[-1,[30],[0]]},\"api_hougeki2\":null,\"api_hougeki3\":null,\"api_raigeki\":{\"api_frai\":[-1,1,0,0,0,0,0],\"api_erai\":[-1,1,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0],\"api_edam\":[-1,68,0,0,0,0,0],\"api_fydam\":[-1,68,0,0,0,0,0],\"api_eydam\":[-1,0,0,0,0,0,0],\"api_fcl\":[-1,1,0,0,0,0,0],\"api_ecl\":[-1,0,0,0,0,0,0]}}"));
            var test_midnight = KanColleLib.TransmissionData.api_req_battle_midnight.Battle.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":\"1\",\"api_nowhps\":[-1,13,10,15,-1,-1,-1,90,56,85,9,0,0],\"api_ship_ke\":[-1,511,522,512,508,508,507],\"api_ship_lv\":[-1,1,1,1,1,1,1],\"api_maxhps\":[-1,14,13,24,-1,-1,-1,90,60,85,48,48,39],\"api_eSlot\":[[509,512,525,-1,-1],[505,506,525,525,-1],[519,523,516,-1,-1],[504,513,513,-1,-1],[504,513,513,-1,-1],[506,513,525,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[9,69,0,19],[13,69,0,21],[19,80,0,24]],\"api_eParam\":[[65,0,70,70],[58,42,30,60],[0,0,30,40],[18,48,10,22],[18,48,10,22],[24,28,12,20]],\"api_touch_plane\":[-1,-1],\"api_flare_pos\":[-1,-1],\"api_hougeki\":{\"api_at_list\":[-1,1,2,3],\"api_df_list\":[-1,[9],[8],[8]],\"api_si_list\":[-1,[-1],[127],[15]],\"api_cl_list\":[-1,[1],[1],[1]],\"api_sp_list\":[-1,0,0,0],\"api_damage\":[-1,[60.1],[38],[51]]}}"));
        }

        static void test_combined_battle()
        {
            var test1 = KanColleLib.TransmissionData.api_req_combined_battle.BattleWater.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":\"1\",\"api_ship_ke\":[-1,527,527,622,622,575,575],\"api_ship_lv\":[-1,1,1,1,1,1,1],\"api_nowhps\":[-1,92,77,50,50,45,40,76,76,43,43,35,35],\"api_maxhps\":[-1,92,77,50,50,45,40,76,76,43,43,35,35],\"api_nowhps_combined\":[-1,24,27,27,42,24,43],\"api_maxhps_combined\":[-1,24,30,27,42,24,43],\"api_midnight_flag\":0,\"api_eSlot\":[[505,506,515,525,-1],[505,506,515,525,-1],[502,502,559,-1,-1],[502,502,559,-1,-1],[502,545,542,-1,-1],[502,545,542,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[102,0,90,92],[79,0,89,92],[75,69,59,71],[75,69,59,71],[29,0,22,59],[29,0,21,49]],\"api_eParam\":[[68,48,40,70],[68,48,40,70],[58,88,44,36],[58,88,44,36],[38,60,30,22],[38,60,30,22]],\"api_fParam_combined\":[[33,69,29,39],[49,79,49,49],[36,60,31,41],[59,79,59,59],[39,69,39,39],[63,139,49,63]],\"api_search\":[1,1],\"api_formation\":[\"14\",1,1],\"api_stage_flag\":[1,1,1],\"api_kouku\":{\"api_plane_from\":[[5,6],[-1]],\"api_stage1\":{\"api_f_count\":84,\"api_f_lostcount\":0,\"api_e_count\":0,\"api_e_lostcount\":0,\"api_disp_seiku\":1,\"api_touch_plane\":[59,-1]},\"api_stage2\":{\"api_f_count\":84,\"api_f_lostcount\":2,\"api_e_count\":0,\"api_e_lostcount\":0},\"api_stage3\":{\"api_frai_flag\":[-1,0,0,0,0,0,0],\"api_erai_flag\":[-1,0,1,1,0,1,1],\"api_fbak_flag\":[-1,0,0,0,0,0,0],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0],\"api_edam\":[-1,0,0,34.1,0,0,0]},\"api_stage3_combined\":{\"api_frai_flag\":[-1,0,0,0,0,0,0],\"api_fbak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0]}},\"api_support_flag\":0,\"api_support_info\":null,\"api_opening_flag\":1,\"api_opening_atack\":{\"api_frai\":[-1,0,0,0,0,0,4],\"api_erai\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0],\"api_edam\":[-1,0,0,0,119,0,0],\"api_fydam\":[-1,0,0,0,0,0,119],\"api_eydam\":[-1,0,0,0,0,0,0],\"api_fcl\":[-1,0,0,0,0,0,1],\"api_ecl\":[-1,0,0,0,0,0,0]},\"api_hourai_flag\":[1,1,1,1],\"api_hougeki1\":{\"api_at_list\":[-1,1,8,4,7,2,11,3,9,5,6],\"api_at_type\":[-1,2,0,2,0,6,0,2,0,0,0],\"api_df_list\":[-1,[12,12],[3],[7,7],[2],[11],[5],[11,11],[6],[11],[8]],\"api_si_list\":[-1,[137,137],[505],[90,90],[505],[115,8,8],[502],[50,123],[502],[-1],[-1]],\"api_cl_list\":[-1,[1,1],[0],[1,1],[0],[1],[0],[1,1],[0],[1],[0]],\"api_damage\":[-1,[170,0],[0],[10,4],[0],[4],[0],[2,3],[0],[128],[0]]},\"api_hougeki2\":{\"api_at_list\":[-1,1,7,2,8,3,9,4,5,6],\"api_at_type\":[-1,6,0,6,0,0,0,0,0,0],\"api_df_list\":[-1,[7],[4],[8],[4],[9],[5],[8],[8],[7]],\"api_si_list\":[-1,[115,137,137],[505],[115,8,8],[505],[50],[502],[90],[-1],[-1]],\"api_cl_list\":[-1,[1],[0],[1],[1],[0],[0],[0],[1],[0]],\"api_damage\":[-1,[8],[0],[7.1],[5],[0],[0],[0],[83.1],[0]]},\"api_hougeki3\":{\"api_at_list\":[-1,4,7,6,2,1,3,5],\"api_at_type\":[-1,0,0,0,0,0,0,0],\"api_df_list\":[-1,[7],[4],[9],[7],[7],[7],[7]],\"api_si_list\":[-1,[6],[505],[-1],[-1],[-1],[-1],[-1]],\"api_cl_list\":[-1,[0],[0],[1],[1],[0],[1],[0]],\"api_damage\":[-1,[0],[0],[31],[4],[0],[6],[0]]},\"api_raigeki\":{\"api_frai\":[-1,1,1,1,1,1,1],\"api_erai\":[-1,5,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,13,0],\"api_edam\":[-1,178,0,0,0,0,0],\"api_fydam\":[-1,32,32,0,11,14,89],\"api_eydam\":[-1,13,0,0,0,0,0],\"api_fcl\":[-1,1,1,1,1,1,1],\"api_ecl\":[-1,1,0,0,0,0,0]}}"));
            var test2 = KanColleLib.TransmissionData.api_req_combined_battle.MidnightBattle.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":\"1\",\"api_ship_ke\":[-1,527,595,595,591,575,575],\"api_ship_lv\":[-1,1,1,1,1,1,1],\"api_nowhps\":[-1,92,77,6,45,11,40,0,0,37,0,0,0],\"api_maxhps\":[-1,92,77,50,50,45,40,76,88,88,48,35,35],\"api_nowhps_combined\":[-1,24,27,27,42,11,43],\"api_maxhps_combined\":[-1,24,30,27,42,24,43],\"api_eSlot\":[[505,506,515,525,-1],[505,505,515,525,-1],[505,505,515,525,-1],[550,550,545,525,-1],[502,545,542,-1,-1],[502,545,542,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[102,0,90,92],[79,0,89,92],[75,69,59,71],[75,69,59,71],[29,0,23,59],[29,0,21,49]],\"api_eParam\":[[68,48,40,70],[79,74,79,89],[79,74,79,89],[58,84,88,55],[38,60,30,22],[38,60,30,22]],\"api_fParam_combined\":[[33,69,29,39],[49,79,49,49],[36,60,31,41],[59,79,59,59],[39,69,39,39],[63,139,49,63]],\"api_touch_plane\":[-1,-1],\"api_flare_pos\":[-1,-1],\"api_hougeki\":{\"api_at_list\":[-1,1,2,3],\"api_df_list\":[-1,[9],[9],[9,9]],\"api_si_list\":[-1,[15],[15],[15,58]],\"api_cl_list\":[-1,[0],[0],[1,1]],\"api_sp_list\":[-1,0,0,3],\"api_damage\":[-1,[0],[0],[79,63]]}}"));
            var test3 = KanColleLib.TransmissionData.api_req_combined_battle.Airbattle.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":\"1\",\"api_ship_ke\":[-1,579,523,523,591,516,516],\"api_ship_lv\":[-1,1,1,1,1,1,1],\"api_nowhps\":[-1,96,78,46,67,54,39,96,70,70,48,40,40],\"api_maxhps\":[-1,96,83,50,67,67,45,96,70,70,48,40,40],\"api_nowhps_combined\":[-1,39,50,39,45,96,34],\"api_maxhps_combined\":[-1,39,50,43,50,96,36],\"api_midnight_flag\":1,\"api_eSlot\":[[547,548,549,549,-1],[520,523,516,-1,-1],[520,523,516,-1,-1],[550,550,545,525,-1],[502,515,-1,-1,-1],[502,515,-1,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[139,0,104,118],[98,0,82,95],[75,69,59,71],[64,0,82,76],[57,0,84,75],[29,0,39,59]],\"api_eParam\":[[25,0,50,80],[0,0,15,35],[0,0,15,35],[58,84,88,55],[15,32,14,16],[15,32,14,16]],\"api_fParam_combined\":[[49,84,63,61],[70,98,68,69],[63,139,49,63],[75,69,59,71],[99,36,70,95],[59,99,59,59]],\"api_search\":[1,1],\"api_formation\":[\"13\",3,2],\"api_stage_flag\":[1,1,1],\"api_kouku\":{\"api_plane_from\":[[4,5,6],[7,8,9]],\"api_stage1\":{\"api_f_count\":196,\"api_f_lostcount\":17,\"api_e_count\":240,\"api_e_lostcount\":85,\"api_disp_seiku\":2,\"api_touch_plane\":[115,-1]},\"api_stage2\":{\"api_f_count\":26,\"api_f_lostcount\":3,\"api_e_count\":101,\"api_e_lostcount\":55,\"api_air_fire\":{\"api_idx\":11,\"api_kind\":5,\"api_use_items\":[122,122,106]}},\"api_stage3\":{\"api_frai_flag\":[-1,0,0,0,1,0,0],\"api_erai_flag\":[-1,1,1,0,1,0,0],\"api_fbak_flag\":[-1,0,1,0,0,0,0],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,9,0,5,0,0],\"api_edam\":[-1,0,36,0,0,0,0]},\"api_stage3_combined\":{\"api_frai_flag\":[-1,1,0,0,0,1,0],\"api_fbak_flag\":[-1,0,1,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0]}},\"api_support_flag\":0,\"api_support_info\":null,\"api_stage_flag2\":[1,1,1],\"api_kouku2\":{\"api_plane_from\":[[4,5,6],[7,8,9]],\"api_stage1\":{\"api_f_count\":176,\"api_f_lostcount\":3,\"api_e_count\":100,\"api_e_lostcount\":52,\"api_disp_seiku\":1,\"api_touch_plane\":[54,-1]},\"api_stage2\":{\"api_f_count\":23,\"api_f_lostcount\":8,\"api_e_count\":24,\"api_e_lostcount\":12,\"api_air_fire\":{\"api_idx\":11,\"api_kind\":5,\"api_use_items\":[122,122,106]}},\"api_stage3\":{\"api_frai_flag\":[-1,0,1,1,0,0,0],\"api_erai_flag\":[-1,0,0,0,0,0,1],\"api_fbak_flag\":[-1,0,0,0,0,0,0],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,5,0,0,0,0],\"api_edam\":[-1,0,0,0,0,0,58]},\"api_stage3_combined\":{\"api_frai_flag\":[-1,0,1,0,0,0,0],\"api_fbak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,0,0]}}}"));
            var test4 = KanColleLib.TransmissionData.api_req_combined_battle.Battle.fromDynamic(DynamicJson.Parse("{\"api_deck_id\":\"1\",\"api_ship_ke\":[-1,625,579,579,595,623,623],\"api_ship_lv\":[-1,1,1,1,1,1,1],\"api_nowhps\":[-1,96,83,50,25,67,45,350,96,96,88,46,46],\"api_maxhps\":[-1,96,83,50,67,67,45,350,96,96,88,46,46],\"api_nowhps_combined\":[-1,32,22,43,50,88,34],\"api_maxhps_combined\":[-1,39,50,43,50,96,36],\"api_midnight_flag\":1,\"api_eSlot\":[[550,550,541,554,-1],[547,548,549,549,-1],[547,548,549,549,-1],[505,505,515,525,-1],[502,559,544,-1,-1],[502,559,544,-1,-1]],\"api_eKyouka\":[[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]],\"api_fParam\":[[139,0,104,118],[98,0,82,95],[75,69,59,71],[64,0,82,76],[57,0,84,75],[29,0,39,59]],\"api_eParam\":[[75,88,70,150],[25,0,50,80],[25,0,50,80],[79,74,79,89],[58,76,48,36],[58,76,48,36]],\"api_fParam_combined\":[[49,84,63,61],[70,98,68,69],[63,139,49,63],[75,69,59,71],[99,36,70,95],[59,99,59,59]],\"api_search\":[1,1],\"api_formation\":[\"14\",3,2],\"api_stage_flag\":[1,1,1],\"api_kouku\":{\"api_plane_from\":[[4,5,6],[7,8,9]],\"api_stage1\":{\"api_f_count\":121,\"api_f_lostcount\":17,\"api_e_count\":224,\"api_e_lostcount\":93,\"api_disp_seiku\":0,\"api_touch_plane\":[-1,-1]},\"api_stage2\":{\"api_f_count\":22,\"api_f_lostcount\":10,\"api_e_count\":93,\"api_e_lostcount\":44,\"api_air_fire\":{\"api_idx\":11,\"api_kind\":5,\"api_use_items\":[122,122,106]}},\"api_stage3\":{\"api_frai_flag\":[-1,0,0,0,1,0,0],\"api_erai_flag\":[-1,0,0,0,0,1,0],\"api_fbak_flag\":[-1,0,0,0,0,1,1],\"api_ebak_flag\":[-1,0,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_ecl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,7.1,4],\"api_edam\":[-1,0,0,0,0,0,0]},\"api_stage3_combined\":{\"api_frai_flag\":[-1,0,0,0,1,0,0],\"api_fbak_flag\":[-1,1,0,0,0,0,0],\"api_fcl_flag\":[-1,0,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,5,0,0]}},\"api_support_flag\":0,\"api_support_info\":null,\"api_opening_flag\":1,\"api_opening_atack\":{\"api_frai\":[-1,0,0,5,0,0,0],\"api_erai\":[-1,5,0,0,0,0,0],\"api_fdam\":[-1,0,0,0,0,8,0],\"api_edam\":[-1,0,0,0,0,83,0],\"api_fydam\":[-1,0,0,83,0,0,0],\"api_eydam\":[-1,8,0,0,0,0,0],\"api_fcl\":[-1,0,0,1,0,0,0],\"api_ecl\":[-1,1,0,0,0,0,0]},\"api_hourai_flag\":[1,1,1,1],\"api_hougeki1\":{\"api_at_list\":[-1,5,10,4,7,3,12,2,9,6,1],\"api_at_type\":[-1,0,0,0,0,0,0,0,0,0,0],\"api_df_list\":[-1,[8],[3],[9],[1],[8],[1],[9],[5],[12],[12]],\"api_si_list\":[-1,[8],[505],[50],[550],[-1],[502],[6],[-1],[122],[-1]],\"api_cl_list\":[-1,[1],[1],[0],[1],[2],[0],[0],[2],[0],[0]],\"api_damage\":[-1,[72],[4],[0],[3],[26],[0],[0.1],[29],[0],[0]]},\"api_raigeki\":{\"api_frai\":[-1,4,0,6,3,4,4],\"api_erai\":[-1,2,0,0,2,0,2],\"api_fdam\":[-1,0,1,0,0,0,0],\"api_edam\":[-1,0,0,11,24,0,85],\"api_fydam\":[-1,10,0,85,11,10,4],\"api_eydam\":[-1,0,0,0,0,0,1],\"api_fcl\":[-1,1,0,1,1,1,1],\"api_ecl\":[-1,0,0,0,0,0,1]},\"api_hougeki2\":{\"api_at_list\":[-1,1,10,2,7,3],\"api_at_type\":[-1,0,0,0,0,0],\"api_df_list\":[-1,[9],[2],[10],[5],[7]],\"api_si_list\":[-1,[128],[505],[8],[550],[90]],\"api_cl_list\":[-1,[1],[1],[1],[1],[0]],\"api_damage\":[-1,[88],[5],[38],[6],[0]]},\"api_hougeki3\":{\"api_at_list\":[-1,1,7,2,10,3],\"api_at_type\":[-1,0,0,0,0,0],\"api_df_list\":[-1,[7],[3],[7],[6],[7]],\"api_si_list\":[-1,[128],[550],[8],[505],[90]],\"api_cl_list\":[-1,[1],[1],[1],[1],[2]],\"api_damage\":[-1,[14],[4],[45],[2],[30]]}}"));
            var testresult = KanColleLib.TransmissionData.api_req_combined_battle.Battleresult.fromDynamic(DynamicJson.Parse("{\"api_ship_id\":[-1,625,579,579,595,623,623],\"api_win_rank\":\"S\",\"api_get_exp\":250,\"api_mvp\":1,\"api_mvp_combined\":4,\"api_member_lv\":102,\"api_member_exp\":2051651,\"api_get_base_exp\":350,\"api_get_ship_exp\":[-1,1260,420,420,420,420,420],\"api_get_ship_exp_combined\":[-1,630,420,420,840,420,420],\"api_get_exp_lvup\":[[446906,458000],[693637,701500],[295491,296200],[795318,851500],[511987,527000],[190299,195800]],\"api_get_exp_lvup_combined\":[[1220567,1223000],[481551,491500],[847033,851500],[465104,474500],[941314,1000000],[609176,631500]],\"api_dests\":6,\"api_destsf\":1,\"api_quest_name\":\"\u5357\u592a\u5e73\u6d0b\u6d77\u57df\",\"api_quest_level\":12,\"api_enemy_info\":{\"api_level\":\"\",\"api_rank\":\"\",\"api_deck_name\":\"\u6df1\u6d77\u4efb\u52d9\u90e8\u968a\u65d7\u8266\u8266\u968a\"},\"api_first_clear\":0,\"api_get_flag\":[0,1,0],\"api_get_ship\":{\"api_ship_id\":125,\"api_ship_type\":\"\u91cd\u5de1\u6d0b\u8266\",\"api_ship_name\":\"\u718a\u91ce\",\"api_ship_getmes\":\"\u3054\u304d\u3052\u3093\u3088\u3046\u3001<br>\u308f\u305f\u304f\u3057\u304c\u91cd\u5de1\u3001\u718a\u91ce\u3067\u3059\u308f\uff01\"},\"api_get_exmap_rate\":0,\"api_get_exmap_useitem_id\":0,\"api_escape_flag\":0,\"api_escape\":null}"));
        }
    }
}
