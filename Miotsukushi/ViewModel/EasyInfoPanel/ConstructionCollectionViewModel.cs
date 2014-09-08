using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class ConstructionCollectionViewModel : ViewModelBase
    {
        ConstructionViewModel[] vm = { new ConstructionViewModel(0), new ConstructionViewModel(1), new ConstructionViewModel(2), new ConstructionViewModel(3) };

        public ConstructionViewModel this[int i]
        {
            get
            {
                return vm[i];
            }
        }
    }
}
