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
        public string name;

        /// <summary>
        /// 名前の読み
        /// </summary>
        public string name_yomi;

        /// <summary>
        /// 艦種ID
        /// </summary>
        public int shiptype;

        /// <summary>
        /// 建造時間（分）
        /// </summary>
        public int buildingtime;

        /// <summary>
        /// 燃料
        /// </summary>
        public int fuel_max;

        /// <summary>
        /// 弾薬
        /// </summary>
        public int ammo_max;

        public static CharacterData fromKanColleLib(KanColleLib.TransmissionData.api_start2.start2.MstShip data)
        {
            return new CharacterData()
            {
                name = data.name,
                name_yomi = data.yomi,
                shiptype = data.stype,
                buildingtime = data.buildtime.HasValue ? data.buildtime.Value : 0,
                fuel_max = data.fuel_max.HasValue ? data.fuel_max.Value : 0,
                ammo_max = data.bull_max.HasValue ? data.bull_max.Value : 0,
            };
        }
    }
}
