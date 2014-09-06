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

        public static CharacterData fromKanColleLib(KanColleLib.TransmissionData.api_start2.start2.MstShip data)
        {
            return new CharacterData()
            {
                name = data.name,
                name_yomi = data.yomi,
                shiptype = data.stype
            };
        }
    }
}
