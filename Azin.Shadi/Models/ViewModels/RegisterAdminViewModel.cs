using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azin.Shadi.Models.ViewModels
{
    public class RegisterAdminViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را پر کنید")]        
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Family { get; set; }

        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} اجباری است!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} اجباری است!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن مطابقت ندارد!")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        [RegularExpression("(09)[0-9]{9}", ErrorMessage = "فرمت شماره موبایل اشتباه است!")]
        public string Mobile { get; set; }

        [Display(Name = "دپارتمان فعالیت")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Department { get; set; }
    }
}