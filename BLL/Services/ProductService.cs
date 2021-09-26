using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService:GenericService<Product>
    {
        private static ProductService _instance;
        public  static ProductService getInstance()
        {
            if (_instance==null)
            {
                _instance = new ProductService();
            }
            return _instance;
        }
    }
}
