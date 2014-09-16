using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class FleetsViewModel : ViewModelBase
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

        public FleetsViewModel(int id)
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
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems.Count > 0)
                        Ships.Insert(e.NewStartingIndex, new ShipViewModel() { shipid = model.fleetdata[id].ships[e.NewStartingIndex] });
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    Ships.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    Ships.RemoveAt(e.OldStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    Ships[e.OldStartingIndex] = new ShipViewModel() { shipid = model.fleetdata[id].ships[e.OldStartingIndex] };
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:

                    break;
            }
            System.Diagnostics.Debug.Write("id" + id + " ship: ");
            foreach (var i in Ships)
            {
                System.Diagnostics.Debug.Write(i.shipid + ",");
            }

            System.Diagnostics.Debug.WriteLine("");
        }
    }
}
