using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.ViewModels
{
    public class ProductDesc
    {
        public Product Product { get; set; }
        public List<Size> ProductSizes { get; set; }
        public List<Color> ProductColors { get; set; }
        public List<Category> ProductCategory { get; set; }
    }
}
