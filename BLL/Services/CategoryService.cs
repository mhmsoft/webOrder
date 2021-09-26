using Dal.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService:GenericService<Category>
    {
        private static CategoryService _instance;
        public static CategoryService getInstance()
        {
            if (_instance==null)
            {
                _instance = new CategoryService();
            }
            return _instance;
        }
    }
}
