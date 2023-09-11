using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Azin.Shadi.Web.Pages.Admin.Product;

[PermissionChecker(11)]
public class CreateModel : PageModel
{
    private readonly IProductService _service;

    public CreateModel(IProductService service)
    {
        _service = service;
    }

    [BindProperty]
    public DAL.Entities.Product.Product Product { get; set; }

    public void OnGet()
    {
        ViewData["Groups"] = _service.GetProductGroupForManageProduct();
        ViewData["Statuses"] = new SelectList(_service.GetProductStatuses(), "Value", "Text");
    }

    public IActionResult OnPost(IFormFile? productPictureUp)
    {
        ViewData["Groups"] = _service.GetProductGroupForManageProduct();
        ViewData["Statuses"] = new SelectList(_service.GetProductStatuses(), "Value", "Text");

        if (!ModelState.IsValid) return Page();

        _service.AddProduct(Product, productPictureUp);

        return RedirectToPage("Index");
    }
}
