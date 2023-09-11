using Azin.Shadi.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Permission
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }

        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
