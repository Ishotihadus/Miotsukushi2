using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{

    class QuestData
    {
        public class Quest
        {
            public int Id;
            public int Progress;
            public int State;
            public string Name;
            public string Description;
            public int Type;
            public int Category;
        }

        public List<List<Quest>> Pages = new List<List<Quest>>();

        public List<Quest> Executingquest = null;

        /// <summary>
        /// 遂行中の任務数
        /// </summary>
        public int? ExecCount = null;

        public void SetQuestList(KanColleLib.TransmissionData.api_get_member.Questlist questlist)
        {
            while (questlist.page_count > Pages.Count)
                Pages.Add(null);

            while (questlist.page_count < Pages.Count)
                Pages.RemoveAt(Pages.Count - 1);

            ExecCount = questlist.exec_count;

            if (questlist.disp_page > questlist.page_count || questlist.disp_page <= 0)
                return;

            Pages[questlist.disp_page - 1] = new List<Quest>();
            foreach (var qdata in questlist.list)
            {
                Pages[questlist.disp_page - 1].Add(
                    new Quest() { Id = qdata.no, Progress = qdata.progress_flag, State = qdata.state, Name = qdata.title, Description = qdata.detail, Type = qdata.type, Category = qdata.category });
            }

            for (var i = questlist.disp_page - 2; i >= 0; i--)
            {
                // 自分の前のページの最後のidが自分の一番前より後の数字になっている
                if (Pages[i] != null && (Pages[i].Count != 5 || Pages[i][4].Id >= Pages[questlist.disp_page - 1][0].Id))
                    // それはおかしいので爆破
                    Pages[i] = null;
            }

            // 同様に、自分より後のページにある任務の最初のidが、今のページの一番最後の任務よりも小さい場合はおかしい
            for (var i = questlist.disp_page; i < Pages.Count; i++)
            {
                if (Pages[i] != null && (Pages[i].Count == 0 || Pages[i][0].Id <= Pages[questlist.disp_page - 1][questlist.list.Count - 1].Id))
                    Pages[i] = null;
            }

            AppendExecutingQuest();

#if DEBUG
            PrintDebugQuestList();
#endif


            OnQuestChange(new System.EventArgs());
        }

#if DEBUG
        void PrintDebugQuestList()
        {
            for (int i = 0; i < pages.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("Page #" + (i + 1));
                if (pages[i] == null)
                {
                    System.Diagnostics.Debug.WriteLine(" Unknown");
                    continue;
                }

                for (int j = 0; j < pages[i].Count; j++)
                {
                    System.Diagnostics.Debug.WriteLine(" Quest #" + (j + 1) + " : " + pages[i][j].name);
                }
            }

            System.Diagnostics.Debug.WriteLine("Executing Quests:");
            if (executingquest == null)
                System.Diagnostics.Debug.WriteLine(" Unknown");
            else
            {
                foreach (var quest in executingquest)
                {
                    System.Diagnostics.Debug.WriteLine(" " + quest.name);
                }
                if (executingquest.Count < exec_count.Value)
                    System.Diagnostics.Debug.WriteLine(" And other " + (exec_count.Value - executingquest.Count) + " unknown quest(s).");
            }
        }
#endif

        void AppendExecutingQuest()
        {
            Executingquest = new List<Quest>();
            foreach(var page in Pages)
            {
                if (page == null)
                    continue;

                foreach(var quest in page)
                {
                    if (quest.State >= 2)
                        Executingquest.Add(quest);
                }
            }
            
        }

        public event EventHandler QuestChange;
        protected virtual void OnQuestChange(System.EventArgs e) { if (QuestChange != null) { QuestChange(this, e); } }
    }

    
}
