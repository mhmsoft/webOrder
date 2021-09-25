using Abstract;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interface
{
    interface ICategoryRepository:IGenericRepository<Category>
    {
    }
}
