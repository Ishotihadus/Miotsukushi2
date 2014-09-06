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
        Dictionary<int, CharacterData> charamaster = new Dictionary<int,CharacterData>();
        Dictionary<int, ShiptypeData> shiptypemaster = new Dictionary<int,ShiptypeData>();
        Dictionary<int, MissionData> missionmaster = new Dictionary<int,MissionData>();
        ObservableCollection<ShipData> shipdata = new ObservableCollection<ShipData>();

        public KanColleModel()
        {
            FiddlerApplication.Startup(0, FiddlerCoreStartupFlags.ChainToUpstreamGateway);
            URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", FiddlerApplication.oProxy.ListenPort), "<local>");

            kclib = new KanColleNotifier();
            kclib.GetStart2 += kclib_GetStart2;
            kclib.GetPortPort += kclib_GetPortPort;
            kclib.GetGetmemberShip2 += kclib_GetGetmemberShip2;
            kclib.GetGetmemberShip3 += kclib_GetGetmemberShip3;
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
            });

            await Task.Run(() =>
            {
                shiptypemaster = new Dictionary<int, ShiptypeData>();
                foreach (var stype in response.data.mst_stype)
                {
                    shiptypemaster.Add(stype.id, new ShiptypeData() { name = stype.name });
                }
                System.Diagnostics.Debug.WriteLine("Get: api_start2/mst_stype");
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
    }
}
