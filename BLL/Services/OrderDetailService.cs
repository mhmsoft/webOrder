using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderDetailService:GenericService<OrderDetail>
    {
        private static OrderDetailService _instance;
        public  static OrderDetailService getInstance()
        {
            if (_instance==null)
            {
                _instance = new OrderDetailService();
            }
            return _instance;
        }
    }
}
