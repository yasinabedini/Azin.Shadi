using System.ComponentModel.DataAnnotations;

namespace Azin.Shadi.DAL.Entities.Product
{
    public class ProductStatus
    {
        public int Id { get; set; }

        [Display(Name = "عنوان وضعیت")]        
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [MaxLength(100)]
        public string Title { get; set; }

        public List<Product> Products { get; set; }
    }
    
}
