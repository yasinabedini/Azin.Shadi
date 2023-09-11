using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.ProductGroup;

[PermissionChecker(20)]
public class CreateModel : PageModel
{
    private readonly IProductService _productService;

    public CreateModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public DAL.Entities.Product.ProductGroup ProductGroup { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        _productService.AddProductGroup(ProductGroup);
        return RedirectToPage("Index");
    }
}
