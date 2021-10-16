using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService:GenericService<Comment>
    {
        private static CommentService _instance;
        public static CommentService getInstance()
        {
            if (_instance == null)
            {
                _instance = new CommentService();
            }
            return _instance;
        }
    }
}
