using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.EventArgs
{
    class StringEventArgs
    {
        public string message;

        public StringEventArgs(string message)
        {
            this.message = message;
        }
    }
}
