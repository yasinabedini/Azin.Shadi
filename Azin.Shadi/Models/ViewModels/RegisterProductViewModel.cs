using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azin.Shadi.Models.ViewModels
{
    public class RegisterProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Name { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public string Category { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public int Price { get; set; }

        [Display(Name = "نام برند")]
        public string Brand { get; set; }

        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "{0} را پر کنید")]
        public int Inventory { get; set; }
 
        [Display(Name = "تصویر کالا")]
        public string ImageName { get; set; }
    }
}