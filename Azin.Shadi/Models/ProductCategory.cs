using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azin.Shadi.Models
{
    public class ProductCategory
    {
        [Display(Name="آیدی")]
        [Key]       
        public int Id { get; set; }

        [Display(Name="نام دسته بندی")]
        [Required]
        public string Name { get; set; }        
    }
}