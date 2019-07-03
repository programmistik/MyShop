using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Product
    {
        public Product()
        {
            
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public decimal Price { get; set; }
        public bool Discount { get; set; }
        public decimal DiscountPrice { get; set; }

        public ICollection<ProductSizes> AvalableSizes { get; set; }
    }
}
