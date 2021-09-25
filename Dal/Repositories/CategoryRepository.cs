using Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Context;
using Dal.Interface;

namespace Dal.Repositories
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
    }
}
