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
            for (int i = 0; i < model.questdata.executingquest.Count; i++)
            {
                var quest = model.questdata.executingquest[i];
                if (Quests.Count <= i)
                {
                    Quests.Add(new QuestViewModel(quest));
                }
                else if(!Quests[i].DetailVisibility)
                {
                    Quests[i] = new QuestViewModel(quest);
                }
                else if(Quests[i].ID != quest.id)
                {
                    Quests.Insert(i, new QuestViewModel(quest));
                }
            }

            for (int i = model.questdata.executingquest.Count; i < Quests.Count; i++)
            {
                if (Quests[i].DetailVisibility)
                {
                    Quests.RemoveAt(i);
                    --i;
                }
            }

            int nullcount = (from _ in Quests where !_.DetailVisibility select _).Count();

            while (nullcount < model.questdata.exec_count - model.questdata.executingquest.Count)
            {
                Quests.Add(new QuestViewModel(null));
                ++nullcount;
            }

            while (nullcount > model.questdata.exec_count - model.questdata.executingquest.Count)
            {
                var nl = Quests.FirstOrDefault(_ => !_.DetailVisibility);
                if (nl != null)
                    Quests.Remove(nl);
                --nullcount;
            }
            
            QuestListVisibility = true;
        }
    }
}
