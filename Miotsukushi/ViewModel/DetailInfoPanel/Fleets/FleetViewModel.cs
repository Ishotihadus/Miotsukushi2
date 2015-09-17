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

        public ObservableCollection<ShipViewModel> Ships { get; set; }

        public FleetViewModel(int id)
        {
            Ships = new ObservableCollection<ShipViewModel>();
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(Ships, new object());

            this.id = id;
            model = Model.MainModel.Current.KancolleModel;
            model.Fleetdata.ExListChanged += Fleetdata_ExListChanged;
        }

        private void Fleetdata_ExListChanged(object sender, Model.ExListChangedEventArgs e)
        {
            if(e.ChangeType == Model.ExListChangedEventArgs.ChangeTypeEnum.Added && e.ChangedIndex == id)
            {
                model.Fleetdata[id].Ships.CollectionChanged += ships_CollectionChanged;
                model.Fleetdata.ExListChanged -= Fleetdata_ExListChanged;
            }
        }

        void ships_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems.Count > 0)
                        Ships.Insert(e.NewStartingIndex, new ShipViewModel(model.Fleetdata[id].Ships[e.NewStartingIndex]));
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    Ships.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    Ships.RemoveAt(e.OldStartingIndex);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    Ships[e.OldStartingIndex] = new ShipViewModel(model.Fleetdata[id].Ships[e.OldStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    for (var i = 0; i < model.Fleetdata[id].Ships.Count; i++)
                    {
                        if (i < Ships.Count)
                            Ships[i] = new ShipViewModel(model.Fleetdata[id].Ships[i]);
                        else
                            Ships.Add(new ShipViewModel(model.Fleetdata[id].Ships[i]));
                    }
                    break;
            }
        }
    }
}
