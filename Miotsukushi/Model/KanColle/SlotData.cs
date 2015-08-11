using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class SlotData : NotifyModelBase
    {
        public int id;
        public int itemid;

        private int? _alv;

        /// <summary>
        /// 艦載機熟練度
        /// </summary>
        public int? alv
        {
            get
            {
                return _alv;
            }

            set
            {
                if (_alv != value)
                {
                    _alv = value;
                    OnPropertyChanged(() => alv);
                }
            }
        }


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
