using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Order.Commerce.Models.ViewModels.Account
{
    public class VmResetPassword
    {
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Boş bırakılmaz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Boş bırakılmaz")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minumum 6 karakter girmeniz gerekir")]
        public string newPassword { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Boş bırakılmaz")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Minumum 5 karakter girmeniz gerekir")]
        [Compare("newPassword", ErrorMessage = "Parolanız eşleşmiyor")]
        public string comfirmPassword { get; set; }

        public string resetCode { get; set; }
    }
}