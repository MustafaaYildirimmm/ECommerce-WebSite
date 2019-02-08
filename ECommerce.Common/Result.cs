using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common
{
    public class Result<T>
    {
        //resultProccess tarafindan kullnailmak üzere ,
        //crud işlemlerin başarılı olup olmadığını ayrıca istenilen dataların kolayca taşınabilmesi için generic sınıf oluşturuldu.
      
        public string UserMessage { get; set; }
        public bool IsSucceded { get; set; }
        public T ProccessResult { get; set; }

    }
}
