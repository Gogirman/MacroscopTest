using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreStandart.Exceptions
{
    /// <summary>
    ///   Исключение переполнения количества запросов.
    /// </summary>
    public class OverloadRequestException : Exception
    {
        public OverloadRequestException(string message)
            : base(message)
        { }

        public OverloadRequestException() : base()
        {

        }
    }
}
