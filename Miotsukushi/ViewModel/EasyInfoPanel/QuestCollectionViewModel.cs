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
            model = Miotsukushi.Model.MainModel.Current.KancolleModel;
            model.Questdata.QuestChange += Questdata_QuestChange;
            QuestListVisibility = false;
        }

        private void Questdata_QuestChange(object sender, EventArgs e)
        {
            for (var i = 0; i < model.Questdata.Executingquest.Count; i++)
            {
                var quest = model.Questdata.Executingquest[i];
                if (Quests.Count <= i)
                {
                    Quests.Add(new QuestViewModel(quest));
                }
                else if(!Quests[i].DetailVisibility)
                {
                    Quests[i] = new QuestViewModel(quest);
                }
                else if(Quests[i].ID != quest.Id)
                {
                    Quests.Insert(i, new QuestViewModel(quest));
                }
                else
                {
                    Quests[i] = new QuestViewModel(quest);
                }
            }

            for (var i = model.Questdata.Executingquest.Count; i < Quests.Count; i++)
            {
                if (Quests[i].DetailVisibility)
                {
                    Quests.RemoveAt(i);
                    --i;
                }
            }

            var nullcount = (from _ in Quests where !_.DetailVisibility select _).Count();

            while (nullcount < model.Questdata.ExecCount - model.Questdata.Executingquest.Count)
            {
                Quests.Add(new QuestViewModel(null));
                ++nullcount;
            }

            while (nullcount > model.Questdata.ExecCount - model.Questdata.Executingquest.Count)
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
