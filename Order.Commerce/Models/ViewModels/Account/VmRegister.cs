using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Order.Commerce.Models.ViewModels.Account
{
    public class VmRegister
    {
        [Display(Name="İsim")]
        public string Name { get; set; }
        [Display(Name = "E-posta")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        [StringLength(255, ErrorMessage = "En az 5 karakter olması gerekir", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Şifre Doğrulama")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [StringLength(255, ErrorMessage = "En az 5 karakter olması gerekir", MinimumLength = 5)]
        public string ComfirmPassword { get; set; }
    }
}