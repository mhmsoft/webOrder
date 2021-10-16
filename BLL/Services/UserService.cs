using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService:GenericService<users>
    {
        private static UserService _instance;
        public static UserService getInstance()
        {
            if (_instance == null)
            {
                _instance = new UserService();
            }
            return _instance;
        }
    }
}
