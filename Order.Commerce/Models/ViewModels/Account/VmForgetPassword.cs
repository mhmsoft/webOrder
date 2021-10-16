using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Order.Commerce.Models.ViewModels.Account
{
    public class VmForgetPassword
    {
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Boş bırakılmaz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir Email adresi giriniz")]
        public string Email { get; set; }
    }
}