using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    ///   Модель документа.
    /// </summary>
    public  class Document
    {
        public Guid Id
        { get; set; }

        public string Name
        { get; set; }

        public enum RequestStatuses
        { 
            NotSent = 0,
            Sent = 1,
            ServerError = 2
        }

        public RequestStatuses RequestStatus
        { get; set; }

        public bool? IsPalindrome
        { get; set; }

        public string Text
        { get; set; }

        public Document()
        {
            Id = Guid.NewGuid();
        }
    }
}
