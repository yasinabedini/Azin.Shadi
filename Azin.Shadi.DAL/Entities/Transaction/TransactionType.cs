using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Transaction
{
    //TransactionType
    // Id = 1 => OnlinePayment
    // Id = 2 => Withdraw from the wallet
    // Id = 3 => Recharge wallet

    public class TransactionType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionTypeId { get; set; }

        [Required]
        [Display(Name = "عنوان")]
        [MaxLength(30, ErrorMessage = "طول رشته بیشتر از حد مجاز است! (حداکثر 30 کاراکتر)")]
        public string InvoiceTypeTitle { get; set; }
    }
}
