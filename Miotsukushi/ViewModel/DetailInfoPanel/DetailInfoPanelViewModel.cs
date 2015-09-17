using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel
{
    class DetailInfoPanelViewModel : ViewModelBase
    {
        public List<string> TabTitle { get; private set; } = new List<string>()
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
                "戦闘詳細解析",
                "戦闘結果",
                "マップ詳細"
            };


        private int _SelectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }

            set
            {
                if (_SelectedIndex != value)
                {
                    _SelectedIndex = value;
                    OnPropertyChanged(() => SelectedIndex);
                }
            }
        }


        public Fleets.FleetsCollectionViewModel FleetsCollection { get; private set; } = new Fleets.FleetsCollectionViewModel();

        public Expedition.ExpeditionFleetCollectionViewModel ExpeditionCollection { get; private set; } = new Expedition.ExpeditionFleetCollectionViewModel();

        public DetailInfoPanelViewModel()
        {
            SelectedIndex = 0;

            // タブ移動の設定
            Model.MainModel.Current.KancolleModel.Battlemodel.BattleAnalyzed += (_, __) => SelectedIndex = 11;
            Model.MainModel.Current.KancolleModel.Sortiemodel.SortieStarted += (_, __) => SelectedIndex = 10;
            Model.MainModel.Current.KancolleModel.Sortiemodel.CellAdvanced += (_, __) => SelectedIndex = 10;
            Model.MainModel.Current.KancolleModel.Sortiemodel.GoBackPort += (_, __) => SelectedIndex = 0;
        }
    }
}
