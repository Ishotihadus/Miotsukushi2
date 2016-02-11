using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
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
            BindingOperations.EnableCollectionSynchronization(List, new object());

            _kcmodel = MainModel.Current.KancolleModel;

            _modelCollection = _kcmodel.Shipdata;
            MakeTable();

            _modelCollection.CollectionChanged += _modelCollection_CollectionChanged;
        }

        private void MakeTable()
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(MakeTable);

            foreach (var shipData in _modelCollection)
                List.Add(new ShipListItemViewModel(_kcmodel, shipData));
        }

        private void AddToTable(ShipData shipData, int pos)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(() => AddToTable(shipData, pos));

            List.Insert(pos, new ShipListItemViewModel(_kcmodel, shipData));
        }

        private void MoveTable(int from, int to)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(() => MoveTable(from, to));

            List.Move(from, to);
        }

        private void RemoveTable(int index)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(() => RemoveTable(index));

            List.RemoveAt(index);
        }

        private void _modelCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddToTable(_modelCollection[e.NewStartingIndex], e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    MoveTable(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveTable(e.OldStartingIndex);
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
