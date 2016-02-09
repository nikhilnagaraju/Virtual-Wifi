using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_wifi
{
    public class AdminException : Exception
    {
        public AdminException()
            : base() { }

        public AdminException(string message)
            : base(message) { }

        public AdminException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public AdminException(string message, Exception innerException)
            : base(message, innerException) { }

        public AdminException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
