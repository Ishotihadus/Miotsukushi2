using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;

namespace Miotsukushi.Model.KanColle
{
    class DebuggerModel
    {
        KanColleNotifier kclib;

        public DebuggerModel(KanColleNotifier kclib)
        {
            this.kclib = kclib;
        }

        public bool IsAvailable(string url, string req, string res)
        {
            if (url == null || req == null || res == null)
                return false;

            if (!res.StartsWith("svdata={"))
                return false;
            

            return true;
        }

        public void RaiseEvent(string url, string req, string res)
        {
            if (url == null || req == null || res == null)
                return;

            kclib.ForceRaiseEvent(url, req, res);
        }
    }
}
