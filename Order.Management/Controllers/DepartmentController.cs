using BLL.Services;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Management.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View(DepartmentService.getInstance().GetAll());
        }
        // formu göstermek
        public ActionResult Create()
        {
            return View();
        }
        // Kaydetme işini yapan action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DepartmentService.getInstance().Add(entity);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model yükleme hatası!";
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
            return View(DepartmentService.getInstance().Get(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DepartmentService.getInstance().Update(model);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model yükleme hatası!";
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
            return View(DepartmentService.getInstance().Get(Id));
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteDepartment(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DepartmentService.getInstance().Delete(Id);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model yükleme hatası!";
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