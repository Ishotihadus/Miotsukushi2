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

        public void RaiseEventFromFile()
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var lines = System.IO.File.ReadAllLines(ofd.FileName);
                for (int i = 0; i < lines.Length / 3; i++)
                {
                    var url = lines[i * 3];
                    var req = lines[i * 3 + 1];
                    var res = lines[i * 3 + 2];
                    RaiseEvent(url, req, res);
                }
            }
        }
    }
}
