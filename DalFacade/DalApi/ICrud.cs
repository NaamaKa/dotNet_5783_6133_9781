using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
       public void Add(T entity);
       public void Update(T entity);
       public void Delete(T entity);
       public T Get(int id);
       public IEnumerable<T> GetAll();
    }
}
