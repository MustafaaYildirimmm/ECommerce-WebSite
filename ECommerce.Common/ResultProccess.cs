using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;

namespace ECommerce.Common
{
    public class ResultProccess<T>
    {
        //veritabanı delete,insert ,update işlemleri icin db degişiklerini kaydetme ve kontrol mekanizması kuruldu.
        public Result<int> GetResult(ECommerceEntities db)
        {
            Result<int> result = new Result<int>();
            int sonuc = db.SaveChanges();
            if (sonuc>0)
            {
                result.UserMessage = "Başarili";
                result.IsSucceded = true;
                result.ProccessResult = sonuc;
            }
            else
            {
                result.UserMessage = "Başarisiz";
                result.IsSucceded = false;
                result.ProccessResult = sonuc;
            }

            return result;
        }

        //db den tabloları listeleme işlemi için kontrol mekanizması

        public Result<List<T>> GetListResult(List<T> data)
        {
            Result<List<T>> result = new Result<List<T>>();

            if (data!=null)
            {
                result.UserMessage = "Başarili";
                result.IsSucceded = true;
                result.ProccessResult = data;
            }
            else
            {
                result.UserMessage = "Başarisiz";
                result.IsSucceded = false;
                result.ProccessResult = data;
            }

            return result;
        }

        //id ye göre çekilen data lar için kontrol mekanizması

        public Result<T> GetT(T data)
        {
            Result<T> result = new Result<T>();
            if (data!=null)
            {
                result.UserMessage = "Başarılı";
                result.IsSucceded = true;
                result.ProccessResult = data;
            }
            else
            {
                result.UserMessage = "Başarısız";
                result.IsSucceded = false;
                result.ProccessResult = data;
            }
            return result;
        }
    }
}
