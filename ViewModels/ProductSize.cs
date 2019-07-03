﻿using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.ViewModels
{
    public class ProductSize
    {
        public Product Product { get; set; }
        public ICollection<Size> ProductSizes { get; set; }
        public ICollection<Size> SizeOfProduct { get; set; }
    }
}