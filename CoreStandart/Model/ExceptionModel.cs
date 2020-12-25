using System;
using System.Collections.Generic;
using System.Text;

namespace CoreStandart.Model
{
    public class ExceptionModel
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionType { get; set; }
    }
}
