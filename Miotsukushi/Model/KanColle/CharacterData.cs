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

        /// <summary>
        /// 改造レベル
        /// </summary>
        public int? aftership_lv;

        /// <summary>
        /// 改造後ID
        /// </summary>
        public int? aftership_id;

        /// <summary>
        /// リソースのファイル名
        /// </summary>
        public string resource_id;

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
                aftership_lv = data.afterlv.HasValue && data.afterlv.Value != 0 ? data.afterlv.Value : (int?)null,
                aftership_id = data.aftershipid.HasValue && data.afterlv.Value != 0 ? data.aftershipid.Value : (int?)null
            };
        }
    }
}
