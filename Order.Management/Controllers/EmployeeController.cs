using BLL.Services;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Management.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View(EmployeeService.getInstance().GetAll()) ;
        }
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(DepartmentService.getInstance().GetAll(), "departmentId", "departmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeService.getInstance().Add(model);
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
            ViewBag.Departments = new SelectList(DepartmentService.getInstance().GetAll(), "departmentId", "departmentName", EmployeeService.getInstance().Get(Id).departmentId);

            return View(EmployeeService.getInstance().Get(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeService.getInstance().Update(model);
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
            return View(EmployeeService.getInstance().Get(Id));
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeService.getInstance().Delete(Id);
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