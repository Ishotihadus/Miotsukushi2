using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class FleetViewModel : ViewModelBase
    {
        private int id;
        private KanColleModel model;

        private ObservableCollection<ShipViewModel> _Ships = new ObservableCollection<ShipViewModel>();
        public ObservableCollection<ShipViewModel> Ships
        {
            get
            {
                return _Ships;
            }

            set
            {
                if (_Ships != value)
                {
                    _Ships = value;
                    OnPropertyChanged("Ships");
                }
            }
        }

        public FleetViewModel(int id)
        {
            this.id = id;
            model = Model.MainModel.Current.kancolleModel;
            model.fleetdata.ItemAdded += fleetdata_ItemAdded;
        }

        void fleetdata_ItemAdded(object sender, EventArgs e)
        {
            if (model.fleetdata.Count > id)
            {
                model.fleetdata[id].ships.CollectionChanged += ships_CollectionChanged;
                model.fleetdata.ItemAdded -= fleetdata_ItemAdded;
            }
        }

        void ships_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // なんかしらないけどUIスレッドとは別のところでObservableCollectionを死ぬとか云々
            App.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                // あとでDisposeするViewModelを指定してあげる（たぶんこれがいい）
                List<ShipViewModel> disposeitem = new List<ShipViewModel>();
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        if (e.NewItems.Count > 0)
                            Ships.Insert(e.NewStartingIndex, new ShipViewModel(model.fleetdata[id].ships[e.NewStartingIndex]));
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        Ships.Move(e.OldStartingIndex, e.NewStartingIndex);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        disposeitem.Add(Ships[e.OldStartingIndex]);
                        Ships.RemoveAt(e.OldStartingIndex);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        disposeitem.Add(Ships[e.OldStartingIndex]);
                        Ships[e.OldStartingIndex] = new ShipViewModel(model.fleetdata[id].ships[e.OldStartingIndex]);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        foreach (var d in Ships)
                        {
                            disposeitem.Add(d);
                        }
                        for (int i = 0; i < model.fleetdata[id].ships.Count; i++)
                        {
                            if (i < Ships.Count)
                                Ships[i] = new ShipViewModel(model.fleetdata[id].ships[i]);
                            else
                                Ships.Add(new ShipViewModel(model.fleetdata[id].ships[i]));
                        }
                        break;
                }
                foreach (var d in disposeitem)
                {
                    d.Dispose();
                }
                disposeitem = null;
            });
        }
    }
}
