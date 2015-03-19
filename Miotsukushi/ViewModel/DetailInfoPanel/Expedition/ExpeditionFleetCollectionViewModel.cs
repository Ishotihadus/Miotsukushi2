using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Expedition
{
    class ExpeditionFleetCollectionViewModel
    {
        ExpeditionFleetViewModel[] vm = { new ExpeditionFleetViewModel(1), new ExpeditionFleetViewModel(2), new ExpeditionFleetViewModel(3) };

        public ExpeditionFleetViewModel this[int index]
        {
            get
            {
                return vm[index];
            }
        }
    }
}
