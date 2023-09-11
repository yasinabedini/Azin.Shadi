using Azin.Shadi.DAL.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Order
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Count { get; set; }
        
        [Required]
        public int Price { get; set; }

        [Required]
        public int SumPrice { get; set; }


        public Order Order { get; set; }
        public Product.Product Product { get; set; }
    }
}
