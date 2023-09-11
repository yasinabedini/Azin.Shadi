using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.DTOs
{
    public class PayOrderViewModel
    {
        public int  Id { get; set; }
        public int SumPrice { get; set; }
        public int PaymentMethod { get; set; }
        public int  ProductCount { get; set; }
        public string? Address { get; set; }
        public int UserWalletBalance { get; set; }
    }
}
