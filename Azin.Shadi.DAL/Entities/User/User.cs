using Azin.Shadi.DAL.Entities.Discount;
using Azin.Shadi.DAL.Entities.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.User;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Username { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(80, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "تایید ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]    
    public bool EmailConfrimation { get; set; }


    [Display(Name = "تصویر پروفایل")]    
    public string ProfileName { get; set; }

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Password { get; set; }

    [Display(Name = "کد فعالسازی")]
    [MaxLength(50,ErrorMessage ="{0} نمی تواند بیشتر از {1} باشد")]
    public string ActivationCode { get; set; }

    [Display(Name = "وضعیت")]
    [DefaultValue(true)]
    public bool IsActive { get; set; }

    [Display(Name = "تاریخ ثبت نام")]        
    public DateTime RegisterDate { get; set; }

    [Display(Name = "آدرس")]

    public string? Address { get; set; }

    [DefaultValue(false)]
    public bool IsDelete { get; set; }    


    public ICollection<Transaction.Transaction> Transactions { get; set; }
    public ICollection<Order.Order> Orders { get; set; }
    public ICollection<UserDiscounts> UserDiscounts { get; set; }
    public ICollection<Comment.Comment> Comments { get; set; }
}
