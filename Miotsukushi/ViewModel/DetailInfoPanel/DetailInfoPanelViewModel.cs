using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel
{
    class DetailInfoPanelViewModel : ViewModelBase
    {
        public List<string> TabTitle { get; private set; }

        public DetailInfoPanelViewModel()
        {
            TabTitle = new List<string>()
            {
                "総合",
                "第1艦隊",
                "第2艦隊",
                "第3艦隊",
                "第4艦隊",
                "遠征"
            };
        }
    }
}
