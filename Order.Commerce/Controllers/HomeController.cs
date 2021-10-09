using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using PagedList;

namespace Order.Commerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(CategoryService.getInstance().GetAll());
        }
        public ActionResult Products(int? catId,int? page, int? orderBy)
        {
            int _page = page ?? 1;
            int _pageSize =  2;
            
            //Tüm kategorileri view'e gönder
            ViewBag.Categories = CategoryService.getInstance().GetAll();

            string[] includes = new string[] { "images", "Category" };
            var products = ProductService.getInstance().GetAllArray(includes);

            if (orderBy.HasValue)
            {
                // name -A to Z
                if (orderBy==1)
                {
                    products = products.OrderBy(x => x.productName);
                    ViewBag.orderBy = 1;
                }
                // name -Z to A
                else if (orderBy==2)
                {
                    products = products.OrderByDescending(x => x.productName);
                    ViewBag.orderBy = 2;
                }
                //price -A to Z
                else if (orderBy==3)
                {
                    products = products.OrderBy(x => x.UnitPrice);
                    ViewBag.orderBy = 3;
                }
                //price -Z to A
                else
                {
                    products = products.OrderByDescending(x => x.UnitPrice);
                    ViewBag.orderBy = 4;
                }
            }
            if (catId.HasValue)
            {
                products = products.Where(x => x.categoryId == catId).ToList();
                ViewBag.catId = catId;
            }
            return View(products.ToPagedList(_page, _pageSize));
        }

        
    }
}