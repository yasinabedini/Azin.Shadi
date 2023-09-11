using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Permission
{
    public class Permission
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        [Display(Name = "عنوان دسترسی")]
        [Required(ErrorMessage = "{0} را وارد کنید!")]
        [MaxLength(150)]
        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Permission? Parent { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
