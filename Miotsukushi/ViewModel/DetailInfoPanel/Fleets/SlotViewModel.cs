using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class SlotViewModel : ViewModelBase
    {
        public int slotid;

        #region プロパティ定義

        private Brush _ItemTypeBrush;
        public Brush ItemTypeBrush
        {
            get
            {
                return _ItemTypeBrush;
            }

            set
            {
                if (_ItemTypeBrush != value)
                {
                    _ItemTypeBrush = value;
                    OnPropertyChanged("ItemTypeBrush");
                }
            }
        }

        private string _ItemType;
        public string ItemType
        {
            get
            {
                return _ItemType;
            }

            set
            {
                if (_ItemType != value)
                {
                    _ItemType = value;
                    OnPropertyChanged("ItemType");
                }
            }
        }

        private string _ItemName;
        public string ItemName
        {
            get
            {
                return _ItemName;
            }

            set
            {
                if (_ItemName != value)
                {
                    _ItemName = value;
                    OnPropertyChanged("ItemName");
                }
            }
        }

        private bool _IsEmpty;
        public bool IsEmpty
        {
            get
            {
                return _IsEmpty;
            }

            set
            {
                if (_IsEmpty != value)
                {
                    _IsEmpty = value;
                    OnPropertyChanged("IsEmpty");
                }
            }
        }

        private int _OnSlotCount;
        public int OnSlotCount
        {
            get
            {
                return _OnSlotCount;
            }

            set
            {
                if (_OnSlotCount != value)
                {
                    _OnSlotCount = value;
                    OnPropertyChanged("OnSlotCount");
                }
            }
        }


        #endregion
    }
}
