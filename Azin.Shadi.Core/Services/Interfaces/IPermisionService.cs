using Azin.Shadi.DAL.Entities.Permission;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Role
        List<Role> GetRoles();
        List<Role> GetDeletedRoles();
        void AddRoleToUserRole(int roleId, int UserId);
        void UpdateUserRoles(string username, List<int> RolesId);
        void CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        Role GetRoleById(int id);
        #endregion

        #region Permission
        string GetAuthonticatedUserUsername(HttpContext context);
        bool CheckPermission(string username, int permissionId);
        List<Permission> GetPermission();
        void AddPermissionToRole(int roleId, List<int> permissionIds);
        void UpdateRolePermissions(int roleId, List<int> permissionIds);
        List<Permission> GetPermissionByRole(int roleId);
        #endregion
    }
}
