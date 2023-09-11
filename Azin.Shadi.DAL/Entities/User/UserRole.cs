using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {
            
        }

        [Key]
        public int UR_Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }

    }
}
