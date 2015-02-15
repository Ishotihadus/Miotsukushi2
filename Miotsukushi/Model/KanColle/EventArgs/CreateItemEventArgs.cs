using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.EventArgs
{
    class CreateItemEventArgs : System.EventArgs
    {
        public string name;
        public string type;
        public int item_id;
        public int type_id;
        public int id;
        public bool success;
    }
}
