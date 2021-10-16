using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Commerce.Models.ViewModels
{
    public class VmProductWithComment
    {
        public Product Product { get; set; }
        public  IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Product> productsByCategory { get; set; }
    }
}