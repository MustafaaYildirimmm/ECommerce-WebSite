using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common
{
    public class Result<T>
    {
        public string UserMessage { get; set; }
        public bool IsSucceded { get; set; }
        public T ProccessResult { get; set; }

    }
}
