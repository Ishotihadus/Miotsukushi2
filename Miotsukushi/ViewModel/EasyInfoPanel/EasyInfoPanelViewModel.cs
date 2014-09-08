using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class EasyInfoPanelViewModel : ViewModelBase
    {
        public ExpeditionCollectionViewModel ExpeditionCollection { get; private set; }
        public ConstructionCollectionViewModel ConstructionCollection { get; private set; }

        public EasyInfoPanelViewModel()
        {
            ExpeditionCollection = new ExpeditionCollectionViewModel();
            ConstructionCollection = new ConstructionCollectionViewModel();
        }
    }
}
