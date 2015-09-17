using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class ItemData
    {
        public string Name;

        /// <summary>
        /// よくわかんない
        /// </summary>
        public int Type;


        public int TypeCardtype;
        public int TypeEquiptype;
        public int TypeIcontype;
        

        /// <summary>
        /// 火力
        /// </summary>
        public int Firepower;

        /// <summary>
        /// 雷装
        /// </summary>
        public int Torpedo;

        /// <summary>
        /// 爆装
        /// </summary>
        public int Bombing;

        /// <summary>
        /// 対空
        /// </summary>
        public int AntiAir;

        /// <summary>
        /// 対潜
        /// </summary>
        public int AntiSubmarines;

        /// <summary>
        /// 索敵
        /// </summary>
        public int Reconnaissance;

        /// <summary>
        /// 命中
        /// </summary>
        public int HitRate;

        /// <summary>
        /// 回避
        /// </summary>
        public int Evasion;

        /// <summary>
        /// 装甲
        /// </summary>
        public int Armor;

        /// <summary>
        /// 射程
        /// </summary>
        public int Range;
    }
}
