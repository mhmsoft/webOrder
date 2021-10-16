using BLL.Services;
using Dal.Context;
using Order.Commerce.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Order.Commerce.Controllers
{
   
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [NonAction]
        public bool isExistUser(string username)
        {
            var user = UserService.getInstance().GetAll().Where(a => a.email == username).FirstOrDefault();
            return user == null ? false : true;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(VmRegister register)
        {
            
            string message = "";
            if (ModelState.IsValid)
            {
                if (isExistUser(register.Email))
                {
                    message = "Bu maile kayıtlı bir kullanıcı mevcuttur.";
                    ViewBag.Message = message;
                    return View();
                }
                users user = new users();
                user.email = register.Email;
                // parolalar şifreleniyor
                user.password = System.Web.Helpers.Crypto.Hash(register.Password);
                user.rePassword = Crypto.Hash(register.ComfirmPassword);
                // aktivasyon kodu üretiyoruz.
                user.activationCode =  Guid.NewGuid().ToString().ToUpper();
                user.roleId = 2;
                // oluşturulan kullanıcının mail doğrulması başlangıçta olsun.
                user.isMailVerified = false;

                user.createdDate = DateTime.Now;
                //Kaydet
                UserService.getInstance().Add(user);
                // mail gönder
                

                message = SendVerificationLinkEmail(user.email, user.activationCode) +"Kaydınız başarılı şekilde gerçekleştirildi. Hesap aktivasyon linki "
                     + user.email + " adresinize gönderilmiştir.Doğrulamayı unutmayınız.";
              
                ViewBag.Message = message;
               
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(VmLogin login, string ReturnUrl)
        {
            string message = "";
            int sayac;
          
            if (ModelState.IsValid)
            {
                users _user = UserService.getInstance().GetAll().Where(x => x.email == login.Email).FirstOrDefault();
                if (_user == null)
                {
                    message = "Böyle bir Email kayıtlı değil";
                    ViewBag.Message = message;
                    return View();
                }
                sayac = _user.loginAttempt ?? 0;
                // kullanıcı mail doğrulaması yapmışsa al, yapmamışsa false al.
                bool verify = _user.isMailVerified ?? false;
                if (!verify)
                {
                    message = " Mail doğrulaması yapılmamış";
                    ViewBag.message = message;
                    sayac++;
                    _user.loginAttempt = sayac;
                    UserService.getInstance().Update(_user);
                    ViewBag.Message = message;
                    return View();
                }
                // kullanıcı aktif değilse
                if (_user.isActive==false)
                {
                    sayac++;
                    message = "Hesabınız askıya alınmıştır";
                    ViewBag.Message = message;
                    _user.loginAttempt = sayac;
                    UserService.getInstance().Update(_user);
                   
                }
                login.Password = Crypto.Hash(login.Password);
                // şifreler tutuyorsa
                if (string.Compare(login.Password, _user.password) == 0)
                {
                    _user.loginTime = DateTime.Now;
                    _user.loginAttempt = sayac;
                    _user.isActive = true;
                    //_user.hostName = GetIp();
                    UserService.getInstance().Update(_user);

                    Session["username"] = _user.email;

                    int timeOut =  10;
                    // form autentication oluşturalım.
                    var ticket = new FormsAuthenticationTicket(login.Email,false,timeOut);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeOut);
                    cookie.HttpOnly = true;
                    // Cookie ekleniyor
                    FormsAuthentication.SetAuthCookie("userName",false);
                    Response.Cookies.Add(cookie);

                    //// admin için
                    //if (_user.roleId == 1)
                    //    return Redirect("~/Panel/Category");

                    // return Url yerel bir url ise
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    sayac++;
                    _user.loginAttempt = sayac;
                    UserService.getInstance().Update(_user);
                    message = "Giriş yapılamadı.Parola yanlış!";
                }

            }
            
            ViewBag.Message = message;
            return View();
        }
        //mail adresinizdeki doğrulamayı yapmak için
        [HttpGet]

        public ActionResult VerifyAccount(string id)
        {
           
            
            var result = UserService.getInstance().GetAll().Where(a => a.activationCode == new Guid(id).ToString().ToUpper()).FirstOrDefault();
            if (result != null)
            {
                result.isMailVerified = true;
                UserService.getInstance().Update(result);
              
                ViewBag.Message = "Hesabınız başarıyla aktif edilmiştir. Giriş yapabilirsiniz";

            }
            else
            {
                
                ViewBag.Message = "Geçersiz istek";
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ForgetPassword(VmForgetPassword forgetPassword)
        {
            string message = "";
           
            if (forgetPassword!=null)
            {
                users _user = UserService.getInstance().GetAll().Where(x => x.email == forgetPassword.Email).FirstOrDefault();
                if (_user != null)
                {
                    Guid resetCode = Guid.NewGuid();
                    _user.resetCode = resetCode.ToString().ToUpper();
                    UserService.getInstance().Update(_user);

                    SendResetCodeEmail(forgetPassword.Email, resetCode.ToString().ToUpper());
                   
                    message = "Parola Sıfırlama işlemi başarılı şekilde gerçekleştirildi. Parola sifirlama linki "
                            + forgetPassword.Email  + " adresinize gönderilmiştir.";

                }
                else
                {
                    message = "Geçersiz Eposta";
                }
            }
            else
            {
                message = "Email alanı boş olamaz";
            }
            ViewBag.Message = message;
           
            return View();
        }
        [AllowAnonymous]
        public ActionResult ResetPassword(string id)
        {
            VmResetPassword rp = new VmResetPassword()
            {
                resetCode = id
            };
            return View(rp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ResetPassword(VmResetPassword rp)
        {
            string message = "";
            
            if (ModelState.IsValid)
            {
                users _user = UserService.getInstance().GetAll().Where(x => x.resetCode == rp.resetCode).FirstOrDefault();
                if (_user != null)
                {
                    _user.password = Crypto.Hash(rp.newPassword);
                    _user.rePassword = Crypto.Hash(rp.comfirmPassword);
                    UserService.getInstance().Update(_user);
                    
                    message = "Şifreniz başarıyla değiştirildi";
                }
                else
                    message = "Geçersiz kod";
            }
            ViewBag.Message = message;
            
            return View();
        }



        [NonAction]
        public void SendResetCodeEmail(string emailID, string resetCode)
        {
            SmtpSection network = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                var resetUrl = "/Account/ResetPassword/" + resetCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, resetUrl);
                var fromEmail = new MailAddress(network.Network.UserName, " Parola Sıfırlama");
                var toEmail = new MailAddress(emailID);

                string subject = " Parola Sıfırlama!";
                string body = "<br/><br/>İsteğiniz üzere  Parola sıfırlama işleminiz talebi alınmıştır. Onaylamak için aşağıdaki bağlantıya tıklayınız" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
                var smtp = new SmtpClient
                {
                    Host = network.Network.Host,
                    Port = network.Network.Port,
                    EnableSsl = network.Network.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = network.Network.DefaultCredentials,
                    Credentials = new NetworkCredential(network.Network.UserName, network.Network.Password)
                };
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string SendVerificationLinkEmail(string emailID, string activationCode)
        {
            SmtpSection network = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                var verifyUrl = "/Account/VerifyAccount/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                var fromEmail = new MailAddress(network.Network.UserName, "Site Üyeliği");
                var toEmail = new MailAddress(emailID);

                string subject = " sitemize Hoşgeldiniz!";
                string body = "<br/><br/> hesabınız başarıyla oluşturulmuştur. Hesabınız aktive etmek için aşağıdaki linke tıklayınız" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
                var smtp = new SmtpClient
                {
                    Host = network.Network.Host,
                    Port = network.Network.Port,
                    EnableSsl = network.Network.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = network.Network.DefaultCredentials,
                    Credentials = new NetworkCredential(network.Network.UserName, network.Network.Password)
                };
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
                return "Mail gönderme başarılı";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Authorize(Roles ="User")]
        public ActionResult MyOrders()
        {
            return View();
        }

    }
}