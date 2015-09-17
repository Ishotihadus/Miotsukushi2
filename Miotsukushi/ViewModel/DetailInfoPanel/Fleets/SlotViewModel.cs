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
        public Model.KanColle.SlotData slotmodel;

        #region プロパティ定義

        private Color _ItemTypeColor;
        public Color ItemTypeColor
        {
            get
            {
                return _ItemTypeColor;
            }

            set
            {
                if (_ItemTypeColor != value)
                {
                    _ItemTypeColor = value;
                    OnPropertyChanged(() => ItemTypeColor);
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
                    OnPropertyChanged(() => ItemType);
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
                    OnPropertyChanged(() => ItemName);
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
                    OnPropertyChanged(() => IsEmpty);
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
                    OnPropertyChanged(() => OnSlotCount);
                }
            }
        }

        private int _ALV;
        public int ALV
        {
            get
            {
                return _ALV;
            }

            set
            {
                if (_ALV != value)
                {
                    _ALV = value;
                    OnPropertyChanged(() => ALV);
                }
            }
        }


        #endregion

        public SlotViewModel(int slotid)
        {
            var model = Model.MainModel.Current.KancolleModel;
            this.slotid = slotid;

            slotmodel = model.Slotdata.FirstOrDefault(_ => _.Id == slotid);

            if (slotmodel == null)
            {
                IsEmpty = true;
                ItemName = "空き";
                ALV = 0;
                ItemType = "空き";
                ItemTypeColor = System.Windows.Media.Colors.Transparent;
            }
            else
            {
                ALV = slotmodel.Alv.HasValue ? slotmodel.Alv.Value : 0;

                var slotitemdata = slotmodel.Iteminfo;
                if (slotitemdata != null)
                {
                    ItemTypeColor = Tools.KanColleTools.GetSlotItemEquipTypeColor(slotitemdata.TypeEquiptype);
                    ItemName = slotitemdata.Name;
                    if (model.SlotitemEquiptypemaster.ContainsKey(slotitemdata.TypeEquiptype))
                        ItemType = model.SlotitemEquiptypemaster[slotitemdata.TypeEquiptype].Name;
                    else
                        ItemType = "不明";
                }
                else
                {
                    ItemTypeColor = System.Windows.Media.Colors.Transparent;
                    ItemName = "不明";
                    ItemType = "不明";
                }

                slotmodel.PropertyChanged += Slotmodel_PropertyChanged;
            }
        }

        private void Slotmodel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Alv":
                    ALV = slotmodel.Alv.HasValue ? slotmodel.Alv.Value : 0;
                    break;
            }
        }
    }
}
