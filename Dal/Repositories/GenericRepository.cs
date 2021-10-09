using Abstract;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        //public T Get(int Id)
        //{
        //    using (var db = new context())
        //    {
        //        return db.Set<T>().Find(Id);
        //    }

        //}
        // 57      images
        public T Get(int Id, string include = null)
        {
            using (var db = new context())
            {
                var result = db.Set<T>().Find(Id);
                if (!string.IsNullOrEmpty(include))
                    db.Entry(result).Collection(include).Load();
                return result;


            }
        }
        
        public IEnumerable<T> GetAll(string include = null)
        {
            using (var db = new context())
            {
                return string.IsNullOrEmpty(include)? db.Set<T>().ToList() : db.Set<T>().Include(include). ToList();
            }

        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            
            using (var db = new context())
            {
               
                var query = db.Set<T>().AsNoTracking();
                foreach (string item in includes)
                {
                    query = query.Include(item);
                }
                return query.ToList();
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
