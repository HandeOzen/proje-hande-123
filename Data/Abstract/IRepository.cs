using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Abstract;

public interface IRepository<T>
{
   T GetById(int id);
  List<T> GetAll();
    void Create(T entity);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}