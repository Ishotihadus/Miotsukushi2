using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.EventArgs
{
    public class GetSessionDataEventArgs : System.EventArgs
    {
        /// <summary>
        /// Fiddlerから受け取ったSession
        /// </summary>
        public Fiddler.Session session;
    }
}
