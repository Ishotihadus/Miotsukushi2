using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class ItemData
    {
        public string name;

        /// <summary>
        /// よくわかんない
        /// </summary>
        public int type;


        public int type_cardtype;
        public int type_equiptype;
        public int type_icontype;
        

        /// <summary>
        /// 火力
        /// </summary>
        public int firepower;

        /// <summary>
        /// 雷装
        /// </summary>
        public int torpedo;

        /// <summary>
        /// 爆装
        /// </summary>
        public int bombing;

        /// <summary>
        /// 対空
        /// </summary>
        public int anti_air;

        /// <summary>
        /// 対潜
        /// </summary>
        public int anti_submarines;

        /// <summary>
        /// 索敵
        /// </summary>
        public int reconnaissance;

        /// <summary>
        /// 命中
        /// </summary>
        public int hit_rate;

        /// <summary>
        /// 回避
        /// </summary>
        public int evasion;

        /// <summary>
        /// 装甲
        /// </summary>
        public int armor;

        /// <summary>
        /// 射程
        /// </summary>
        public int range;
    }
}
