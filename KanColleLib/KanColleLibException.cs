using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib
{
    public class KanColleLibException : Exception
    {
        public KanColleLibException() : base() { }
        public KanColleLibException(string message) : base(message) { }
        public KanColleLibException(string message, Exception innerException) : base(message, innerException) { }
    }
}
