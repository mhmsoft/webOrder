using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Management.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View(OrderService.getInstance().GetAll("Customer"));
        }
        public ActionResult Delete(int Id)
        {
            return View(OrderService.getInstance().Get(Id));
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OrderService.getInstance().Delete(Id);
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
        public ActionResult Detail(int Id)
        {
            return View(OrderDetailService.getInstance().GetAll("Product").Where(x => x.orderId == Id).ToList());
        }
        //Onaylama
        public ActionResult Comfirm(int Id)
        {
            Dal.Context.Order myModel=OrderService.getInstance().Get(Id);
            myModel.isConfirmed1 = true;
            OrderService.getInstance().Update(myModel);
            return RedirectToAction("/");
        }
    }
}