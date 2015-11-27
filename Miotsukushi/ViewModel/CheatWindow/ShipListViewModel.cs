using System.Collections.ObjectModel;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class ShipListViewModel : ViewModelBase
    {
        public ObservableCollection<ShipListItemViewModel> List { get; private set; }  = new ObservableCollection<ShipListItemViewModel>();
    }
}
