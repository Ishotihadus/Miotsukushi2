using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class SlotData
    {
        public int id;
        public int itemid;

        /// <summary>
        /// 装備アイテムの情報を直接取得する
        /// </summary>
        public ItemData iteminfo
        {
            get
            {
                if (MainModel.Current != null && MainModel.Current.kancolleModel != null && MainModel.Current.kancolleModel.slotitemmaster != null &&
                    MainModel.Current.kancolleModel.slotitemmaster.ContainsKey(itemid))
                    return MainModel.Current.kancolleModel.slotitemmaster[itemid];
                else
                    return null;
            }
        }
    }
}
