using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class ProductSizes
    {
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }
        
    }
}
