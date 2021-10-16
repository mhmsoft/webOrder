using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Order.Commerce.Models.ViewModels.Account
{
    public class VmLogin
    {
        [Required(ErrorMessage ="Email boş bırakılamaz")]
        [Display(Name = "E-posta")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password boş bırakılamaz")]
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}