using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService:GenericService<images>
    {
        private static ImageService _instance;
        public static ImageService getInstance()
        {
            if (_instance == null)
            {
                _instance = new ImageService();
            }
            return _instance;
        }
    }
}
