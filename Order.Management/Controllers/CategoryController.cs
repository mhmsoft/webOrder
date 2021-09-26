using BLL.Services;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Management.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View(CategoryService.getInstance().GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryService.getInstance().Add(entity);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model Yükleme Hatası";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        public ActionResult Edit(int Id)
        {
            return View(CategoryService.getInstance().Get(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryService.getInstance().Update(model);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model Yükleme Hatası";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        public ActionResult Delete(int Id)
        {
            return View(CategoryService.getInstance().Get(Id));
        }
        
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteCategory(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoryService.getInstance().Delete(Id);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model Yükleme Hatası";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}