using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
public interface ICrud<T> where T : struct
{
    T Get(Func<T?, bool>? predict);
    IEnumerable<T?> GetAll(Func<T?, bool>? predict = null);
    public int Add(T entity);
    public void Update(T entity);
    public void Delete(int id);
    public T Get(int id);
    public List<T?> GetAll();
}
