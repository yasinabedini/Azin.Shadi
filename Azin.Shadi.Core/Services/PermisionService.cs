using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Context;
using Azin.Shadi.DAL.Entities.Permission;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AzinShadiContext _context;
        private readonly IUserService _service;

        public PermissionService(AzinShadiContext context, IUserService service)
        {
            _context = context;
            _service = service;
        }

        public void AddPermissionToRole(int roleId, List<int> permissionIds)
        {
            foreach (var permissionId in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
            }
            _context.SaveChanges();
        }

        public void AddRoleToUserRole(int roleId, int UserId)
        {
            _context.UserRoles.Add(new UserRole
            {
                UserId = UserId,
                RoleId = roleId
            });
            _context.SaveChanges();
        }

        public bool CheckPermission(string username, int permissionId)
        {
            User user = _context.Users.Single(t => t.Username == username);
            List<int> roles = _context.UserRoles.Where(t => t.UserId == user.UserId).Select(t => t.RoleId).ToList();
            var permission = _context.RolePermissions.Where(u => u.PermissionId == permissionId).ToList();

            if (!roles.Any())
            {
                return false;
            }

            foreach (var role in roles)
            {
                if (permission.Any(t => t.RoleId == role))
                {
                    return true;
                }
            }
            return false;
        }

        public void CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public string GetAuthonticatedUserUsername(HttpContext context)
        {
            var claimsIdentity = context.User.Identity as ClaimsIdentity;
            var username = claimsIdentity.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value;
            return username;
        }

        public List<Role> GetDeletedRoles()
        {
            return _context.Roles.IgnoreQueryFilters().Where(t => t.IsDelete).ToList();
        }

        public List<Permission> GetPermission()
        {
            return _context.Permissions.ToList();
        }

        public List<Permission> GetPermissionByRole(int roleId)
        {
            List<int> permissionIds = _context.RolePermissions.Where(t => t.RoleId == roleId).Select(t => t.PermissionId).ToList();

            List<Permission> permissions = new List<Permission>();

            foreach (var permissionId in permissionIds)
            {
                permissions.Add(_context.Permissions.Find(permissionId));
            }
            return permissions;
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.Find(id);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void UpdateRolePermissions(int roleId, List<int> permissionIds)
        {
            _context.RolePermissions.Where(t => t.RoleId == roleId).ToList().ForEach(r => _context.RolePermissions.Remove(r));
            _context.SaveChanges();
            foreach (var permissionId in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });
            }
            _context.SaveChanges();
        }

        public void UpdateUserRoles(string username, List<int> RolesId)
        {
            User user = _service.GetUserByUsername(username);
            foreach (var item in _context.UserRoles.Where(t => t.UserId == user.UserId))
            {
                _context.Remove(item);
            }
            _context.SaveChanges();
            foreach (var item in RolesId)
            {
                _context.UserRoles.Add(new UserRole
                {
                    UserId = user.UserId,
                    RoleId = item
                });
            }
            _context.SaveChanges();
        }
    }
}
