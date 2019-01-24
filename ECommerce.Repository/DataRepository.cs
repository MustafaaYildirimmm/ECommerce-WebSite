using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;
using ECommerce.Common;

namespace ECommerce.Repository
{
    //abstract class instance alinamaz kalitim verilerek kullanilir.Ayrica çoklu kalitim verilemez!
    public abstract class DataRepository<T>
    {
        public abstract Result<int> Insert(T item);
        public abstract Result<int> Delete(T item);
        public abstract Result<int> Update(T item);
        public abstract Result<List<T>> List();
    }
}
