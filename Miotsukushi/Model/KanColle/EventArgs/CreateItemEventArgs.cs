using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.EventArgs
{
    class CreateItemEventArgs : System.EventArgs
    {
        public string Name;
        public string Type;
        public int ItemId;
        public int TypeId;
        public int Id;
        public bool Success;
    }
}
