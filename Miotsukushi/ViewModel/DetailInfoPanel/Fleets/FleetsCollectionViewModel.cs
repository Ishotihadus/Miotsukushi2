using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Fleets
{
    class FleetsCollectionViewModel
    {
        FleetViewModel[] vm = { new FleetViewModel(0), new FleetViewModel(1), new FleetViewModel(2), new FleetViewModel(3) };

        public FleetViewModel this[int index]
        {
            get
            {
                return vm[index];
            }
        }
    }
}
