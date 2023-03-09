using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azin.Shadi.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }

        public ProductCategory() { }
    }
}