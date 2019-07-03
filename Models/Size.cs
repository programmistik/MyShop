using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductSizes> AvalableSizes { get; set; }
    }
}
