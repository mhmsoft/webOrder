using BLL.Services;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Create(Category entity,HttpPostedFileBase pics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (pics!=null)
                    {
                        using (var br = new BinaryReader(pics.InputStream))
                        {
                            entity.imageName = br.ReadBytes(pics.ContentLength);
                        }
                    }
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
        public ActionResult Edit(Category model, HttpPostedFileBase pics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        if (pics != null)
                        {
                            using (var br = new BinaryReader(pics.InputStream))
                            {
                                model.imageName = br.ReadBytes(pics.ContentLength);
                            }
                        }
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