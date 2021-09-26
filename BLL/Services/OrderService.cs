using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService:GenericService<Order>
    {
        private static OrderService _instance;
        public static OrderService getInstance()
        {
            if (_instance==null)
            {
                _instance = new OrderService();
            }
            return _instance;
        }
    }
}
