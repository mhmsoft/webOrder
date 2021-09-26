using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService:GenericService<Customer>
    {
        //singleton design pattern;

        private static CustomerService _instance;
        public static CustomerService getInstance()
        {
            if (_instance==null)
            {
                _instance = new CustomerService();
            }
            return _instance;
        }
    }
}
