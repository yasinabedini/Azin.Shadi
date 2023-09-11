using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public int PermissionId { get; set; }
        public IPermissionService _Permission { get; set; }

        public PermissionCheckerAttribute(int permissionId)
        {
            PermissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _Permission = context.HttpContext.RequestServices.GetService(typeof(IPermissionService)) as IPermissionService;
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = _Permission.GetAuthonticatedUserUsername(context.HttpContext);
                if (!_Permission.CheckPermission(username, PermissionId))
                {
                    context.HttpContext.Response.Redirect("/Login");
                }
            }
            else
            {
                context.HttpContext.Response.Redirect("/Login");
            }
        }
    }
}
