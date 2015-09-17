using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class CharacterData
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name;

        /// <summary>
        /// 名前の読み
        /// </summary>
        public string NameYomi;

        /// <summary>
        /// 艦種ID
        /// </summary>
        public int Shiptype;

        /// <summary>
        /// 建造時間（分）
        /// </summary>
        public int Buildingtime;

        /// <summary>
        /// 燃料
        /// </summary>
        public int FuelMax;

        /// <summary>
        /// 弾薬
        /// </summary>
        public int AmmoMax;

        /// <summary>
        /// 改造レベル
        /// </summary>
        public int? AftershipLv;

        /// <summary>
        /// 改造後ID
        /// </summary>
        public int? AftershipId;

        /// <summary>
        /// リソースのファイル名
        /// </summary>
        public string ResourceId;

        /// <summary>
        /// スロット数
        /// </summary>
        public int SlotCount;

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed;

        public static CharacterData FromKanColleLib(KanColleLib.TransmissionData.api_start2.start2.MstShip data)
        {
            return new CharacterData()
            {
                Name = data.name,
                NameYomi = data.yomi,
                Shiptype = data.stype,
                Buildingtime = data.buildtime ?? 0,
                FuelMax = data.fuel_max ?? 0,
                AmmoMax = data.bull_max ?? 0,
                AftershipLv = data.afterlv.HasValue && data.afterlv.Value != 0 ? data.afterlv.Value : (int?)null,
                AftershipId = data.afterlv.HasValue && data.aftershipid.HasValue && data.afterlv.Value != 0 ? data.aftershipid.Value : (int?)null,
                SlotCount = data.slot_num ?? 0,
                Speed = data.soku ?? 0
            };
        }
    }
}
