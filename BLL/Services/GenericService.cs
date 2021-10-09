
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
                    //57 , images
        public T Get(int Id,string include=null)
        {
            return repo.Get(Id,include);
        }
       

        public IEnumerable<T> GetAll(string includes = null)
        {
            return repo.GetAll(includes);
        }

        public void Update(T model)
        {
            repo.update(model);
        }
    }
}
