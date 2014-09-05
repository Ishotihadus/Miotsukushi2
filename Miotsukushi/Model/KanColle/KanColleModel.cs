using System;
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
        Dictionary<int, CharacterData> charamaster;
        Dictionary<int, ShiptypeData> shiptypemaster;
        Dictionary<int, MissionData> missionmaster;

        public KanColleModel()
        {
            FiddlerApplication.Startup(0, FiddlerCoreStartupFlags.ChainToUpstreamGateway);
            URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", FiddlerApplication.oProxy.ListenPort), "<local>");

            kclib = new KanColleNotifier();
            kclib.GetStart2 += kclib_GetStart2;
        }

        async void kclib_GetStart2(object sender, KanColleLib.TransmissionRequest.RequestBase request, KanColleLib.TransmissionData.Svdata<KanColleLib.TransmissionData.api_start2.Start2> response)
        {
            await Task.Run(() =>
            {
                charamaster = new Dictionary<int, CharacterData>();
                foreach (var chara in response.data.mst_ship)
                {
                    charamaster.Add(chara.id, new CharacterData()
                    {
                        name = chara.name,
                        name_yomi = chara.yomi,
                        shiptype = chara.stype
                    });
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
                    missionmaster.Add(mission.id, new MissionData() { name = mission.name });
                }
                System.Diagnostics.Debug.WriteLine("Get: api_start2/mst_mission");
            });
        }
    }
}
