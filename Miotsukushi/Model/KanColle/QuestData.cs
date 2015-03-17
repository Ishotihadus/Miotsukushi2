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
            public int id;
            public int progress;
            public int state;
            public string name;
            public string description;
            public int type;
            public int category;
        }

        public List<List<Quest>> pages = new List<List<Quest>>();

        public List<Quest> executingquest = null;

        /// <summary>
        /// 遂行中の任務数
        /// </summary>
        public int? exec_count = null;

        public void SetQuestList(KanColleLib.TransmissionData.api_get_member.Questlist questlist)
        {
            while (questlist.page_count > pages.Count)
                pages.Add(null);

            while (questlist.page_count < pages.Count)
                pages.RemoveAt(pages.Count - 1);

            exec_count = questlist.exec_count;

            if (questlist.disp_page > questlist.page_count || questlist.disp_page <= 0)
                return;

            pages[questlist.disp_page - 1] = new List<Quest>();
            foreach(var qdata in questlist.list)
            {
                pages[questlist.disp_page - 1].Add(
                    new Quest() { id = qdata.no, progress = qdata.progress_flag, state = qdata.state, name = qdata.title, description = qdata.detail, type = qdata.type, category = qdata.category });
            }

            for (int i = questlist.disp_page - 2; i >= 0; i--)
            {
                // 自分の前のページの最後のidが自分の一番前より後の数字になっている
                if (pages[i] != null && (pages[i].Count != 5 || pages[i][4].id >= pages[questlist.disp_page - 1][0].id))
                    // それはおかしいので爆破
                    pages[i] = null;
            }

            // 同様に、自分より後のページにある任務の最初のidが、今のページの一番最後の任務よりも小さい場合はおかしい
            for(int i = questlist.disp_page; i < pages.Count; i++)
            {
                if (pages[i] != null && pages[i][0].id <= pages[questlist.disp_page - 1][questlist.list.Count - 1].id)
                    pages[i] = null;
            }

            AppendExecutingQuest();


            // PrintDebugQuestList();


            OnQuestChange(new System.EventArgs());
        }


        /* 
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

        */

        void AppendExecutingQuest()
        {
            executingquest = new List<Quest>();
            foreach(var page in pages)
            {
                if (page == null)
                    continue;

                foreach(var quest in page)
                {
                    if (quest.state >= 2)
                        executingquest.Add(quest);
                }
            }
            
        }

        public event EventHandler QuestChange;
        protected virtual void OnQuestChange(System.EventArgs e) { if (QuestChange != null) { QuestChange(this, e); } }
    }

    
}
