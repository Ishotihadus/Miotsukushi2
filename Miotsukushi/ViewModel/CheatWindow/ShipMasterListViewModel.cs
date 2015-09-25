﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipMasterListViewModel : ViewModelBase
    {
        readonly ObservableCollection<ShipMasterItem> _shipList;

        public ICollectionView ShipListCollection { get; }

        private string _searchText = "";
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(() => SearchText);
                    ShipListCollection.Refresh();
                }
            }
        }

        public ShipMasterListViewModel()
        {
            _shipList = new ObservableCollection<ShipMasterItem>();
            BindingOperations.EnableCollectionSynchronization(_shipList, new object());
            var kcmodel = Model.MainModel.Current.KancolleModel;
            if (kcmodel.InitializeCompleted)
                MakeTable();
            else
                kcmodel.InitializeComplete += (_, __) => MakeTable();

            ShipListCollection = CollectionViewSource.GetDefaultView(_shipList);
            
            ShipListCollection.Filter += o =>
            {
                var shipMasterItem = o as ShipMasterItem;
                if (shipMasterItem?.Name != null)
                {
                    return shipMasterItem.Name.Contains(SearchText);
                }
                return true;
            };
        }

        private void MakeTable()
        {
            var kcmodel = Model.MainModel.Current.KancolleModel;
            foreach (var c in kcmodel.Charamaster)
                _shipList.Add(new ShipMasterItem
                {
                    Id = c.Key,
                    Name = c.Value.Name,
                    ShipType = kcmodel.Shiptypemaster.ContainsKey(c.Value.Shiptype) ? kcmodel.Shiptypemaster[c.Value.Shiptype].Name : "不明",
                    ShipTypeId = c.Value.Shiptype,
                    Yomi = c.Value.NameYomi,
                    ResourceId = c.Value.ResourceId,
                    AfterShipLv = c.Value.AftershipLv,
                    AfterShipName = c.Value.AftershipId.HasValue ? kcmodel.Charamaster[c.Value.AftershipId.Value].Name : null
                });
        }
    }

}
