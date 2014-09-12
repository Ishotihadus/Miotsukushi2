using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class DockingCollectionViewModel : ViewModelBase
    {
        DockingViewModel[] vm = { new DockingViewModel(0), new DockingViewModel(1), new DockingViewModel(2), new DockingViewModel(3) };

        public DockingViewModel this[int i]
        {
            get
            {
                return vm[i];
            }
        }
    }
}
