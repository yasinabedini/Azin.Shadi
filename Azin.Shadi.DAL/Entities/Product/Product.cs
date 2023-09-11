using Azin.Shadi.DAL.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Product
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public string? Description { get; set; }

        [Display(Name = "توضیحات ")]
        public string? HideDescription { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public int Price { get; set; }

        [Display(Name = "وضعیت")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Display(Name = "حذف شده ؟ ")]
        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        [Display(Name = "تصویر کالا")]        
        public string? PictureName { get; set; }        

        [Display(Name = "وضعیت موجودی")]
        public int StatusId { get; set; }

        [Display(Name = "گروه محصول")]
        [Required]
        public int ProductGroupId { get; set; }

        [Display(Name = "موجودی فعلی محصول")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        public int Inventory { get; set; }

        public DateTime CreateDate { get; set; }



        #region Relations

        [ForeignKey("StatusId")]
        public ProductStatus? ProductStatus { get; set; }

        [ForeignKey("ProductGroupId")]
        public ProductGroup? ProductGroup { get; set; }
        public List<OrderLine>? OrderLines { get; set; }
        public ICollection<Comment.Comment>? Comments { get; set; }
        #endregion
    }
}
