using BLL.Services;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Management.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        // tüm müşterileri göster
        public ActionResult Index()
        {
            return View(CustomerService.getInstance().GetAll());
        }

        //http://servername:serverportu/controlerName/ActionName
        //htpp://localhost:13410/Customer/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] //post methodu ile gönderilen requesti yakalama
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerService.getInstance().Add(model);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model yükleme Hatası";
                    return View();
                }
                   
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Model yükleme Hatası "+ex.Message;
                return View();
            }
            
        }

        public ActionResult Edit(int Id)
        {
            Customer customer = CustomerService.getInstance().Get(Id);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerService.getInstance().Update(model);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model yükleme Hatası";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Model yükleme Hatası " + ex.Message;
                return View();
            }

        }
        public ActionResult

    }
}