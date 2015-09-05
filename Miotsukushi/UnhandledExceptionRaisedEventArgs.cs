using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi
{
    public class UnhandledExceptionRaisedEventArgs : EventArgs
    {
        public Exception innerException;
        public object exceptionObject;
    }
}
