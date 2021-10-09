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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {

            return View(ProductService.getInstance().GetAll());
        }
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(CategoryService.getInstance().GetAll(),"categoryId","categoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model,IEnumerable<HttpPostedFileBase> pics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductService.getInstance().Add(model);
                    if (pics.Count() > 0 && pics.First() != null)
                    {
                        foreach (var pic in pics)
                        {
                            using (var br = new BinaryReader(pic.InputStream))
                            {
                                images newImg = new images()
                                {
                                    productId = model.productId,
                                    imgName = br.ReadBytes(pic.ContentLength)
                                };
                                ImageService.getInstance().Add(newImg);
                            }
                        }

                    }
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
                ViewBag.Error = ex.Message;
                return View();
            }
            
            
        } 
        // Product/Edit/57
        public ActionResult Edit(int Id)
        {
            // Edit view içerisinde Categori dropdown listesinin içeriğini doldurmak ve selected value ayarlıyoruz
            ViewBag.Categories = new SelectList(CategoryService.getInstance().GetAll(), "categoryId", "categoryName", ProductService.getInstance().Get(Id).categoryId);

            // ilgili ürüne ait resimleri view'e göndermek için.
            // ViewBag.Images = ImageService.getInstance().GetAll().Where(x => x.productId == Id).ToList();

            return View(ProductService.getInstance().Get(Id,"images"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model,IEnumerable<HttpPostedFileBase> pics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductService.getInstance().Update(model);
                    if (pics.Count() > 0 && pics.First()!=null)
                    {
                        foreach (var pic in pics)
                        {
                            using (var br = new BinaryReader(pic.InputStream))
                            {
                                images newImg = new images()
                                {
                                    productId = model.productId,
                                    imgName = br.ReadBytes(pic.ContentLength)
                                };
                                ImageService.getInstance().Add(newImg);
                            }
                        }

                    }
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
                ViewBag.Error = ex.Message;
                return View();
            }


        }
        [HttpPost]
        public void deleteImage(int ImageId)
        {
            ImageService.getInstance().Delete(ImageId);
        }
        
        public ActionResult Delete(int Id)
        {
            return View(ProductService.getInstance().Get(Id));
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductService.getInstance().Delete(Id);
                    return RedirectToAction("/");
                }
                else
                {
                    ViewBag.Error = "Model yükleme hatası";
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