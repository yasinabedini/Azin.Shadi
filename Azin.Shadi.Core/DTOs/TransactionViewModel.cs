using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.DTOs
{
    public class WalletRechargeViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class TransactionViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "تاریخ تراکنش")]
        public DateTime PayDate { get; set; }

        [Display(Name = "مقدار به ریال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(80, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "نوع تراکنش")]
        public int TransactionTypeId { get; set; }
    }
}
