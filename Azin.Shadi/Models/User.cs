using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azin.Shadi.Models
{
    public class User
    {
        [Display(Name = "آیدی")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public int id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Family { get; set; }

        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Password { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل خود را به درستی وارد کنید!")]
        public string Eamil { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        [RegularExpression("(09){9}[0-9]", ErrorMessage = "شماره موبایل را درست وارد کنید")]
        public string Mobile { get; set; }

        [Display(Name = "موجودی کیف پول")]
        [Required]
        public int Wallet_Balance { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required]
        public System.DateTime RegisterDate { get; set; }

        [Display(Name = "وضعیت")]
        [Required]
        public bool IsActive { get; set; }

        [Display(Name = "آخرین بازدید")]
        public Nullable<System.DateTime> LastLoginDate { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public string ImageName { get; set; }
    }
}
