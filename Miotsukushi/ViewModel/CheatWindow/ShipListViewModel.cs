using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using Miotsukushi.Model;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipListViewModel : ViewModelBase
    {
        public ObservableCollection<ShipListItemViewModel> List { get; }  = new ObservableCollection<ShipListItemViewModel>();

        private readonly KanColleModel _kcmodel;
        private readonly ObservableCollection<ShipData> _modelCollection;

        public ShipListViewModel()
        {
            _kcmodel = MainModel.Current.KancolleModel;

            _modelCollection = _kcmodel.Shipdata;
            MakeTable();

            _modelCollection.CollectionChanged += _modelCollection_CollectionChanged;
        }

        private void MakeTable()
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(MakeTable);
                return;
            }

            foreach (var shipData in _modelCollection)
                List.Add(new ShipListItemViewModel(_kcmodel, shipData));
        }

        private void _modelCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => _modelCollection_CollectionChanged(sender, e));
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    List.Insert(e.NewStartingIndex, new ShipListItemViewModel(_kcmodel, _modelCollection[e.NewStartingIndex]));
                    break;
                case NotifyCollectionChangedAction.Move:
                    List.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    List.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
