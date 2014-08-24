using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.start2;

namespace KanColleLib.TransmissionData
{
    /// <summary>
    /// /kcsapi/api_start2
    /// </summary>
    public class Start2
    {
        public List<MstShip> mst_ship;
        public List<MstShipgraph> mst_shipgraph;
        public List<MstSlotitemEquiptype> mst_slotitem_equiptype;
        public List<MstStype> mst_stype;
        public List<MstSlotitem> mst_slotitem;
        public List<MstSlotitemgraph> mst_slotitemgraph;
        public List<MstFurniture> mst_furniture;
        public List<MstFurnituregraph> mst_furnituregraph;
        public List<MstUseitem> mst_useitem;
        public List<MstPayitem> mst_payitem;
        // mst_item_shopは実用性低いので未実装
        public List<MstMaparea> mst_maparea;
        public List<MstMapinfo> mst_mapinfo;
        public List<MstMapBGM> mst_mapbgm;
        public List<MstMission> mst_mission;
        // mst_constは実用性低いので未実装
        // mst_shipupgradeも実用性低いので未実装

        /// <summary>
        /// /kcsapi/api_start2.api_data
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Start2 fromDynamic(dynamic json)
        {
            Start2 start2 = new Start2();

            start2.mst_ship = new List<MstShip>();
            foreach (var data in json.api_mst_ship)
                start2.mst_ship.Add(MstShip.fromDynamic(data));

            start2.mst_shipgraph = new List<MstShipgraph>();
            foreach (var data in json.api_mst_shipgraph)
                start2.mst_shipgraph.Add(MstShipgraph.fromDynamic(data));

            start2.mst_slotitem_equiptype = new List<MstSlotitemEquiptype>();
            foreach (var data in json.api_mst_slotitem_equiptype)
                start2.mst_slotitem_equiptype.Add(MstSlotitemEquiptype.fromDynamic(data));

            start2.mst_stype = new List<MstStype>();
            foreach (var data in json.api_mst_stype)
                start2.mst_stype.Add(MstStype.fromDynamic(data));

            start2.mst_slotitem = new List<MstSlotitem>();
            foreach (var data in json.api_mst_slotitem)
                start2.mst_slotitem.Add(MstSlotitem.fromDynamic(data));

            start2.mst_slotitemgraph = new List<MstSlotitemgraph>();
            foreach (var data in json.api_mst_slotitemgraph)
                start2.mst_slotitemgraph.Add(MstSlotitemgraph.fromDynamic(data));

            start2.mst_furniture = new List<MstFurniture>();
            foreach (var data in json.api_mst_furniture)
                start2.mst_furniture.Add(MstFurniture.fromDynamic(data));

            start2.mst_furnituregraph = new List<MstFurnituregraph>();
            foreach (var data in json.api_mst_furnituregraph)
                start2.mst_furnituregraph.Add(MstFurnituregraph.fromDynamic(data));

            start2.mst_useitem = new List<MstUseitem>();
            foreach (var data in json.api_mst_useitem)
                start2.mst_useitem.Add(MstUseitem.fromDynamic(data));

            start2.mst_payitem = new List<MstPayitem>();
            foreach (var data in json.api_mst_payitem)
                start2.mst_payitem.Add(MstPayitem.fromDynamic(data));

            start2.mst_maparea = new List<MstMaparea>();
            foreach (var data in json.api_mst_maparea)
                start2.mst_maparea.Add(MstMaparea.fromDynamic(data));

            start2.mst_mapinfo = new List<MstMapinfo>();
            foreach (var data in json.api_mst_mapinfo)
                start2.mst_mapinfo.Add(MstMapinfo.fromDynamic(data));

            start2.mst_mapbgm = new List<MstMapBGM>();
            foreach (var data in json.api_mst_mapbgm)
                start2.mst_mapbgm.Add(MstMapBGM.fromDynamic(data));

            start2.mst_mission = new List<MstMission>();
            foreach (var data in json.api_mst_mission)
                start2.mst_mission.Add(MstMission.fromDynamic(data));

            return start2;
        }
    }
}
