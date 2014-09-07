using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class ExpeditionCollectionViewModel : ViewModelBase
    {
        ExpeditionViewModel[] vm = { new ExpeditionViewModel(1), new ExpeditionViewModel(2), new ExpeditionViewModel(3) };

        public ExpeditionViewModel this[int i]
        {
            get
            {
                return vm[i];
            }
        }
    }
}
