using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Azin.Shadi.Web.Pages.Admin.Product;

[PermissionChecker(10)]
public class IndexModel : PageModel
{

    private readonly IProductService _service;

    public IndexModel(IProductService service)
    {
        _service = service;
    }

    public ShowProductForAdminViewModel ShowProduct { get; set; }

    public void OnGet(int pageId = 1, string nameFilter = "", string groupNameFilter = "")
    {
        ShowProduct = _service.GetProductsForAdmin(pageId, nameFilter, groupNameFilter);
        ViewData["Groups"] = _service.GetProductGroups();
    }
}
