using Abstract;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        public void delete(int Id)
        {
            using (var db = new context())
            {
                db.Entry(Get(Id)).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public T Get(int Id)
        {
            using (var db = new context())
            {
                return db.Set<T>().Find(Id);
            }
            
        }

        public IEnumerable<T> GetAll()
        {
            using (var db = new context())
            {
                return db.Set<T>().ToList();
            }
            
        }

        public void save(T model)
        {
            using (context db = new context())
            {
                 db.Set<T>().Add(model);
                 db.SaveChanges();
            }
        }

        public void update(T model)
        {
            using (context db = new context())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
