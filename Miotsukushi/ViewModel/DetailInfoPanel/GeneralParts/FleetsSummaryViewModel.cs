using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.GeneralParts
{
    class FleetsSummaryViewModel : ViewModelBase
    {
        public FleetSummaryViewModel[] FleetSummaryViewModel = { new GeneralParts.FleetSummaryViewModel(0), new GeneralParts.FleetSummaryViewModel(1),
            new GeneralParts.FleetSummaryViewModel(2), new GeneralParts.FleetSummaryViewModel(3)};
    }
}
