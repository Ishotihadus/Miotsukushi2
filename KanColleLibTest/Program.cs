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
            test_get_member();
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
            string api_material = System.IO.File.ReadAllText("api_get_member/material.txt");
            var material = KanColleLib.TransmissionData.api_get_member.Material.fromDynamic(DynamicJson.Parse(api_material).api_data);

            string api_deck = System.IO.File.ReadAllText("api_get_member/deck.txt");
            var deck = KanColleLib.TransmissionData.api_get_member.Deck.fromDynamic(DynamicJson.Parse(api_deck).api_data);

            string api_kdock = System.IO.File.ReadAllText("api_get_member/kdock.txt");
            var kdock = KanColleLib.TransmissionData.api_get_member.KDock.fromDynamic(DynamicJson.Parse(api_kdock).api_data);

            string api_basic = System.IO.File.ReadAllText("api_get_member/basic.txt");
            var basic = KanColleLib.TransmissionData.api_get_member.Basic.fromDynamic(DynamicJson.Parse(api_basic).api_data);

            string api_furniture = System.IO.File.ReadAllText("api_get_member/furniture.txt");
            var furniture = KanColleLib.TransmissionData.api_get_member.Furniture.fromDynamic(DynamicJson.Parse(api_furniture).api_data);

            string api_mapcell = System.IO.File.ReadAllText("api_get_member/mapcell.txt");
            var mapcell = KanColleLib.TransmissionData.api_get_member.Mapcell.fromDynamic(DynamicJson.Parse(api_mapcell).api_data);

            string api_mapinfo = System.IO.File.ReadAllText("api_get_member/mapinfo.txt");
            var mapinfo = KanColleLib.TransmissionData.api_get_member.Mapinfo.fromDynamic(DynamicJson.Parse(api_mapinfo).api_data);

            string api_mission = System.IO.File.ReadAllText("api_get_member/mission.txt");
            var mission = KanColleLib.TransmissionData.api_get_member.Mission.fromDynamic(DynamicJson.Parse(api_mission).api_data);

            string api_ndock = System.IO.File.ReadAllText("api_get_member/ndock.txt");
            var ndock = KanColleLib.TransmissionData.api_get_member.NDock.fromDynamic(DynamicJson.Parse(api_ndock).api_data);

            string api_ship2 = System.IO.File.ReadAllText("api_get_member/ship2.txt");
            var ship2 = KanColleLib.TransmissionData.api_get_member.Ship2.fromDynamic(DynamicJson.Parse(api_ship2).api_data);

            string api_unsetslot = System.IO.File.ReadAllText("api_get_member/unsetslot.txt");
            var unsetslot = KanColleLib.TransmissionData.api_get_member.Unsetslot.fromDynamic(DynamicJson.Parse(api_unsetslot).api_data);

            string api_ship3 = System.IO.File.ReadAllText("api_get_member/ship3.txt");
            var ship3 = KanColleLib.TransmissionData.api_get_member.Ship3.fromDynamic(DynamicJson.Parse(api_ship3).api_data);

            string api_ship3_2 = System.IO.File.ReadAllText("api_get_member/ship3_2.txt");
            var ship3_2 = KanColleLib.TransmissionData.api_get_member.Ship3.fromDynamic(DynamicJson.Parse(api_ship3_2).api_data);

            string api_slot_item = System.IO.File.ReadAllText("api_get_member/slot_item.txt");
            var slot_item = KanColleLib.TransmissionData.api_get_member.SlotItem.fromDynamic(DynamicJson.Parse(api_slot_item).api_data);

            string api_questlist = System.IO.File.ReadAllText("api_get_member/questlist.txt");
            var questlist = KanColleLib.TransmissionData.api_get_member.Questlist.fromDynamic(DynamicJson.Parse(api_questlist).api_data);
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
    }
}
