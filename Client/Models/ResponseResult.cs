using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    ///   Модель ответа на запрос.
    /// </summary>
    public class ResponseResult
    {
        public bool Answer
        { get; set; }

        public Guid DocumentId
        { get; set; }

        
    }
}
