using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azin.Shadi.Models.ViewModels
{
    public class AdminLoginViewModel
    {
        [Display(Name ="موبایل")]
        [Required(ErrorMessage =" {0} را وارد کنید")]        
        public string Mobile { get; set; }

        [Display(Name = "پسورد")]
        [Required(ErrorMessage = " {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}