using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract
{
    public interface IGenericRepository<T>
    {
        void save(T model);
        void update(T model);
        void delete(int Id);
        T Get(int Id);
        IEnumerable<T> GetAll();

    }
}
