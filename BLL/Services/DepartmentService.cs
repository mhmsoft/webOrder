using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DepartmentService:GenericService<Department>
    {
        private static DepartmentService _instance;
        public  static DepartmentService getInstance()
        {
            if (_instance==null)
            {
                _instance = new DepartmentService();
            }
            return _instance;
        }
    }
}
