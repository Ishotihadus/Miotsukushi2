using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi
{
    public class UnhandledExceptionRaisedEventArgs : EventArgs
    {
        public string ExceptionType;
        public Exception InnerException;
        public object ExceptionObject;
    }
}
