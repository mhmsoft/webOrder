
using Abstract;
using Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    public class GenericService<T> : IGenericService<T> where T : class
    {
        GenericRepository<T> repo = new GenericRepository<T>();
        public void Add(T model)
        {
            repo.save(model);
        }

        public void Delete(int Id)
        {
            repo.delete(Id);
        }

        public T Get(int Id)
        {
            return repo.Get(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return repo.GetAll();
        }

        public void Update(T model)
        {
            repo.update(model);
        }
    }
}
