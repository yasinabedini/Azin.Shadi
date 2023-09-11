using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Discount;

public class UserDiscounts
{
    [Key]
    public int UI_Id { get; set; }
    public int UserId { get; set; }
    public string Code { get; set; }

    public User.User User { get; set; }
}
