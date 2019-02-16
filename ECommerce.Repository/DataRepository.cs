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
    //crud işlemlerini birer metot olarak her bir tablo için ayrı ayrı oluştumak yerine abstract sınıf oluşturarak inheritince yolu ile sadece gövdelerini doldurarak kolayca kullanabilmek amaci ile oluşturuldu.
    //özellikle sonradan eklemiş oldugum, id ler icin M generic i gibi güncellemeler repositorylere(miras verdigim siniflar) kolayca uygulanabiliyor.
    public abstract class DataRepository<T,M>
    {
        public abstract Result<int> Insert(T item);
        public abstract Result<int> Delete(M id);
        public abstract Result<int> Update(T item);
        public abstract Result<T> GetById(M id);
        public abstract Result<List<T>> List();
        public abstract Result<List<T>> GetLatestObj(int Quantity);
    }
}
