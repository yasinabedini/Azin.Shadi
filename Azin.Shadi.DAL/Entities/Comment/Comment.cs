using Azin.Shadi.DAL.Entities.Product;
using Azin.Shadi.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Comment;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Display(Name ="متن نظر")]
    [Required(ErrorMessage ="{0} را وارد کنید!")]
    [MaxLength(500)]
    public string Text { get; set; }

    [Display(Name = "خوانده شده توسط ادمین؟")]
    [Required]
    public bool IsAdminRead { get; set; }

    [Display(Name = "تاریخ ثبت نظر")]
    [Required]
    public DateTime CreateDate { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public User.User User { get; set; }
    public Product.Product Product { get; set; }


}
