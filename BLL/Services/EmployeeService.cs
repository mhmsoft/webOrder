using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService:GenericService<Employer>
    {
        private static EmployeeService _instance;
        public static EmployeeService getInstance()
        {
            if (_instance==null)
            {
                _instance = new EmployeeService();
            }
            return _instance;
        }
    }
}
