using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Product
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [MaxLength(100)]
        public string Title { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<ProductGroup> ProductGroups { get; set; }

        public List<Product> Products { get; set; }
    }
}
