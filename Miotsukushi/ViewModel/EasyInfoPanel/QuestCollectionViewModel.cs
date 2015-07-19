using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class QuestCollectionViewModel : ViewModelBase
    {
        public ObservableCollection<QuestViewModel> Quests { get; private set; }

        KanColleModel model;

        private bool _QuestListVisibility;
        public bool QuestListVisibility
        {
            get
            {
                return _QuestListVisibility;
            }

            set
            {
                if (_QuestListVisibility != value)
                {
                    _QuestListVisibility = value;
                    OnPropertyChanged(() => QuestListVisibility);
                }
            }
        }


        public QuestCollectionViewModel()
        {
            Quests = new ObservableCollection<QuestViewModel>();
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(Quests, new object());
            model = Miotsukushi.Model.MainModel.Current.kancolleModel;
            model.questdata.QuestChange += Questdata_QuestChange;
            QuestListVisibility = false;
        }

        private void Questdata_QuestChange(object sender, EventArgs e)
        {
            while(Quests.Count > 0)
                Quests.RemoveAt(0);
            foreach(var quest in model.questdata.executingquest)
            {
                Quests.Add(new QuestViewModel(quest));
            }
            for (int i = 0; i < model.questdata.exec_count - model.questdata.executingquest.Count; i++)
            {
                Quests.Add(new QuestViewModel(null));
            }
            QuestListVisibility = true;
        }
    }
}
