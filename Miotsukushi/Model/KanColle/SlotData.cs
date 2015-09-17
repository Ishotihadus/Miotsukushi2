using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class SlotData : NotifyModelBase
    {
        public int Id;
        public int Itemid;

        private int? _alv;

        /// <summary>
        /// 艦載機熟練度
        /// </summary>
        public int? Alv
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
                    OnPropertyChanged(() => Alv);
                }
            }
        }


        /// <summary>
        /// 装備アイテムの情報を直接取得する
        /// </summary>
        public ItemData Iteminfo
        {
            get
            {
                if (MainModel.Current != null && MainModel.Current.KancolleModel != null && MainModel.Current.KancolleModel.Slotitemmaster != null &&
                    MainModel.Current.KancolleModel.Slotitemmaster.ContainsKey(Itemid))
                    return MainModel.Current.KancolleModel.Slotitemmaster[Itemid];
                else
                    return null;
            }
        }
    }
}
