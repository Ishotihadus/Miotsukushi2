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

        public Fleets.FleetsCollectionViewModel FleetsCollection { get; private set; } = new Fleets.FleetsCollectionViewModel();

        public Expedition.ExpeditionFleetCollectionViewModel ExpeditionCollection { get; private set; } = new Expedition.ExpeditionFleetCollectionViewModel();

        public DetailInfoPanelViewModel()
        {
            TabTitle = new List<string>()
            {
                "総合",
                "第1艦隊",
                "第2艦隊",
                "第3艦隊",
                "第4艦隊",
                "遠征",
                "建造",
                "入渠",
                "入渠待ち艦",
                "任務",
                "出撃総合",
                "戦闘総合",
                "敵艦隊詳細",
                "戦闘結果",
                "マップ詳細"
            };
        }
    }
}
