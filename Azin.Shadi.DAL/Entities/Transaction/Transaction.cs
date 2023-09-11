using Azin.Shadi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Transaction
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Display(Name = "مبلغ به ریال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "پرداخت شده")]
        public bool IsComplete { get; set; }

        [Display(Name = "مقدار به ریال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(80, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "نوع تراکنش")]
        public int TransactionTypeId { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public DAL.Entities.User.User Related_User { get; set; }        

        [Display(Name = "تاریخ تراکنش")]
        public DateTime PayDate { get; set; }
    }
}
